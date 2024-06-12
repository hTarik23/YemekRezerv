using Microsoft.AspNetCore.Mvc;
using Rezervasyon.Models;
using Rezervasyon.Insfracture;


namespace Rezervasyon.Controllers
{
    public class RestorantController : Controller
    {
        public IActionResult RestorantGir()
        {
            return View();
        }

        [HttpPost]
        public IActionResult RestorantGir(RestorantAddModel RestorantAddModel)
        {
            var dataaccess = new DataAccess();
            var result = dataaccess.AddRestorant(RestorantAddModel.Ad, RestorantAddModel.Adress, RestorantAddModel.TelefonNumarası);
			return RedirectToAction("Restorantlar");

		}
        public async Task<IActionResult> Restorantlar()
        {
            var dataaccess = new DataAccess();
            var viewmodel = await dataaccess.TumRestoratlariGetir();
            if (viewmodel is null || !viewmodel.Any())
            {
                return RedirectToAction("RestorantGir");
            }
            return View(viewmodel);
        }

        public async Task<IActionResult> RestorantDuzenle(int restorantId)
        {
            var dataaccess = new DataAccess();
            var RestorantList = await dataaccess.TumRestoratlariGetir();
            var resultModel = RestorantList.Where(x => x.RestoranID == restorantId).Select(x => new RestorantDuzenle(x.RestoranID, x.Ad, x.Adress, x.Telefon)).FirstOrDefault();
            return View(resultModel);
        }

        [HttpPost]
        public IActionResult RestorantSil(int Restorantd)
        {
            var dataaccess = new DataAccess();
            var result = dataaccess.RestorantSil(Restorantd);
            return RedirectToAction("Restorantlar");

        }
        [HttpPost]
        public IActionResult RestorantDuzenle(RestorantDuzenle RestorantDuzenle)
        {
            var dataaccess = new DataAccess();
            dataaccess.UpdateRestorant(RestorantDuzenle.RestoranID, RestorantDuzenle.Ad, RestorantDuzenle.Adress, RestorantDuzenle.Telefon);
            return RedirectToAction("Restorantlar");
        }
    }
}
