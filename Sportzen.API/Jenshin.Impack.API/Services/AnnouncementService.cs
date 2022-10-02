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

namespace Sportzen.API.Services
{
    [ApiController]
    [Route("Announcement")]
    public class AnnouncementService : BaseService
    {
        public AnnouncementService(ILogger<BaseService> logger) : base(logger)
        {
        }

        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(typeof(AnnouncementOutput), StatusCodes.Status200OK)]
        public IActionResult GetAnnouncement()
        {
            try
            {
                var objJSON = new AnnouncementOutput();
                objJSON.Data = Helper.AnnouncementHelper.GetAnnouncement();
                return new OkObjectResult(objJSON);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new OutputBase(ex));
            }
        }

        [HttpGet]
        [Route("LastAnnouncement")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(AnnouncementOutput), StatusCodes.Status200OK)]
        public IActionResult GetLastAnnouncement()
        {
            try
            {
                var objJSON = new AnnouncementOutput();
                objJSON.Data = Helper.AnnouncementHelper.GetLastAnnouncement();
                return new OkObjectResult(objJSON);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new OutputBase(ex));
            }
        }

    }
}