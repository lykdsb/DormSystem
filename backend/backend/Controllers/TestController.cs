using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;


namespace backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TestController : ControllerBase
    {

        [HttpGet]
        public async Task<String> Get()
        {
            return await Task.Run<string>(test);
        }
        public static string test()
        {
            return "test";
        }
    }
}
