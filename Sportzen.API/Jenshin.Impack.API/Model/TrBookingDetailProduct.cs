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
    [Table("trBookingDetailProduct")]
    public class TrBookingDetailProduct : BaseModel
    {

        [Column("BookingDetailProductID")]
        [Key]
        public int BookingDetailProductID { get; set; }
        [Column("BookingID")]
        public int BookingID { get; set; }
        [Column("ProductID")]
        public int ProductID { get; set; }
        [Column("BookingProductQty")]
        public int BookingProductQty { get; set; }
        [Column("ProductPrice")]
        public int ProductPrice { get; set; }
       
    }
}
