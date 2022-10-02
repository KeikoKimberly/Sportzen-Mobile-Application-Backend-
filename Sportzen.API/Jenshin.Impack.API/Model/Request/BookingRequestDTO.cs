using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Sportzen.API.Output;

namespace Sportzen.API.Model.Request
{
  public class BookingRequestDTO
  {
    [Required]
    public int CourtID { get; set; }
    [Required]
    public int MemberID { get; set; }
    [Required]
    public int BookingCourtQty { get; set; }
    [Required]
    public int NumberOfPeople { get; set; }
    [Required]
    public DateTime BookingDateTimeStart { get; set; }
    [Required]
    public DateTime BookingDateTimeEnd { get; set; }
    [Required]
    public string PaymentMethod { get; set; }
    public List<BookingProduct> BookingProduct { get; set; }
  }
}
