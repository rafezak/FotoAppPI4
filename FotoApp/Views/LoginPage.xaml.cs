using FotoApp.Repositories;
using FotoApp.Views;
namespace FotoApp;

public partial class LoginPage : ContentPage
{
	private readonly UserRepository _userRepository;
    public LoginPage()
	{
		InitializeComponent();
        _userRepository = new UserRepository();

    }

    private async void OnLoginClicked(object sender, EventArgs e)
    {
        var username = UsernameEntry.Text;
        var password = PasswordEntry.Text;

        var user = _userRepository.LoginUser(username, password);
        if (user != null)
        {
            await DisplayAlert("Welcome", $"Hello {user.Username} ({user.Role})!", "OK");

            await SecureStorage.SetAsync("username", username);
            await SecureStorage.SetAsync("password", password);


            // Pass the current user to AppShell
            Routing.RegisterRoute("HomePage", typeof(HomePage));

            Application.Current.MainPage = new AppShell(user);

            // AppShell.xaml.cs
            await Shell.Current.GoToAsync("///HomePage");
        }
        else
        {
            await DisplayAlert("Error", "Invalid username or password.", "OK");
        }
    }


    private async void OnRegisterClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new RegisterPage());
    }
}