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
    [Table("TrAnnouncement")]
    public class TrAnnouncement : BaseModel
    {
        [Column("AnnouncementID")]
        [Key]
        public int AnnouncementID { get; set; }
        [Column("SportsID")]
        public int SportsID { get; set; }
        [Column("AnnouncementName")]
        public string AnnouncementName { get; set; }
        [Column("AnnouncementPhoto")]
        public string AnnouncementPhoto { get; set; }
        [Column("AnnouncementDescription")]
        public string AnnouncementDescription { get; set; }
        [Column("AnnouncementType")]
        public string AnnouncementType { get; set; }
        [Column("AnnouncementDuration")]
        public DateTime AnnouncementDuration { get; set; }
    }
}
