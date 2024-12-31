namespace affin_objects
{
    public class User : Prospect
    {
        public int? UserId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public int? ProspectId { get; set; }
        public string? RegisterDate { get; set; }
    }
}
