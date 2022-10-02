using System;
using System.Collections.Generic;
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
  [Route("Category")]
  public class CategoryService : BaseService
  {
    public CategoryService(ILogger<BaseService> logger) : base(logger)
    {
    }

    [HttpGet]
    [Produces("application/json")]
    [ProducesResponseType(typeof(CategoryOutput), StatusCodes.Status200OK)]
    public IActionResult GetAllCategory()
    {
      try
      {
        var objJSON = new CategoryOutput();
        objJSON.Data = Helper.CategoryHelper.GetAllCategory();
        return new OkObjectResult(objJSON);
      }
      catch (Exception ex)
      {
        return StatusCode(500, new OutputBase(ex));
      }
    }

    [HttpGet]
    [Route("Court")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(CourtOutput), StatusCodes.Status200OK)]
    public IActionResult GetCourt([FromQuery] int SportsID)
    {
      try
      {
        var objJSON = new CourtOutput();
        objJSON.Data = Helper.CategoryHelper.GetCourt(SportsID);
        return new OkObjectResult(objJSON);
      }
      catch (Exception ex)
      {
        return StatusCode(500, new OutputBase(ex));
      }
    }


    [HttpGet]
    [Route("Product")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(ProductOutput), StatusCodes.Status200OK)]
    public IActionResult GetProduct([FromQuery] int SportsID)
    {
      try
      {
        var objJSON = new ProductOutput();
        objJSON.Data = Helper.CategoryHelper.GetProduct(SportsID);
        return new OkObjectResult(objJSON);
      }
      catch (Exception ex)
      {
        return StatusCode(500, new OutputBase(ex));
      }
    }


  }
}