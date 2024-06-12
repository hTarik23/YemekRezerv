using Microsoft.AspNetCore.Mvc;
using Rezervasyon.Models;
using Rezervasyon.Insfracture;


namespace Rezervasyon.Controllers
{
    public class MusteriController : Controller
    {
        public IActionResult MusteriGir()
        {
            return View();
        }

        [HttpPost]
        public IActionResult MusteriGir(MusteriAddModel musteriAddModel)
        {
            var dataaccess = new DataAccess();
            var result = dataaccess.AddMusteri(musteriAddModel.Ad, musteriAddModel.Soyad, musteriAddModel.TelefonNumarası);
			return RedirectToAction("Musteriler");

		}
		public async Task<IActionResult> Musteriler()
        {
            var dataaccess = new DataAccess();
            var viewmodel = await dataaccess.TumMusterileriGetir();
            if (viewmodel is null || !viewmodel.Any()) 
            {
                return RedirectToAction("MusteriGir");
            }
            return View(viewmodel);
        }

        public async Task<IActionResult> MusteriDuzenle(int musteriId) 
        {
			var dataaccess = new DataAccess();
			var musteriList = await dataaccess.TumMusterileriGetir();
            var resultModel = musteriList.Where(x => x.MusteriID == musteriId).Select(x => new MusteriDuzenle(x.MusteriID,x.Ad,x.Soyad,x.Telefon)).FirstOrDefault();
            return View(resultModel);
        }

        [HttpPost]
        public IActionResult MusteriSil(int MusterId) 
        {
            var dataaccess = new DataAccess();
            var result = dataaccess.MusteriSil(MusterId);
			return RedirectToAction("Musteriler");

		}
		[HttpPost]
		public IActionResult MusteriDuzenle(MusteriDuzenle musteriDuzenle)
		{
			var dataaccess = new DataAccess();
            dataaccess.UpdateMusteri(musteriDuzenle.MusteriID, musteriDuzenle.Ad, musteriDuzenle.Soyad, musteriDuzenle.Telefon);
			return RedirectToAction("Musteriler");
		}
	}
}
