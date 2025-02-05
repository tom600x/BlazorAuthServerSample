using Azure.Core;
using BlazorAuthServerSample.Data;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Identity.Web;
using Microsoft.Identity.Web.UI;
using System.Net;

namespace BlazorAuthServerSample
{
    public class Program
    {
         public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add authentication services with OpenID Connect and Microsoft Identity Web App
            builder.Services.AddAuthentication(OpenIdConnectDefaults.AuthenticationScheme)
                .AddMicrosoftIdentityWebApp(options =>
                {
                    builder.Configuration.Bind("AzureAd", options);
                    options.Events.OnRedirectToIdentityProvider = context =>
                    {  
                        string host =  context.HttpContext.Request.Host.Value;
                        context.ProtocolMessage.RedirectUri = $"https://{host}/signin-oidc";
                        return Task.CompletedTask;
                    };
                });

            // Add core authentication services
            builder.Services.AddAuthenticationCore();
            // Add WeatherForecastService as a singleton service
            builder.Services.AddSingleton<WeatherForecastService>();
            // Add Razor Pages services
            builder.Services.AddRazorPages();
            // Add Blazor Server services
            builder.Services.AddServerSideBlazor();
            // Add controllers with views and Microsoft Identity UI
            builder.Services.AddControllersWithViews().AddMicrosoftIdentityUI();
            // Add custom claims transformation service
            builder.Services.AddTransient<IClaimsTransformation, MyClaimsTransformation>();

            builder.Services.AddHttpContextAccessor(); // Enable HttpContext access

            var app = builder.Build();

            // Configure forwarded headers options
            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedProto | ForwardedHeaders.XForwardedHost
            });

            if (!app.Environment.IsDevelopment())
            {
                // Use exception handler and HSTS in non-development environments
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            // Use HTTPS redirection
            app.UseHttpsRedirection();
            // Serve static files
            app.UseStaticFiles();
            // Use routing
            app.UseRouting();
            // Use authentication
            app.UseAuthentication();
            // Use authorization
            app.UseAuthorization();

            // Map controllers
            app.MapControllers();
            // Map Blazor Hub
            app.MapBlazorHub();
            // Map fallback to page
            app.MapFallbackToPage("/_Host");
            // Run the application
            app.Run();
        }
    }
}
