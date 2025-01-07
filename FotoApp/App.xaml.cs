using FotoApp.Repositories;

namespace FotoApp
{
    public partial class App : Application
    {
        private UserRepository _userRepository;

        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new Waitpage());
        }

        protected override async void OnStart()
        {
            // Initialize the user repository
            _userRepository = new UserRepository();

            // Check if user credentials are saved
            var username = await SecureStorage.GetAsync("username");
            var password = await SecureStorage.GetAsync("password");

            if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password))
            {
                var user = _userRepository.LoginUser(username, password);
                if (user != null)
                {
                    // Pass the current user to AppShell
                    Application.Current.MainPage = new AppShell(user);

                    // Navigate to HomePage
                    await Shell.Current.GoToAsync("///HomePage");
                }
                else
                {
                    // Clear invalid credentials
                    SecureStorage.Remove("username");
                    SecureStorage.Remove("password");

                    // Show login page
                    Application.Current.MainPage = new NavigationPage(new LoginPage());
                }
            }
            else
            {
                // Show login page
                Application.Current.MainPage = new NavigationPage(new LoginPage());
            }
        }
    }
}
