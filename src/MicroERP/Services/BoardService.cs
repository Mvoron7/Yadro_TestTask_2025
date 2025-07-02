using MicroERP.Abstractions;
using MicroERP.Models;

namespace MicroERP.Services;

public class BoardService(ILogger<BoardService> l, IBoardRepository r)
    : IBoardService
{
    private readonly ILogger<BoardService> logger = l;
    private readonly IBoardRepository repository = r;

    public async Task<PsBoard> CreateBoardAsync()
    {
        
        var board = PsBoard.Create();
        board.ChangeStage(BoardStage.None, BoardStage.Registration);
        await repository.AddAsync(board);
        await repository.SaveChangesAsync();
        return board;
    }

    public async Task<PsBoard> ChangeBoardStageAsync(int boardId, BoardStage newStage)
    {
        var board = await repository.GetByIdAsync(boardId);

        if (board == null)
        {
            logger.LogError("В запросе изменения статуса указанная плата не существует {boardId}", boardId);
            throw new ArgumentException($"Плата {boardId} не найдена", nameof(boardId));
        }

        BoardStage currentStage = board.StageHistory.MaxBy(s => s.Timestamp)!.NewStage;
        bool isValid = CheckMove(currentStage, newStage);
        if (!isValid)
        {
            logger.LogError("В запросе изменения статуса платы {boardId} указано недопустимое изменение c {currentStage} на {newStage}", boardId, currentStage, newStage);
            throw new ArgumentException($"Плате {boardId} не может быть назначен статус {newStage}", nameof(newStage));
        }

        board.ChangeStage(currentStage, newStage);
        await repository.SaveChangesAsync();
        return board;
    }

    public async Task<List<PsBoard>> GetAsync()
    {
        var boards = await repository.GetAsync();
        return boards;
    }

    public async Task<PsBoard> GetAsync(int boardId)
    {
        var board = await repository.GetByIdAsync(boardId);

        if (board == null)
        {
            logger.LogError("В запросе осмотра истории указанная плата не существует {boardId}", boardId);
            throw new ArgumentException($"Плата {boardId} не найдена", nameof(boardId));
        }

        return board;
    }

    /// <summary>
    /// Проверяет допустимость изменения статуса платы
    /// </summary>
    /// <param name="oldStage">Текущий статус</param>
    /// <param name="newStage">Новый статус</param>
    /// <returns>True - если изменение допустимо</returns>
    private bool CheckMove(BoardStage oldStage, BoardStage newStage)
    {
        return (oldStage == BoardStage.Registration && newStage == BoardStage.ComponentInstallation)
            || (oldStage == BoardStage.ComponentInstallation && newStage == BoardStage.QualityControl)
            || (oldStage == BoardStage.QualityControl && newStage == BoardStage.Packaging)
            || (oldStage == BoardStage.QualityControl && newStage == BoardStage.Repair)
            || (oldStage == BoardStage.Repair && newStage == BoardStage.QualityControl);
    }
}
