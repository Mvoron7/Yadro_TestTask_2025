namespace MicroERP.Models;

/// <summary>
/// Этапы производственного процесса
/// </summary>
public enum BoardStage
{
    /// <summary>
    /// Неопределённый этап (по умолчанию)
    /// </summary>
    None = 0,

    /// <summary>
    /// Регистрация изделия в системе
    /// </summary>
    Registration,

    /// <summary>
    /// Установка компонентов на изделие
    /// </summary>
    ComponentInstallation,

    /// <summary>
    /// Контроль качества изделия
    /// </summary>
    QualityControl,

    /// <summary>
    /// Ремонт (если выявлены дефекты)
    /// </summary>
    Repair,

    /// <summary>
    /// Финальная упаковка готового изделия
    /// </summary>
    Packaging
}
