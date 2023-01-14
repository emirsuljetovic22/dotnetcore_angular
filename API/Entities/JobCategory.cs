namespace API.Entities
{
    public class JobCategory
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public ICollection<Job> Job { get; set; }
    }
}
