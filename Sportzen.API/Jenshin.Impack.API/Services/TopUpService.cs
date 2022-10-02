using System;
using System.Reflection;
using System.Collections.Generic;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

using Sportzen.API.Model;
using Sportzen.API.Output;

using Binus.WS.Pattern.Output;
using Binus.WS.Pattern.Service;
using Sportzen.API.Model.Request;

namespace Sportzen.API.Services
{
    [ApiController]
    [Route("topup")]
    public class TopUpService : BaseService
    {
        public TopUpService(ILogger<BaseService> logger) : base(logger)
        {
        }

        [HttpPost]
        [Produces("application/json")]
        [ProducesResponseType(typeof(TopUpOutput), StatusCodes.Status200OK)]
        public IActionResult TopUp([FromBody] TopUpRequestDTO data)
        {
            try
            {
                var objJSON = new TopUpOutput();
                objJSON.Message = Helper.TopUpHelper.TopUp(data);
                
                return new OkObjectResult(objJSON);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new OutputBase(ex));
            }
        }
    }
}