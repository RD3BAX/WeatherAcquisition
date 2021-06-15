﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WeatherAcquisition.DAL.Context;
using WeatherAcquisition.DAL.Entities.Base;
using WeatherAcquisition.Interfaces.Base.Repositories;

namespace WeatherAcquisition.DAL.Repositories
{
    public class DbRepository<T> : IRepository<T> where T : Entity, new()
    {
        #region Поля

        private readonly DataDB _db;

        #endregion // Поля

        #region Свойства

        protected DbSet<T> Set { get; }

        protected virtual IQueryable<T> Items => Set;

        #endregion // Свойства

        #region Методы

        public async Task<bool> ExistId(int Id, CancellationToken Cancel = default)
        {
            return await Items.AnyAsync(item => item.Id == Id, Cancel).ConfigureAwait(false);
        }

        public async Task<bool> Exist(T item, CancellationToken Cancel = default)
        {
            if (item is null) throw new ArgumentException(nameof(item));

            return await Items.AnyAsync(i => i.Id == item.Id, Cancel).ConfigureAwait(false);
        }

        public async Task<int> GetCount(CancellationToken Cancel = default)
        {
            return await Items.CountAsync(Cancel).ConfigureAwait(false);
        }

        public async Task<IEnumerable<T>> GetAll(CancellationToken Cancel = default)
        {
            return await Items.ToArrayAsync(Cancel).ConfigureAwait(false);
        }

        public async Task<IEnumerable<T>> Get(int Skip, int Count, CancellationToken Cancel = default)
        {
            //return await Items
            //    .Skip(Skip)
            //    .Take(Count)
            //    .ToArrayAsync(Cancel);

            if (Count <= 0) 
                return Enumerable.Empty<T>();

            // Если наследник переопределит Items OrderBy не будет использоваться
            IQueryable<T> query = Items switch
            {
                IOrderedQueryable<T> ordered_query => ordered_query,
                { } q => q.OrderBy(i => i.Id)
            };

            if (Skip > 0)
                query = query.Skip(Skip);
            return await query.Take(Count).ToArrayAsync(Cancel).ConfigureAwait(false);
        }

        protected record Page(IEnumerable<T> Items, int TotalCount, int PageIndex, int PageSize) : IPage<T>;
        public async Task<IPage<T>> GetPage(int PageIndex, int PageSize, CancellationToken Cancel = default)
        {
            if (PageSize <= 0) return new Page(Enumerable.Empty<T>(), PageSize, PageIndex, PageSize);
            //if (PageSize <= 0) return new Page(Enumerable.Empty<T>(), await GetCount(Cancel).ConfigureAwait(false), PageIndex, PageSize);

            var query = Items;
            var total_coutn = await query.CountAsync(Cancel).ConfigureAwait(false);
            if(total_coutn == 0)
                return new Page(Enumerable.Empty<T>(), 0, PageIndex, PageSize);

            if (PageIndex > 0)
                query = query.Skip(PageIndex * PageSize);
            query = query.Take(PageSize);

            var items = await query.ToArrayAsync(Cancel).ConfigureAwait(false);

            return new Page(items, total_coutn, PageIndex, PageSize);

        }

        public async Task<T> GetById(int Id, CancellationToken Cancel = default)
        {
            //// Возвращает первый попавшийся объект с этим ID
            //return await Items.FirstOrDefaultAsync(item => item.Id == Id, Cancel).ConfigureAwait(false);

            //// Проверяет что ID уникален иначе возвращает ошибку
            //return await Items.SingleOrDefaultAsync(item => item.Id == Id, Cancel).ConfigureAwait(false);

            switch (Items)
            {
                case DbSet<T> set:
                    return await set.FindAsync(new object[] { Id }, Cancel).ConfigureAwait(false);
                case { } items:
                    return await items.SingleOrDefaultAsync(item => item.Id == Id, Cancel).ConfigureAwait(false);
                default:
                    throw new InvalidOperationException("Ошибка в определении источника данных");
            }
        }

        public async Task<T> Add(T item, CancellationToken Cancel = default)
        {
            if (item is null) throw new ArgumentNullException(nameof(item));

            //_db.Entry(item).State = EntityState.Added;
            //Set.Add(item);
            await _db.AddAsync(item, Cancel).ConfigureAwait(false);
            await _db.SaveChangesAsync(Cancel).ConfigureAwait(false);
            return item;
        }

        public async Task<T> Update(T item, CancellationToken Cancel = default)
        {
            if (item is null) throw new ArgumentNullException(nameof(item));

            //_db.Entry(item).State = EntityState.Modified;
            //Set.Update(item);
            _db.Update(item);
            await _db.SaveChangesAsync(Cancel).ConfigureAwait(false);
            return item;
        }

        public async Task<T> Delete(T item, CancellationToken Cancel = default)
        {
            if (item is null) throw new ArgumentNullException(nameof(item));

            if (!await ExistId(item.Id, Cancel))
                return null;

            //_db.Entry(item).State = EntityState.Deleted;
            //Set.Add(item);
            _db.Remove(item);
            await _db.SaveChangesAsync(Cancel).ConfigureAwait(false);
            return item;

        }

        public async Task<T> DeleteById(int Id, CancellationToken Cancel = default)
        {
            // Поиск объекта по идентификатору в кеше
            var item = Set.Local.FirstOrDefault(i => i.Id == Id);
            // Получаем идентификатор если он не был найден локально
            if (item is null)
                item = await Set
                    .Select(i => new T { Id = i.Id })
                    .FirstOrDefaultAsync(i => i.Id == Id, Cancel)
                    .ConfigureAwait(false);
            if (item is null)
                return null;

            return await Delete(item, Cancel).ConfigureAwait(false);
        }

        #endregion // Методы

        #region Конструктор

        public DbRepository(DataDB db)
        {
            this._db = db;
            Set = _db.Set<T>();
        }

        #endregion // Конструктор
    }
}
