using FotoApp;
using FotoApp.Modles;
using FotoApp.Repositories;

namespace FotoApp.Views;

public partial class Assignments : ContentPage
{
	private readonly AssignmentRepository _repository;


    public Assignments()
	{
        _repository = new AssignmentRepository();

		_repository.AddSampleData();


        InitializeComponent();


		
		
	}
}