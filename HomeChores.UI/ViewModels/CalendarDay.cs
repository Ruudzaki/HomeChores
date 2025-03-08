namespace HomeChores.UI.ViewModels;

public class CalendarDay
{
    public DateTime Date { get; set; }
    public int ChoreCount { get; set; }
    public bool IsCurrentMonth { get; set; }

    public bool IsToday => Date == DateTime.Today;
}