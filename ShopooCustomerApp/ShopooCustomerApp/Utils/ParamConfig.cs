namespace ShopooCustomerApp.Utils
{
    public class ParamConfig
    {
        public readonly static object _LockInstance = new object();
        private static ParamConfig _Instance;
        private readonly IConfiguration _Configuration;

        public static ParamConfig Instance
        {
            get
            {
                if (_Instance == null)
                {
                    lock (_LockInstance)
                    {
                        _Instance = new ParamConfig();
                    }
                }

                return _Instance;
            }
        }

        private ParamConfig()
        {
            _Configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();
        }

        public string API_URL
        {
            get
            {
                return _Configuration.GetValue<string>("API_URL");
            }
        }
    }
}
