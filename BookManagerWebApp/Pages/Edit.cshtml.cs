using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BookManagerModels;
using BookManagerRepository;
using System.Security.Cryptography.X509Certificates;

namespace BookManagerWebApp.Pages
{
    public class EditModel : PageModel
    {
        private readonly IBookRepository _bookRepository;
        public EditModel(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }
        [BindProperty]
        public Book Book { get; set; } = new Book();

        public async Task<IActionResult> OnGetAsync(int id)
        {
            // 從 Repository 取得要編輯的書籍資料
            var getBook = await _bookRepository.GetBookAsync(id);
            if (getBook == null)
            {
                return NotFound();
            }
            Book = getBook;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            // 檢查資料是否有效
            if (!ModelState.IsValid)
            {
                return Page();
            }

            //更新書籍
            await _bookRepository.UpsertBookAsync(Book);
            return RedirectToPage("./Index");
        }
    }
}
