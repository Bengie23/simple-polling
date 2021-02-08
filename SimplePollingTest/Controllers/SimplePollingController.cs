using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace SimplePollingTest.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class SimplePollingController : ControllerBase
    {
        private readonly SimplePolling _simplePolling;
        private readonly ILogger<SimplePollingController> _logger;

        public SimplePollingController(ILogger<SimplePollingController> logger, SimplePolling simplePolling)
        {
            _simplePolling = simplePolling;
            _logger = logger;
        }

        [HttpGet]
        public string Get()
        {
            DateTime limit = DateTime.UtcNow.AddMinutes(1);

            while (DateTime.UtcNow < limit)
            {
                var value = _simplePolling.TryGet();

                if (value != null)
                {
                    return value;
                }
            }

            return "Timeout";
        }

        [HttpGet]
        [Route("set")]
        public void Set(string value)
        {
            _simplePolling.Set(value);

        }
    }
}
