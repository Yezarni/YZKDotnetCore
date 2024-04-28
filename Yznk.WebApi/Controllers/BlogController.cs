using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata;
using Yznk.WebApi.Db;
using Yznk.WebApi.Model;

namespace Yznk.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogController : ControllerBase
    {
        private readonly AppDbContext _context;

        public BlogController()
        {
            _context = new AppDbContext();
        }
        [HttpGet]
        public IActionResult Read()
        {
            var lst = _context.Blog.ToList();
            return Ok(lst);
        }
        [HttpGet("{id}")]
        public IActionResult Edit(int id)
        {
            var item = _context.Blog.FirstOrDefault(x=> x.BlogId ==id);
            if(item is null)
            {
                return NotFound("no data found");
            }
            return Ok(item);
        }
       
        [HttpPost]
        public IActionResult Create(BlogModel Blog)
        {
            _context.Blog.Add(Blog);
            var result =_context.SaveChanges();
            string message = result > 0 ? "saving successful." : "saving failed.";
            return Ok(message);
        }
        [HttpPut("{id}")]
        public IActionResult Update(int id, BlogModel Blog)
        {
            var item = _context.Blog.FirstOrDefault(x => x.BlogId == id);
            if (item is null)
            {

                return NotFound("no data found");
            }
            item.BlogTitle = Blog.BlogTitle;
            item.BlogAuthor = Blog.BlogAuthor;
            item.BlogContent = Blog.BlogContent;
            var result = _context.SaveChanges();
            string message = result > 0 ? "updating successful." : "updating failed.";
            return Ok(message);
        }
        [HttpPatch]
        public IActionResult Patch(int id, BlogModel Blog)
        {
            var item = _context.Blog.FirstOrDefault(x => x.BlogId == id);
            if (item is null)
            {

                return NotFound("no data found");
            }
            if(string.IsNullOrEmpty(item.BlogTitle))
            {
                item.BlogTitle = Blog.BlogTitle;
            }
            if (string.IsNullOrEmpty(item.BlogAuthor))
            {
                item.BlogAuthor = Blog.BlogAuthor;
                    }
            if (!string.IsNullOrEmpty(item.BlogContent))
            { item.BlogContent = Blog.BlogContent;
            }
            
            
            var result = _context.SaveChanges();
            string message = result > 0 ? "updating successful." : "updating failed.";
            return Ok(message);
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var item = _context.Blog.FirstOrDefault(x => x.BlogId == id);
            if (item is null)
            {

                return NotFound("no data found");
            }
            _context.Blog.Remove(item);
            var result = _context.SaveChanges();
            string message = result > 0 ? "deleting successful." : "deleting failed.";
            return Ok(message);
            
        }
    }
}
