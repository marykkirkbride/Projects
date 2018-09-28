namespace Events.Controllers
{
    using System.IO;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc;

    [Produces("application/json")]
    [Route("api/Pic")]
    public class PicController : Controller
    {
        private readonly IHostingEnvironment _env;
        public PicController(IHostingEnvironment env)
        {
            _env = env;
        }

        //GET APIs
        [HttpGet]
        [Route("{id}")]
        public IActionResult GetImage(int id)
        {
            var webRoot = _env.WebRootPath;
            var path = Path.Combine(webRoot + "/Pics/", "events-" + id + ".png");
            var buffer = System.IO.File.ReadAllBytes(path);
            return File(buffer, "image/png");
        }
    }
}