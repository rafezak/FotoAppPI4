namespace FotoApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            // Set the startup page to AppShell
            MainPage = new LoginPage();

            // Navigate to InlogPage
        }
    }
}
