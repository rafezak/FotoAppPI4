using FotoApp.ViewModels;
using SQLite;
using FotoApp.JoinTables;
using FotoApp.Modles;
using FotoApp.Repositories;

namespace FotoApp.Views;

public partial class AssignmentDetailsPage : ContentPage
{
    private readonly AssignmentRepository _assignmentRepository;
    private readonly PictureRepository _pictureRepository;
    private AssignmentViewModel _currentAssignment;

    public AssignmentDetailsPage(AssignmentViewModel selectedAssignment)
	{
		InitializeComponent();

        _currentAssignment = selectedAssignment;

        _assignmentRepository = new AssignmentRepository();
        _pictureRepository = new PictureRepository();

        var currentUser = AppShell.LoggedInUser;
        if (currentUser != null)
        {
            // Check if the user has already joined this assignment
            var hasJoined = _assignmentRepository.HasUserJoinedAssignment(currentUser.Id, selectedAssignment.Id);
            SetButtonVisibility(hasJoined);
        }

        BindingContext = selectedAssignment;







    }

    private void SetButtonVisibility(bool hasJoined)
    {
        // If the user has joined the assignment, hide the "Join Assignment" button and show the "Upload Picture" button
        JoinAssignmentButton.IsVisible = !hasJoined;
        UploadPictureButton.IsVisible = hasJoined;
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

    private async void OnUploadPictureClicked(object sender, EventArgs e)
    {
        try
        {
            // Open media picker to select an image
            var result = await FilePicker.PickAsync(new PickOptions
            {
                FileTypes = FilePickerFileType.Images,
                PickerTitle = "Select a picture to upload"
            });

            if (result != null)
            {
                // Save the picture to the database
                var newPicture = new Picture
                {
                    AssignmentId = _currentAssignment.Id, // Use the current assignment's ID
                    ImagePath = result.FullPath,
                    UploadedAt = DateTime.Now
                };

                _pictureRepository.AddPicture(newPicture);

                // Refresh the pictures displayed
                LoadPictures();

                await DisplayAlert("Success", "Picture uploaded successfully!", "OK");
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"Failed to upload picture: {ex.Message}", "OK");
        }
    }


    private void LoadPictures()
    {
        if (_currentAssignment == null)
            return;

        // Fetch pictures for the current assignment
        var pictures = _pictureRepository.GetPicturesForAssignment(_currentAssignment.Id);
        PicturesCollectionView.ItemsSource = pictures;
    }

}


