namespace Worktastic.Models
{
    public class JobPosting
    {
        public int Id { get; set; }

        public required string JobTitle { get; set; }

        public required string JobLocation { get; set; }

        public required string JobDescription { get; set; }

        public required int Salary { get; set; }

        public required DateTime StartDate { get; set; }

        public required string CompanyName { get; set; }

        public required string ContactPhone { get; set; }

        public required string ContactMail { get; set; }

        public required string ContactWebsite { get; set; }

        public required byte[] CompanyImage { get; set; }

        public required string OwnerUserName {get;set;}
    }
}