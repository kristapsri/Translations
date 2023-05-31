using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using System.Data.SqlClient;
using System.Text;
using TranslationsAdmin.Repositories;
using TranslationsAdmin.Services;

namespace TranslationsAdmin
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Configure sql connection
            string connectionString = builder.Configuration.GetConnectionString("LocalTranslationsConnection");
            SqlConnection connection = new SqlConnection(connectionString);

            builder.Services.AddHttpContextAccessor();
            builder.Services.AddSingleton<IPasswordHashService, PasswordHashService>();

            builder.Services.AddSingleton<ISqlServerConnection, SqlServerConnection>();
            builder.Services.AddSingleton<ILanguageRepository, LanguageRepository>();
            builder.Services.AddSingleton<ILanguageModelService, LanguageModelService>();
            builder.Services.AddSingleton<IUserRepository, UserRepository>();
            builder.Services.AddSingleton<IUserService, UserService>();

            builder.Services.AddSingleton<IKafkaProducerService, KafkaProducerService>();

            builder.Services.AddSingleton<SqlConnection>(connection);

            builder.Services.AddHostedService<KafkaConsumerService>();

            builder.Services.AddControllers();

            builder.Services.AddLogging(loggingBuilder => {
                loggingBuilder.ClearProviders();
                loggingBuilder.AddSerilog(new LoggerConfiguration()
                   .MinimumLevel
                   .Debug()
                   .WriteTo.File("Logs/log.txt", rollingInterval: RollingInterval.Day)
                   .WriteTo.Console(outputTemplate: "{Timestamp:HH:mm} [{Level}] ({ThreadId}) {Message}{NewLine}{Exception}")
                   .CreateLogger());
            });

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] { }
                    }
                });
            });

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidIssuer = builder.Configuration["Jwt:Issuer"],
                    ValidAudience = builder.Configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
                };
            });

            var app = builder.Build();
            app.UseAuthentication();
            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }


            app.Run();
        }
    }
}