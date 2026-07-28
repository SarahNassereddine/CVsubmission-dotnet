namespace lab5.Models
{
    public class CV
    {
        public int Id { set; get; }
        public string FirstName { set; get; }
        public string LastName { set; get; }

        public DateOnly Birthday { set; get; }
        public List<string> Nationalities { set; get; }
        public string Gender { set; get; }
        public List<string> Skills { set; get; }

        public string Email { set; get; }
        public string Password { set; get; }
        public string PhotoPath { set; get; }
        
    }
}
