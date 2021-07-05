﻿using WeatherAcquisition.API.Controllers.Base;
using WeatherAcquisition.DAL.Entities;
using WeatherAcquisition.Interfaces.Base.Repositories;

namespace WeatherAcquisition.API.Controllers
{
    public class DataSourcesController : EntityController<DataSource>
    {
        #region Поля


        #endregion // Поля


        #region Конструктор

        public DataSourcesController(IRepository<DataSource> Repository) : base(Repository) {}

        #endregion // Конструктор
    }
}
