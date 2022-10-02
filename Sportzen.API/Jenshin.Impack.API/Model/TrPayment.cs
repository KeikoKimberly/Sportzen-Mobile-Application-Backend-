using Binus.WS.Pattern.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Sportzen.API.Model
{
    [DatabaseName("Sportzen")]
    [Table("trPayment")]

    public class TrPayment : BaseModel
    {
        [Column("PaymentID")]
        [Key]
        public int PaymentID { get; set; }
        [Column("BookingID")]
        public int BookingID { get; set; }
        [Column("PaymentMethod")]
        public string PaymentMethod { get; set; }
        [Column("PaymentCreated")]
        public DateTime PaymentCreated { get; set; }
        [Column("PaymentStatus")]
        public bool PaymentStatus { get; set; }
    }
}
