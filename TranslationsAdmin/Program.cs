using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using System.Globalization;
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

            builder.Services.AddSingleton<ISqlServerConnection, SqlServerConnection>();
            builder.Services.AddSingleton<ILanguageRepository, LanguageRepository>();
            builder.Services.AddSingleton<ILanguageModelService, LanguageModelService>();
            builder.Services.AddSingleton<SqlConnection>(connection);

            builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");
            builder.Services.AddControllersWithViews();

            // Configure supported cultures
            builder.Services.Configure<RequestLocalizationOptions>(options =>
            {
                var supportedCultures = new[]
                {
                    new CultureInfo("en-US"), // English (United States)
                    new CultureInfo("lv")
                };
                options.DefaultRequestCulture = new RequestCulture("en");
                options.SupportedCultures = supportedCultures;
                options.SupportedUICultures = supportedCultures;
            });

            // Add services to the container.

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseRequestLocalization();
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}