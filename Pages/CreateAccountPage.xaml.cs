namespace SCTS___Android_Remote_Control_Application
{
    public partial class CreateAccountPage : ContentPage
    {
        private const string TempCode = "258369"; // code temporaire
        private const string ExistingLicense = "420130"; // licence existante pour test

        public CreateAccountPage()
        {
            InitializeComponent();
        }

        private async void OnBackButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        private async void ValidateLicenseClicked(object sender, EventArgs e)
        {
            if (LicenseEntry.Text == ExistingLicense)
            {
                await DisplayAlertAsync("Erreur", "Utilisateur existant", "OK");
                // Tu peux rediriger vers LoginPage
            }
            else
            {
                StepLicense.IsVisible = false;
                StepPassword.IsVisible = true;
            }
        }

        private async void ValidatePasswordClicked(object sender, EventArgs e)
        {
            string pwd = PasswordEntry.Text ?? "";
            if (pwd.Length == 6 && pwd.All(c => char.IsLetterOrDigit(c)))
            {
                StepPassword.IsVisible = false;
                StepEmail.IsVisible = true;
            }
            else
            {
                await DisplayAlertAsync("Erreur", "Le mot de passe doit faire 6 caractères lettres/chiffres", "OK");
            }
        }

        private async void SendVerificationCodeClicked(object sender, EventArgs e)
        {
            // Ici on simule l'envoi
            await DisplayAlertAsync("Code envoyé", $"Le code de vérification est {TempCode}", "OK");
            StepEmail.IsVisible = false;
            StepCode.IsVisible = true;
        }

        private async void ValidateCodeClicked(object sender, EventArgs e)
        {
            if (CodeEntry.Text == TempCode)
            {
                await DisplayAlertAsync("Succès", "Compte créé !", "OK");
                Preferences.Set("IsLoggedIn", true);
                Application.Current.MainPage = new AppShell();
            }
            else
            {
                await DisplayAlertAsync("Erreur", "Code incorrect", "OK");
            }
        }
    }
}