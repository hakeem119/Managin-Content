namespace ManagingContent.Models
{
    public class Content
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string FileUrl { get; set; } 
        public bool IsPublished { get; set; }
        public DateTime UploadDate { get; set; }
        public int TeacherId { get; set; }
        public Teacher Teacher { get; set; }
    }
}
