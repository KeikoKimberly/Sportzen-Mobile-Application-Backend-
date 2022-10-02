using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Binus.WS.Pattern.Entities;
using Binus.WS.Pattern.Helper;
using Sportzen.API.Model;
using Sportzen.API.Model.Request;
using Sportzen.API.Output;

namespace Sportzen.API.Helper
{
  public class TopUpHelper
  {

    public static string TopUp(TopUpRequestDTO data)
    {
      if (data.TopUpMethod == null || data.Nominal == 0 || data.MemberID == 0)
      {
        throw new Exception("TopUpMethod, Nominal, MemberID Need To Be Provided");
      }

      string message = "Top Up Success";

      try
      {
        var member = EntityHelper.Get<MsMember>(x => x.MemberID == data.MemberID).FirstOrDefault();

        if (member == null) throw new Exception("Member Not Found!");

        EntityHelper.Add<TrTopUp>(new TrTopUp()
        {
          TopUpDate = DateTime.Now,
          TopUpMethod = data.TopUpMethod,
          TopUpBalance = data.Nominal,
          TopUpStatus = 1,
          MemberID = data.MemberID,
          NotificationID = null
        });

        var topUp = EntityHelper.Get<MsMember>().Where(x => x.MemberID == data.MemberID)
            .Select(x => new MsMember()
            {
              MemberID = x.MemberID,
              MemberName = x.MemberName,
              MemberAddress = x.MemberAddress,
              MemberEmail = x.MemberEmail,
              MemberBankAccount = x.MemberBankAccount,
              MemberPassword = x.MemberPassword,
              MemberPhoneNumber = x.MemberPhoneNumber,
              MemberPhoto = x.MemberPhoto,
              EWalletBalance = x.EWalletBalance + data.Nominal,
            })
            .FirstOrDefault();

        EntityHelper.Update<MsMember>(topUp);


      }
      catch (Exception ex)
      {
        throw new Exception(ex.Message);
      }

      return message;
    }


  }
}