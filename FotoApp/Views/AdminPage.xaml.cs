using FotoApp.Modles;

namespace FotoApp.Views;


public partial class AdminPage : ContentPage
{
	public AdminPage()
	{
		InitializeComponent();
	}

    protected override void OnAppearing()
    {
        base.OnAppearing();

        // Retrieve the user from BindingContext
        if (BindingContext is User currentUser)
        {
            if (currentUser.Role != UserRoles.Admin)
            {
                DisplayAlert("Unauthorized", "You do not have access to this page.", "OK");
                Shell.Current.GoToAsync("//"); // Redirect to the home page
            }

        }
        
    }
}