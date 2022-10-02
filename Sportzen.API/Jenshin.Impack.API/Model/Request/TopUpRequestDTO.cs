namespace Sportzen.API.Model.Request
{
  public class TopUpRequestDTO
  {
    public int MemberID { get; set; }
    public int Nominal { get; set; }
    public string TopUpMethod { get; set; }
  }
}
