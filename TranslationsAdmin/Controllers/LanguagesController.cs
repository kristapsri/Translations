using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TranslationsAdmin.Controllers
{
    public class LanguagesController : Controller
    {
        // GET: TranslationController
        public ActionResult Index()
        {
            return View();
        }

        // GET: TranslationController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: TranslationController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TranslationController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: TranslationController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: TranslationController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: TranslationController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: TranslationController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
