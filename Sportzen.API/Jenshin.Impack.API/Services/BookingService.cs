using System;
using System.Collections.Generic;
using System.Reflection;
using Binus.WS.Pattern.Entities;
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
  [Route("booking")]
  public class BookingService : BaseService
  {
    public BookingService(ILogger<BaseService> logger) : base(logger)
    {
    }

    /*
        GET /booking/check
        Request -> BookingAvailibilityRequestDTO
        Response -> BookingAvailibilityOutput
    */

    [HttpPost]
    [Route("check")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(BookingAvailibilityOutput), StatusCodes.Status200OK)]
    public IActionResult GetBookingAvailibility([FromBody] BookingAvailibilityRequestDTO data)
    {
      try
      {
        var objJSON = new BookingAvailibilityOutput();
        objJSON.Available = Helper.BookingHelper.GetBookingAvailibility(data);
        return new OkObjectResult(objJSON);
      }
      catch (Exception ex)
      {
        return StatusCode(500, new OutputBase(ex));
      }
    }

    /*
        GET /booking/finish
        Request -> MemberID
        Response -> BookingFinishedOutput
    */

    [HttpGet]
    [Route("finish")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(BookingFinishedOutput), StatusCodes.Status200OK)]
    public IActionResult GetFinishedBooking([FromQuery] int MemberID)
    {
      try
      {
        var objJSON = new BookingFinishedOutput();
        objJSON.BookingList = Helper.BookingHelper.GetFinishedBooking(MemberID);
        return new OkObjectResult(objJSON);
      }
      catch (Exception ex)
      {
        return StatusCode(500, new OutputBase(ex));
      }
    }

    /*
        GET /booking/pending
        Request -> MemberID
        Response -> BookingPendingOutput
    */


    [HttpGet]
    [Route("pending")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(BookingPendingOutput), StatusCodes.Status200OK)]
    public IActionResult GetPendingBooking([FromQuery] int MemberID)
    {
      try
      {
        var objJSON = new BookingPendingOutput();
        objJSON.BookingList = Helper.BookingHelper.GetPendingBooking(MemberID);
        return new OkObjectResult(objJSON);
      }
      catch (Exception ex)
      {
        return StatusCode(500, new OutputBase(ex));
      }
    }


    /*
        POST /booking
        Request -> BookingRequestDTO
        Response -> BookingOutput
    */

    [HttpPost]
    [Produces("application/json")]
    [ProducesResponseType(typeof(BookingOutput), StatusCodes.Status200OK)]
    public IActionResult CreateBooking([FromBody] BookingRequestDTO data)
    {
      try
      {
        var objJSON = new BookingOutput();
        objJSON.Success = Helper.BookingHelper.CreateBooking(data);
        return new OkObjectResult(objJSON);
      }
      catch (Exception ex)
      {
        return StatusCode(500, new OutputBase(ex));
      }

    }

    /*
        POST /booking/finish
        Request -> BookingID
        Response -> BookingOutput
    */

    [HttpPost]
    [Route("Finish")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(BookingOutput), StatusCodes.Status200OK)]
    public IActionResult UpdateBookingToPaid([FromBody] IDRequestDTO data)
    {
      try
      {
        var objJSON = new BookingOutput();
        objJSON.Success = Helper.BookingHelper.UpdateBookingToPaid(data.id);
        return new OkObjectResult(objJSON);
      }
      catch (Exception ex)
      {
        return StatusCode(500, new OutputBase(ex));
      }

    }
  }
}
