using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using WeatherAcquisition.Interfaces.Base.Entities;

namespace WeatherAcquisition.Interfaces.Base.Repositories
{
    public interface IRepository<T> where T : IEntity
    {
        /// <summary>
        /// Проверяет наличие объекта в репозитории по идентификатору
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="Cancel"></param>
        /// <returns></returns>
        Task<bool> ExistId(int Id, CancellationToken Cancel = default);
        //async Task<bool> ExistId(int Id, CancellationToken Cancel = default) => await GetById(Id, Cancel) is not null;

        /// <summary>
        /// Проверяет наличие сущности в репозитории
        /// </summary>
        /// <param name="item"></param>
        /// <param name="Cancel"></param>
        /// <returns></returns>
        Task<bool> Exist(T item, CancellationToken Cancel = default);

        /// <summary>
        /// Количество элементов в репозитории
        /// </summary>
        /// <param name="Cancel"></param>
        /// <returns></returns>
        Task<int> GetCount(CancellationToken Cancel = default);

        /// <summary>
        /// Возвращает все
        /// </summary>
        /// <param name="Cancel"></param>
        /// <returns></returns>
        Task<IEnumerable<T>> GetAll(CancellationToken Cancel = default);

        /// <summary>
        /// Получение элементов указывая какое количество получить и какое пропустить
        /// </summary>
        /// <param name="Skip">Сколько пропустить</param>
        /// <param name="Count">Сколько получить</param>
        /// <param name="Cancel"></param>
        /// <returns></returns>
        Task<IEnumerable<T>> Get(int Skip, int Count, CancellationToken Cancel = default);

        /// <summary>
        /// Получение страницы с данными
        /// </summary>
        /// <param name="PageIndex">Номер страницы</param>
        /// <param name="PageSize">Размер страницы</param>
        /// <param name="Cancel"></param>
        /// <returns></returns>
        Task<IPage<T>> GetPage(int PageIndex, int PageSize, CancellationToken Cancel = default);

        /// <summary>
        /// Получение сущности по идентификатору
        /// </summary>
        /// <param name="Id">Идентификатор сущности</param>
        /// <param name="Cancel"></param>
        /// <returns>Объект или NUll</returns>
        //async Task<T> GetById(int Id, CancellationToken Cancel = default) => (await GetAll(Cancel)).FirstOrDefault(item => item.Id == Id);
        Task<T> GetById(int Id, CancellationToken Cancel = default);

        /// <summary>
        /// Добавляет сущность в репозиторий
        /// </summary>
        /// <param name="item"></param>
        /// <param name="Cancel"></param>
        /// <returns>Объект или NUll</returns>
        Task<T> Add(T item, CancellationToken Cancel = default);

        /// <summary>
        /// Изменяет сущность в репозиторий
        /// </summary>
        /// <param name="item"></param>
        /// <param name="Cancel"></param>
        /// <returns>Объект или NUll</returns>
        Task<T> Update(T item, CancellationToken Cancel = default);

        /// <summary>
        /// Изымает сущность из репозитория
        /// </summary>
        /// <param name="item"></param>
        /// <param name="Cancel"></param>
        /// <returns>Объект или NUll</returns>
        Task<T> Delete(T item, CancellationToken Cancel = default);

        /// <summary>
        /// Удаление сущности по идентификатору
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="Cancel"></param>
        /// <returns>Объект или NUll</returns>
        Task<T> DeleteById(int Id, CancellationToken Cancel = default);
    }

    public interface IPage<out T>
    {
        /// <summary>
        /// Перечисление элементов страницы 
        /// </summary>
        IEnumerable<T> Items { get; }

        /// <summary>
        /// Полное количество возможных для извлечения элементов
        /// </summary>
        int TotalCount { get; }

        /// <summary>
        /// Индекс текущей страницы
        /// </summary>
        int PageIndex { get; }

        /// <summary>
        /// Размер страницы
        /// </summary>
        int PageSize { get; }

        /// <summary>
        /// Полное количество страниц
        /// </summary>
        int TotalPagesCount => (int)Math.Ceiling((double)TotalCount / PageSize);
    }
}
