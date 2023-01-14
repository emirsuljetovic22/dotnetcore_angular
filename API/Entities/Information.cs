using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities
{
    public class Information
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Url { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
        public string ImageUrl { get; set; }
        public ICollection<InfoCategory> Category { get; set; }
    }
}
