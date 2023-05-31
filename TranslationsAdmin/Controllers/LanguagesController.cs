using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TranslationsAdmin.Models;
using TranslationsAdmin.Services;

namespace TranslationsAdmin.Controllers
{
    [Route("api/languages")]
    [ApiController]
    public class LanguagesController : Controller
    {
        private readonly ILanguageModelService _languageModelService;
        private readonly ILogger<LanguagesController> _logger;

        public LanguagesController(ILanguageModelService languageModelService, ILogger<LanguagesController> logger)
        {
            _languageModelService = languageModelService;
            _logger = logger;
        }

        [Authorize]
        [HttpGet("all")]
        public async Task<IActionResult> GetAll()
        {
            _logger.LogInformation("All languages requested");

            return Ok(await _languageModelService.GetAll());
        }

        [Authorize]
        [HttpGet("detail")]
        public async Task<IActionResult> Details(int id)
        {
            var language = await _languageModelService.Get(id);
            return language != null ? Ok(language) : NotFound();
        }

        [Authorize]
        [HttpPost("create")]
        public async Task<IActionResult> Create([Bind("Id,Locale,Name")] LanguageModel language)
        {
            var result = await _languageModelService.Create(language);

            return result != null ? Ok(result) : BadRequest();
        }

        [Authorize]
        [HttpPost("update")]
        public async Task<IActionResult> Update(int id, [Bind("Id,Locale,Name")] LanguageModel language)
        {
            if (id != language.Id)
            {
                return BadRequest();
            }

            bool result = await _languageModelService.Update(language);

            return result ? Ok() : NotFound();
        }
    }
}
