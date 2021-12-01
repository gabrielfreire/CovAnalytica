using CovAnalytica.Server.Background;
using CovAnalytica.Server.Data;
using CovAnalytica.Shared.Interfaces;
using CovAnalytica.Shared.Models;
using CovAnalytica.Shared.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Reflection;

namespace CovAnalytica.Server.Extensions
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHttpClient();
            services.AddTransient<IGithubService, GithubService>();
            services.AddTransient<ICovidDataCSVService, CovidDataCSVService>();
            services.AddTransient<IQueryService, QueryService>();
            services.AddScoped<IDatabaseRepository, DatabaseRepository>();

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlite("Filename=coviddata.db", 
                    options => options.MigrationsAssembly(Assembly.GetExecutingAssembly().FullName)));

            services.AddSingleton<IMemoryStorage<CompleteCovidData>, TimeseriesDataMemoryStorage>();
            services.AddSingleton<IMemoryStorage<SelectionCovidData>, TotalsPerCountryDataMemoryStorage>();

            services.AddSingleton<BackgroundTasks>();
            services.AddSingleton<IHostedService>(sp => sp.GetService<BackgroundTasks>());

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "CovAnalytica API v1",
                    Version = "1.0",
                    Description = "REST API for CovAnalytica"
                });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
            return services;
        }
    }
}
