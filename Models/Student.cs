namespace ManagingContent.Models
{
    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Course { get; set; }
        public string PaymentProof { get; set; } 
        public bool IsVerified { get; set; }
    }
}
