using BlazorAuthServerSample.Data;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Identity.Web;
using Microsoft.Identity.Web.UI;
using System.Security.Claims;

namespace BlazorAuthServerSample
{
    public class Program
    {
 

        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services
                .AddAuthentication(OpenIdConnectDefaults.AuthenticationScheme)
                .AddMicrosoftIdentityWebApp(
                    builder.Configuration.GetSection("AzureAd"));

            //builder.Services
            //    .AddAuthorization(
            //        policy => policy.FallbackPolicy = policy.DefaultPolicy);
            //builder.Services.AddScoped<MyAuthenticationStateProvider>();
            //builder.Services.AddScoped<AuthenticationStateProvider>(sp => sp.GetRequiredService<MyAuthenticationStateProvider>());

            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("AuthenticatedUser", policy =>
                    policy.RequireAuthenticatedUser());
            });

            builder.Services.AddSingleton<WeatherForecastService>();
            builder.Services.AddRazorPages();

            builder.Services
                .AddServerSideBlazor();
                //.AddMicrosoftIdentityConsentHandler();

            builder.Services
                .AddControllersWithViews()
                .AddMicrosoftIdentityUI();

            builder.Services.AddTransient<IClaimsTransformation, MyClaimsTransformation>();

            var app = builder.Build();

            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization(); 

   
            app.MapControllers();
            app.MapBlazorHub();
            app.MapFallbackToPage("/_Host");
            app.Run();
        }
    }
}
