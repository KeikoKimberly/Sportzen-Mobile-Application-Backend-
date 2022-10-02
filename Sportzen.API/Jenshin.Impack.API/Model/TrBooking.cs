using Binus.WS.Pattern.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Sportzen.API.Model
{
    [DatabaseName("Sportzen")]
    [Table("TrBooking")]
    public class TrBooking : BaseModel
    {
        [Column("BookingID")]
        [Key]
        public int BookingID { get; set; }
        [Column("MemberID")]
        public int MemberID { get; set; }
        [Column("BookingDateTimeStart")]
        public DateTime BookingDateTimeStart { get; set; }
        [Column("BookingDateTimeEnd")]
        public DateTime BookingDateTimeEnd { get; set; }
        [Column("NumberOfpeople")]
        public int NumberOfPeople { get; set; }

    }
}
