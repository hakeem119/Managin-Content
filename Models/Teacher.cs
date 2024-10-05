namespace ManagingContent.Models
{
    public class Teacher
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Content> Contents { get; set; }
    }
}
