using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace ASN
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRouting(options => options.LowercaseUrls = true);

            // Add distributed memory cache
            builder.Services.AddDistributedMemoryCache();

            // Add controllers with views
            builder.Services.AddControllersWithViews();

            // Authentication configuration
            builder.Services.AddAuthentication(options =>
            {
                // Set authentication options here
            });

            builder.Services.AddAuthorization();

            builder.Services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromSeconds(3600); // Set session timeout
            });

            // Register IHttpContextAccessor as a singleton
            builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            builder.Services.AddScoped<Filters.FilterAuthCheck>();

            // Load configuration from appsettings.json
            builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                                 .AddEnvironmentVariables();

            // Clear and configure logging providers
            builder.Logging.ClearProviders();
            // Add additional logging providers if needed, e.g., builder.Logging.AddConsole();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts(); 
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseSession(); 
            app.UseRouting();

            app.UseAuthentication(); 
            app.UseAuthorization();  

           
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Account}/{action=Login}/{id?}");

            // Run the application
            app.Run();
        }
    }
}
