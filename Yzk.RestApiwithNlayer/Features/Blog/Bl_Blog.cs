using Microsoft.EntityFrameworkCore;

namespace Yzk.RestApiwithNlayer.Features.Blog
{
    public class Bl_Blog
    {
        private readonly BlogDataAccess _access;

        public Bl_Blog() 
        { 
             _access = new BlogDataAccess();
        }

        public List<BlogModel> GetBlogs()
        {
            var lst = _access.GetBlog();
            return lst;
        }

        public BlogModel GetBlog(int id)
        { 
            var item = _access.GetBlog(id);
            return item;
        }

        public int CreateBlog(BlogModel requestModel)
        {
            var result = _access.CreateBlog(requestModel);
            return result;
        }

        public int UpdateBlog(int id, BlogModel requestModel)
        {
            var result = _access.UpdateBlog(id, requestModel);
            return result;
        }

        public int PatchBlog(int id, BlogModel requestModel)
        {
            var result = _access.PatchBlog(id, requestModel);
            return result;
        }



        public int DeleteBlog(int id)
        {
            var result = _access.DeleteBlog(id);
            return result;
        }
    }
}
