namespace RazorHotelDB23.Services
{
    public class Secret
    {
        private static string _connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=HotelDB23;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        public static string MyProperty
        {
            get { return _connectionString; }
        }
    }
}
