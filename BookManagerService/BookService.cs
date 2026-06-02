using BookManagerModels;
using BookManagerRepository;



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
        /// 測試資料用 預設插入一筆假資料
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Book>> IsBookExist()
        {
            if (Books == null || !Books.Any())
            {
                await _bookRepository.UpsertBookAsync(new Book
                {
                    Name = "測試書本",
                    Publishing = "測試出版社",
                    ISBN = "978-986-320-123-4",
                    Author = "測試作家",
                    IsBorrowed = false,
                    Borrower = null,
                    BorrowedTime = null,
                    ShouldReturnTime = null,
                    LastReturnTime = null
                });

                Books = await _bookRepository.GetBooksAsync();
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
