using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorHotelDB23.Interfaces;
using RazorHotelDB23.Models;

namespace RazorHotelDB23.Pages.Hotels
{
    public class UpdateHotelModel : PageModel
    {
        [BindProperty]
        public Hotel HotelToUpdate { get; set; }

        private IHotelService _hotelService;

        public UpdateHotelModel(IHotelService hotelService)
        {
            _hotelService = hotelService;
        }

        public async Task OnGet(int hotelNr)
        {
            HotelToUpdate = await _hotelService.GetHotelFromIdAsync(hotelNr);
        }

        public async Task<IActionResult> OnPost(int hotelNr)
        {
            bool ok = await _hotelService.UpdateHotelAsync(HotelToUpdate, hotelNr);
            if (ok)
            {
                return RedirectToPage("GetAllHotels");
            }
            else
            {
                return Page();
            }
        }
    }
}
