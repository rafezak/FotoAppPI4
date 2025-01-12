using FotoApp.Modles;
using FotoApp.Repositories;
namespace FotoApp.Views
{ 

    
    public partial class CreateAssignment : ContentPage
    {
        private readonly AssignmentRepository _assignmentRepository;
        private readonly ThemeRepository _themeRepository;
        private readonly UserRepository _userRepository;

        public CreateAssignment()
        {
            InitializeComponent();

            // Initialize repositories
            _assignmentRepository = new AssignmentRepository();
            _themeRepository = new ThemeRepository();
            _userRepository = new UserRepository();

            // Load themes when the page initializes
            LoadThemes();

            
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            // Check if the BindingContext contains "IdeaDescription"
            if (BindingContext is { } context && context.GetType().GetProperty("IdeaDescription") is { } prop)
            {
                string ideaDescription = prop.GetValue(context)?.ToString();
                if (!string.IsNullOrEmpty(ideaDescription))
                {
                    DescriptionEditor.Text = ideaDescription; // Pre-fill the description field
                }
            }
        }


        private async void LoadThemes()
        {
            // Fetch all themes from the database
            var themes = _themeRepository.GetAllThemes();
            ThemePicker.ItemsSource = themes;
        }

        private async void OnCreateAssignmentClicked(object sender, EventArgs e)
        {
            var currentuser = AppShell.LoggedInUser;

            // Validate input
            if (string.IsNullOrWhiteSpace(TitleEntry.Text) ||
                string.IsNullOrWhiteSpace(DescriptionEditor.Text) ||
                ThemePicker.SelectedItem == null)
            {
                await DisplayAlert("Error", "Please fill out all fields and select a theme.", "OK");
                return;
            }

            if (currentuser.Points < 1)
            {
                DisplayAlert("Error", "You need at least 1 point to create an assignment.", "OK");
                return;
            }

            currentuser.Points -= 1;

            _userRepository.UpdateUser(currentuser);

            // Create a new assignment
            var newAssignment = new Assignment
            {
                Name = TitleEntry.Text,
                Description = DescriptionEditor.Text,
           
            };

            // Insert the assignment into the database
            _assignmentRepository.AddAssignment(newAssignment);

            // Link the assignment to the selected theme
            var selectedTheme = ThemePicker.SelectedItem as Theme;
            if (selectedTheme != null)
            {
                _assignmentRepository.AddThemeToAssignment(newAssignment.Id, selectedTheme.Id);
            }

            // Confirm success and navigate back
            await DisplayAlert("Success", "Assignment created and linked to theme!", "OK");
            await Navigation.PopAsync();
        }
    }
}
