using Data.Context;
using Data.Interface;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Models;
using System.Security.Cryptography;

namespace Data.Repository
{
    public class UserRepository: IUserRepository
    {
        private readonly ChallengeContext _context;


        public UserRepository(ChallengeContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public bool UserExists(string email)
        {
            return _context.User.Any(u => u.Email == email);
        }

        public void AddUser(User user)
        {
            byte[] salt = RandomNumberGenerator.GetBytes(128 / 8); 

            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: user.PasswordHash!,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 100000,
                numBytesRequested: 256 / 8));

            // Combine the salt and password
            string saltPlusHash = Convert.ToBase64String(salt) + ":" + hashed;
            user.PasswordHash = saltPlusHash;

            _context.User.Add(user);
            _context.SaveChanges();
        }

        public User GetUserByEmailAndPassword(string email, string password)
        {
            var user = _context.User.FirstOrDefault(u => u.Email == email);
            if (user == null) return null;

            // Extract salt and hash from stored password
            var parts = user.PasswordHash.Split(':');
            if (parts.Length != 2) return null; // Invalid format

            var salt = Convert.FromBase64String(parts[0]);
            var storedHash = parts[1];

            // Hash the input password with the extracted salt
            string hashedInputPassword = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 100000,
                numBytesRequested: 256 / 8));

            // Compare the hashed input password with the stored hash
            if (hashedInputPassword == storedHash)
            {
                return user;
            }

            return null;
        }
    }
}
