using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using RecipeNest.BackendServices;
using Microsoft.Extensions.DependencyInjection;

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
            builder.Services.AddHttpClient<AuthService>();

            return builder.Build();
        }
    }
}
