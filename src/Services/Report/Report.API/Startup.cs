using Common.Infrastructure.BackgroundService;
using Common.Infrastructure.BackgroundService.Interfaces;
using Common.Middleware.ExceptionHandler;
using Contact.Grpc.Protos;
using EventBus.Messages.Common;
using HealthChecks.UI.Client;
using MassTransit;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Report.API.BackgroundServices;
using Report.API.Configuration;
using Report.API.Data;
using Report.API.Data.Interfaces;
using Report.API.EventBusConsumer;
using Report.API.GrpcServices;
using Report.API.Repositories;
using Report.API.Repositories.Interfaces;
using System;
using System.Text;

namespace Report.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddScoped<IReportContext, ReportContext>();
            services.AddScoped<IReportRepository, ReportRepository>();
            services.AddAutoMapper(typeof(Startup));

            // Grpc Configuration
            services.AddGrpcClient<ContactProtoService.ContactProtoServiceClient>
                        (o => o.Address = new Uri(ReportAppConfiguration.GetContactGrpcUrl()));
            services.AddScoped<ContactGrpcService>();

            // MassTransit-RabbitMQ Configuration
            services.AddMassTransit(config => {

                config.AddConsumer<ReportBackgroundServiceConsumer>();

                config.UsingRabbitMq((ctx, cfg) => {
                    cfg.Host(ReportAppConfiguration.GetRabbitMQHostAddress());

                    cfg.ReceiveEndpoint(EventBusConstants.ReportBackgroundServiceQueue, c => {
                        c.ConfigureConsumer<ReportBackgroundServiceConsumer>(ctx);
                    });
                });
            });
            services.AddMassTransitHostedService();

            services.AddScoped<ReportBackgroundServiceConsumer>();

            //services.AddSingleton<MonitorLoop>();
            services.AddHostedService<QueuedHostedService>();
            services.AddSingleton<IBackgroundTaskQueue>(ctx => {
                if (!int.TryParse(ReportAppConfiguration.GetQueueCapacity(), out var queueCapacity))
                    queueCapacity = 100;
                return new DefaultBackgroundTaskQueue(queueCapacity);
            });

            //services.AddHostedService<ReportBackgroundService>();
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Report.API", Version = "v1" });
            });

            services.AddHealthChecks().AddMongoDb(ReportAppConfiguration.GetMongoConnectionString(), "MongoDb Healt", HealthStatus.Degraded);

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

            }

            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Report.API v1"));

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseCustomExceptionHandler();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHealthChecks("/hc", new HealthCheckOptions()
                {
                    Predicate = _ => true,
                    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
                });
            });
        }
    }
}
