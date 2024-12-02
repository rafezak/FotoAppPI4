using FotoApp.Modles;
using FotoApp.Repositories;

namespace FotoApp.Views;


public partial class AdminPage : ContentPage
{
    private readonly UserRepository _userRepository;
    public AdminPage()
	{
		InitializeComponent();
        _userRepository = new UserRepository();
        Loadusers();

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

    private void Loadusers()
    {
        var users = _userRepository.GetAllUsers();
        UserListView.ItemsSource = users;
    }

    private void OnUserSelected(object sender, SelectedItemChangedEventArgs e)
    {
        if (e.SelectedItem is User selectedUser)
        {
            // Display additional details about the selected user
            DisplayAlert("User Selected", $"Username: {selectedUser.Username}\nPoints: {selectedUser.Points}", "OK");

            // Optionally, you could set the ListView's SelectedItem to null to clear the selection
            UserListView.SelectedItem = null;
        }
    }



    private async void OnEditPointsClicked(object sender, EventArgs e)
    {
        var button = sender as Button;
        if (button?.BindingContext is User user)
        {
            string result = await DisplayPromptAsync("Edit Points", $"Set new points for {user.Username}:", initialValue: user.Points.ToString());
            if (int.TryParse(result, out int newPoints))
            {
                _userRepository.UpdateUserPoints(user.Id, newPoints);
                Loadusers(); // Refresh the list
            }
        }
    }

    private async void DeleteUser_Clicked(object sender, EventArgs e)
    {
        var button = sender as Button;
        if (button?.BindingContext is User user)
        {
            bool confirmDelete = await DisplayAlert("Confirm Delete", "Are you sure you want to delete this user?", "Yes", "No");
            if (confirmDelete)
            {
                _userRepository.DeleteUser(user.Id);
                Loadusers(); // Reload the user list after deletion
            }
        }
    }
}