using TranslationsAdmin.Models;
using TranslationsAdmin.Repositories;

namespace TranslationsAdmin.Services;

public interface ILanguageModelService
{
    Task<LanguageModel?> Get(int id);
    Task<IEnumerable<LanguageModel>> GetAll(int page = 1, int perPage = 10);
    Task Create(LanguageModel language);
    Task Update(LanguageModel language);
}

public class LanguageModelService : ILanguageModelService
{
    private readonly ILanguageRepository _languageRepository;

    public LanguageModelService(ILanguageRepository languageRepository)
    {
        _languageRepository = languageRepository;
    }

    public async Task<LanguageModel?> Get(int id)
    {
        return await _languageRepository.Get(id);
    }

    public async Task<IEnumerable<LanguageModel>> GetAll(int page = 1, int perPage = 10)
    {
        return await _languageRepository.GetAll(page,perPage);
    }

    public async Task Create(LanguageModel language)
    {
        try
        {
            await _languageRepository.Create(language.Locale, language.Name);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }

    public async Task Update(LanguageModel language)
    {
        try
        {
            await _languageRepository.Update(language.Id, language.Locale, language.Name);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }
}