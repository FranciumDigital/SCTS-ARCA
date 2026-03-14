namespace SCTS___Android_Remote_Control_Application
{
    public partial class SettingsPage : ContentPage
    {
        public SettingsPage()
        {
            InitializeComponent();
        }

        private void TestButtonClicked(object sender, EventArgs e)
        {

        }

        private void LogoutButtonClicked(object sender, EventArgs e)
        {
            Preferences.Set("IsLoggedIn", false);

            if (Application.Current?.Windows.Count > 0)
            {
                Application.Current.Windows[0].Page = new LoginPage();
            }
        }
    }
}
