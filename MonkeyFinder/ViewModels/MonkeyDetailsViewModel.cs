using CommunityToolkit.Mvvm.ComponentModel;

namespace MonkeyFinder.ViewModels;

[QueryProperty(nameof(Monkey), "Monkey")]
public partial class MonkeyDetailsViewModel : BaseViewModel
{
    [ObservableProperty]
    Monkey? monkey;
    public MonkeyDetailsViewModel()
    {
        Title = "Monkey Details";
    }
}
