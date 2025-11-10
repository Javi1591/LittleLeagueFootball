using LittleLeagueFootball.Data;
using LittleLeagueFootball.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Text.Json;

namespace LittleLeagueFootball
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            // Configure Entity Framework and SQL Server
            builder.Services.AddDbContext<LeagueContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("LeagueContext")));

            // Register Healthz Check
            builder.Services.AddHealthChecks()
                .AddDbContextCheck<LeagueContext>("Database");  // Holds health check for LeagueContext

            // Register ILeagueService and LeagueService for Dependency Injection
            //  Scoped lifetime
            builder.Services.AddScoped<ILeagueService, LeagueService>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseAuthorization();

            app.MapStaticAssets();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}")
                .WithStaticAssets();

            // Define Healthz Response Writer
            //  Custom JSON response for health checks
            //  Use static async Task WriteHealthResponse
            static async Task WriteHealthResponse(HttpContext context, HealthReport report)
            {

                // Set content type to application/json
                context.Response.ContentType = "application/json";

                // Create a list to hold all health check results
                var checksList = new List<object>();

                // foreach Loop to step through each health check entry
                foreach (var entry in report.Entries)
                {

                    // For each check, record its name, current status, error type
                    checksList.Add(new
                    {
                        name = entry.Key,                               // Holds the name of the dependency
                        status = entry.Value.Status.ToString(),         // Holds current status
                        error = entry.Value.Exception?.GetType().Name   // Holds error type (No SECRETS)
                    });
                }

                // Build the overall health report object
                var body = new
                {
                    status = report.Status.ToString(), // Overall app status
                    checks = checksList                // Each dependency's result
                };

                // Convert health report to FORMATTED JSON string
                // Use JsonSerializer with indented option
                var options = new JsonSerializerOptions { WriteIndented = true };
                string json = JsonSerializer.Serialize(body, options);

                // Send JSON back
                await context.Response.WriteAsync(json);
            }

            // Map Healthz Endpoint
            //  Use /healthz path
            app.MapHealthChecks("/healthz", new HealthCheckOptions
            {

                // Use WriteHealthResponse to format output
                ResponseWriter = WriteHealthResponse,

                // Map health status to HTTP status codes
                //  Healthy and Degraded = 200, Unhealthy = 503
                //  Use ResultStatusCodes dictionary
                ResultStatusCodes =
                {
                    [HealthStatus.Healthy] = StatusCodes.Status200OK,                   // Healthy (200)
                    [HealthStatus.Degraded] = StatusCodes.Status200OK,                  // Warning (200)
                    [HealthStatus.Unhealthy] = StatusCodes.Status503ServiceUnavailable  // ERROR (503)
                }
            });

            app.Run();
        }
    }
}
