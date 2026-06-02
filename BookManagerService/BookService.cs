using BookManagerModels;
using BookManagerRepository;
using System.Text.Json;



namespace BookManagerService
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;
        // IEnumerable<Book> 是一個泛型介面，表示一個可列舉的集合，這裡用來存放從 Repository 取得的書籍資料
        public IEnumerable<Book> Books { get; set; } = new List<Book>();
        public BookService(IBookRepository bookRepository) //建構式注入
        {
            _bookRepository = bookRepository;
        }

        public async Task<IEnumerable<Book>> GetwithKeyword(string SearchKeyword)
        {
            Books = await _bookRepository.SearchBooksAsync(SearchKeyword);
            return Books;
        }

        public async Task<IEnumerable<Book>> GetwithoutKeyword()
        {
            Books = await _bookRepository.GetBooksAsync();
            return Books;
        }

        /// <summary>
        /// 檢測書本資料 沒有書 則插入假資料
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Book>> IsBookExist()
        {

            Books = await _bookRepository.GetBooksAsync();

            if (Books == null || !Books.Any())
            {
                var filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "booksdataExample.json");

                if (File.Exists(filePath))
                {
                    var jsonText = await File.ReadAllTextAsync(filePath);
                    var theNewBooks = JsonSerializer.Deserialize<List<Book>>(jsonText);

                    if (theNewBooks != null)
                    {
                        // 呼叫批次寫入
                        await _bookRepository.AddMultipleBooksAsync(theNewBooks);
                        Books = await _bookRepository.GetBooksAsync();
                    }
                }
            }

            return Books;
        }

        public async Task<Book?> GetBookAsync(int id)
        {
            return await _bookRepository.GetBookAsync(id);
        }

        public async Task<IEnumerable<Book>> UpsertBookAsync(Book book)
        {
            await _bookRepository.UpsertBookAsync(book);
            return await _bookRepository.GetBooksAsync();
        }
        public async Task<IEnumerable<Book>> DeleteBookAsync(int id)
        {
            await _bookRepository.DeleteBookAsync(id);
            return await _bookRepository.GetBooksAsync();
        }
    }
}
