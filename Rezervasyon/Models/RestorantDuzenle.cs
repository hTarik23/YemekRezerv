namespace Rezervasyon.Models
{
	public class RestorantDuzenle
	{
		public int RestoranID { get; set; }
		public string Ad { get; set; }
		public string Adress { get; set; }
		public string Telefon { get; set; }

        public RestorantDuzenle(int restoranID, string ad, string adress, string tel)
        {
			RestoranID = restoranID;
			Ad = ad;
			Adress = adress;
			Telefon = tel;
        }
        public RestorantDuzenle()
        {
            
        }
    }
}
