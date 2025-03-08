using System.Collections.ObjectModel;

namespace HomeChores.UI.ViewModels;

public class ChoreGroup : ObservableCollection<ChoreItemViewModel>
{
    public ChoreGroup(DateTime plannedDate, IEnumerable<ChoreItemViewModel> chores)
        : base(chores)
    {
        PlannedDate = plannedDate;
    }

    public DateTime PlannedDate { get; }
}