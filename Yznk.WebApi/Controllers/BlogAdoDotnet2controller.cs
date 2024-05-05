using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

using System.Data.SqlClient;
using System.Numerics;
using System.Reflection.Metadata;
using YZKDotnetCore.WebApi;
using Yznk.WebApi.Model;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using Yzk.share;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.Identity.Client;
using static Yzk.share.AdoDotnetService;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Yznk.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogAdoDotnet2controller : ControllerBase
    {
        private readonly AdoDotnetService _adoDotnetService = new AdoDotnetService();
       // public object ConnectionStrings { get; private set; }

        [HttpGet]
        public IActionResult GetBlog()
        {
            string query = "select * from Tbl_Blog";
            var lst = _adoDotnetService.Query<BlogModel>(query);
            return Ok(lst);
           
        }

        [HttpGet("{id}")]
        public IActionResult GetBlog(int id, string query)
        {
           // string query = "select * from Tbl_Blog where Blogid = @Blogid";
           // AdoDotnetParameter[] parameters = new AdoDotnetParameter[1];
          //  parameters[0] = new AdoDotnetParameter("@BlogId", id);
          //  var lst = _adoDotnetService.QueryFirstOrDefault<BlogModel>(query, parameters);
            
            
            var item = _adoDotnetService.QueryFirstOrDefault<BlogModel>(query,
                new AdoDotnetParameter("@BlogId", id)
                );

            if (item == null)
            {
                return NotFound("no data found");
            }
            return Ok(item);
        }
        [HttpPost]
        public IActionResult CreateBlog(BlogModel blog)
        {
            string query = @"INSERT INTO [dbo].[Tbl_Blog]
           ([BlogTitle]
           ,[BlogAuthor]
           ,[BlogContent])
     VALUES
           (@BlogTitle
           ,@BlogAuthor
           ,@BlogContent)";
            int result = _adoDotnetService.Execute(query,
                new AdoDotnetParameter("@BlogTitle", blog.BlogTitle),
                new AdoDotnetParameter("@BlogAuthor", blog.BlogAuthor),
                new AdoDotnetParameter("@BlogContent", blog.BlogContent)
                );

            string message = result > 0 ? "saving successful." : "saving failed.";
            // Console.WriteLine(message);
            return Ok(message);
        }
        [HttpPut("{id}")]
        public IActionResult UpdateBlog(int id, BlogModel blog)
        {
            //var item = FindById(id);
            if (blog == null)
            {
                return NotFound("no data found");
            }
            blog.BlogId = id;
            string query = @"UPDATE [dbo].[Tbl_Blog]
   SET [BlogTitle] = @BlogTitle
      ,[BlogAuthor] = @BlogAuthor
      ,[BlogContent] = @BlogContent
 WHERE [BlogId] = @BlogId ";
            SqlConnection connection = new SqlConnection(ConnectionString.SqlConnectionStringBuilder.ConnectionString);
            connection.Open();
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@BlogId", id);
            cmd.Parameters.AddWithValue("@BlogTitle", blog.BlogTitle);
            cmd.Parameters.AddWithValue("@BlogAuthor", blog.BlogAuthor);
            cmd.Parameters.AddWithValue("@BlogContent", blog.BlogContent);
            int result = cmd.ExecuteNonQuery();
            connection.Close();

            string message = result > 0 ? "Updating successful." : "Updating failed.";
            // Console.WriteLine(message);
            return Ok(message);


        }
        [HttpPatch("{id}")]
        public IActionResult PatchBlog(int id , BlogModel blog) 
        {
            if (blog == null)
            {
                return NotFound("no data found");

            }
            string conditions = string.Empty;
            if (!string.IsNullOrEmpty(blog.BlogTitle))
            {
                conditions += "[BlogTitle] = @BlogTitle";
            }
            if (!string.IsNullOrEmpty(blog.BlogAuthor))
            {
                conditions += "[BlogAuthor] = @BlogAuthor";
            }
            if (!string.IsNullOrEmpty(blog.BlogContent))
            {
                conditions += "[BlogContent] = @BlogContent";
            }
            if (conditions.Length == 0)
            {
                return NotFound("no data to update");
            }
            blog.BlogId = id;
            SqlConnection connection = new SqlConnection(ConnectionString.SqlConnectionStringBuilder.ConnectionString);
            connection.Open();

            string query = @"UPDATE [dbo].[Tbl_Blog]
   SET [BlogTitle] = @BlogTitle
      ,[BlogAuthor] = @BlogAuthor
      ,[BlogContent] = @BlogContent
 WHERE [BlogId] = @BlogId ";
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@BlogId", id);
            cmd.Parameters.AddWithValue("@BlogTitle", blog.BlogTitle);
            cmd.Parameters.AddWithValue("@BlogAuthor", blog.BlogAuthor);
            cmd.Parameters.AddWithValue("@BlogContent", blog.BlogContent);
            int result = cmd.ExecuteNonQuery();
            connection.Close();
            string message = result > 0 ? "Updating successful." : "Updating failed";
            return Ok(message);

        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (id == null)
            {
                return NotFound("no data found");
            }
            string query = @"Delete From [dbo].[Tbl_Blog]
 WHERE [BlogId] = @BlogId ";
            SqlConnection connection = new SqlConnection(ConnectionString.SqlConnectionStringBuilder.ConnectionString);
            connection.Open();

            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@BlogId", id);
            int result = cmd.ExecuteNonQuery();
            connection.Close();

            string message = result > 0 ? "Deleting successful." : "Deleting failed.";
            return Ok(message);
        }
        


    }




}
