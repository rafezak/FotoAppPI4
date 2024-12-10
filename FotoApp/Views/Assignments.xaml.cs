using FotoApp;
using FotoApp.Modles;
using FotoApp.Repositories;

namespace FotoApp.Views;

public partial class Assignments : ContentPage
{
	private readonly AssignmentRepository _repository;

	
    public Assignments()
	{
        InitializeComponent();
        _repository = new AssignmentRepository();

        LoadAssignments();














    }

    private void LoadAssignments()
    {
        var assignments = _repository.GetAssignmentsWithThemes();
        AssignmentsCollectionView.ItemsSource = assignments;
        
    }

     private void OnAssignmentButtonClicked(object sender, TappedEventArgs e)
    {
    }

    private void NewPageToolbar_Clicked(object sender, EventArgs e)
    {
        
        Navigation.PushAsync(new CreateAssignment());
    }

    private void Details_Clicked(object sender, EventArgs e)
    {
        if (sender is Button button && button.CommandParameter is int assignmentId)
        {
            // Fetch the assignments with themes
            var assignments = _repository.GetAssignmentsWithThemes();

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

       
    }
}