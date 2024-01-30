using Models;


namespace Data.Interface
{
    public interface IUserRepository
    {
        bool UserExists(string email);
        void AddUser(User user);
        User GetUserByEmailAndPassword(string email, string password);
    }
}
