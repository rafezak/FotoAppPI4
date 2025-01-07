using FotoApp.Repositories;
using System;
using Microsoft.Maui.Controls;
using FotoApp.Modles;
using FotoApp.Views;

namespace FotoApp.Views;

public partial class Profile : ContentPage
{

    private readonly UserRepository _userRepository;
    private readonly AssignmentRepository _assignmentRepository;
    public Profile()
	{
		InitializeComponent();

        _assignmentRepository = new AssignmentRepository();
        _userRepository = new UserRepository();

        LoadUserInfo();
        LoadJoinedAssignments();

    }

    private async void OnLogoutClicked(object sender, EventArgs e)
    {
        // Clear saved credentials
        SecureStorage.Remove("username");
        SecureStorage.Remove("password");

        // Navigate to login page
        Application.Current.MainPage = new NavigationPage(new LoginPage());
    }


    private void LoadUserInfo()
    {
        var currentuser = AppShell.LoggedInUser;

        if (currentuser != null)
        {
            Namelabel.Text = "username: " + currentuser.Username;
            PointLabel.Text = "Points: " + currentuser.Points.ToString();
            RoleLabel.Text = "Role " + currentuser.Role;


        }

        else
        {
            DisplayAlert("Error", "User not found", "OK");

        }

    }

    private void LoadJoinedAssignments()
    {
        var currentUser = AppShell.LoggedInUser;

        if (currentUser != null)
        {
            var assignments = _assignmentRepository.GetAssignmentsWithThemesForUser(currentUser.Id);

            AssignmentsStackLayout.Children.Clear();

            foreach (var assignment in assignments)
            {
                // Create a button for each joined assignment
                var assignmentButton = new Button
                {
                    Text = assignment.Name,
                    FontSize = 18,
                    Padding = new Thickness(10),
                    BackgroundColor = Colors.LightGray,
                    HorizontalOptions = LayoutOptions.FillAndExpand
                };

                // Set the assignment ID as the CommandParameter
                assignmentButton.CommandParameter = assignment.Id;

                // Attach the Clicked event handler
                assignmentButton.Clicked += OnAssignmentButtonClicked;

                AssignmentsStackLayout.Children.Add(assignmentButton);
            }
        }
        else
        {
            DisplayAlert("Error", "Unable to load joined assignments.", "OK");
        }
    }


    private void OnAssignmentButtonClicked(object sender, EventArgs e)
    {
        if (sender is Button button && button.CommandParameter is int assignmentId)
        {
            // Fetch the assignments with themes
            var assignments = _assignmentRepository.GetAssignmentsWithThemes();

            // Find the selected assignment based on the assignment ID
            var selectedAssignment = assignments.FirstOrDefault(a => a.Id == assignmentId);

            if (selectedAssignment != null)
            {
                // Navigate to the details page, passing the selected assignment
                Navigation.PushAsync(new AssignmentDetailsPage(selectedAssignment));
            }
            else
            {
                DisplayAlert("Error", "Assignment not found.", "OK");
            }
        }
        else
        {
            DisplayAlert("Error", "Invalid button click or missing assignment ID.", "OK");
        }
    }

}