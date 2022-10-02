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
    [Table("TrTopUp")]
    public class TrTopUp : BaseModel
    {
        [Column("TopUpID")]
        [Key]
        public int TopUpID { get; set; }
        [Column("MemberID")]
        public int MemberID { get; set; }
        [Column("NotificationID")]
        public int ? NotificationID { get; set; }
        [Column("TopUpMethod")]
        public string TopUpMethod { get; set; }
        [Column("TopUpDate")]
        public DateTime TopUpDate { get; set; }
        [Column("TopUpBalance")]
        public int TopUpBalance  { get; set; }
        [Column("TopUpStatus")]
        public int TopUpStatus { get; set; }
        
    }
}
