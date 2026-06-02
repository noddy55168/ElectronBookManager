using BookManagerRepository;
using BookManagerService;
using ElectronNET.API;
using ElectronNET.API.Entities;


namespace BookManagerWebApp
{
    public class Program
    {
        /// <summary>
        /// 配置builder 註冊服務(進容器) 建置builder
        /// 配置HTTP請求管道 檢查環境~~啟動應用程式
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string[] args)
        {
            // Create a builder for the web application
            // 用系統預先設置好的設定來建立一個 WebApplication 的實例
            var builder = WebApplication.CreateBuilder(args);


            // Add services to the container.
            // 註冊 Razor Pages 服務進 "容器"
            // 應用程式就可以使用 Razor Pages 功能來處理 HTTP 請求和生成 HTML 頁面
            builder.Services.AddRazorPages();
            builder.UseElectron(args, ElectronAppReady);


            // Add database context and repository services(手動添加)
            // 將 BookManagerDbContext 類別註冊進 "容器"
            // 這樣應用程式就可以使用依賴注入來獲取 BookManagerDbContext 的實例
            builder.Services.AddDbContext<BookManagerDbContext>();
            // 將 IBookRepository 介面和 BookRepository 類別註冊進 "容器"
            builder.Services.AddScoped<IBookRepository, BookRepository>();
            // 註冊 IndexService
            builder.Services.AddScoped<IBookService, BookService>();

            //實例化 WebHost，並得到該實例
            var app = builder.Build();


            // Configure the HTTP request pipeline.
            // 配置 HTTP 請求管道
            // 檢查是否為Development環境 如果不是 則報錯
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
            }

            // 讓靜態檔案 (wwwroot) 可由 Electron/瀏覽器存取
            app.UseStaticFiles();

            // 啟用路由匹配
            app.UseRouting();

            // 啟用授權檢查
            app.UseAuthorization();

            // 映射頁面路徑
            app.MapRazorPages();

            // 啟動應用程式，開始監聽 HTTP 請求
            app.Run();
        }

        public static async Task ElectronAppReady()
        {
            var browserWindow = await Electron.WindowManager.CreateWindowAsync(new BrowserWindowOptions { Show = false });

            browserWindow.OnReadyToShow += () => browserWindow.Show();
        }


    }
}
