using Common.Infrastructure.Configuration;

namespace Contact.Infrastructure.Configuration
{
    public static class ContactAppConfiguration
    {
        public static BaseAppConfiguration BaseConfig
        {
            get { return BaseAppConfiguration.BaseConfig; }
        }

        public static string GetPostgreConnectionString()
        {
            return BaseConfig.ConfigurationRoot["DatabaseSettings:ConnectionString"];
        }
    }
}
