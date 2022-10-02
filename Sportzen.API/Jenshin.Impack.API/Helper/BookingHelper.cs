using System;
using System.Collections;
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
  public class BookingHelper
  {
    public static int GetBookingAvailibility(BookingAvailibilityRequestDTO data)
    {
      var availableCourt = 0;

      try
      {
        if (data.BookingDateTimeStart == null || data.BookingDateTimeEnd == null || data.CourtID == 0)
        {
          throw new Exception("BookingDateTimeStart, BookingDateTimeEnd, CourtID Need To Be Filled!");
        }

        if (data.BookingDateTimeStart < DateTime.Now || data.BookingDateTimeEnd < DateTime.Now)
        {
          throw new Exception("Bookings Can Only Be Made For The Future");
        }

        if (data.BookingDateTimeEnd < data.BookingDateTimeStart)
        {
          throw new Exception("Booking DateTimeStart Can't Be Higher Than DateTimeEnd");
        }


        var trCourt = EntityHelper.Get<TrCourt>(e => e.CourtID == data.CourtID).FirstOrDefault();

        var trBookingDetailCourt = EntityHelper.Get<TrBookingDetailCourt>(e => e.CourtID == data.CourtID).ToList();

        var trBooking = EntityHelper.Get<TrBooking>(e => e.BookingDateTimeStart >= data.BookingDateTimeStart && e.BookingDateTimeEnd <= data.BookingDateTimeEnd || e.BookingDateTimeStart >= data.BookingDateTimeStart && e.BookingDateTimeEnd <= data.BookingDateTimeStart || e.BookingDateTimeEnd >= data.BookingDateTimeStart && e.BookingDateTimeEnd <= data.BookingDateTimeEnd).ToList();

        var payment = EntityHelper.Get<TrPayment>().ToList().Where(e => (e.PaymentStatus == false && (DateTime.Now - e.PaymentCreated).TotalHours < 1) || e.PaymentStatus == true);

        var bookingMbookingDetail = trBooking
            .Join(trBookingDetailCourt,
               keyBooking => keyBooking.BookingID,
               keyBookingDetailCourt => keyBookingDetailCourt.BookingID,
               (s1, s2) => new
               {
                 s2.CourtID,
                 s1.BookingID
               }
            ).ToList()
            .Join(payment,
                leftKey => leftKey.BookingID,
                rightKey => rightKey.BookingID,
                (s1, s2) => new
                {
                  s1.CourtID
                }
            );

        var totalBookedOrder = bookingMbookingDetail.Where(e => e.CourtID == data.CourtID).ToList().Count;


        if (totalBookedOrder < trCourt.CourtQuantity)
        {
          availableCourt = trCourt.CourtQuantity - totalBookedOrder;
        }
      }
      catch (Exception ex)
      {
        throw new Exception(ex.Message);
      }

      return availableCourt;
    }

    public static List<BookingFinished> GetFinishedBooking(int memberID)
    {


      var returnValue = new List<BookingFinished>();

      try
      {
        //Validation
        if (memberID == 0) throw new Exception("MemberID Must be Provided!");

        var member = EntityHelper.Get<MsMember>(e => e.MemberID == memberID).FirstOrDefault();

        if (member == null) throw new Exception("Member not Exists!");

        var booking = EntityHelper.Get<TrBooking>(e => e.MemberID == memberID).ToList();
        var payment = EntityHelper.Get<TrPayment>(e => e.PaymentStatus == true).ToList();
        var bookingDetailProduct = EntityHelper.Get<TrBookingDetailProduct>().ToList();
        var product = EntityHelper.Get<TrProduct>().ToList();
        var sports = EntityHelper.Get<MsSports>().ToList();
        var bookingDetailCourt = EntityHelper.Get<TrBookingDetailCourt>().ToList();
        var court = EntityHelper.Get<TrCourt>().ToList();



        if (booking != null || payment != null)
        {
          returnValue = booking
         .Join(
             payment,
             bookingKey => bookingKey.BookingID,
             paymentKey => paymentKey.BookingID,
             (s1, s2) => new
             {
               PaymentID = s2.PaymentID,
               BookingID = s1.BookingID,
               BookingDateTimeStart = s1.BookingDateTimeStart,
               BookingDateTimeEnd = s1.BookingDateTimeEnd
             }
         ).ToList()
         .Join(
             bookingDetailCourt,
             bookingKey => bookingKey.BookingID,
             bookingDetailCourtKey => bookingDetailCourtKey.BookingID,
             (s1, s2) => new
             {
               PaymentID = s1.PaymentID,
               BookingID = s1.BookingID,
               CourtID = s2.CourtID,
               BookingDateTimeStart = s1.BookingDateTimeStart,
               BookingDateTimeEnd = s1.BookingDateTimeEnd,
               CourtPrice = s2.CourtPrice,
               BookingCourtQty = s2.BookingCourtQty
             }
          ).ToList()
         .Join(
             court,
             bookingDetailCourt => bookingDetailCourt.CourtID,
             court => court.CourtID,
             (s1, s2) => new
             {
               PaymentID = s1.PaymentID,
               BookingID = s1.BookingID,
               CourtID = s1.CourtID,
               SportsID = s2.SportsID,
               BookingDateTimeStart = s1.BookingDateTimeStart,
               BookingDateTimeEnd = s1.BookingDateTimeEnd,
               CourtPrice = s1.CourtPrice,
               BookingCourtQty = s1.BookingCourtQty,
               CourtPhoto = s2.CourtPhoto
             }
          ).ToList()
         .Join(
             sports,
             courtKey => courtKey.SportsID,
             sportsKey => sportsKey.SportsID,
             (s1, s2) => new BookingFinished
             {
               BookingID = s1.BookingID,
               BookingDateTimeStart = s1.BookingDateTimeStart,
               BookingDateTimeEnd = s1.BookingDateTimeEnd,
               CourtPrice = s1.CourtPrice,
               BookingCourtQty = s1.BookingCourtQty,
               SportsCategory = s2.SportsCategory,
               CourtPhoto = s1.CourtPhoto
             }
          )
         .ToList();


          foreach (var data in returnValue)
          {
            int rentHour = Convert.ToInt32(Math.Ceiling(((data.BookingDateTimeEnd - data.BookingDateTimeStart).TotalMinutes) / 60));
            int totalPrice = data.CourtPrice * data.BookingCourtQty * rentHour;

            var productMerged = bookingDetailProduct
                .Where(e => e.BookingID == data.BookingID).ToList()
                .Join(
                    product,
                    bookingDetailProductKey => bookingDetailProductKey.ProductID,
                    productKey => productKey.ProductID,
                    (s1, s2) => new BookingProduct
                    {
                      ProductName = s2.ProductName,
                      BookingProductQty = s1.BookingProductQty,
                      ProductPrice = s2.ProductPrice,
                      ProductId = s1.ProductID
                    }
                 )
                .ToList();
            data.ProductList = productMerged;

            foreach (var x in productMerged)
            {
              totalPrice += x.ProductPrice;
            }

            data.TotalPrice = totalPrice;
          }
        }
      }
      catch (Exception ex)
      {
        throw new Exception(ex.Message);
      }

      return returnValue;
    }

    public static List<BookingPending> GetPendingBooking(int memberID)
    {
      var returnValue = new List<BookingPending>();

      try
      {
        if (memberID == 0) throw new Exception("MemberID Must be Provided!");

        var member = EntityHelper.Get<MsMember>(e => e.MemberID == memberID).FirstOrDefault();

        if (member == null) throw new Exception("Member not Exists!");

        var booking = EntityHelper.Get<TrBooking>(e => e.MemberID == memberID).ToList();
        var payment = EntityHelper.Get<TrPayment>(e => e.PaymentStatus == false).ToList().Where(e => (DateTime.Now - e.PaymentCreated).TotalHours < 1);
        var bookingDetailProduct = EntityHelper.Get<TrBookingDetailProduct>().ToList();
        var product = EntityHelper.Get<TrProduct>().ToList();
        var bookingDetailCourt = EntityHelper.Get<TrBookingDetailCourt>().ToList();
        var court = EntityHelper.Get<TrCourt>().ToList();

        if (booking != null || payment != null)
        {
          returnValue = booking
         .Join(
             payment,
             bookingKey => bookingKey.BookingID,
             paymentKey => paymentKey.BookingID,
             (s1, s2) => new
             {
               PaymentID = s2.PaymentID,
               BookingID = s2.BookingID,
               BookingDateTimeStart = s1.BookingDateTimeStart,
               BookingDateTimeEnd = s1.BookingDateTimeEnd
             }
         ).ToList()
         .Join(
             bookingDetailCourt,
             bookingKey => bookingKey.BookingID,
             bookingDetailCourtKey => bookingDetailCourtKey.BookingID,
             (s1, s2) => new
             {
               PaymentID = s1.PaymentID,
               BookingID = s2.BookingID,
               CourtID = s2.CourtID,
               BookingDateTimeStart = s1.BookingDateTimeStart,
               BookingDateTimeEnd = s1.BookingDateTimeEnd,
               CourtPrice = s2.CourtPrice,
               BookingCourtQty = s2.BookingCourtQty
             }
          ).ToList()
         .Join(
             court,
             bookingDetailCourt => bookingDetailCourt.CourtID,
             court => court.CourtID,
             (s1, s2) => new BookingPending
             {
               BookingID = s1.BookingID,
               BookingDateTimeStart = s1.BookingDateTimeStart,
               BookingDateTimeEnd = s1.BookingDateTimeEnd,
               BookingCourtQty = s1.BookingCourtQty,
               CourtPrice = s1.CourtPrice,
               CourtPhoto = s2.CourtPhoto,
               CourtName = s2.CourtName
             }
          ).ToList();

          foreach (var data in returnValue)
          {
            var productMerged = bookingDetailProduct
                .Where(e => e.BookingID == data.BookingID).ToList()
                .Join(
                    product,
                    bookingDetailProductKey => bookingDetailProductKey.ProductID,
                    productKey => productKey.ProductID,
                    (s1, s2) => new BookingProduct
                    {
                      ProductName = s2.ProductName,
                      BookingProductQty = s1.BookingProductQty,
                      ProductId = s1.ProductID,
                      ProductPrice = s2.ProductPrice,
                    }
                 )
                .ToList();
            data.ProductList = productMerged;
          }
        }
      }
      catch (Exception ex)
      {
        throw new Exception(ex.Message);
      }

      return returnValue;
    }

    public static int CreateBooking(BookingRequestDTO data)
    {
      try
      {

        int isCourtAvailable = GetBookingAvailibility(new BookingAvailibilityRequestDTO()
        {
          BookingDateTimeStart = data.BookingDateTimeStart,
          BookingDateTimeEnd = data.BookingDateTimeEnd,
          CourtID = data.CourtID,
        });

        var Court = EntityHelper.Get<TrCourt>(e => e.CourtID == data.CourtID).FirstOrDefault();
        var Product = EntityHelper.Get<TrProduct>();

        if (isCourtAvailable == 0 || data.BookingCourtQty > isCourtAvailable)
        {
          throw new Exception("Court Not Available!");
        }

        EntityHelper.Add<TrBooking>(new TrBooking()
        {
          MemberID = data.MemberID,
          BookingDateTimeStart = data.BookingDateTimeStart,
          BookingDateTimeEnd = data.BookingDateTimeEnd,
          NumberOfPeople = data.NumberOfPeople
        });

        int lastBookingID = EntityHelper.Get<TrBooking>()
             .OrderByDescending(e => e.BookingID)
             .Take(1)
             .Select(e => e.BookingID)
             .ToList()
             .FirstOrDefault();

        EntityHelper.Add<TrPayment>(new TrPayment()
        {
          PaymentStatus = false,
          PaymentMethod = data.PaymentMethod,
          PaymentCreated = DateTime.Now,
          BookingID = lastBookingID,
        });

        EntityHelper.Add<TrBookingDetailCourt>(new TrBookingDetailCourt()
        {
          BookingID = lastBookingID,
          CourtID = data.CourtID,
          BookingCourtQty = data.BookingCourtQty,
          CourtPrice = Court.CourtPrice
        });


        foreach (var item in data.BookingProduct)
        {
          var itemProduct = Product.Where(e => e.ProductID == item.ProductId).FirstOrDefault();
          EntityHelper.Add<TrBookingDetailProduct>(new TrBookingDetailProduct()
          {
            BookingID = lastBookingID,
            ProductID = item.ProductId,
            BookingProductQty = item.BookingProductQty,
            ProductPrice = itemProduct.ProductPrice
          });
        }

      }
      catch (Exception ex)
      {
        throw new Exception(ex.Message);
      }

      return 1;
    }

    public static int UpdateBookingToPaid(int bookingID)
    {
      if (bookingID == 0) throw new Exception("BookingID Need To Be Provided!");
      try
      {
        TrBooking bookingData = EntityHelper.Get<TrBooking>(e => e.BookingID == bookingID).FirstOrDefault();

        if (bookingData == null) throw new Exception("Booking Not Found!");




        var paymentData = EntityHelper.Get<TrPayment>(e => e.BookingID == bookingData.BookingID && e.PaymentStatus == false).FirstOrDefault();

        if (paymentData == null) throw new Exception("Booking Already Paid!");

        if ((DateTime.Now - paymentData.PaymentCreated).TotalHours > 1) throw new Exception("Booking already expired!");

        var userData = EntityHelper.Get<MsMember>(e => e.MemberID == bookingData.MemberID).FirstOrDefault();
        var productData = EntityHelper.Get<TrBookingDetailProduct>(e => e.BookingID == bookingData.BookingID).ToList();
        var courtData = EntityHelper.Get<TrBookingDetailCourt>(e => e.BookingID == bookingData.BookingID).FirstOrDefault();

        int totalBuy = 0;

        foreach (var item in productData)
        {
          int price = item.ProductPrice * item.BookingProductQty;
          totalBuy += price;
        }

        int rentHour = Convert.ToInt32(Math.Ceiling(((bookingData.BookingDateTimeEnd - bookingData.BookingDateTimeStart).TotalMinutes) / 60));
        totalBuy += courtData.CourtPrice * courtData.BookingCourtQty * rentHour;

        if (paymentData.PaymentMethod == "Wallet")
        {
          if (totalBuy > userData.EWalletBalance)
          {
            throw new Exception("You Don't Have Enough Balance!");
          }

          EntityHelper.Update<MsMember>(new MsMember()
          {
            MemberID = userData.MemberID,
            MemberName = userData.MemberName,
            MemberPhoneNumber = userData.MemberPhoneNumber,
            MemberEmail = userData.MemberEmail,
            MemberPassword = userData.MemberPassword,
            MemberAddress = userData.MemberAddress,
            MemberBankAccount = userData.MemberBankAccount,
            MemberPhoto = userData.MemberPhoto,
            EWalletBalance = userData.EWalletBalance - totalBuy,
          });
        }

        EntityHelper.Update<TrPayment>(new TrPayment()
        {
          PaymentID = paymentData.PaymentID,
          PaymentStatus = true,
          PaymentMethod = paymentData.PaymentMethod,
          PaymentCreated = paymentData.PaymentCreated,
          BookingID = paymentData.BookingID,
        });

      }
      catch (Exception ex)
      {
        throw new Exception(ex.Message);
      }

      return 1;
    }
  }
}
