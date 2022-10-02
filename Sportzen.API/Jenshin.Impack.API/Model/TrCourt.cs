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
  [Table("TrCourt")]
  public class TrCourt : BaseModel
  {
    [Column("CourtID")]
    [Key]
    public int CourtID { get; set; }
    [Column("SportsID")]
    public int SportsID { get; set; }
    [Column("CourtName")]
    public string CourtName { get; set; }
    [Column("CourtLocation")]
    public string CourtLocation { get; set; }
    [Column("CourtQuantity")]
    public int CourtQuantity { get; set; }
    [Column("MaxCapacity")]
    public int MaxCapacity { get; set; }
    [Column("CourtDescription")]
    public string CourtDescription { get; set; }
    [Column("CourtPhoto")]
    public string CourtPhoto { get; set; }
    [Column("CourtPrice")]
    public int CourtPrice { get; set; }
  }
}
