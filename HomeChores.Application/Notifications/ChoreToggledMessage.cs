namespace HomeChores.Application.Notifications;

public record ChoreToggledMessage(Guid ChoreId, bool IsCompleted);