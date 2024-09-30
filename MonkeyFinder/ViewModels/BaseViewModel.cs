using System.ComponentModel;
using System.Runtime.CompilerServices;
using CommunityToolkit.Mvvm.ComponentModel;

namespace MonkeyFinder.ViewModels;

// public class BaseViewModel : INotifyPropertyChanged
// {
//     bool isBusy;
//     string? title;

//     public bool IsBusy
//     {
//         get => isBusy;
//         set
//         {
//             if (isBusy == value)
//                 return;

//             isBusy = value;
//             OnPropertyChanged(nameof(IsBusy));
//             OnPropertyChanged(nameof(IsNotBusy));
//         }
//     }

//     public bool IsNotBusy => !IsBusy;

//     public string? Title
//     {
//         get => title;
//         set
//         {
//             if (title == value)
//                 return;

//             title = value;
//             // OnPropertyChanged(nameof(Title));
//             OnPropertyChanged();
//         }
//     }


//     public event PropertyChangedEventHandler? PropertyChanged;

//     // CallerMemberName set propertyName to name of the property that changed when not provided
//     public void OnPropertyChanged([CallerMemberName] string? propertyName = null)
//     {
//         PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
//     }
// }

// use CommunityToolkit.Mvvm to generate above
public partial class BaseViewModel : ObservableObject
{
    public BaseViewModel()
    {
    }

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(IsNotBusy))]
    bool isBusy;
    [ObservableProperty]
    string? title;

    public bool IsNotBusy => !IsBusy;

}