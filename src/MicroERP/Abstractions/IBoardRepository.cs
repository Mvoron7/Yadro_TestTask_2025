using MicroERP.Models;

namespace MicroERP.Abstractions;

/// <summary>
/// Репозиторий для работы с печатными платами (Printed Circuit Boards) в системе
/// </summary>
public interface IBoardRepository
{
    /// <summary>
    /// Асинхронно получает плату по уникальному идентификатору
    /// </summary>
    /// <param name="id">Идентификатор платы</param>
    /// <returns>Найденная плата или null, если плата с указанным идентификатором не существует</returns>
    Task<PsBoard?> GetByIdAsync(int id);

    /// <summary>
    /// Асинхронно получает список всех плат в системе
    /// </summary>
    /// <returns>Список всех плат.</returns>
    Task<List<PsBoard>> GetAsync();

    /// <summary>
    /// Асинхронно добавляет новую плату в систему
    /// </summary>
    /// <param name="board">Добавляемая плата</param>
    Task AddAsync(PsBoard board);

    /// <summary>
    /// Асинхронно сохраняет все изменения в хранилище данных
    /// </summary>
    Task SaveChangesAsync();
}
