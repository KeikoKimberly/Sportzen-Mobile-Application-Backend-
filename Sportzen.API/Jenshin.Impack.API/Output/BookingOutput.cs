using System;
using System.Collections.Generic;
using Binus.WS.Pattern.Output;
using Sportzen.API.Model;

namespace Sportzen.API.Output
{

  public class BookingOutput : OutputBase
  {
    public BookingOutput()
    {
      this.Success = 0;
    }

    public int Success { get; set; }
  }

  public class BookingAvailibilityOutput : OutputBase
  {
    public BookingAvailibilityOutput()
    {
      this.Available = 0;
    }
    public int Available { get; set; }
  }

  public class BookingFinishedOutput : OutputBase
  {
    public BookingFinishedOutput()
    {
      this.BookingList = new List<BookingFinished>();
    }

    public List<BookingFinished> BookingList { get; set; }
  }

  public class BookingPendingOutput : OutputBase
  {
    public BookingPendingOutput()
    {
      this.BookingList = new List<BookingPending>();
    }

    public List<BookingPending> BookingList { get; set; }
  }

  public class BookingProduct
  {
    public int ProductId { get; set; }
    public string ProductName { get; set; }
    public int BookingProductQty { get; set; }
    public int ProductPrice { get; set; }
  }

  public class BookingFinished
  {
    public int BookingID { get; set; }
    public string SportsCategory { get; set; }
    public string CourtPhoto { get; set; }
    public DateTime BookingDateTimeStart { get; set; }
    public DateTime BookingDateTimeEnd { get; set; }
    public int CourtPrice { get; set; }
    public int BookingCourtQty { get; set; }
    public int TotalPrice { get; set; }
    public List<BookingProduct> ProductList { get; set; }

  }

  public class BookingPending
  {
    public int BookingID { get; set; }
    public string CourtName { get; set; }
    public string CourtPhoto { get; set; }
    public DateTime BookingDateTimeStart { get; set; }
    public DateTime BookingDateTimeEnd { get; set; }
    public int CourtPrice { get; set; }
    public int BookingCourtQty { get; set; }
    public List<BookingProduct> ProductList { get; set; }

  }



}
