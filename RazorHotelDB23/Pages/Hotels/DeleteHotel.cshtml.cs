using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorHotelDB23.Interfaces;
using RazorHotelDB23.Models;

namespace RazorHotelDB23.Pages.Hotels
{
    public class DeleteHotelModel : PageModel
    {
        public Hotel HotelToDelete { get; set; }
        private IHotelService _hotelService;
        public DeleteHotelModel(IHotelService hotelService)
        {
            _hotelService = hotelService;
        }
        public async Task OnGet(int hotelNr)
        {
            HotelToDelete = await _hotelService.GetHotelFromIdAsync(hotelNr);
        }
        public async Task<IActionResult> OnPost(int hotelNr)
        {
            await _hotelService.DeleteHotelAsync(hotelNr);
            return RedirectToPage("GetAllHotels");
        }
    }
}
