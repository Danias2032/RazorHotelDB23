using Microsoft.VisualStudio.TestTools.UnitTesting;
using RazorHotelDB23.Models;
using RazorHotelDB23.Services;
using System.Collections.Generic;

namespace HotelTest
{
    [TestClass]
    public class HotelServiceTest
    {
        private string connectionString = Secret.MyProperty;
        [TestMethod]
        public void TestAddHotel()
        {
            HotelService hotelService = new HotelService(connectionString);
            List<Hotel> hotels = hotelService.GetAllHotelAsync().Result;

            int numberOfHotelsBefore = hotels.Count;
            Hotel newHotel = new Hotel(10000, "TestHotel", "TestVej");
            bool ok = hotelService.CreateHotelAsync(newHotel).Result;
            hotels = hotelService.GetAllHotelAsync().Result;

            int numberOfHotelsAfter = hotels.Count;
            Hotel h = hotelService.DeleteHotelAsync(newHotel.HotelNr).Result;

            Assert.AreEqual(numberOfHotelsBefore + 1, numberOfHotelsAfter);
            Assert.IsTrue(ok);
            Assert.AreEqual(h.HotelNr, newHotel.HotelNr);
        }
    }
}