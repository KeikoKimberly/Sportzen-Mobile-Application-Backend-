using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Binus.WS.Pattern.Entities;
using Binus.WS.Pattern.Helper;
using Sportzen.API.Model;
using Sportzen.API.Output;


namespace Sportzen.API.Helper
{
  public class CategoryHelper
  {
    public static List<Sports> GetAllCategory()
    {
      var returnValue = new List<Sports>();

      var sports = EntityHelper.Get<MsSports>().ToList();

      try
      {
        if (sports != null)
        {
          returnValue = sports.Select(x => new Sports
          {
            ID = x.SportsID,
            SportsCategory = x.SportsCategory
          }).ToList();
        }
      }
      catch (Exception ex)
      {
        throw new Exception(ex.Message);
      }

      return returnValue;
    }

    public static List<Court> GetCourt(int SportsID)
    {

      if (SportsID == 0) throw new Exception("Sports ID must be provided!");

      var returnValue = new List<Court>();

      try
      {
        var court = EntityHelper.Get<TrCourt>(x => x.SportsID == SportsID).ToList();

        returnValue = court.Select(x => new Court
        {
          CourtID = x.CourtID,
          CourtName = x.CourtName,
          CourtDescription = x.CourtDescription,
          CourtLocation = x.CourtLocation,
          CourtQuantity = x.CourtQuantity,
          CourtPhoto = x.CourtPhoto,
          MaxCapacity = x.MaxCapacity,
          SportsID = x.SportsID,
          CourtPrice = x.CourtPrice,

        }).ToList();

      }
      catch (Exception ex)
      {
        throw new Exception(ex.Message);
      }

      return returnValue;
    }

    public static List<Product> GetProduct(int id)
    {
      if (id == 0)
      {
        throw new Exception("Sports ID must be provided!");
      }

      var returnValue = new List<Product>();

      var product = EntityHelper.Get<TrProduct>(x => x.SportsID == id).ToList();

      try
      {
        returnValue = product.Select(x => new Product
        {
          ProductID = x.ProductID,
          ProductName = x.ProductName,
          ProductDescription = x.ProductDescription,
          ProductPhoto = x.ProductPhoto,
          ProductStock = x.ProductStock,
          ProductPrice = x.ProductPrice,
          SportsID = x.SportsID
        }).ToList();

      }
      catch (Exception ex)
      {
        throw new Exception(ex.Message);
      }

      return returnValue;
    }
  }

}