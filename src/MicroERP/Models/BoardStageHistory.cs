namespace MicroERP.Models;

/// <summary>
/// Перемещение по производственному процессу
/// </summary>
public class BoardStageHistory
{
    /// <summary>
    /// Уникальный идентификатор записи
    /// </summary>
    public int Id { get; private set; }

    /// <summary>
    /// Идентификатор платы
    /// </summary>
    public int BoardId { get; private set; }

    /// <summary>
    /// Предыдущий этап
    /// </summary>
    public BoardStage OldStage { get; private set; }

    /// <summary>
    /// Новый этап
    /// </summary>
    public BoardStage NewStage { get; private set; }

    /// <summary>
    /// Метка времени
    /// </summary>
    public DateTime Timestamp { get; private set; }

    internal PsBoard Board { get; private set; }

    private BoardStageHistory() { }

    public static BoardStageHistory Create(PsBoard board, BoardStage oldStage, BoardStage newStage)
    {
        return new BoardStageHistory
        {
            BoardId = board.Id,
            OldStage = oldStage,
            NewStage = newStage,
            Timestamp = DateTime.UtcNow
        };
    }
}
