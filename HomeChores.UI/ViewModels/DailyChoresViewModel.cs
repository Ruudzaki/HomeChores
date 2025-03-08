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

    [ObservableProperty] private string title = string.Empty;

    public DailyChoresViewModel(IMediator mediator)
    {
        _mediator = mediator;
        Chores = new ObservableCollection<ChoreItemViewModel>();
    }

    public ObservableCollection<ChoreItemViewModel> Chores { get; }

    // The query parameter coming from the URI, e.g. date=2025-03-14
    public string DateParam { get; set; }

    public DateTime SelectedDate { get; set; }

    // Returns a value between 0.0 and 1.0 based on completed tasks.
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
        if (query.TryGetValue("date", out var dateObj) && dateObj is string dateStr &&
            DateTime.TryParse(dateStr, out var date))
        {
            SelectedDate = date;
            Title = $"Chores for {SelectedDate:MMM dd}";
            await LoadDailyChores();
        }
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

        OnPropertyChanged(nameof(ProgressPercentage));
    }

    [RelayCommand]
    private async Task GoBackAsync()
    {
        // Navigate back to the Calendar page.
        await Shell.Current.GoToAsync("///CalendarPage");
    }

    private void OnChoreItemPropertyChanged(object sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(ChoreItemViewModel.IsCompleted))
            // A chore’s IsCompleted changed. Recalculate progress.
            OnPropertyChanged(nameof(ProgressPercentage));
    }
}