using RazorHotelDB23.Models;

namespace RazorHotelDB23.Interfaces
{
    public interface IRoomService
    {
        /// <summary>
        /// Henter alle værelser til et hotel fra databasen
        /// </summary>
        /// <param name="hotelNr">Nummeret på hotellet</param>
        /// <returns>Liste af værelser</returns>
        Task<List<Room>> GetAllRoomsAsync(int hotelNr);

        /// <summary>
        /// Henter et specifik værelse fra database 
        /// </summary>
        /// <param name="roomNr">Udpeger det værelse der ønskes fra databasen</param>
        /// <param name="hotelNr">Nummeret på hotellet</param>
        /// <returns>Retunere det fundne værelse, ellers null hvis værelset ikke findes</returns>
        Task<Room> GetRoomFromIdAsync(int roomNr, int hotelNr);

        /// <summary>
        /// Indsætter et nyt værelse i databasen
        /// </summary>
        /// <param name="room">Værelset der skal indsættes</param>
        /// <param name="hotelNr">Nummeret på hotellet</param>
        /// <returns>Sandt hvis det er gået godt ellers falsk</returns>
        Task<bool> CreateRoomAsync(int hotelNr, Room room);

        /// <summary>
        /// Opdaterer en værelset i databasen
        /// </summary>
        /// <param name="room">De nye værdier til værelset</param>
        /// <param name="roomNr">Nummer på det værelse der skal opdateres</param>
        /// <param name="hotelNr">Nummeret på hotellet</param>
        /// <returns>Sandt hvis det er gået godt ellers falsk</returns>
        Task<bool> UpdateRoomAsync(Room room, int roomNr, int hotelNr);

        /// <summary>
        /// Sletter et værelse fra databasen
        /// </summary>
        /// <param name="roomNr">Nummer på det værelse der skal slettes</param>
        /// <param name="hotelNr">Nummeret på hotellet</param>
        /// <returns>Det værelse der er slettet fra databasen, returnere null hvis værelset ikke findes</returns>
        Task<Room> DeleteRoomAsync(int roomNr, int hotelNr);

    }
}
