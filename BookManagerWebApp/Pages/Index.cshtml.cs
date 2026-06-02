using BookManagerModels;
using BookManagerRepository;
using BookManagerService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BookManagerWebApp.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IBookRepository _bookRepository;
        private readonly IBookService _index;

        // 透過 DI 取得 Repository
        public IndexModel(IBookRepository bookRepository, IBookService index)
        {
            _bookRepository = bookRepository;
            _index = index;
        }
        // IEnumerable 為「資料結構中的資料，是否可被列舉」
        // .Net 對「是否可被列舉」這件事的定義上，就是認定是否具備取得「列舉器」(Enumerator) 的能力
        // 可以將 Enumerator 視為是一個巡覽器，負責將它所附屬的資料集合中的元素，一個一個的取出並回傳
        //1. 要讓自定義的資料型別，具備「可被列舉」的能力，需要實作 IEnumerable 或是 IEnumerable<T> 界面
        //2. 在 IEnumerable 與 IEnumerable<T> 界面中，定義了 GetEnumerator() 方法，回傳列舉器 (IEnumerator) 物件
        //3. 列舉器是真正實作，巡訪各個資料元素的主體物件
        //4. IEnumerable<T> 是 IEnumerable 的泛型版本，提供了更強的型別安全性和更好的性能
        //參考:https://vegeee-csharp.blogspot.com/2017/02/c-ienumerableienumerator-ienumerable.html
        public IEnumerable<Book> Books { get; set; } = new List<Book>();

        [BindProperty(SupportsGet = true)] //讓Keyword可以從網址傳入
        public string? SearchKeyword { get; set; }

        [BindProperty] //檢查書本借閱狀態
        public string? Status { get; set; }


        public async Task OnGetAsync()
        {
            if (!string.IsNullOrEmpty(SearchKeyword))
            {
                // 從 Repository 取得書 (有SearchKeyword)
                Books = await _index.GetwithKeyword(SearchKeyword);
            }
            else
            {
                // 從 Repository 取得全部書籍
                Books = await _index.GetwithoutKeyword();

                // 在資料庫沒有對應書籍的情況下，才新增一筆測試資料
                Books = await _index.IsBookExist();
            }
        }


        // 對應前端 asp-page-handler="Delete" 的 POST 請求
        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            await _bookRepository.DeleteBookAsync(id);
            return RedirectToPage();
        }
    }


}
