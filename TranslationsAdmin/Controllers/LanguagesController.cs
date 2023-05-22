using Microsoft.AspNetCore.Mvc;
using TranslationsAdmin.Models;
using TranslationsAdmin.Services;

namespace TranslationsAdmin.Controllers
{
    public class LanguagesController : Controller
    {
        private readonly ILanguageModelService _languageModelService;
        public LanguagesController(ILanguageModelService languageModelService)
        {
            _languageModelService = languageModelService;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _languageModelService.GetAll());
        }

        public async Task<IActionResult> Details(int id)
        {
            var language = await _languageModelService.Get(id);
            if (language == null)
            {
                return NotFound();
            }
            return View(language);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Locale,Name")] LanguageModel language)
        {
            if (ModelState.IsValid)
            {
                await _languageModelService.Create(language);
                return RedirectToAction(nameof(Index));
            }
            return View(language);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var language = await _languageModelService.Get(id);
            if (language == null)
            {
                return NotFound();
            }
            return View(language);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Locale,Name")] LanguageModel language)
        {
            if (id != language.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _languageModelService.Update(language);
                return RedirectToAction(nameof(Index));
            }

            return View(language);
        }
    }
}
