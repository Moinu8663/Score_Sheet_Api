using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;

namespace score_sheet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MockController : ControllerBase
    {
        [HttpPost]
        public IActionResult post(int a,int b,int c)
        {
            var add = a + b + c;
            decimal per = add/3;
            var d = per>60;
            string result;
            if (d)
            {
                result = "pass";
            }
            else
            {
                result = "fail";
            }
            return StatusCode(200,$"{add},{per},{result}");
        }

    }
}
