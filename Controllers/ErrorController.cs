using Microsoft.AspNetCore.Mvc;

namespace Orderly.Controllers
{
    [ApiController]
    [Route("error")]
    public class ErrorController : ControllerBase
    {
        [HttpGet]
        public IActionResult Index()
        {
            return Content("<h1>Wystąpił błąd</h1><p>Proszę spróbować później.</p>", "text/html");
        }
    }
}