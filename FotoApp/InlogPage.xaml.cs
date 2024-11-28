namespace FotoApp;

public partial class InlogPage : ContentPage
{
	public InlogPage()
	{
		InitializeComponent();
	}

    private void LoginButton_Clicked(object sender, EventArgs e)
    {

		Navigation.PushAsync(new MainPage());

    }
}