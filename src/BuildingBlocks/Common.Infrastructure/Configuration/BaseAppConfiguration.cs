using Microsoft.Extensions.Configuration;
using System;

namespace Common.Infrastructure.Configuration
{
    public class BaseAppConfiguration
    {
        private static BaseAppConfiguration _baseConfig;
        public static BaseAppConfiguration BaseConfig 
        { 
            get 
            {
                if (_baseConfig == null)
                    _baseConfig = new BaseAppConfiguration();
                return _baseConfig;
            } 
        }

        private IConfigurationRoot _configurationBuilder;
        public IConfigurationRoot ConfigurationRoot
        {
            get
            {
                if(_configurationBuilder == null)
                {
                    _configurationBuilder = new ConfigurationBuilder()
                        .SetBasePath(AppContext.BaseDirectory)
                        .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                        .AddEnvironmentVariables()
                        .Build();
                }
                return _configurationBuilder;
            }
        }

        public string GetAppVersion()
        {
            return ConfigurationRoot["Version"];
        }

        public string GetElastic()
        {
            return ConfigurationRoot["ElasticConfiguration:Uri"];
        }
    }
}
