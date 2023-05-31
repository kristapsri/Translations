using TranslationsAdmin.Models;
using TranslationsAdmin.Repositories;

namespace TranslationsAdmin.Services;
public interface ILanguageModelService
{
    Task<LanguageModel?> Get(int id);
    Task<IEnumerable<LanguageModel>> GetAll(int page = 1, int perPage = 10);
    Task<LanguageModel?> Create(LanguageModel language);
    Task<bool> Update(LanguageModel language);
}

public class LanguageModelService : ILanguageModelService
{
    private readonly ILanguageRepository _languageRepository;
    private readonly ILogger<LanguageModelService> _logger;
    private readonly IKafkaProducerService _kafkaProducer;

    public LanguageModelService(ILanguageRepository languageRepository, ILogger<LanguageModelService> logger, IKafkaProducerService kafkaProducerService)
    {
        _languageRepository = languageRepository;
        _logger = logger;
        _kafkaProducer = kafkaProducerService;
    }

    public async Task<LanguageModel?> Get(int id)
    {
        return await _languageRepository.Get(id);
    }

    public async Task<IEnumerable<LanguageModel>> GetAll(int page = 1, int perPage = 10)
    {
        return await _languageRepository.GetAll(page, perPage);
    }

    public async Task<LanguageModel?> Create(LanguageModel language)
    {
        try
        {
            var languageData = await _languageRepository.Create(language.Locale, language.Name);
            return languageData;
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
        }

        return null;
    }

    public async Task<bool> Update(LanguageModel language)
    {
        try
        {
            await _languageRepository.Update(language.Id, language.Locale, language.Name);
            _kafkaProducer.SendLanguageUpdatedLog($"{language.Name} updated!");
            return true;
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return false;
        }
    }
}