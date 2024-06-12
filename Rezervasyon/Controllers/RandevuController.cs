using Microsoft.AspNetCore.Mvc;
using Rezervasyon.Insfracture;
using Rezervasyon.Models;
using System.Linq;

namespace Rezervasyon.Controllers
{
    public class RandevuController : Controller
    {
        public async Task<IActionResult> Index()
        {
            var dataaccess = new DataAccess();
            var musteri = await dataaccess.TumMusterileriGetir();
            var restorant = await dataaccess.TumRestoratlariGetir();
            var viewmodel = new RandevuModel(restorant, musteri);
            return View(viewmodel);
        }

        [HttpPost]
        public IActionResult RandevuAl(RandevuModel randevuModel)
        {

            var dataaccess = new DataAccess();
            var result = dataaccess.AddReservation(randevuModel.CustomerId, randevuModel.RestaurantId, randevuModel.Date, randevuModel.Time, randevuModel.NumberOfPeople);
			return RedirectToAction("Randevular");
		}

		public async Task<IActionResult> Randevular()
        {

            var dataaccess = new DataAccess();
            var model = await dataaccess.TumRezervasyonlariGetir();
			if (model is null || !model.Any()) 
            {
				return RedirectToAction("Index");
			}
			return View(model);
        }

        [HttpGet]
        public async Task<JsonResult> RandevuSaatleri(int restorantId)
        {
            List<string> zamamlar = new List<string>
    {
        "17:00",
        "17:30",
        "18:00",
        "18:30",
        "19:00",
        "19:30",
        "20:00",
        "20:30",
        "21:00",
        "21:30",
        "22:00",
        "22:30",
        "23:00",
        "23:30"
    };

            var dataaccess = new DataAccess();
            var getData = await dataaccess.TumRandevulariGetir();
            var varolanlar = getData.Where(x => x.RestaurantID == restorantId).ToList();
            var zamanlarTimeSpan = zamamlar.Select(time => TimeSpan.Parse(time)).ToList();
            var varolanTimes = varolanlar.Select(v => v.Time).ToList();
            var secilebilenler = zamanlarTimeSpan.Where(x => !varolanTimes.Contains(x)).ToList();
            var secilebilenlerString = secilebilenler.Select(x => x.ToString(@"hh\:mm")).ToList();

            return Json(secilebilenlerString);
        }

    }
}
