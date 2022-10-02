using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Binus.WS.Pattern.Entities;
using Binus.WS.Pattern.Helper;
using Sportzen.API.Model;
using Sportzen.API.Model.Request;
using Sportzen.API.Output;
using BC = BCrypt.Net.BCrypt;

namespace Sportzen.API.Helper
{
  public class MemberHelper
  {

    public static SpecificMember GetSpecificMember(int id)
    {
      if (id == 0) throw new Exception("MemberID Need To Be Provided!");
      var returnValue = new SpecificMember();

      try
      {
        var member = EntityHelper.Get<MsMember>(e => e.MemberID == id).FirstOrDefault();

        if (member == null) throw new Exception("Member Not Found!");

        var booking = EntityHelper.Get<TrBooking>(e => e.MemberID == id).ToList();
        var payment = EntityHelper.Get<TrPayment>(e => e.PaymentStatus == true).ToList();

        var totalBookingPaid = 0;

        if (booking != null && payment != null)
        {
          totalBookingPaid = booking.Join(
              payment,
              bookingKey => bookingKey.BookingID,
              paymentKey => paymentKey.BookingID,
              (s1, s2) => new
              {
                BookingID = s1.BookingID,
              }
          ).ToList().Count;
        }

        returnValue = new SpecificMember()
        {
          MemberID = member.MemberID,
          MemberName = member.MemberName,
          MemberPhoneNumber = member.MemberPhoneNumber,
          MemberEmail = member.MemberEmail,
          MemberPassword = member.MemberPassword,
          MemberAddress = member.MemberAddress,
          MemberBankAccount = member.MemberBankAccount,
          MemberPhoto = member.MemberPhoto,
          EWalletBalance = member.EWalletBalance,
          TotalMemberBooking = totalBookingPaid
        };

      }
      catch (Exception ex)
      {
        throw new Exception(ex.Message);
      }

      return returnValue;
    }

    public static MemberOutput AddNewMember(MsMember data)
    {

      if (data.MemberPassword == "" || data.MemberName == "" || data.MemberEmail == "" || data.MemberPhoneNumber == "")
      {
        throw new Exception("All Field Need to Be Filled!");
      }

      var returnValue = new MemberOutput();

      try
      {
        // Validation
        int isEmailExists = EntityHelper.Get<MsMember>(e => e.MemberEmail == data.MemberEmail).ToList().Count;

        if (isEmailExists > 0) throw new Exception("Email already used!");

        var cryptedPassword = BC.HashPassword(data.MemberPassword, 12);

        EntityHelper.Add<MsMember>(new MsMember()
        {
          MemberName = data.MemberName,
          MemberPhoneNumber = data.MemberPhoneNumber,
          MemberEmail = data.MemberEmail,
          MemberPassword = cryptedPassword
        });

        var member = EntityHelper.Get<MsMember>(e => e.MemberPassword == cryptedPassword && e.MemberEmail == data.MemberEmail).FirstOrDefault();

        if (member == null)
        {
          throw new Exception("Database Error!");
        }

        var memberData = new SpecificMember()
        {
          MemberID = member.MemberID,
          MemberName = member.MemberName,
          MemberPhoneNumber = member.MemberPhoneNumber,
          MemberEmail = member.MemberEmail,
          MemberPassword = member.MemberPassword,
        };

        returnValue.Data = memberData;
        returnValue.Success = 1;

      }
      catch (Exception ex)
      {
        throw new Exception(ex.Message);
      }

      return returnValue;
    }


    public static MemberOutput UpdateMember(MsMember data)
    {

      if (data.MemberID == 0) throw new Exception("Member ID must be provided!");

      var member = EntityHelper.Get<MsMember>(x => x.MemberID == data.MemberID).FirstOrDefault();

      if (member == null) throw new Exception("Member not found");

      var returnValue = new MemberOutput();

      try
      {

        var isUpdated = EntityHelper.Update<MsMember>(new MsMember()
        {
          MemberID = data.MemberID,
          MemberName = data.MemberName ?? member.MemberName,
          MemberPhoneNumber = data.MemberPhoneNumber ?? member.MemberPhoneNumber,
          MemberEmail = data.MemberEmail ?? member.MemberEmail,
          MemberPassword = data.MemberPassword == null ? member.MemberPassword : BC.HashPassword(data.MemberPassword),
          MemberAddress = data.MemberAddress ?? member.MemberAddress,
          MemberBankAccount = data.MemberBankAccount ?? member.MemberBankAccount,
          MemberPhoto = data.MemberPhoto ?? member.MemberPhoto,
          EWalletBalance = member.EWalletBalance,
        });

        var updatedMember = EntityHelper.Get<MsMember>(x => x.MemberID == data.MemberID).FirstOrDefault();

        var returnData = new SpecificMember()
        {
          MemberID = data.MemberID,
          MemberName = data.MemberName ?? member.MemberName,
          MemberPhoneNumber = data.MemberPhoneNumber ?? member.MemberPhoneNumber,
          MemberEmail = data.MemberEmail ?? member.MemberEmail,
          MemberAddress = data.MemberAddress ?? member.MemberAddress,
          MemberBankAccount = data.MemberBankAccount ?? member.MemberBankAccount,
          MemberPhoto = data.MemberPhoto ?? member.MemberPhoto,
          EWalletBalance = member.EWalletBalance,
        };

        returnValue.Data = returnData;
        returnValue.Success = 1;

      }
      catch (Exception ex)
      {
        throw new Exception(ex.Message);
      }

      // END OF CODE
      return returnValue;
    }

    public static MemberOutput Login(LoginRequestDTO data)
    {
      var returnData = new MemberOutput();

      try
      {
        if (data.Email == null || data.Password == null)
        {
          throw new Exception("Email or Password Neeed To Be Provided!");
        }

        var member = EntityHelper.Get<MsMember>(e => e.MemberEmail == data.Email).FirstOrDefault();


        if (member == null || !BC.Verify(data.Password, member.MemberPassword))
        {
          throw new Exception("Email or Password Wrong!");
        }

        var booking = EntityHelper
          .Get<TrBooking>(e => e.MemberID == member.MemberID)
          .ToList();

        var payment = EntityHelper.Get<TrPayment>(e => e.PaymentStatus == true);

        var totalBookingPaid = 0;

        if (payment != null && booking != null)
        {
          totalBookingPaid = booking.Join(
              payment,
              bookingKey => bookingKey.BookingID,
              paymentKey => paymentKey.BookingID,
              (s1, s2) => new
              {
                BookingID = s1.BookingID,
              }
          ).ToList().Count;
        }

        var memberData = new SpecificMember()
        {
          MemberID = member.MemberID,
          MemberName = member.MemberName,
          MemberAddress = member.MemberAddress,
          MemberEmail = member.MemberEmail,
          MemberPassword = member.MemberPassword,
          MemberBankAccount = member.MemberBankAccount,
          MemberPhoneNumber = member.MemberPhoneNumber,
          MemberPhoto = member.MemberPhoto,
          EWalletBalance = member.EWalletBalance,
          TotalMemberBooking = totalBookingPaid
        };

        returnData.Data = memberData;
        returnData.Success = 1;


      }
      catch (Exception ex)
      {
        throw new Exception(ex.Message);
      }

      return returnData;
    }

    public static List<Order> GetLast3Order(int memberId)
    {
      var returnValue = new List<Order>();

      try
      {
        if (memberId == 0)
        {
          throw new Exception("id must be filled!");
        }

        var book = EntityHelper.Get<TrBooking>(x => x.MemberID == memberId).ToList();
        var bookDetail = EntityHelper.Get<TrBookingDetailCourt>().ToList();
        var court = EntityHelper.Get<TrCourt>().ToList();
        var payment = EntityHelper.Get<TrPayment>().ToList().Where(e => (((DateTime.Now - e.PaymentCreated).TotalHours < 1 && e.PaymentStatus == false)) || e.PaymentStatus == true);
        var bookingDetailProduct = EntityHelper.Get<TrBookingDetailProduct>().ToList();
        var product = EntityHelper.Get<TrProduct>().ToList();

        if (book.Count > 0)
        {
          returnValue = book.Join(
             bookDetail,
             left => left.BookingID,
             right => right.BookingID,
             (left, right) => new Order()
             {
               BookingID = left.BookingID,
               BookingDateTimeStart = left.BookingDateTimeStart,
               BookingDateTimeEnd = left.BookingDateTimeEnd,
               CourtID = right.CourtID
             }).Join(court,
             leftkey => leftkey.CourtID,
             rightkey => rightkey.CourtID,
             (leftkey, rightkey) => new Order()
             {
               BookingID = leftkey.BookingID,
               BookingDateTimeStart = leftkey.BookingDateTimeStart,
               BookingDateTimeEnd = leftkey.BookingDateTimeEnd,
               CourtID = leftkey.CourtID,
               CourtLocation = rightkey.CourtLocation,
               CourtName = rightkey.CourtName
             }).Join(payment,
             leftKey => leftKey.BookingID,
             rightKey => rightKey.BookingID,
             (leftkey, rightkey) => new Order()
             {
               BookingID = leftkey.BookingID,
               BookingDateTimeStart = leftkey.BookingDateTimeStart,
               BookingDateTimeEnd = leftkey.BookingDateTimeEnd,
               CourtID = leftkey.CourtID,
               CourtLocation = leftkey.CourtLocation,
               CourtName = leftkey.CourtName,
               PaymentStatus = rightkey.PaymentStatus,
               PaymentCreated = rightkey.PaymentCreated,
             }
          ).OrderByDescending(book => book.BookingID).Take(3).ToList();
        }

        foreach (var item in returnValue)
        {
          var itemBookingDetailProduct = bookingDetailProduct.Where(e => e.BookingID == item.BookingID);
          var itemCourt = bookDetail.Where(e => e.CourtID == item.CourtID).FirstOrDefault();

          int rentHour = Convert.ToInt32(Math.Ceiling(((item.BookingDateTimeEnd - item.BookingDateTimeStart).TotalMinutes) / 60));
          int totalBuy = itemCourt.CourtPrice * itemCourt.BookingCourtQty * rentHour;

          foreach (var i in itemBookingDetailProduct)
          {
            int price = i.ProductPrice * i.BookingProductQty;
            totalBuy += price;
          }
          item.TotalPrice = totalBuy;
        }
      }
      catch (Exception ex)
      {
        throw new Exception(ex.Message);
      }

      return returnValue;
    }



  }
}
