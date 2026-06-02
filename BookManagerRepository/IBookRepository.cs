using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BookManagerModels;

namespace BookManagerRepository
{
    public interface IBookRepository
    {
        /// <summary>
        /// 取得所有書籍
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<Book>> GetBooksAsync();
        /// <summary>
        /// 用ID取單本書籍
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Book?> GetBookAsync(int id);
        /// <summary>
        /// 用關鍵字搜尋書籍
        /// </summary>
        /// <param name="keyWord"></param>
        /// <returns></returns>
        Task<IEnumerable<Book>> SearchBooksAsync(string keyWord);
        /// <summary>
        /// 新增或更新書籍
        /// </summary>
        /// <param name="book"></param>
        /// <returns></returns>
        Task UpsertBookAsync(Book book);
        /// <summary>
        /// 刪除書籍
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task DeleteBookAsync(int id);

    }
}
