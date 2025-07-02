namespace MicroERP.Models;

/// <summary>
/// Этап производственного процесса
/// </summary>
public class PsBoard
{
    /// <summary>
    /// Уникальный идентификатор записи
    /// </summary>
    public int Id { get; private set; }

    /// <summary>
    /// История движения по этапам производственного процесса
    /// </summary>
    public ICollection<BoardStageHistory> StageHistory { get; private set; } = new List<BoardStageHistory>();

    private PsBoard() { }


    public static PsBoard Create()
    {
        return new PsBoard();
    }

    public void ChangeStage(BoardStage oldStage, BoardStage newStage)
    {
        StageHistory.Add(BoardStageHistory.Create(this, oldStage, newStage));
    }
}
