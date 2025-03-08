using System.Collections.ObjectModel;
using System.ComponentModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HomeChores.Application.Queries;
using MediatR;

namespace HomeChores.UI.ViewModels;

[QueryProperty(nameof(DateParam), "date")]
public partial class DailyChoresViewModel : ObservableObject, IQueryAttributable
{
    private readonly IMediator _mediator;

    // Backing field for SelectedDate
    private DateTime _selectedDate = DateTime.Today;

    [ObservableProperty] private string title = string.Empty;

    public DailyChoresViewModel(IMediator mediator)
    {
        _mediator = mediator;
        Chores = new ObservableCollection<ChoreItemViewModel>();
    }

    public ObservableCollection<ChoreItemViewModel> Chores { get; }

    public string DateParam { get; set; }

    // Let the user pick the date. Whenever it changes, reload chores.
    public DateTime SelectedDate
    {
        get => _selectedDate;
        set
        {
            if (SetProperty(ref _selectedDate, value))
            {
                Title = $"Chores for {value:MMM dd}";
                // Reload chores automatically whenever the user picks a new date
                _ = LoadDailyChores();
            }
        }
    }

    public double ProgressPercentage
    {
        get
        {
            if (Chores.Count == 0) return 0;
            var completed = Chores.Count(item => item.IsCompleted);
            return (double)completed / Chores.Count;
        }
    }

    public async void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        // If the user came from the calendar, we parse the date from the URI
        if (query.TryGetValue("date", out var dateObj) && dateObj is string dateStr &&
            DateTime.TryParse(dateStr, out var date))
            SelectedDate = date; // triggers property setter, which calls LoadDailyChores
    }

    public async Task LoadDailyChores()
    {
        var dailyChores = await _mediator.Send(new GetDailyChoresQuery(SelectedDate.Date));
        Chores.Clear();
        foreach (var chore in dailyChores)
        {
            var vm = new ChoreItemViewModel(chore, _mediator);
            vm.PropertyChanged += OnChoreItemPropertyChanged;
            Chores.Add(vm);
        }

        // Recalculate progress after reloading
        OnPropertyChanged(nameof(ProgressPercentage));
    }

    private void OnChoreItemPropertyChanged(object sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(ChoreItemViewModel.IsCompleted))
            OnPropertyChanged(nameof(ProgressPercentage));
    }

    [RelayCommand]
    private async Task GoBackAsync()
    {
        // Navigate back to Calendar
        await Shell.Current.GoToAsync("///CalendarPage");
    }
}