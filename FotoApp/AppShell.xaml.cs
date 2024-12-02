using FotoApp.Views;
using FotoApp.Modles;
namespace FotoApp
{
    public partial class AppShell : Shell
    {
        private readonly User _currentUser;
        public User LoggedInUser { get; private set; }


        public AppShell(User currentUser)
        {
            InitializeComponent();
            _currentUser = currentUser;
            LoggedInUser = currentUser;
            BindingContext = this;

            // Add the Admin toolbar item only if the user is an Admin
            if (_currentUser.Role == UserRoles.Admin)
            {
                var adminToolbarItem = new ToolbarItem
                {
                    Text = "Admin",
                    IconImageSource = "admin_icon.png", // Optional icon
                    Command = new Command(async () =>
                    {
                        // Navigate to Admin page
                        Navigation.PushAsync(new AdminPage());
                    })
                };

                // Add the toolbar item for admins
                ToolbarItems.Add(adminToolbarItem);
            }

            if (_currentUser.Role == UserRoles.Admin)
            {
                var addThemeToolbarItem = new ToolbarItem
                {
                    Text = "Add Theme",
                    IconImageSource = "add_theme_icon.png", // Optional icon
                    Command = new Command(async () =>
                    {
                        // Navigate to AddThemePage
                        await Shell.Current.GoToAsync(nameof(CreateTheme));
                    })
                };

                // Add the toolbar item for admins
                ToolbarItems.Add(addThemeToolbarItem);
            }

            // Register the AddThemePage route
            Routing.RegisterRoute(nameof(CreateTheme), typeof(CreateTheme));
        }

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

    






            private void HomeToolbar_Clicked(object sender, EventArgs e)
            {
                Navigation.PushAsync(new MainPage());

            }

            private void NewPageToolbar_Clicked(object sender, EventArgs e)
            {
                Navigation.PushAsync(new CreateAssignment());
            }

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
        
    }
}
