namespace SCTS___Android_Remote_Control_Application
{
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        private void LoginButtonClicked(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(licenceEntry.Text) && !string.IsNullOrWhiteSpace(passwordEntry.Text) && licenceEntry.Text == "420130" && passwordEntry.Text == "123456")
            {
                generalErrorLabel.IsVisible = false;
                OnLoginSuccess();
            }
            else
            {
                generalErrorLabel.IsVisible = true;
            }
        }

        private void OnLoginSuccess()
        {
            Preferences.Set("IsLoggedIn", true);

            // Remplace la page principale par AppShell
            if (Application.Current?.Windows.Count > 0)
            {
                Application.Current.Windows[0].Page = new AppShell();
            }
        }

        private async void CreateAccountButtonClicked(object sender, EventArgs e)
        {
            // Navigate to create account page
            await Navigation.PushAsync(new CreateAccountPage());
        }

        private void OnForgotPasswordTapped(object sender, TappedEventArgs e)
        {

        }
    }
}