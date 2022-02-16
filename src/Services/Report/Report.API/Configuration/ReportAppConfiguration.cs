using Common.Infrastructure.Configuration;

namespace Report.API.Configuration
{
    public static class ReportAppConfiguration
    {
        public static BaseAppConfiguration BaseConfig
        {
            get { return BaseAppConfiguration.BaseConfig; }
        }

        public static string GetMongoConnectionString()
        {
            return BaseConfig.ConfigurationRoot["DatabaseSettings:ConnectionString"];
        }

        public static string GetMongoDatabaseName()
        {
            return BaseConfig.ConfigurationRoot["DatabaseSettings:DatabaseName"];
        }

        public static string GetContactGrpcUrl()
        {
            return BaseConfig.ConfigurationRoot["GrpcSettings:ContactUrl"];
        }

        public static string GetRabbitMQHostAddress()
        {
            return BaseConfig.ConfigurationRoot["EventBusSettings:HostAddress"];
        }

        public static string GetQueueCapacity()
        {
            return BaseConfig.ConfigurationRoot["QueueCapacity"];
        }

        public static string GetFileServerUrl()
        {
            return BaseConfig.ConfigurationRoot["FileServerUrl"];
        }

    }
}
