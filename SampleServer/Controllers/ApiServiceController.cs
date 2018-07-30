using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

using Common;

namespace SampleServer.Controllers
{
    public class ApiServiceController : ApiController
    {
        public string Get()
        {
            try
            {
                FileHandler.WriteAsync("Hello from windows service!");
            }
            catch { }

            return "Hello from windows service!";
        }
    }
}
