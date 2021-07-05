using WeatherAcquisition.API.Controllers.Base;
using WeatherAcquisition.DAL.Entities;
using WeatherAcquisition.Interfaces.Base.Repositories;

namespace WeatherAcquisition.API.Controllers
{
    public class DataValuesController : EntityController<DataValue>
    {
        #region Поля


        #endregion // Поля

        #region Конструктор

        public DataValuesController(IRepository<DataValue> Repository) : base(Repository) { }

        #endregion // Конструктор
    }
}