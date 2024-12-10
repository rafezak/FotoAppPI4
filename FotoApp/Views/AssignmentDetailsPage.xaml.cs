using FotoApp.ViewModels;
using SQLite;
using FotoApp.JoinTables;
using FotoApp.Modles;
using FotoApp.Repositories;

namespace FotoApp.Views;

public partial class AssignmentDetailsPage : ContentPage
{
    private readonly AssignmentRepository _assignmentRepository;

    public AssignmentDetailsPage(AssignmentViewModel selectedAssignment)
	{
		InitializeComponent();
        BindingContext = selectedAssignment;
        
        _assignmentRepository = new AssignmentRepository();




    }

    private void JoinAssignmentButton_Clicked(object sender, EventArgs e)
    {
        var currentUser = AppShell.LoggedInUser;  // Assuming you have the logged-in user available
        var viewModel = BindingContext as AssignmentViewModel;  // Cast to AssignmentViewModel

        if (currentUser != null && viewModel != null && viewModel.Id != null)
        {
            // Join the assignment by adding a record to the AssignmentUser table
            _assignmentRepository.JoinAssignment(currentUser.Id, viewModel.Id);

            DisplayAlert("Success", "You have successfully joined the assignment.", "OK");
        }
        else
        {
            DisplayAlert("Error", "User or assignment not found.", "OK");
        }
    }
}


