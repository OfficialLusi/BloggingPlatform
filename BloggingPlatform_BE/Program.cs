using BloggingPlatform_BE.Application.Services;
using BloggingPlatform_BE.Domain.Interfaces;
using BloggingPlatform_BE.Infrastructure.Repository;

namespace BloggingPlatform_BE;

public class Program
{
    private static void Main(string[] args)
    {
        // 1. crate builder
        WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

        // 2. adding configuration
        ConfigurationManager configuration = builder.Configuration;

        // 3. getting dir and paths from config
        #region sqlite db handling
        string baseDir = Directory.GetCurrentDirectory();
        string dbName = configuration["DatabaseName"];
        string dbPath = Path.Combine(baseDir, dbName);
        string connectionString = configuration.GetConnectionString("DBConnection");
        #endregion

        // 4. adding logging
        builder.Services.AddLogging(configure =>
        {
            configure.AddConsole();
            configure.SetMinimumLevel(LogLevel.Information);
        });

        // 5. adding services
        builder.Services.AddSingleton<IRepositoryService, RepositoryService>(provider =>
        {
            // Getting logger from di container
            ILogger<IRepositoryService> repoLogger = provider.GetRequiredService<ILogger<IRepositoryService>>();

            return new RepositoryService(connectionString, dbName, repoLogger);
        });

        builder.Services.AddSingleton<IAuthenticationService, AuthenticationService>(provider =>
        {
            // Getting repository service from container
            IRepositoryService repositoryService = provider.GetRequiredService<IRepositoryService>();

            return new AuthenticationService(repositoryService);
        });

        builder.Services.AddSingleton<IApplicationService, ApplicationService>(provider =>
        {
            // Getting dependencies from di container
            IRepositoryService repositoryService = provider.GetRequiredService<IRepositoryService>();
            IAuthenticationService authService = provider.GetRequiredService<IAuthenticationService>();
            ILogger<ApplicationService> appLogger = provider.GetRequiredService<ILogger<ApplicationService>>();

            return new ApplicationService(dbPath, repositoryService, baseDir, appLogger, authService);
        });

        builder.Services.AddControllers();

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        // 6. build application
        WebApplication app = builder.Build();

        // 7. creating db if not exists
        // handling creating database at the start of the application
        using (IServiceScope scope = app.Services.CreateScope())
        {
            IServiceProvider services = scope.ServiceProvider;
            IApplicationService applicationService = services.GetRequiredService<IApplicationService>();
            applicationService.CreateDataBase();
        }

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }

}
