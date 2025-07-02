using MicroERP.Abstractions;
using MicroERP.Models;
using Microsoft.AspNetCore.Mvc;

namespace MicroERP.Controllers;

[ApiController]
[Route("[controller]")]
public class BoardController(ILogger<BoardController> l, IBoardService s)
    : Controller
{
    private readonly ILogger<BoardController> logger = l;
    private readonly IBoardService service = s;

    [HttpGet("CreateBoard")]
    public async Task<ActionResult<PsBoard>> RegisterNew()
    {
        try
        {
            var newBoard = await service.CreateBoardAsync();
            return Ok(newBoard);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "При регистрации новой платы произошла ошибка");
            return StatusCode(StatusCodes.Status500InternalServerError, new { Error = "Internal server error" });
        }
    }

    [HttpGet(@"ChangeBoardStage/{boardId}/{newStage}")]
    public async Task<ActionResult<PsBoard>> ChangeStage(int boardId, BoardStage newStage)
    {
        try
        {
            var board = await service.ChangeBoardStageAsync(boardId, newStage);
            return Ok(board);
        }
        catch (ArgumentException ex)
        {
            logger.LogError(ex, "В запросе изменения статуса платы произошла ошибка");
            return StatusCode(StatusCodes.Status400BadRequest, new { Error = "Bad Request" });
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "В запросе изменения статуса платы произошла ошибка");
            return StatusCode(StatusCodes.Status500InternalServerError, new { Error = "Internal server error" });
        }
    }

    [HttpGet("Info")]
    public async Task<ActionResult<List<PsBoard>>> GetBoardList()
    {
        try
        {
            var boards = await service.GetAsync();
            return Ok(boards);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "При получении списка плат произошла ошибка");
            return StatusCode(StatusCodes.Status500InternalServerError, new { Error = "Internal server error" });
        }
    }

    [HttpGet(@"Info/{boardId}")]
    public async Task<ActionResult<PsBoard>> GetBoardInfo(int boardId)
    {
        try
        {
            var boards = await service.GetAsync(boardId);
            return Ok(boards);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "При получении истории платы произошла ошибка");
            return StatusCode(StatusCodes.Status400BadRequest, new { Error = "Bad Request" });
        }
    }
}
