using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using WeatherAcquisition.Interfaces.Base.Entities;
using WeatherAcquisition.Interfaces.Base.Repositories;

namespace WeatherAcquisition.WebAPIClients.Repository
{
    public class WebRepository<T> : IRepository<T> where T : IEntity
    {
        #region Поля

        private readonly HttpClient _Client;

        #endregion // Поля

        #region Методы

        public async Task<bool> ExistId(int Id, CancellationToken Cancel = default)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> Exist(T item, CancellationToken Cancel = default)
        {
            throw new NotImplementedException();
        }

        public async Task<int> GetCount(CancellationToken Cancel = default)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<T>> GetAll(CancellationToken Cancel = default)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<T>> Get(int Skip, int Count, CancellationToken Cancel = default)
        {
            throw new NotImplementedException();
        }

        public async Task<IPage<T>> GetPage(int PageIndex, int PageSize, CancellationToken Cancel = default)
        {
            throw new NotImplementedException();
        }

        public async Task<T> GetById(int Id, CancellationToken Cancel = default)
        {
            throw new NotImplementedException();
        }

        public async Task<T> Add(T item, CancellationToken Cancel = default)
        {
            throw new NotImplementedException();
        }

        public async Task<T> Update(T item, CancellationToken Cancel = default)
        {
            throw new NotImplementedException();
        }

        public async Task<T> Delete(T item, CancellationToken Cancel = default)
        {
            throw new NotImplementedException();
        }

        public async Task<T> DeleteById(int Id, CancellationToken Cancel = default)
        {
            throw new NotImplementedException();
        }

        #endregion // Методы

        #region Конструктор

        public WebRepository(HttpClient Client)
        {
            _Client = Client;
        }

        #endregion // Конструктор
    }
}
