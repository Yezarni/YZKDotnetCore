using System.Security.AccessControl;
using System.Security.Cryptography.Xml;
using System.Xml.Linq;

namespace Yzk.RestApiwithNlayer.Features.Blog
{
    public class BlogDataAccess
    {
        //Data Access
        private readonly AppDbcontext _context;
        public BlogDataAccess()
        {
            _context = new AppDbcontext();
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

        public int DeleteBlog(int id)
        {
            var item = _context.Blog.FirstOrDefault(x => x.BlogId == id);
            if (item == null) return 0;

          _context.Blog.remove(item);

            var result = _context.SaveChanges();
            return result;
        }
    }
}
