using FotoApp.Modles;
using FotoApp.Repositories;
namespace FotoApp.Views
{ 

    
    public partial class CreateAssignment : ContentPage
    {
        private readonly AssignmentRepository _assignmentRepository;
        private readonly ThemeRepository _themeRepository;

        public CreateAssignment()
        {
            InitializeComponent();

            // Initialize repositories
            _assignmentRepository = new AssignmentRepository();
            _themeRepository = new ThemeRepository();

            // Load themes when the page initializes
            LoadThemes();
        }

        private async void LoadThemes()
        {
            // Fetch all themes from the database
            var themes = _themeRepository.GetAllThemes();
            ThemePicker.ItemsSource = themes;
        }

        private async void OnCreateAssignmentClicked(object sender, EventArgs e)
        {
            // Validate input
            if (string.IsNullOrWhiteSpace(TitleEntry.Text) ||
                string.IsNullOrWhiteSpace(DescriptionEditor.Text) ||
                ThemePicker.SelectedItem == null)
            {
                await DisplayAlert("Error", "Please fill out all fields and select a theme.", "OK");
                return;
            }

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
