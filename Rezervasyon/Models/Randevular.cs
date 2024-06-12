namespace Rezervasyon.Models
{
    public class Randevular
    {
        public int ReservationID { get; set; }
        public int CustomerID { get; set; }
        public int RestaurantID { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan Time { get; set; }
        public int NumberOfPeople { get; set; }
    }
    public class Rezervasyonlar
    {
        public int RezervasyonID { get; set; }
        public int MusteriID { get; set; }
        public string MusteriAdi { get; set; }
        public string MusteriSoyadi { get; set; }
        public int RestoranID { get; set; }
        public string RestoranAdi { get; set; }
        public DateTime Tarih { get; set; }
        public TimeSpan Saat { get; set; }
        public int KisiSayisi { get; set; }
    }
}
