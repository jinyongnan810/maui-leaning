using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace MonkeyFinder.ViewModels;

[QueryProperty(nameof(Monkey), "Monkey")]
public partial class MonkeyDetailsViewModel : BaseViewModel
{
    readonly IMap map;
    [ObservableProperty]
    Monkey? monkey;
    public MonkeyDetailsViewModel(IMap map)
    {
        this.map = map;
        Title = "Monkey Details";
    }

    [RelayCommand]
    async Task OpenMap()
    {
        if (IsBusy)
            return;
        IsBusy = true;
        try
        {
            await map.OpenAsync(Monkey?.Latitude ?? 0, Monkey?.Longitude ?? 0, new MapLaunchOptions()
            {
                Name = Monkey?.Name ?? string.Empty,
                NavigationMode = NavigationMode.None
            });
        }
        catch (TaskCanceledException ex)
        {
            Debug.WriteLine($"OpenMap Cancelled: {ex}");
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"OpenMap Error: {ex}");
            await Shell.Current.DisplayAlert("Error", "Unable to open map.", "OK");
        }
        finally
        {
            IsBusy = false;
        }

    }
}
