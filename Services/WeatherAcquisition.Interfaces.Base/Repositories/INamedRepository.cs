using System.Threading;
using System.Threading.Tasks;
using WeatherAcquisition.Interfaces.Base.Entities;

namespace WeatherAcquisition.Interfaces.Base.Repositories
{
    public interface INamedRepository<T> : IRepository<T> where T : INamedEntity
    {
        /// <summary>
        /// Проверяет наличие сущности с указанным именем
        /// </summary>
        /// <param name="Name">Имя сущности</param>
        /// <param name="Cancel"></param>
        /// <returns></returns>
        Task<bool> ExistName(string Name, CancellationToken Cancel = default);

        /// <summary>
        /// Находит сущность с указанным именем
        /// </summary>
        /// <param name="Name">Имя сущности</param>
        /// <param name="Cancel"></param>
        /// <returns>Найденная сущность</returns>
        Task<T> GetByName(string Name, CancellationToken Cancel = default);

        /// <summary>
        /// Удаляет сущность по указанному имени
        /// </summary>
        /// <param name="Name">Имя сущности</param>
        /// <param name="Cancel"></param>
        /// <returns>Удаленная сущность</returns>
        Task<T> DeleteByName(string Name, CancellationToken Cancel = default);
    }
}
