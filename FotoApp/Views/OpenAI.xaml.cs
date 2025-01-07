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
            string prompt = "give me a random assignment idea for users to take a picture of something ";
            string response = await _openai.GetChatGPTResponse(prompt);

            await DisplayAlert("ChatGPT Response", response, "OK");
        }
        catch (Exception ex)
        {
            await DisplayAlert("kaas", ex.Message, "OK");
        }

    }
}