using FotoApp.Services;

namespace FotoApp.Views;

public partial class OpenAI : ContentPage
{
    private readonly OpenAiService _openai;
    public OpenAI()
	{
		InitializeComponent();
		_openai = new OpenAiService("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");
    }


    private async void OnChatClicked(object sender, EventArgs e)
    {
        try
        {
            string prompt = "What is the weather in the Netherlands today?";
            string response = await _openai.GetChatGPTResponse(prompt);

            await DisplayAlert("ChatGPT Response", response, "OK");
        }
        catch (Exception ex)
        {
            await DisplayAlert("kaas", ex.Message, "OK");
        }
    }

   private async void Idea_Clicked(object sender, EventArgs e)
   {
        try
        {
            waitlabel.IsVisible = true;
            waitlabel.Text = "Thinking... please wait";
            string prompt = "give me a random assignment idea for users to take a picture of something. Only the idea ";
            string response = await _openai.GetChatGPTResponse(prompt);


            string formattedResponse = string.Join("\n", response.Split(' ').Chunk(6).Select(chunk => string.Join(" ", chunk)));
            string action = await DisplayActionSheet(formattedResponse, "Cancel", null, null, "Create Assignment");
            waitlabel.IsVisible = false;





            if (action == "OK")
            {
                // User chose OK, do nothing or close the popup
                waitlabel.IsVisible = false;
            }
            else if (action == "Create Assignment")
            {
                waitlabel.IsVisible = false;

                // Navigate to CreateAssignmentPage and set BindingContext with response
                var createAssignmentPage = new CreateAssignment
                {
                    BindingContext = new { IdeaDescription = response }
                };

                // Passing the response
                await Navigation.PushAsync(createAssignmentPage);

            }
        }
        catch (Exception ex) {
            await DisplayAlert("alert", ex.Message, "OK");
        }
   }
}