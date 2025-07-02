using MicroERP.Models;

namespace MicroERP.Abstractions;

/// <summary>
/// Сервис для управления жизненным циклом печатных плат (PCB)
/// </summary>
public interface IBoardService
{
    /// <summary>
    /// Создает новую печатную плату с начальными параметрами
    /// </summary>
    /// <returns>
    /// Созданная печатная плата. Стадия (Stage) устанавливается в <see cref="BoardStage.Registration"/>.
    /// Генерируется новый уникальный серийный номер.
    /// </returns>
    Task<PsBoard> CreateBoardAsync();

    /// <summary>
    /// Изменяет стадию производства для указанной платы
    /// </summary>
    /// <param name="boardId">Идентификатор изменяемой платы</param>
    /// <param name="newStage">Новая стадия производства</param>
    /// <returns>
    /// Обновленная печатная плата
    /// </returns>
    /// <exception cref="ArgumentException">
    /// Возникает, если плата с указанным ID не найдена или переход между стадиями недопустим.
    /// </exception>
    Task<PsBoard> ChangeBoardStageAsync(int boardId, BoardStage newStage);

    /// <summary>
    /// Получает список всех печатных плат в системе
    /// </summary>
    /// <returns>Список всех плат.</returns>
    Task<List<PsBoard>> GetAsync();

    /// <summary>
    /// Получает печатную плату по идентификатору
    /// </summary>
    /// <param name="boardId">Идентификатор искомой платы</param>
    /// <returns>
    /// Найденная печатная плата.
    /// </returns>
    Task<PsBoard> GetAsync(int boardId);
}
