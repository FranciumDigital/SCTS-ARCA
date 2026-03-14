using Microsoft.Extensions.DependencyInjection;
using SCTS___Android_Remote_Control_Application;
using Microsoft.Maui.Storage;

namespace SCTS___Android_Remote_Control_Application
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            bool isLoggedIn = Preferences.Get("IsLoggedIn", false);

            if (isLoggedIn)
            {
                return new Window(new AppShell());
            }
            else
            {
                return new Window(new NavigationPage(new LoginPage()));
            }
        }
    }
}