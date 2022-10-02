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
    [Table("trBookingDetailCourt")]
    public class TrBookingDetailCourt : BaseModel
    {
        [Column("BookingID")]
        [Key]
        public int BookingID { get; set; }
        [Column("CourtID")]
        public int CourtID { get; set; }
        [Column("BookingCourtQty")]
        public int BookingCourtQty { get; set; }
        [Column("CourtPrice")]
        public int CourtPrice { get; set; }
       
    }
}
