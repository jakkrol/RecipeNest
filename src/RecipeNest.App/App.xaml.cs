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
            return new Window(new AppShell());
        }
    }
}