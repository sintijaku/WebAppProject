
namespace Project.MODEL.Entities
{
    public class AppUser
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool IsActive { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public AppUser()
        {
            IsActive = true;
        }
    }
}

