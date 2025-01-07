namespace FotoApp.Views;

using System.Collections.ObjectModel;
using FotoApp.Repositories;

public partial class HomePage : ContentPage
{
    private readonly AssignmentRepository _repository;
    public ObservableCollection<Assignments> Assignments { get; set; }

    public HomePage()
    {
        InitializeComponent();

        _repository = new AssignmentRepository();
        Assignments = new ObservableCollection<Assignments>();


    }

   


}