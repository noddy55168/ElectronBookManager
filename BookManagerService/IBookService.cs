using BookManagerModels;

namespace BookManagerService
{
    public interface IBookService
    {
        Task<IEnumerable<Book>> GetwithKeyword(string keyWord);
        Task<IEnumerable<Book>> GetwithoutKeyword();
        Task<IEnumerable<Book>> IsBookExist();
        Task<Book?> GetBookAsync(int id);
        Task<IEnumerable<Book>> UpsertBookAsync(Book book);
        Task<IEnumerable<Book>> DeleteBookAsync(int id);

    }
}
