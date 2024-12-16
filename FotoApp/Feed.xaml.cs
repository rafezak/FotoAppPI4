using FotoApp.Modles;
using FotoApp.Repositories;
using FotoApp.ViewModels;
using System.Collections.ObjectModel;

namespace FotoApp;

public partial class Feed : ContentPage
{
    private readonly PictureRepository _pictureRepository = new PictureRepository();
    private readonly CommentRepository _commentRepository = new CommentRepository();
    private readonly AssignmentRepository _assignmentRepository = new AssignmentRepository();

    public ObservableCollection<Assignment> Assignments { get; set; }
    public ObservableCollection<PictureCommentViewModel> Pictures { get; set; }
    public Feed()
	{
		InitializeComponent();
        _commentRepository = new CommentRepository();
        _pictureRepository = new PictureRepository();
        _assignmentRepository = new AssignmentRepository();

        Assignments = new ObservableCollection<Assignment>(_assignmentRepository.GetAllAssignments());
        Pictures = new ObservableCollection<PictureCommentViewModel>();

        BindingContext = this;
    }

    private void Assignmentpicker_SelectedIndexChanged(object sender, EventArgs e)
    {
        var picker = sender as Picker;
        var selectedAssignment = picker.SelectedItem as Assignment;

        if (selectedAssignment != null)
        {
            // Fetch pictures for the selected assignment
            var pictures = _pictureRepository.GetPicturesForAssignment(selectedAssignment.Id);

            // Clear existing pictures
            Pictures.Clear();

            // Populate pictures with associated comments
            foreach (var picture in pictures)
            {
                var comments = _commentRepository.GetCommentsForPicture(picture.Id);
                Pictures.Add(new PictureCommentViewModel(picture, comments));
            }


        }
    }

    private void OnAssignmentSelectedChanged(object sender, EventArgs e)
    {
        var selectedAssignmentId = Assignmentpicker.SelectedIndex + 1; // Assuming assignment ids are 1-based.
        LoadPicturesForAssignment(selectedAssignmentId);
        

    }

    // Load pictures for the selected assignment
    private void LoadPicturesForAssignment(int assignmentId)
    {
        var pictures = _pictureRepository.GetPicturesForAssignment(assignmentId);

        var picturesWithComments = new ObservableCollection<PictureCommentViewModel>();

        foreach (var picture in pictures)
        {
            var comments = _commentRepository.GetCommentsForPicture(picture.Id);

            // Create the view model to hold the picture and its comments
            picturesWithComments.Add(new PictureCommentViewModel(picture, comments));
        }

        // Bind the pictures and comments to the CollectionView
        PictureCollectionView.ItemsSource = picturesWithComments;
    }

    // Handle Like button click
    private void OnLikeClicked(object sender, EventArgs e)
    {
        if (sender is Button button && button.CommandParameter is int pictureId)
        {
            _pictureRepository.LikePicture(pictureId);
            LoadPicturesForAssignment(Assignmentpicker.SelectedIndex + 1);  // Refresh pictures after liking

        }
    }

    // Handle Comment button click
    private async void OnCommentClicked(object sender, EventArgs e)
    {
        if (sender is Button button && button.CommandParameter is int pictureId)
        {
            var comment = await DisplayPromptAsync("Add Comment", "Enter your comment:");

            if (!string.IsNullOrWhiteSpace(comment))
            {
                // Add the comment to the database
                _commentRepository.AddComment(pictureId, AppShell.LoggedInUser.Id, comment);

                // Refresh pictures and their comments
                LoadPicturesForAssignment(Assignmentpicker.SelectedIndex + 1);
            }
        }
    }

}