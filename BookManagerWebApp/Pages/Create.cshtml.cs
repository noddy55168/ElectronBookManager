using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BookManagerModels;
using BookManagerRepository;

namespace BookManagerWebApp.Pages
{
    public class CreateModel : PageModel
    {
        private readonly IBookRepository _bookRepository;
        public CreateModel(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        // BindProperty 會將model綁定到 Book 屬性上，在 OnPostAsync 方法中可以直接使用 Book 來存取資料
        // ref:https://learn.microsoft.com/zh-tw/aspnet/core/mvc/models/model-binding?view=aspnetcore-10.0#targets
        [BindProperty]
        public Book Book { get; set; } = new Book();
        public void OnGet()
        {
            // 初始化頁面的邏輯
        }
        public async Task<IActionResult> OnPostAsync()
        {
            // 檢查資料是否有效
            if (!ModelState.IsValid)
            {
                return Page();
            }
            //創建書籍
            await _bookRepository.UpsertBookAsync(Book);
            return RedirectToPage("./Index");
        }
    }
}
