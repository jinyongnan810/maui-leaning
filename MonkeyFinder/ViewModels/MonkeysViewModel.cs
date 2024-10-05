using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MonkeyFinder.Services;

namespace MonkeyFinder.ViewModels;

public partial class MonkeysViewModel : BaseViewModel
{
    private MonkeyService monkeyService;
    private IConnectivity connectivity;
    private IGeolocation geolocation;

    [ObservableProperty]
    public List<Monkey>? monkeys;

    // public Command LoadMonkeysCommand { get; }
    public MonkeysViewModel(MonkeyService monkeyService, IConnectivity connectivity, IGeolocation geolocation)
    {
        Title = "Monkeys Finder";
        this.monkeyService = monkeyService;
        this.connectivity = connectivity;
        this.geolocation = geolocation;
        // this.LoadMonkeysCommand = new Command(async()=>await LoadMonkeys());
    }

    [RelayCommand]
    async Task LoadMonkeys()
    {
        if (IsBusy)
            return;
        try
        {
            if (connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                await Shell.Current.DisplayAlert("No Internet", "No internet connection available", "OK");
                return;
            }
            IsBusy = true;
            Debug.Print("Loading Monkeys.");
            var monkeys = await monkeyService.GetMonkeys();
            Debug.Print("Monkeys loaded.");
            if (monkeys != null)
            {
                if (Monkeys is null)
                {
                    Monkeys = new List<Monkey>(monkeys);
                }
                else if (monkeys.Count != 0)
                {
                    Monkeys?.Clear();
                    Monkeys?.AddRange(monkeys);
                }

            }
        }
        catch (TaskCanceledException ex)
        {
            Debug.WriteLine($"LoadMonkeys Cancelled: {ex}");
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"LoadMonkeys Error: {ex}");
            await Shell.Current.DisplayAlert("Error", "Unable to load monkeys.", "OK");
        }
        finally
        {
            IsBusy = false;
        }

    }

    [RelayCommand]
    async Task GoToMonkeyDetail(Monkey monkey)
    {
        if (monkey == null)
            return;
        await Shell.Current.GoToAsync($"{nameof(DetailsPage)}", true, new Dictionary<string, object> { { nameof(Monkey), monkey } });
    }

    [RelayCommand]
    async Task GetClosestMonkey()
    {
        if (IsBusy || this.Monkeys?.Count == 0)
            return;

        var status = await Permissions.CheckStatusAsync<Permissions.LocationWhenInUse>();

        if (status != PermissionStatus.Granted)
        {
            var permissionStatus = await Permissions.RequestAsync<Permissions.LocationWhenInUse>();
            if (permissionStatus != PermissionStatus.Granted)
            {
                await Shell.Current.DisplayAlert("Location Permission Denied", "Unable to get location.", "OK");
                return;
            }
        }

        try
        {
            if (connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                await Shell.Current.DisplayAlert("No Internet", "No internet connection available", "OK");
                return;
            }
            IsBusy = true;
            Debug.Print("Getting closest monkey.");
            var location = await geolocation.GetLastKnownLocationAsync();
            location ??= await geolocation.GetLocationAsync(new GeolocationRequest(GeolocationAccuracy.Medium, timeout: TimeSpan.FromSeconds(30)));
            if (location == null)
                return;
            var closest = Monkeys?.OrderBy(m => location.CalculateDistance(m.Latitude, m.Longitude, DistanceUnits.Kilometers)).FirstOrDefault();
            if (closest is null) return;
            Debug.Print("Getting closest monkey finished.");
            await Shell.Current.DisplayAlert("Info", $"{closest.Name} at {closest.Location}", "OK");
        }
        catch (TaskCanceledException ex)
        {
            Debug.WriteLine($"GetClosestMonkey Cancelled: {ex}");
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"GetClosestMonkey Error: {ex}");
            await Shell.Current.DisplayAlert("Error", "Unable to get closest monkey.", "OK");
        }
        finally
        {
            IsBusy = false;
        }

    }
}
