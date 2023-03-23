namespace RazorHotelDB23.Models
{
    public class Hotel
    {
        public int HotelNr { get; set; }
        public string Navn { get; set; }
        public string Adresse { get; set; }

        public Hotel()
        {

        }

        public Hotel(int hotelNr, string navn, string adresse)
        {
            HotelNr = hotelNr;
            Navn = navn;
            Adresse = adresse;
        }
        
        public override string ToString()
        {
            return $"{nameof(HotelNr)}: {HotelNr}, {nameof(Navn)}: {Navn}, {nameof(Adresse)}: {Adresse}";
        }
    }
}
