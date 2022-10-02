using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Binus.WS.Pattern.Model;

namespace Sportzen.API.Model
{
  [DatabaseName("Sportzen")]
  [Table("appsNotification")]
  public class MsNotification : BaseModel
  {
    [Column("NotificationID")]
    [Key]
    public int NotificationID { get; set; }
    [Column("NotificationHeader")]
    public string NotificationHeader { get; set; }
    [Column("MemberID")]
    public int MemberID { get; set; }
    [Column("NotificationStatus")]
    public bool NotificationStatus { get; set; }
  }
}
