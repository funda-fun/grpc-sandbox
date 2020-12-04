using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Service.Controllers
{
    public class ThingController : Controller
    {
        [HttpPost("thing")]
        public Task<IActionResult> GetStuff([FromBody] Thing thing)
        {
            return Task.FromResult<IActionResult>(Ok(new
            {
                count = 1
            }));
        }
    }

    public class Thing
    {
        public string Url { get; set; }
    }
}
