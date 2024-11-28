using FotoApp.Views;

namespace FotoApp
{
    
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(InlogPage), typeof(InlogPage));

        }


        private void HomeToolbar_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new MainPage());

        }

        private void NewPageToolbar_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new CreateAssignment());
        }

        private void Assignments_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Assignments());

        }
    }
}
