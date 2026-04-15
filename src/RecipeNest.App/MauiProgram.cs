using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using RecipeNest.BackendServices;
using Microsoft.Extensions.DependencyInjection;
using RecipeNest.Services;
using RecipeNest.ViewModels;
using SkiaSharp.Views.Maui.Controls.Hosting;
using RecipeNest.DbConfig;

namespace RecipeNest
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseSkiaSharp()
                .UseMauiCommunityToolkit()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG
    		builder.Logging.AddDebug();
#endif
            //builder.Services.AddSingleton(sp =>
            //{
            //    return new HttpClient
            //    {
            //        BaseAddress = new Uri("http://localhost:5264/")
            //    };
            //});

            //builder.Services.AddHttpClient<BaseApiService>(c =>
            //{
            //    c.BaseAddress = new Uri("http://localhost:5264/");
            //});
            builder.Services.AddHttpClient(string.Empty, c =>
            {
                c.BaseAddress = new Uri("http://localhost:5264/");
            });
            builder.Services.AddHttpClient<RecipeApiService>(c =>
            {
                c.BaseAddress = new Uri("https://www.themealdb.com/api/json/v1/1/");
            });


            // --- SERWISY LOGICZNE I SESJA ---
            builder.Services.AddSingleton<UserSession>();
            builder.Services.AddSingleton<RecipeService>(); 
            builder.Services.AddSingleton<ShoppingListService>();

            // --- SERWISY API ---
            //builder.Services.AddSingleton<RecipeApiService>();
            builder.Services.AddSingleton<UserApiService>();
            builder.Services.AddSingleton<AuthService>();

            // --- VIEWMODELE ---
            builder.Services.AddTransient<RecipeLibraryViewModel>();
            builder.Services.AddTransient<RecipeDetailPageViewModel>();
            builder.Services.AddTransient<MainPageViewModel>();
            builder.Services.AddTransient<RecipeViewModel>();
            builder.Services.AddTransient<AddRecipeViewModel>();
            builder.Services.AddTransient<AddShoppingListViewModel>();
            builder.Services.AddTransient<ShoppingListViewModel>();
            builder.Services.AddTransient<ShoppingListDetailsViewModel>();



            // --- STRONY ---
            builder.Services.AddTransient<MainPage>();
            builder.Services.AddTransient<RecipesLibraryPage>();
            builder.Services.AddTransient<RecipeDetailPage>();
            builder.Services.AddTransient<AddRecipePage>();
            builder.Services.AddTransient<AddShoppingListPage>();


            // ---- BAZA -----
            builder.Services.AddSingleton<RecipeNestDb>();

            return builder.Build();
        }
    }
}
