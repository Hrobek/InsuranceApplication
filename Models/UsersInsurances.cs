namespace InsureApp.Models
{
	public class UsersInsurances
	{
		public int Id { get; set; }
		public int UserId { get; set; }
		public Users User { get; set; }
		public int InsuranceId { get; set; }
		public Insurances Insurance { get; set; }
		public decimal Price { get; set; }
		public DateTime ValidUntil { get; set; }
	}
}
