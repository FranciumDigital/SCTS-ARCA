using System.Diagnostics;
using System.Text.Json;

namespace SCTS___Android_Remote_Control_Application;

public partial class RunsPage : ContentPage
{
    private readonly Stopwatch _stopwatch = new();
    private bool _running = false;
    private CancellationTokenSource? _uiTimerCts;
    private const string PrefsKey = "RunTimes";

    public RunsPage()
    {
        InitializeComponent();
        LoadHistory();
    }

    private void StartStopButtonClicked(object sender, EventArgs e)
    {
        if (!_running)
        {
            _stopwatch.Start();
            _running = true;
            StartStopButton.Text = "Arrêter";
            _uiTimerCts = new CancellationTokenSource();
            Device.StartTimer(TimeSpan.FromMilliseconds(30), () =>
            {
                if (_uiTimerCts?.IsCancellationRequested == true) return false;
                TimerLabel.Text = FormatElapsed(_stopwatch.Elapsed);
                return !_uiTimerCts.IsCancellationRequested;
            });
        }
        else
        {
            _stopwatch.Stop();
            _running = false;
            StartStopButton.Text = "Démarrer";
            _uiTimerCts?.Cancel();
        }
    }

    private void ResetButtonClicked(object sender, EventArgs e)
    {
        _stopwatch.Reset();
        TimerLabel.Text = FormatElapsed(_stopwatch.Elapsed);
    }

    private async void SaveTimeButtonClicked(object sender, EventArgs e)
    {
        var time = FormatElapsed(_stopwatch.Elapsed);
        var list = LoadTimesFromPrefs();
        list.Insert(0, time); // newest first
        SaveTimesToPrefs(list);
        await DisplayAlert("Temps enregistré", time, "OK");
        LoadHistory();
    }

    private string FormatElapsed(TimeSpan ts)
    {
        return string.Format("{0:00}:{1:00}.{2:000}", (int)ts.TotalMinutes, ts.Seconds, ts.Milliseconds);
    }

    private void LoadHistory()
    {
        var list = LoadTimesFromPrefs();
        HistoryCollection.ItemsSource = list;
    }

    private static List<string> LoadTimesFromPrefs()
    {
        var json = Preferences.Get(PrefsKey, string.Empty);
        if (string.IsNullOrWhiteSpace(json)) return new List<string>();
        try
        {
            return JsonSerializer.Deserialize<List<string>>(json) ?? new List<string>();
        }
        catch
        {
            return new List<string>();
        }
    }

    private static void SaveTimesToPrefs(List<string> times)
    {
        var json = JsonSerializer.Serialize(times);
        Preferences.Set(PrefsKey, json);
    }
}

