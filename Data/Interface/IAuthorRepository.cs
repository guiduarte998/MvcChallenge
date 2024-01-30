using Models;

namespace Data.Interface
{
    public interface IAuthorRepository
    {
        Author Add(Author author);
        Author GetAuthorById(Guid id);
        IEnumerable<Author> GetAll();
        void AddAuthor(Author author);
        void UpdateAuthor(Author author);
        void DeleteAuthor(Guid id);
    }
}