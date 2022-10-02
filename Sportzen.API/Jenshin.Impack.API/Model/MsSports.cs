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
    [Table("MsSports")]
    public class MsSports : BaseModel
    {
        [Column("SportsID")]
        [Key]
        public int SportsID { get; set; }
        [Column("SportsCategory")]
        public string SportsCategory { get; set; }
       
    }
}
