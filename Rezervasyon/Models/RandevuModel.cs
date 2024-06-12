using Microsoft.AspNetCore.Mvc;

namespace Rezervasyon.Models
{
    public class RandevuModel
    {
        public int CustomerId { get; set; }

        public int RestaurantId { get; set; }

        public DateTime Date { get; set; }

        public TimeSpan Time { get; set; }

        public int NumberOfPeople { get; set; }

        public List<Restorantlar> Restorantlars { get; set; }
        public List<Musteriler> Musterilers { get; set;}
        public RandevuModel(List<Restorantlar> restorantlars, List<Musteriler> musterilers)
        {
            Restorantlars = restorantlars;
            Musterilers = musterilers;
        }
        public RandevuModel()
        {
            
        }
    }
}
