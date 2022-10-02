using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sportzen.API.Model.Request
{
    public class BookingAvailibilityRequestDTO
    {
        public int CourtID { get; set; }
        public DateTime BookingDateTimeStart { get; set; }
        public DateTime BookingDateTimeEnd { get; set; }    
    }
}
