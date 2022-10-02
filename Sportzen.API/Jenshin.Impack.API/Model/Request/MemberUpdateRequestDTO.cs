using Binus.WS.Pattern.Model;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Sportzen.API.Model.Request
{
    public class MemberUpdateRequestDTO
    {
        public int MemberID { get; set; }
        public string MemberName { get; set; }
        public string MemberPhoneNumber { get; set; }
        public string MemberEmail { get; set; }
        public string MemberPassword { get; set; }
        public string MemberAddress { get; set; }
        public long MemberBankAccount { get; set; }
        public string MemberRankStatus { get; set; }
        public string MemberPhoto { get; set; }
        public int EWalletBalance { get; set; }
        public IFormFile files { get; set; }
    }
}
