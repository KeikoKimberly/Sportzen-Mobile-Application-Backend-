using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Binus.WS.Pattern.Output;
using Binus.WS.Pattern.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Sportzen.API.Model;
using Sportzen.API.Model.Request;
using Sportzen.API.Output;


namespace Sportzen.API.Services
{
  [ApiController]
  [Route("member")]
  public class MemberService : BaseService
  {

    public MemberService(ILogger<BaseService> logger) : base(logger)
    {
    }

    /*
        GET /member/specific
        Request -> MemberID
        Response -> SpesificMemberOutput
    */
    [HttpGet]
    [Route("specific")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(MemberOutput), StatusCodes.Status200OK)]

    public IActionResult GetSpecificMember([FromQuery] int MemberID)
    {
      try
      {
        var objJSON = new SpesificMemberOutput();
        objJSON.Data = Helper.MemberHelper.GetSpecificMember(MemberID);
        return new OkObjectResult(objJSON);
      }
      catch (Exception ex)
      {
        return StatusCode(500, new OutputBase(ex));
      }
    }

    /*
        POST /member
        Request -> MsMember
        Response -> MemberOutput
    */

    [HttpPost]
    [Produces("application/json")]
    [ProducesResponseType(typeof(MemberOutput), StatusCodes.Status200OK)]

    public IActionResult AddNewUser([FromBody] MsMember data)
    {
      try
      {
        var objJSON = new MemberOutput();
        objJSON = Helper.MemberHelper.AddNewMember(data);
        return new OkObjectResult(objJSON);
      }
      catch (Exception ex)
      {
        return StatusCode(500, new OutputBase(ex));
      }
    }



    /*
        PATCH /member
        Request -> MsMember
        Response -> MemberOutput
    */

    [HttpPatch]
    [Produces("application/json")]
    [ProducesResponseType(typeof(MemberOutput), StatusCodes.Status200OK)]

    public IActionResult UpdateUser([FromBody] MsMember data)
    {
      try
      {
        var objJSON = new MemberOutput();
        objJSON = Helper.MemberHelper.UpdateMember(data);
        return new OkObjectResult(objJSON);
      }
      catch (Exception ex)
      {
        return StatusCode(500, new OutputBase(ex));
      }
    }

    /*
       POST /member/login
       Request -> LoginRequestDTO
       Response -> MemberOutput
    */

    [HttpPost]
    [Route("Login")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(MemberOutput), StatusCodes.Status200OK)]

    public IActionResult Login([FromBody] LoginRequestDTO data)
    {
      try
      {
        var objJSON = new MemberOutput();
        objJSON = Helper.MemberHelper.Login(data);
        return new OkObjectResult(objJSON);
      }
      catch (Exception ex)
      {
        return StatusCode(500, new OutputBase(ex));
      }
    }

    [HttpGet]
    [Route("Order")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(OrderOuput), StatusCodes.Status200OK)]
    public IActionResult GetLast3Order([FromQuery] int MemberID)
    {
      try
      {
        var objJSON = new OrderOuput();
        objJSON.Data = Helper.MemberHelper.GetLast3Order(MemberID);
        return new OkObjectResult(objJSON);
      }
      catch (Exception ex)
      {
        return StatusCode(500, new OutputBase(ex));
      }
    }

    [HttpGet]
    [Route("Notification")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(NotificationOutput), StatusCodes.Status200OK)]
    public IActionResult GetNotification([FromQuery] int MemberID)
    {
      try
      {
        var objJSON = new NotificationOutput();
        objJSON.Data = Helper.NotifHelper.GetNotification(MemberID);
        return new OkObjectResult(objJSON);
      }
      catch (Exception ex)
      {
        return StatusCode(500, new OutputBase(ex));
      }
    }
  }
}
