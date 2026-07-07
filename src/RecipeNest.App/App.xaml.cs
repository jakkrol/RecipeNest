namespace RecipeNest
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            Task.Run(async () => await RecipeNest.TesseractFolder.TesseractHelper.CopyTessdataFilesAsync()).Wait();
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            var shell = IPlatformApplication.Current.Services.GetService<AppShell>();
            return new Window(shell);
            //return new Window(new AppShell());
        }
    }
}