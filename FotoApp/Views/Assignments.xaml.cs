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
        // Fetch assignments with themes
        var assignments = _repository.GetAssignmentsWithThemes();
        AssignmentsCollectionView.ItemsSource = assignments;
    }
}