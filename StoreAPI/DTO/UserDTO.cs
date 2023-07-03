namespace StoreAPI.DTO
{
    public class UserDTO
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName
        {
            get { return FirstName + " " + LastName; }
        }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
    }
}
