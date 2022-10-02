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
  [Table("TrProduct")]
  public class TrProduct : BaseModel
  {
    [Column("ProductID")]
    [Key]
    public int ProductID { get; set; }
    [Column("SportsID")]
    public int SportsID { get; set; }
    [Column("ProductName")]
    public string ProductName { get; set; }
    [Column("ProductStock")]
    public int ProductStock { get; set; }
    [Column("ProductDescription")]
    public string ProductDescription { get; set; }
    [Column("ProductPhoto")]
    public string ProductPhoto { get; set; }
    [Column("ProductPrice")]
    public int ProductPrice { get; set; }
  }
}
