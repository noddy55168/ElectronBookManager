using BookManagerModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookManagerRepository
{
    public class BookRepository : IBookRepository
    {
        private readonly BookManagerDbContext _context;
        public BookRepository(BookManagerDbContext context) //建構式注入
        {
            //初始化資料庫
            //確保資料庫已創建
            _context = context;
            _context.Database.EnsureCreated();
        }

        /// <summary>
        /// 用ID取單本書
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<Book?> GetBookAsync(int id) => _context.Books.FirstOrDefaultAsync(b => b.id == id);
        /// <summary>
        /// 取所有書籍
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Book>> GetBooksAsync() => await _context.Books.ToListAsync();

        /// <summary>
        /// 關鍵字搜尋書籍
        /// </summary>
        /// <param name="keyWord"></param>
        /// <returns></returns>
        public async Task<IEnumerable<Book>> SearchBooksAsync(string keyWord)
        {
            return await _context.Books
            .Where(b => b.Name.Contains(keyWord)
                || b.Publishing.Contains(keyWord)
                || b.ISBN.Contains(keyWord)
                || b.Author.Contains(keyWord)
                || b.Tag.Contains(keyWord)
                || b.Borrower.Contains(keyWord)
            )
            .ToListAsync();
        }

        /// <summary>
        /// 新增或更新書籍
        /// </summary>
        /// <param name="book"></param>
        /// <returns></returns>
        public async Task UpsertBookAsync(Book book)
        {
            Book? existingBook = await _context.Books.FirstOrDefaultAsync(b => b.id == book.id);

            if (existingBook != null)
            {
                // 更新現有書籍
                existingBook.Name = book.Name;
                existingBook.Publishing = book.Publishing;
                existingBook.ISBN = book.ISBN;
                existingBook.Author = book.Author;
                existingBook.Tag = book.Tag;

                existingBook.IsBorrowed = book.IsBorrowed;
                existingBook.Borrower = book.Borrower;
                existingBook.BorrowedTime = book.BorrowedTime;
                existingBook.ShouldReturnTime = book.ShouldReturnTime;
                existingBook.LastReturnTime = book.LastReturnTime;
            }
            else
            {
                // 新增新書籍
                _context.Books.Add(book);
            }
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// 批次新增書籍
        /// </summary>
        /// <param name="books"></param>
        /// <returns></returns>
        public async Task AddMultipleBooksAsync(IEnumerable<Book> books)
        {
            foreach (var book in books)
            {
                // 將 id 設為 0
                book.id = 0;
                _context.Books.Add(book);
            }
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// 刪除書籍
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task DeleteBookAsync(int id)
        {
            Book? bookToDelete = await _context.Books.FirstOrDefaultAsync(b => b.id == id);
            if (bookToDelete != null)
            {
                _context.Books.Remove(bookToDelete);
                await _context.SaveChangesAsync();
            }
        }
    }

}
