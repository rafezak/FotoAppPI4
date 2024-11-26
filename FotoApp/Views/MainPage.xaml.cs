
using FotoApp.Modles;
using SQLite;
using FotoApp.Views;
namespace FotoApp
{
    using System.Collections.ObjectModel;
    using FotoApp.Repositories;

    public partial class MainPage : ContentPage
    {
        private readonly AssignmentRepository _repository;
        public ObservableCollection<Assignment> Assignments { get; set; }
    
        public MainPage()
        {
            InitializeComponent();

            _repository = new AssignmentRepository();
            Assignments = new ObservableCollection<Assignment>(_repository.GetAllAssignments());

            // Bind the collection to the CollectionView
            AssignmentsCollectionView.ItemsSource = Assignments;

            // Add a test assignment if the database is empty
            if (!Assignments.Any())
            {
                AddSampleData();
            }
        }

        private void AddSampleData()
        {
            var sampleAssignment = new Assignment
            {
                Name = "Sample Assignment",
                Description = "This is a sample description.",
            
            };

            _repository.AddAssignment(sampleAssignment);
            Assignments.Add(sampleAssignment);
        }

        
    }

}
