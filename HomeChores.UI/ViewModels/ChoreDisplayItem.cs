namespace HomeChores.UI.ViewModels;

public class ChoreDisplayItem
{
    public bool IsHeader { get; set; }
    public string HeaderText { get; set; } = string.Empty;

    // For chores
    public ChoreItemViewModel? ChoreItem { get; set; }
}