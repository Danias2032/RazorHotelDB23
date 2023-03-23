using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
using RazorHotelDB23.Interfaces;
using RazorHotelDB23.Models;

namespace RazorHotelDB23.Services
{
    public class HotelService : Connection, IHotelService
    {
        private string queryString = "select * from Hotel";
        private string queryStringFromID = "select * from Hotel where Hotel_No = @HotelID";
        private string insertSql = "insert into Hotel Values (@ID, @Navn, @Adresse)";
        private string deleteSql = "delete from hotel where Hotel_No = @ID";
        private string updateSql = "update Hotel " +
                                   "set Hotel_No = @HotelID, Name = @Navn, Address = @Adresse " +
                                   "where Hotel_No = @ID";
        private string hotelByNameSql = "select * from Hotel where Name like '%'+@Navn+'%'";
        public HotelService(IConfiguration configuration) : base(configuration)
        {

        }
        public HotelService(string connectionString) : base(connectionString)
        {

        }

        public async Task<bool> CreateHotelAsync(Hotel hotel)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(insertSql, connection))
                {
                    command.Parameters.AddWithValue("@ID", hotel.HotelNr);
                    command.Parameters.AddWithValue("@Navn", hotel.Navn);
                    command.Parameters.AddWithValue("@Adresse", hotel.Adresse);
                    try
                    {
                        await command.Connection.OpenAsync();
                        int noOfRows = await command.ExecuteNonQueryAsync(); //bruges ved update, delete, insert
                        if (noOfRows == 1)
                        {
                            return true;
                        }
                        return false;
                    }
                    catch (SqlException sqlEx)
                    {
                        Console.WriteLine("Database error");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Generel error");
                    }
                }
            }
            return false;
        }
        public async Task<bool> UpdateHotelAsync(Hotel hotel, int hotelNr)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(updateSql, connection))
                {
                    command.Parameters.AddWithValue("@HotelID", hotelNr);
                    command.Parameters.AddWithValue("@Navn", hotel.Navn);
                    command.Parameters.AddWithValue("@Adresse", hotel.Adresse);
                    command.Parameters.AddWithValue("@ID", hotel.HotelNr);
                    try
                    {
                        await command.Connection.OpenAsync();
                        int noOfRows = await command.ExecuteNonQueryAsync();
                        if (noOfRows == 1)
                        {
                            return true;
                        }
                        return false;
                    }
                    catch (SqlException sqlEx)
                    {
                        Console.WriteLine("Database error " + sqlEx.Message);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Generel fejl " + ex.Message);
                    }
                }
            }
            return false;
        }

        public async Task<Hotel> DeleteHotelAsync(int hotelNr)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(deleteSql, connection))
                {
                    command.Parameters.AddWithValue("@ID", hotelNr);
                    try
                    {
                        Hotel hotelToReturn = await GetHotelFromIdAsync(hotelNr);
                        await command.Connection.OpenAsync();
                        int noOfRows = await command.ExecuteNonQueryAsync();
                        return hotelToReturn;
                    }
                    catch (SqlException sqlEx)
                    {
                        Console.WriteLine("Database error " + sqlEx.Message);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Generel fejl " + ex.Message);
                    }
                    return null;
                }
            }
        }

        public async Task<List<Hotel>> GetAllHotelAsync()
        {
            List<Hotel> hoteller = new List<Hotel>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(queryString, connection))
                {
                    try
                    {
                        await command.Connection.OpenAsync();//aSynkront
                        SqlDataReader reader = await command.ExecuteReaderAsync();//aSynkront
                        while (await reader.ReadAsync())
                        {
                            int hotelNr = reader.GetInt32(0);
                            string hotelNavn = reader.GetString(1);
                            string hotelAdr = reader.GetString(2);
                            Hotel hotel = new Hotel(hotelNr, hotelNavn, hotelAdr);
                            hoteller.Add(hotel);
                        }
                    }
                    catch (SqlException sqlEx)
                    {
                        Console.WriteLine("Database error " + sqlEx.Message);
                        return null;
                    }
                    catch (Exception exp)
                    {
                        Console.WriteLine("Generel fejl" + exp.Message);
                        return null;
                    }
                }
            }
            return hoteller;
        }

        public async Task<Hotel> GetHotelFromIdAsync(int hotelNr)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(queryStringFromID, connection))
                {
                    command.Parameters.AddWithValue("@HotelID", hotelNr);
                    try
                    {
                        await command.Connection.OpenAsync();
                        SqlDataReader reader = await command.ExecuteReaderAsync();
                        if (await reader.ReadAsync())
                        {
                            int hotelNo = reader.GetInt32(0);
                            string hotelNavn = reader.GetString(1);
                            string hotelAdr = reader.GetString(2);
                            Hotel hotel = new Hotel(hotelNo, hotelNavn, hotelAdr);
                            return hotel;
                        }
                    }
                    catch (SqlException sqlEx)
                    {
                        Console.WriteLine("Database error " + sqlEx.Message);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Generel fejl " + ex.Message);
                    }
                }
            }
            return null;
        }

        public async Task<List<Hotel>> GetHotelsByNameAsync(string name)
        {
            List<Hotel> hotels = new List<Hotel>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(hotelByNameSql, connection))
                {
                    command.Parameters.AddWithValue("@Navn", name); 
                    try
                    {
                        await command.Connection.OpenAsync();
                        SqlDataReader reader = await command.ExecuteReaderAsync();
                        while (await reader.ReadAsync())
                        {
                            int hotelNr = reader.GetInt32(0);
                            string hotelNavn = reader.GetString(1);
                            string hotelAdr = reader.GetString(2);
                            Hotel hotel = new Hotel(hotelNr, hotelNavn, hotelAdr);
                            hotels.Add(hotel);
                        }
                    }
                    catch (SqlException sqlEx)
                    {
                        Console.WriteLine("Database error " + sqlEx.Message);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Generel fejl " + ex.Message);
                    }
                    return hotels;
                }
            }
            return null;
        }
    }
}