using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WeatherAcquisition.DAL.Entities;
using WeatherAcquisition.Interfaces.Base.Repositories;

namespace WeatherAcquisition.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DataSourcesController : ControllerBase
    {
        #region Поля

        private readonly IRepository<DataSource> _Repository;

        #endregion // Поля

        #region Методы

        [HttpGet("count")]
        public async Task<IActionResult> GetItemsCount() => Ok(await _Repository.GetCount());

        #endregion // Методы

        #region Конструктор

        public DataSourcesController(IRepository<DataSource> Repository)
        {
            _Repository = Repository;
        }

        #endregion // Конструктор
    }
}
