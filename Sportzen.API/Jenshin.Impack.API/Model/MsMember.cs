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
  [Table("MsMember")]
  public class MsMember : BaseModel
  {
    [Column("MemberID")]
    [Key]
    public int MemberID { get; set; }
    [Column("MemberName")]
    public string MemberName { get; set; }
    [Column("MemberPhoneNumber")]
    public string MemberPhoneNumber { get; set; }
    [Column("MemberEmail")]
    public string MemberEmail { get; set; }
    [Column("MemberPassword")]
    public string MemberPassword { get; set; }
    [Column("MemberAddress")]
    public string MemberAddress { get; set; }
    [Column("MemberBankAccount")]
    public string MemberBankAccount { get; set; }
    [Column("MemberPhoto")]
    public string MemberPhoto { get; set; }
    [Column("EWalletBalance")]
    public int EWalletBalance { get; set; }

  }
}
