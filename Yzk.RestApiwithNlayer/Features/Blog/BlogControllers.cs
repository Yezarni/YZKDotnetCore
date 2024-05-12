﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Yzk.RestApiwithNlayer.Features.Blog
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogControllers : ControllerBase
    {
        private readonly Bl_Blog _blog;

        public BlogControllers(Bl_Blog blog)
        {
            _blog =new Bl_Blog();
        }

        [HttpGet]
        public IActionResult Read()
        {
            var lst = _blog.GetBlogs();
            return Ok(lst);
        }
        [HttpGet("{id}")]
        public IActionResult Edit(int id)
        {
            var item = _blog.GetBlog(id);
            if (item is null)
            {
                return NotFound("no data found");
            }
            return Ok(item);
        }

        [HttpPost]
        public IActionResult Create(BlogModel Blog)
        {
            var result = _blog.CreateBlog(Blog);
            string message = result > 0 ? "saving successful." : "saving failed.";
            return Ok(message);
        }
        [HttpPut("{id}")]
        public IActionResult Update(int id, BlogModel Blog)
        {
            var item = _blog.GetBlog(id);
            if (item is null)
            {

                return NotFound("no data found");
            }

            var result = _blog.UpdateBlog(id , Blog);
            string message = result > 0 ? "updating successful." : "updating failed.";
            return Ok(message);
        }
        //[HttpPatch]
        //public IActionResult Patch(int id, BlogModel Blog)
        //{
        //    var item = _context.Blog.FirstOrDefault(x => x.BlogId == id);
        //    if (item is null)
        //    {

        //        return NotFound("no data found");
        //    }
        //    if (string.IsNullOrEmpty(item.BlogTitle))
        //    {
        //        item.BlogTitle = Blog.BlogTitle;
        //    }
        //    if (string.IsNullOrEmpty(item.BlogAuthor))
        //    {
        //        item.BlogAuthor = Blog.BlogAuthor;
        //    }
        //    if (!string.IsNullOrEmpty(item.BlogContent))
        //    {
        //        item.BlogContent = Blog.BlogContent;
        //    }


        //    var result = _context.SaveChanges();
        //    string message = result > 0 ? "updating successful." : "updating failed.";
        //    return Ok(message);
        //}
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var item = _blog.GetBlog(id);
            if (item is null)
            {

                return NotFound("no data found");
            }

            var result = _blog.DeleteBlog(id);

            string message = result > 0 ? "deleting successful." : "deleting failed.";
            return Ok(message);
        }
    }
}