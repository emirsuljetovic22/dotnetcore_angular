using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities
{
    public class Job
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ShortDescription { get; set; }
        public string FullDescription { get; set; }
        public string CompanyName { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }
        public bool ApplyOnWebsite { get; set; }
        public string EmailAddress { get; set; }
        public string PhoneNumber { get; set; }
        public bool Recommended { get; set; }
        public string CreatedBy { get; set; }
        public ICollection<JobCategory> Category { get; set; }
    }
}
