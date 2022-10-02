using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Binus.WS.Pattern.Output;

namespace Sportzen.API.Output
{
  public class CategoryOutput : OutputBase
  {
    public List<Sports> Data { get; set; }

    public CategoryOutput()
    {
      this.Data = new List<Sports>();
    }
  }

  public class CourtOutput : OutputBase
  {
    public List<Court> Data { get; set; }

    public CourtOutput()
    {
      this.Data = new List<Court>();
    }
  }

  public class ProductOutput : OutputBase
  {
    public List<Product> Data { get; set; }

    public ProductOutput()
    {
      this.Data = new List<Product>();
    }
  }

  public class Sports
  {
    public int ID { get; set; }
    public string SportsCategory { get; set; }

  }

  public class Court
  {
    public int CourtID { get; set; }
    public int SportsID { get; set; }
    public string CourtName { get; set; }
    public string CourtLocation { get; set; }
    public int CourtQuantity { get; set; }
    public int MaxCapacity { get; set; }
    public string CourtDescription { get; set; }
    public string CourtPhoto { get; set; }
    public int CourtPrice { get; set; }

  }

  public class Product
  {
    public int ProductID { get; set; }

    public int SportsID { get; set; }
    public string ProductName { get; set; }
    public int ProductStock { get; set; }
    public string ProductDescription { get; set; }
    public string ProductPhoto { get; set; }
    public int ProductPrice { get; set; }
  }
}
