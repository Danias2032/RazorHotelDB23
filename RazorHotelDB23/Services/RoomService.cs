using Microsoft.Data.SqlClient;
using RazorHotelDB23.Interfaces;
using RazorHotelDB23.Models;

namespace RazorHotelDB23.Services
{
    public class RoomService : Connection, IRoomService
    {
        private string createRoomSql = "insert into Room Values (@RoomNr, @ID, @Type, @Price)";
        private string updateRoomSql = "";
        private string deleteRoomSql = "";
        private string getRoomsSql = "select * from Room where Hotel_No = @ID";
        private string getRoomsFromIDSql = "select * from Room where Room_No = @RoomNr";

        public RoomService(IConfiguration configuration) : base(configuration)
        {

        }
        public RoomService(string connectionString) : base(connectionString)
        {

        }

        public async Task<bool> CreateRoomAsync(int hotelNr, Room room)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(createRoomSql, connection))
                {
                    command.Parameters.AddWithValue("@ID", hotelNr);
                    command.Parameters.AddWithValue("@RoomNr", room.RoomNr);
                    command.Parameters.AddWithValue("@Type", room.Types);
                    command.Parameters.AddWithValue("@Price", room.Pris);
                    try
                    {
                        await command.Connection.OpenAsync();
                        int noOfRows = await command.ExecuteNonQueryAsync();
                        return noOfRows == 1;
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

        public async Task<bool> UpdateRoomAsync(Room room, int roomNr, int hotelNr)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(updateRoomSql, connection))
                {
                    command.Parameters.AddWithValue("@RoomNr", roomNr);
                    command.Parameters.AddWithValue("@ID", hotelNr);
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

        public async Task<Room> DeleteRoomAsync(int roomNr, int hotelNr)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(deleteRoomSql, connection))
                {
                    command.Parameters.AddWithValue("@RoomNr", roomNr);
                    command.Parameters.AddWithValue("@ID", hotelNr);
                    try
                    {
                        Room roomToReturn = await GetRoomFromIdAsync(roomNr, hotelNr);
                        await command.Connection.OpenAsync();
                        int noOfRows = await command.ExecuteNonQueryAsync();
                        return roomToReturn;
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

        public async Task<List<Room>> GetAllRoomsAsync(int hotelNr)
        {
            List<Room> rooms = new List<Room>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(getRoomsSql, connection))
                {
                    command.Parameters.AddWithValue("@ID", hotelNr);
                    try
                    {
                        await command.Connection.OpenAsync();
                        SqlDataReader reader = await command.ExecuteReaderAsync();
                        while (await reader.ReadAsync())
                        {
                            int roomNr = reader.GetInt32(0);
                            char roomType = reader.GetString(2)[0];
                            double roomPrice = reader.GetDouble(3);
                            Room room = new Room(roomNr, roomType, roomPrice, hotelNr);
                            rooms.Add(room);
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
            return rooms;
        }

        public async Task<Room> GetRoomFromIdAsync(int roomNr, int hotelNr)
        {
            List<Room> rooms = new List<Room>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(getRoomsFromIDSql, connection))
                {
                    command.Parameters.AddWithValue("@ID", hotelNr);
                    command.Parameters.AddWithValue("RoomNr", roomNr);
                    try
                    {
                        await command.Connection.OpenAsync();
                        SqlDataReader reader = await command.ExecuteReaderAsync();
                        if (await reader.ReadAsync())
                        {
                            char roomType = reader.GetString(2)[0];
                            double roomPrice = reader.GetDouble(3);
                            Room room = new Room(roomNr, roomType, roomPrice, hotelNr);
                            return room;
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
    }
}
