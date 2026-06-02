using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using BookManagerModels; // 確保能讀取到 Book 類別

namespace BookManagerRepository
{
    /// <summary>
    /// Sqlite的配置 Repository或Service都會需要DbContext(建構式注入)
    /// </summary>
    public class BookManagerDbContext : DbContext
    {
        public DbSet<Book> Books { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // 使用 SQLite 資料庫，資料庫檔案為 bookmanager.db
            optionsBuilder.UseSqlite("Data Source=bookmanager.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // 在這裡進行Model的配置，例如設定主鍵、索引等
            modelBuilder.Entity<Book>().HasKey(b => b.id); // 設定 id 為主鍵
        }
    }
}
