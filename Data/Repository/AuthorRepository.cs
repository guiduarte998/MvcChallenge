using Models;
using Data.Context;
using Data.Interface;

namespace Data.Repository
{
    public class AuthorRepository : IAuthorRepository
    {
        private readonly ChallengeContext _context;


        public AuthorRepository(ChallengeContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public Author Add(Author author)
        {
            return _context.Authors.Add(author).Entity;
        }

        public IEnumerable<Author> GetAll()
        {
            return _context.Authors.ToList();
        }

        public Author GetAuthorById(Guid id)
        {
            return _context.Authors.FirstOrDefault(a => a.Id == id);
        }

        public void AddAuthor(Author author)
        {
            author.Id = Guid.NewGuid();
            _context.Authors.Add(author);
            _context.SaveChanges();
        }

        public void UpdateAuthor(Author author)
        {
            var existingAuthor = _context.Authors.FirstOrDefault(a => a.Id == author.Id);
            if (existingAuthor != null)
            {
                existingAuthor.FirstName = author.FirstName;
                existingAuthor.LastName = author.LastName;
                existingAuthor.Email = author.Email;
                existingAuthor.Phone = author.Phone;
                existingAuthor.Address1 = author.Address1;
                existingAuthor.Address2 = author.Address2;
                existingAuthor.City = author.City;
                existingAuthor.State = author.State;
                existingAuthor.Zip = author.Zip;
                existingAuthor.Country = author.Country;
            }
            _context.Authors.Update(existingAuthor);
            _context.SaveChanges();
        }

        public void DeleteAuthor(Guid id)
        {
            var authorToDelete = _context.Authors.FirstOrDefault(a => a.Id == id);
            if (authorToDelete != null)
            {
                _context.Authors.Remove(authorToDelete);
                _context.SaveChanges();
            }
        }
    }
}

