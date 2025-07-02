using MicroERP.Abstractions;
using MicroERP.Models;
using Microsoft.EntityFrameworkCore;

namespace MicroERP.Repositories;

/// <summary>
/// Репозиторий для работы с печатными платами (Printed Circuit Boards) в системе
/// </summary>
public class BoardRepository : IBoardRepository
{
    private readonly AppDbContext context;

    public BoardRepository(AppDbContext c)
    {
        context = c;
    }

    public async Task<List<PsBoard>> GetAsync()
    {
        var boards = await context.Boards.ToListAsync();
        return boards;
    }

    public async Task<PsBoard?> GetByIdAsync(int id)
    {
        return await context.Boards
            .Include(b => b.StageHistory)
            .FirstOrDefaultAsync(b => b.Id == id);
    }

    public async Task AddAsync(PsBoard board)
    {
        await context.Boards.AddAsync(board);
    }

    public async Task SaveChangesAsync()
    {
        await context.SaveChangesAsync();
    }
}
