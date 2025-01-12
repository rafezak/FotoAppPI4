using FotoApp.Views;
using FotoApp.Modles;
namespace FotoApp
{
    public partial class AppShell : Shell
    {
        private readonly User _currentUser;
        public static User LoggedInUser { get; private set; }


        public AppShell(User currentUser)
        {
            InitializeComponent();

            // Register routes for different pages
            Routing.RegisterRoute("HomePage", typeof(HomePage));
            Routing.RegisterRoute("HelperPage", typeof(OpenAI));
            Routing.RegisterRoute("FeedPage", typeof(Feed));
            Routing.RegisterRoute("ProfilePage", typeof(Profile));
            Routing.RegisterRoute("AdminPage", typeof(AdminPage));

            _currentUser = currentUser;
            LoggedInUser = currentUser;
            BindingContext = this;

            // Register route for CreateTheme page
            Routing.RegisterRoute(nameof(CreateTheme), typeof(CreateTheme));

            // Add the Admin toolbar item only if the user is an Admin
            if (_currentUser.Role == UserRoles.Admin)
            {
                Admintab.IsEnabled = true;
            }
        }

        // Property to display the user's points
        public string DisplayPoints
        {
            get
            {
                if (_currentUser != null)
                {
                    return $"Points: {_currentUser.Points}";
                }
                return "Points: 0"; // Default to 0 if no user found
            }
        }

        // Event handler for Home toolbar item click
        private void HomeToolbar_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new HomePage());
        }

        // Event handler for Assignments toolbar item click
        private void Assignments_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Assignments());
        }

      
        private void Admin_Clicked(object sender, EventArgs e)
        {
            // Check if the user is an admin before navigating
            if (_currentUser.Role == UserRoles.Admin)
            {
                Navigation.PushAsync(new AdminPage());
            }
            else
            {
                DisplayAlert("Access Denied", "You do not have permission to access this page.", "OK");
            }
        }

        // Event handler for Profile toolbar item click
        private void Profile_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Profile());
        }

        // Event handler for Feed toolbar item click
        private void Feed_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Feed());
        }

        // Event handler for Helper toolbar item click
        private void Helper_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new OpenAI());
        }
    }
}
