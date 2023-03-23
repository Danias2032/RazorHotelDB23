using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorHotelDB23.Interfaces;
using RazorHotelDB23.Models;

namespace RazorHotelDB23.Pages.Hotels
{
    public class CreateModel : PageModel
    {
        [BindProperty]
        public Hotel Hotel { get; set; }
        private IHotelService _hotelService;

        public CreateModel(IHotelService hotelService)
        {
            _hotelService = hotelService;
        }
        public void OnGet()
        {

        }

        public async Task<IActionResult> OnPostAsync()
        {
            await _hotelService.CreateHotelAsync(Hotel);
            return RedirectToPage("GetAllHotels");
        }
    }
}
