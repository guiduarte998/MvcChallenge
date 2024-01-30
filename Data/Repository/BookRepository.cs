using Data.Context;
using Data.Interface;
using Models;
using static System.Reflection.Metadata.BlobBuilder;


namespace Data.Repository
{
    public class BookRepository : IBookRepository
    {
        private readonly ChallengeContext _context;


        public BookRepository(ChallengeContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public Book Add(Book book)
        {
            return _context.Books.Add(book).Entity;
        }

        public IEnumerable<Book> GetAll()
        {
            return _context.Books.ToList();
        }

        public Book GetBookById(Guid id)
        {
            return _context.Books.FirstOrDefault(a => a.Id == id);
        }

        public void AddBook(Book book)
        {
            book.Id = Guid.NewGuid();
            _context.Books.Add(book);
            _context.SaveChanges();
        }

        public void UpdateBook(Book book)
        {
            var existingBook = _context.Books.FirstOrDefault(a => a.Id == book.Id);
            if (existingBook != null)
            {
                existingBook.Title = book.Title;
                existingBook.Description = book.Description;
                existingBook.ISBN = book.ISBN;
                existingBook.Publisher = book.Publisher;
                existingBook.Language = book.Language;
                existingBook.Price = book.Price;
            }
            _context.Books.Update(existingBook);
            _context.SaveChanges();
        }

        public void DeleteBook(Guid id)
        {
            var bookToDelete = _context.Books.FirstOrDefault(a => a.Id == id);
            if (bookToDelete != null)
            {
                _context.Books.Remove(bookToDelete);
                _context.SaveChanges();
            }
        }
    }
}

