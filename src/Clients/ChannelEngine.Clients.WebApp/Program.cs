using ChannelEngineAPIClient;
using ChannelEngineAPIClient.Helpers;
using ChannelEngineApiModels;
using ChannelEngineContracts;
using ChannelEngineWrapperAPI.BusinessLogics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ChannelEngine.Clients.WebApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddOptions<UrlOptions>().Bind(builder.Configuration.GetSection(UrlOptions.Name));
            builder.Services.AddOptions<OrdersUriOptions>().Bind(builder.Configuration.GetSection(OrdersUriOptions.Name));

            builder.Services.AddSingleton<IHttpClientProviders, HttpClientProviders>();
            builder.Services.AddSingleton<IChannelEngineApiClient, ChannelEngineApiClient>();
            builder.Services.AddSingleton<IChannelEngineAPILogicAgent, ChannelEngineAPILogicAgent>();


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

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