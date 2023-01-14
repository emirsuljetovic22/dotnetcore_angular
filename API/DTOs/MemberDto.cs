namespace API.DTOs
{
    public class MemberDto
    {
        public int Id { get; set; }
        public string? PhotoUrl { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public int? Age { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? LastActive { get; set; }
        public string? About { get; set; }
        public string? City { get; set; }
        public string? Region { get; set; }
        public string? Country { get; set; }
        public ICollection<PhotoDto>? Photos { get; set; }
    }
}

