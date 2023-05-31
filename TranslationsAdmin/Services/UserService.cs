using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TranslationsAdmin.Models;
using TranslationsAdmin.Repositories;

namespace TranslationsAdmin.Services;

public interface IUserService
{
    public Task<IEnumerable<UserModel>> GetAll();
    public Task<string?> GetToken(string username, string password);
    public Task<bool> Create(string username, string password);
}

public class UserService : IUserService
{
    private readonly IConfiguration _config;
    private readonly IUserRepository _userRepository;
    private readonly ILogger<UserService> _logger;
    private readonly IPasswordHashService _passwordHashService;

    public UserService(IConfiguration config, ILogger<UserService> logger, IUserRepository userRepository, IPasswordHashService passwordHashService)
    {
        _config = config;
        _userRepository = userRepository;
        _logger = logger;
        _passwordHashService = passwordHashService;
    }

    public async Task<IEnumerable<UserModel>> GetAll()
    {
        return await _userRepository.GetAll();
    }

    async public Task<string?> GetToken(string username, string password)
    {
        string hashedPassword = _passwordHashService.HashPassword(password);
        Console.WriteLine(hashedPassword);
        var user = await _userRepository.GetUserByCredentials(username, hashedPassword);

        return user == null ? null : GenerateToken(user);
    }

    public async Task<bool> Create(string username, string password)
    { 
        string hashedPassword = _passwordHashService.HashPassword(password);

        try
        {
            await _userRepository.Create(username, hashedPassword);
            return true;
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return false;
        }
    }

    private string GenerateToken(UserModel user)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        var claims = new[] 
        {
            new Claim(JwtRegisteredClaimNames.Sub, _config["Jwt:Subject"]),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
            new Claim("UID", user.Id + ""),
            new Claim("Password", user.Password),
        };

        /*var claims = new[]
        {
  
            new Claim(ClaimTypes.NameIdentifier, user.Username),
        };*/

        var token = new JwtSecurityToken(
            _config["Jwt:Issuer"],
            _config["Jwt:Audience"],
            claims,
            expires: DateTime.Now.AddMinutes(60),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}