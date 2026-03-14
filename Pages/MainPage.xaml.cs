namespace SCTS___Android_Remote_Control_Application
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void StartRunButtonClicked(object sender, EventArgs e)
        {
            // Navigate to create account page
            await Navigation.PushAsync(new RunsPage());
        }
    }
}
