using System.ComponentModel.DataAnnotations;

namespace HomeChores.Domain.Entities;

public class Chore
{
    private Chore()
    {
    }

    public Chore(string title)
    {
        Id = Guid.NewGuid();
        Title = title;
        IsCompleted = false;
    }

    public Chore(string title, DateTime plannedDate)
    {
        Id = Guid.NewGuid();
        Title = title;
        IsCompleted = false;
        PlannedDate = plannedDate;
    }

    [Key] public Guid Id { get; private set; }
    public string Title { get; private set; } = string.Empty;
    public bool IsCompleted { get; private set; }
    public DateTime PlannedDate { get; private set; }

    public void ToggleComplete()
    {
        IsCompleted = !IsCompleted;
    }
}