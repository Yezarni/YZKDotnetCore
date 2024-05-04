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

namespace Yznk.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogAdoDotnetcontroller : ControllerBase
    {
        public object ConnectionStrings { get; private set; }

        [HttpGet]
        public IActionResult GetBlog()
        {
            string query = "select * from Tbl_Blog";
            SqlConnection connection = new SqlConnection(ConnectionString.SqlConnectionStringBuilder.ConnectionString);
            connection.Open();
            Console.WriteLine("Connection opened");


            SqlCommand cmd = new SqlCommand(query, connection);
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sqlDataAdapter.Fill(dt);

            connection.Close();
            //Console.WriteLine("Connection closed");
            List<BlogModel> lst = new List<BlogModel>();
            foreach (DataRow dr in dt.Rows)
            {
                BlogModel blog = new BlogModel();
                blog.BlogId = Convert.ToInt32(dr["BlogId"]);
                blog.BlogTitle = Convert.ToString(dr["BlogTitle"]);
                blog.BlogAuthor = Convert.ToString(dr["BlogAuthor"]);
                blog.BlogContent = Convert.ToString(dr["BlogContent"]);
                lst.Add(blog);
            }
            return Ok(lst);
        }

        [HttpGet("{id}")]
        public IActionResult GetBlog(int id)
        {
            string query = "select * from Tbl_Blog where Blogid = @Blogid";
            SqlConnection connection = new SqlConnection(ConnectionString.SqlConnectionStringBuilder.ConnectionString);
            connection.Open();
            Console.WriteLine("Connection opened");


            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("BlogId", id);
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sqlDataAdapter.Fill(dt);

            connection.Close();
            if (dt.Rows.Count == 0)
            {
                return NotFound("no data found");
            }
            DataRow dr = dt.Rows[0];
            var item = new BlogModel();
            {
                blog.BlogId = Convert.ToInt32(dr["BlogId"]);
                blog.BlogTitle = Convert.ToString(dr["BlogTitle"]);
                blog.BlogAuthor = Convert.ToString(dr["BlogAuthor"]);
                blog.BlogContent = Convert.ToString(dr["BlogContent"]);
            }
            return Ok(dt);
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
            SqlConnection connection = new SqlConnection(ConnectionString.SqlConnectionStringBuilder.ConnectionString);
            connection.Open();


            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@BlogTitle", blog.BlogTitle);
            cmd.Parameters.AddWithValue("@BlogAuthor", blog.BlogAuthor);
            cmd.Parameters.AddWithValue("@BlogContent", blog.BlogContent);
            int result = cmd.ExecuteNonQuery();
            connection.Close();

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
