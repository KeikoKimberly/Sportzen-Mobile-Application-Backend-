using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Binus.WS.Pattern.Output;

namespace Sportzen.API.Output
{
  public class MemberOutput : OutputBase
  {
    public MemberOutput()
    {
      this.Success = 0;
      this.Data = new SpecificMember();
    }

    public int Success { get; set; }
    public SpecificMember Data { get; set; }
  }

  public class SpesificMemberOutput : OutputBase
  {

    public SpesificMemberOutput()
    {
      this.Data = new SpecificMember();
    }
    public SpecificMember Data { get; set; }

  }

  public class SpecificMember
  {
    public int MemberID { get; set; }
    public string MemberName { get; set; }
    public string MemberPhoneNumber { get; set; }
    public string MemberEmail { get; set; }
    public string MemberPassword { get; set; }
    public string MemberAddress { get; set; }
    public string MemberBankAccount { get; set; }
    public string MemberPhoto { get; set; }
    public int EWalletBalance { get; set; }
    public int TotalMemberBooking { get; set; }
  }
}



