using TranslationsAdmin.Models;
using TranslationsAdmin.Services;

namespace TranslationsAdmin.Repositories;

public interface IUserRepository
{
    Task<UserModel?> GetUserByCredentials(string username, string password);
    Task Create(string username, string password);
    Task<UserModel?> Get(int id);
    Task<IEnumerable<UserModel>> GetAll();
}

public class UserRepository : IUserRepository
{
    private readonly ISqlServerConnection _db;
    public UserRepository(ISqlServerConnection db) => _db = db;
    public async Task<UserModel?> GetUserByCredentials(string username, string password)
    {
        var result = await _db.Execute<UserModel, dynamic>("dbo.users_authorize",
            new { Username = username, Password = password });

        return result.FirstOrDefault();
    }
    public async Task Create(string username, string password) => 
        await _db.Execute("dbo.users_create", new { Username = username, Password = password });

    public async Task<UserModel?> Get(int id)
    {
        var result = await _db.Execute<UserModel, dynamic>("dbo.users_get", new { Id = id });

        return result.FirstOrDefault();
    }

    public async Task<IEnumerable<UserModel>> GetAll() => await _db.Execute<UserModel, dynamic>("dbo.users_all", new { });
}