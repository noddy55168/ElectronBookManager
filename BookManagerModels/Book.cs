using System;
using System.Collections.Generic;
using System.Text;

namespace BookManagerModels
{
    /// <summary>
    /// 定義書籍模型，包含書籍的基本信息和借閱狀態
    /// </summary>
    public class Book
    {
        public int id { get; set; }
        //書名
        public string Name { get; set; }

        //出版社
        public string Publishing {  get; set; }
        public string ISBN { get; set; }
        public string Author { get; set; }
        //作品tag 方便分類和搜尋
        public string? Tag { get; set; }

        //借閱狀態
        public bool IsBorrowed { get; set; }
        //借閱者
        public string? Borrower { get; set; }
        //借閱時間
        public DateTime? BorrowedTime { get; set; }
        //應歸還時間
        public DateTime? ShouldReturnTime { get; set; }
        //最後歸還時間
        public DateTime? LastReturnTime { get; set; }

    }
}
