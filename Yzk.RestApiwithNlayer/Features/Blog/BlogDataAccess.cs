using System;
using System.Security.AccessControl;
using System.Security.Cryptography.Xml;
using System.Xml.Linq;
using Yzk.RestApiwithNlayer.Db;

namespace Yzk.RestApiwithNlayer.Features.Blog
{
    public class BlogDataAccess
    {
        //Data Access
        private readonly AppDbContext _context;
        public BlogDataAccess()
        {
            _context = new AppDbContext();
        }
        public List<BlogModel> GetBlog()
        {
           var lst = _context.Blog.ToList();
            return lst;
        }
        public BlogModel GetBlog(int id)
        {
            var item = _context.Blog.FirstOrDefault(x=>x.BlogId ==id);
            return item;
        }
        public int CreateBlog(BlogModel requestModel)
        {
             _context.Blog.Add(requestModel);
            var result = _context.SaveChanges();
            return result;
        }
        public int UpdateBlog(int id, BlogModel requestModel)
        {
            var item = _context.Blog.FirstOrDefault(x => x.BlogId == id);
            if (item == null) return 0;

            item.BlogTitle = requestModel.BlogTitle;
            item.BlogAuthor = requestModel.BlogAuthor;
            item.BlogContent = requestModel.BlogContent;

            var result = _context.SaveChanges();
            return result;
        }

        public int PatchBlog(int id, BlogModel requestModel)
        {
            var item = _context.Blog.FirstOrDefault(x => x.BlogId == id);
            if (item is null) return 0;

            if (!String.IsNullOrEmpty(requestModel.BlogTitle)) item.BlogTitle = requestModel.BlogTitle;
            if (!String.IsNullOrEmpty(requestModel.BlogAuthor)) item.BlogAuthor = requestModel.BlogAuthor;
            if (!String.IsNullOrEmpty(requestModel.BlogContent)) item.BlogContent = requestModel.BlogContent;

            var result = _context.SaveChanges();
            return result;
        }

        public int DeleteBlog(int id)
        {
            var item = _context.Blog.FirstOrDefault(x => x.BlogId == id);
            if (item == null) return 0;

          _context.Blog.Remove(item);

            var result = _context.SaveChanges();
            return result;
        }
    }
}
