using Microsoft.AspNetCore.Mvc;
using RRJConverter.Models;
using RRJConverter.Models.DatabaseModels;
using System.Linq;
using System.Threading.Tasks;

namespace RRJConverter.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OperationsController : ControllerBase
    {
        /// <summary>
        /// Хранит контект данных базы данных
        /// </summary>
        public ApplicationContext ApplicationContext { get; set; }

        public OperationsController(ApplicationContext context)
        {
            ApplicationContext = context;
        }

        /// <summary>
        /// Возвращает данные о конвертации по уникальному идентификатору по запросу /api/GetArchiveOperations/?{id}
        /// </summary>
        /// <param name="id">Представляет собой уникальный идентификатор</param>
        /// <returns>Возращает клиенту статус-код 200 и JSON-данные с данными операции по идентификатору. 
        /// Если данные не были найдены клиенту возврается статус-код 404</returns>
        [HttpGet]
        public async Task<IActionResult> Get(int id)
        {
            var item = await ApplicationContext.ConvertingOperations.FindAsync(id);

            if (item == null)
            {
                return NotFound();
            }
            return Ok(item);   
        }
    }
}
