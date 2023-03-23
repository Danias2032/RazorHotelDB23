namespace RazorHotelDB23.Services
{
    public abstract class Connection
    {
        protected string connectionString;
        public IConfiguration Configuration { get; }

        public Connection(IConfiguration configuration)
        {
            connectionString = Secret.MyProperty;
            Configuration = configuration;
            //connectionString = Configuration["ConnectionStrings:DefaultConnection"];
        }
        public Connection(string connectionString)
        {
            Configuration = null;
            this.connectionString = connectionString;
        }

    }


}
