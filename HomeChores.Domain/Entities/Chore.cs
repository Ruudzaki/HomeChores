namespace HomeChores.Domain.Entities;

public class Chore
{
    public Chore(string title)
    {
        Id = Guid.NewGuid();
        Title = title;
        IsCompleted = false;
    }

    public Guid Id { get; }
    public string Title { get; }
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