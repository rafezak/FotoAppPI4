using FotoApp.Repositories;
using FotoApp.Modles;
using System;

namespace FotoApp.Views
{
    public partial class CreateTheme : ContentPage
    {
        public CreateTheme()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            // Retrieve the user from BindingContext
            if (BindingContext is User currentUser)
            {
                // Check if the current user is an Admin
                if (currentUser.Role != UserRoles.Admin)
                {
                    DisplayAlert("Unauthorized", "You do not have access to this page.", "OK");
                    Shell.Current.GoToAsync("//"); // Redirect to the home page or wherever appropriate
                }
            }
        }

        // Event handler to create a new theme
        private async void OnCreateThemeClicked(object sender, EventArgs e)
        {
            // Retrieve the theme name and color from the entries
            var theme = new Theme
            {
                Name = ThemeNameEntry.Text,

            };

            // Save the theme to the database (use ThemeRepository for this)
            var themeRepo = new ThemeRepository();
            themeRepo.AddTheme(theme);

            // Display success message
            await DisplayAlert("Success", "Theme created successfully.", "OK");

            // Redirect to another page (e.g., home page) after theme creation
            Navigation.PushAsync(new CreateTheme()); // Or use a different page as needed
        }
    }
}




