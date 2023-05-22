using TranslationsAdmin.Models;
using TranslationsAdmin.Services;

namespace TranslationsAdmin.Repositories
{
    public interface ILanguageRepository
    {
        Task<LanguageModel?> Get(int id);
        Task<IEnumerable<LanguageModel>> GetAll(int page = 1, int perPage = 10);
        Task Create(string locale, string name);
        Task Update(int id, string locale, string name);
    }

    public class LanguageRepository : ILanguageRepository
    {
        private readonly ISqlServerConnection _db;
        public LanguageRepository(ISqlServerConnection db) => _db = db;
        public async Task<LanguageModel?> Get(int id)
        {
            var result = await _db.Execute<LanguageModel, dynamic>("dbo.languages_detail", new { Id = id });

            return result.FirstOrDefault();
        }
        public async Task<IEnumerable<LanguageModel>> GetAll(int page = 1, int perPage = 10)
        {
            page = page < 0 ? 1 : page;
            perPage = perPage < 1 ? 1 : 10;
            page--;

            return await _db.Execute<LanguageModel, dynamic>("dbo.languages_select", new { Offset = page, Limit = perPage });
        }

        public Task Create(string locale, string name) => _db.Execute("dbo.languages_create", new
        {
            Locale = locale,
            Name = name
        });

        public Task Update(int id, string locale, string name) => _db.Execute("dbo.languages_update", new
        {
            Id = id,
            Locale = locale,
            Name = name
        });
    }
}
