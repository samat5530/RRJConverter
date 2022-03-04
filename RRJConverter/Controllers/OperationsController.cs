using Microsoft.AspNetCore.Mvc;
using RRJConverter.Domain;
using RRJConverter.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace RRJConverter.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OperationsController : ControllerBase
    {
        private readonly IRepository _repository;

        public OperationsController(IRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Возвращает данные о конвертации по уникальному идентификатору по запросу /api/Operations/?{id}
        /// </summary>
        /// <param name="id">Представляет собой уникальный идентификатор</param>
        /// <returns>Возращает клиенту статус-код 200 и JSON-данные с данными операции по идентификатору. 
        /// Если данные не были найдены клиенту возврается статус-код 404</returns>
        [HttpGet]
        public IActionResult Get(int id)
        {
            var item = _repository.GetOperation(id);

            if (item == null)
            {
                return NotFound();
            }
            return Ok(item);
        }
    }
}
