namespace Rezervasyon.Models
{
	public class MusteriDuzenle
	{
		public int MusteriID { get; set; }
		public string Ad { get; set; }
		public string Soyad { get; set; }
		public string Telefon { get; set; }

        public MusteriDuzenle(int musteriD, string ad, string soyad, string tel)
        {
			MusteriID = musteriD;
			Ad = ad;
			Soyad = soyad;
			Telefon = tel;
        }
        public MusteriDuzenle()
        {
            
        }
    }
}
