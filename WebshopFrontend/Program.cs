using System.Net;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using WebshopFrontend.Components;
using WebshopLibrary;

namespace WebshopFrontend
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddAuthorization();
            builder.Services.AddAuthentication(IdentityConstants.ApplicationScheme)
	            .AddCookie(IdentityConstants.ApplicationScheme);

            builder.Services.AddCascadingAuthenticationState();
            builder.Services.AddScoped<CustomAuthStateProvider>();
            builder.Services.AddScoped<AuthenticationStateProvider>(sp =>
	            sp.GetRequiredService<CustomAuthStateProvider>());
            builder.Services.AddSingleton<CartService>();

            // Add services to the container.
            builder.Services.AddRazorComponents()
                .AddInteractiveServerComponents();

            builder.Services.AddBlazoredLocalStorage();

            builder.Services.AddHttpClient("MyApi", client =>
            {
	            client.BaseAddress = new Uri("https://localhost:7079/");
            }).ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler()
            {
	            UseCookies = true,
	            CookieContainer = new CookieContainer()
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseAntiforgery();

            app.MapStaticAssets();
            app.MapRazorComponents<App>()
                .AddInteractiveServerRenderMode();

            app.Run();
        }
    }
}
