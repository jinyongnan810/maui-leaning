using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.Input;
using MonkeyFinder.Services;

namespace MonkeyFinder.ViewModels;

public partial class MonkeysViewModel : BaseViewModel
{
    private MonkeyService monkeyService;
    public ObservableCollection<Monkey> Monkeys { get; } = [];

    // public Command LoadMonkeysCommand { get; }
    public MonkeysViewModel(MonkeyService monkeyService)
    {
        Title = "Monkeys Finder";
        this.monkeyService = monkeyService;
        // this.LoadMonkeysCommand = new Command(async()=>await LoadMonkeys());
    }

    [RelayCommand]
    async Task LoadMonkeys()
    {
        if (IsBusy)
            return;
        try
        {
            IsBusy = true;
            Debug.Print("Loading Monkeys.");
            var monkeys = await monkeyService.GetMonkeys();
            Debug.Print("Monkeys loaded.");
            if (monkeys != null)
            {
                if (monkeys.Count != 0)
                    Monkeys.Clear();
                foreach (var monkey in monkeys)
                {
                    Monkeys.Add(monkey);
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
            await Shell.Current.DisplayAlert("Error", "Unable to load monkeys", "OK");
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
        await Shell.Current.GoToAsync($"{nameof(DetailsPage)}", true, new Dictionary<string, object> { { "Monkey", monkey } });
    }
}