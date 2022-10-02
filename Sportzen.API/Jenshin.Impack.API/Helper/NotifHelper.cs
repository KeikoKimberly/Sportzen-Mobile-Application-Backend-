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
  public class NotifHelper
  {

    public static List<Notification> GetNotification(int MemberID)
    {
      if (MemberID == 0) throw new Exception("MemberID Need To Be Provided");

      var returnValue = new List<Notification>();

      try
      {
        var member = EntityHelper.Get<MsMember>(e => e.MemberID == MemberID).FirstOrDefault();

        if (member == null) throw new Exception("Member Not Exists!");

        returnValue = EntityHelper.Get<MsNotification>().Where(x => x.MemberID == MemberID && x.NotificationStatus == false)
            .Select(x => new Notification()
            {
              NotificationHeader = x.NotificationHeader
            })
            .ToList();

        var updateItem = EntityHelper.Get<MsNotification>(x => x.MemberID == MemberID).
            Select(x => new MsNotification()
            {
              NotificationID = x.NotificationID,
              MemberID = x.MemberID,
              NotificationHeader = x.NotificationHeader,
              NotificationStatus = true
            }).ToList();

        EntityHelper.Update<MsNotification>(updateItem);

      }
      catch (Exception ex)
      {
        throw new Exception(ex.Message);
      }

      return returnValue;
    }


  }
}