using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Interface
{
    public interface IBookRepository
    {
        Book Add(Book book);
        Book GetBookById(Guid id);
        IEnumerable<Book> GetAll();
        void AddBook(Book book);
        void UpdateBook(Book book);
        void DeleteBook(Guid id);
    }
}
