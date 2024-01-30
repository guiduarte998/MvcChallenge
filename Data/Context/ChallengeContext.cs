using Microsoft.EntityFrameworkCore;
using Models;

namespace Data.Context
{
    public class ChallengeContext : DbContext
    {

        public ChallengeContext(DbContextOptions<ChallengeContext> options)
        : base(options)
        {
        }

        //entities
        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<User> User { get; set; }

    }
}