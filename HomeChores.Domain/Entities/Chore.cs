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

    [Key] public Guid Id { get; private set; }

    public string Title { get; private set; } = string.Empty;
    public bool IsCompleted { get; private set; }

    public void MarkCompleted()
    {
        IsCompleted = true;
    }

    public void MarkIncomplete()
    {
        IsCompleted = false;
    }

    public void ToggleComplete()
    {
        IsCompleted = !IsCompleted;
    }
}