using Microsoft.AspNetCore.Mvc;
using RRJConverter.Models;
using RRJConverter.Models.DatabaseModels;
using System.Linq;
using System.Threading.Tasks;

namespace RRJConverter.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GetArchiveOperationsController : ControllerBase
    {
        public ApplicationContext applicationContext { get; set; }
        public GetArchiveOperationsController(ApplicationContext context)
        {
            applicationContext = context;
        }

        [HttpGet]
        public async Task<ActionResult<ConvertingOperation>> Get(int id)
        {
            var item = await applicationContext.ConvertingOperations.FindAsync(id);

            if (item == null)
            {
                return NotFound();
            }

            return Ok(item);
            
            
            /*var result = applicationContext.ConvertingOperations.Where(x => x.Id == id).FirstOrDefault();

            if (id == 0) return NotFound();
            else return Ok(result);*/
        }
    }

}
