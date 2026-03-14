namespace SCTS___Android_Remote_Control_Application
{
    public partial class HistoryPage : ContentPage
    {
        private const string PrefsKey = "RunTimes";

        public HistoryPage()
        {
            InitializeComponent();
            LoadHistory();
        }

        private void LoadHistory()
        {
            var list = LoadTimesFromPrefs();
            RunsHistoryCollection.ItemsSource = list;
        }

        private static List<string> LoadTimesFromPrefs()
        {
            var json = Preferences.Get(PrefsKey, string.Empty);
            if (string.IsNullOrWhiteSpace(json)) return new List<string>();
            try
            {
                return System.Text.Json.JsonSerializer.Deserialize<List<string>>(json) ?? new List<string>();
            }
            catch
            {
                return new List<string>();
            }
        }
    }
}

