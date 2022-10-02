using System;
using System.Collections.Generic;
using Binus.WS.Pattern.Output;

namespace Sportzen.API.Output
{

  public class OrderOuput : OutputBase
  {
    public List<Order> Data { get; set; }

    public OrderOuput()
    {
      this.Data = new List<Order>();
    }
  }

  public class AnnouncementOutput : OutputBase
  {
    public List<Announcement> Data { get; set; }

    public AnnouncementOutput()
    {
      this.Data = new List<Announcement>();
    }
  }

  public class NotificationOutput : OutputBase
  {
    public List<Notification> Data { get; set; }

    public NotificationOutput()
    {
      this.Data = new List<Notification>();
    }
  }

  public class Order
  {
    public int BookingID { get; set; }

    public int MemberID { get; set; }
    public DateTime BookingDateTimeStart { get; set; }
    public DateTime BookingDateTimeEnd { get; set; }

    public int CourtID { get; set; }
    public string CourtName { get; set; }
    public string CourtLocation { get; set; }

    public bool PaymentStatus { get; set; }
    public DateTime PaymentCreated { get; set; }

    public int TotalPrice { get; set; }

  }

  public class Announcement
  {
    public int AnnouncementID { get; set; }
    public int SportsID { get; set; }
    public string AnnouncementName { get; set; }
    public string AnnouncementPhoto { get; set; }
    public string AnnouncementDescription { get; set; }
    public string AnnouncementType { get; set; }
    public DateTime AnnouncementDuration { get; set; }

  }

  public class Notification
  {
    public string NotificationHeader { get; set; }
  }


}
