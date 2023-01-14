namespace API.Entities
{
    public class InfoCategory
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public ICollection<Information> Info { get; set; }
    }
}
