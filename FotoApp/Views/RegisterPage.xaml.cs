namespace FotoApp.Views;
using FotoApp.Repositories;

public partial class RegisterPage : ContentPage
{
    private readonly UserRepository _userRepository;

    public RegisterPage()
    {
        InitializeComponent();
        _userRepository = new UserRepository();
    }

    private async void OnRegisterClicked(object sender, EventArgs e)
    {
        var username = UsernameEntry.Text;
        var password = PasswordEntry.Text;

        if (_userRepository.RegisterUser(username, password))
        {
            await DisplayAlert("Success", "Registration successful!", "OK");
            await Navigation.PopAsync(); // Navigate back
        }
        else
        {
            await DisplayAlert("Error", "Username already exists.", "OK");
        }
    }
}
