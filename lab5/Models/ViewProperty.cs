namespace lab5.Models
{
    public class ViewProperty
    {
        public int Id { get; set; }
        public Guid PublicToken { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateOnly Birthday { get; set; }
        public List<string> Nationalities { get; set; } = new List<string>();
        public string Gender { get; set; }
        public List<string> Skills { get; set; } = new List<string>();
        public string Email { get; set; }
        public string photoPath { get; set; }
        
    }
}
