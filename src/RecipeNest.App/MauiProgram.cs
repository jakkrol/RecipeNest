using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using RecipeNest.BackendServices;
using Microsoft.Extensions.DependencyInjection;
using RecipeNest.Services;
using RecipeNest.ViewModels;

namespace RecipeNest
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG
    		builder.Logging.AddDebug();
#endif
            builder.Services.AddSingleton(sp =>
            {
                return new HttpClient
                {
                    BaseAddress = new Uri("http://localhost:5264/")
                };
            });


            // --- SERWISY LOGICZNE I SESJA ---
            builder.Services.AddSingleton<UserSession>(); // TO JEST KLUCZOWE
            builder.Services.AddSingleton<RecipeService>(); // TO NAPRAWI TWÓJ BŁĄD
            builder.Services.AddSingleton<ShoppingListService>();

            // --- SERWISY API (Zewnętrzne i Twoje) ---
            builder.Services.AddSingleton<RecipeApiService>();
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


            return builder.Build();
        }
    }
}
