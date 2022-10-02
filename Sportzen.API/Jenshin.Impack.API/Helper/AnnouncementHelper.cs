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
  public class AnnouncementHelper
  {

    public static List<Announcement> GetAnnouncement()
    {
      var returnValue = new List<Announcement>();
      var announcement = EntityHelper.Get<TrAnnouncement>().ToList();

      try
      {
        returnValue = announcement.Select(x => new Announcement
        {
          AnnouncementID = x.AnnouncementID,
          AnnouncementName = x.AnnouncementName,
          AnnouncementDescription = x.AnnouncementDescription,
          AnnouncementType = x.AnnouncementType,
          AnnouncementPhoto = x.AnnouncementPhoto,
          AnnouncementDuration = x.AnnouncementDuration
        }).ToList();
      }
      catch (Exception ex)
      {
        throw new Exception(ex.Message);
      }

      return returnValue;
    }

    public static List<Announcement> GetLastAnnouncement()
    {
      var returnValue = new List<Announcement>();
      var announcement = EntityHelper.Get<TrAnnouncement>().ToList();

      try
      {
        returnValue = announcement.Select(x => new Announcement
        {
          AnnouncementID = x.AnnouncementID,
          AnnouncementName = x.AnnouncementName,
          AnnouncementDescription = x.AnnouncementDescription,
          AnnouncementType = x.AnnouncementType,
          AnnouncementPhoto = x.AnnouncementPhoto,
          AnnouncementDuration = x.AnnouncementDuration
        }).OrderByDescending(announcement => announcement.AnnouncementID).Take(1).ToList();
      }
      catch (Exception ex)
      {
        throw new Exception(ex.Message);
      }

      return returnValue;
    }


  }
}