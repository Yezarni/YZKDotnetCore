using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YZKDotnetCore.ConsoleApp.Dos;

namespace YZKDotnetCore.ConsoleApp.EfcoreExamples
{
    internal class EfcoreExample
    {
        private readonly AppDbContext db = new AppDbContext();
        public void Run()
        {
            Read();
            Edit(1);
            Edit(11);
            // Create("myTitle", "myAuthor", "myContent");
            // Update (2003, "Title2", "Author2", "Content2");
            Delete(2003);
        }

        private void Read()
        {

            var lst = db.Blog.ToList();
            foreach (BlogDto item in lst)
            {
                Console.WriteLine(item.BlogId);
                Console.WriteLine(item.BlogTitle);
                Console.WriteLine(item.BlogAuthor);
                Console.WriteLine(item.BlogContent);
                Console.WriteLine("----------------------------------");
            }
        }

        private void Edit(int id)
        {
            var item = db.Blog.FirstOrDefault(x => x.BlogId == id);
            if (item == null)
            {
                Console.WriteLine("no data found");
                return;
            }
            Console.WriteLine(item.BlogId);
            Console.WriteLine(item.BlogTitle);
            Console.WriteLine(item.BlogAuthor);
            Console.WriteLine(item.BlogContent);

        }
        private void Create(string title, string author, string content)
        {
            var item = new BlogDto
            {
                BlogTitle = title,
                BlogAuthor = author,
                BlogContent = content,
            };
            db.Blog.Add(item);
            int result = db.SaveChanges();
            string message = result > 0 ? "saving successful." : "saving failed.";
            Console.WriteLine(message);
        }

        private void Update(int id, string title, string author, string content)
        {
            var item = db.Blog.AsNoTracking().FirstOrDefault(x => x.BlogId == id);
            if (item == null)
            {
                Console.WriteLine("no data found");
                return;
            }
            item.BlogTitle = title;
            item.BlogAuthor = author;
            item.BlogContent = content;
            int result = db.SaveChanges();

            string message = result > 0 ? "updating successful." : "updating failed.";
            Console.WriteLine(message);
        }

        private void Delete(int id)
        {
            var item = db.Blog.FirstOrDefault(x => x.BlogId == id);
            if (item == null)
            {
                Console.WriteLine("no data found");
                return;
            }
            db.Blog.Remove(item);
            int result = db.SaveChanges();
            string message = result > 0 ? "delecting successful." : "delecting failed.";
            Console.WriteLine(message);
        }
    }
}
