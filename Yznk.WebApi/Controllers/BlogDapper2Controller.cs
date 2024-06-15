using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using Yzk.share;
using YZKDotnetCore.WebApi;
using Yznk.WebApi.Model;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Yznk.WebApi;

[Route("api/[controller]")]
[ApiController]
public class BlogDapper2Controller : ControllerBase
{
    //private readonly DapperService _dapperService =
    //    new DapperService(ConnectionString.SqlConnectionStringBuilder.ConnectionString);

    private readonly DapperService _dapperService;

    public BlogDapper2Controller(DapperService dapperService)
    {
        _dapperService = dapperService;
    }

    // public object ConnectionStrings { get; private set; }

    //read
    [HttpGet]
    public IActionResult GetBlog()
    {
        string query = "select * from tbl_Blog";
       // using IDbConnection db = new SqlConnection(ConnectionString.SqlConnectionStringBuilder.ConnectionString);
       // List<BlogModel> lst = db.Query<BlogModel>(query).ToList();
        var lst = _dapperService.Query<BlogModel>(query).ToList();
        return Ok(lst);
    }
    [HttpGet("{id}")]
    public IActionResult GetBlog(int id)
    {
      //  string query = "select * from tbl_blog where blogod = @blogid";
       // using IDbConnection db = new SqlConnection(ConnectionString.SqlConnectionStringBuilder.ConnectionString);
       // var item = db.Query<BlogModel>(query, new BlogModel { BlogId = id}).FirstOrDefault();
      var item = FindById(id);
        if (item == null)
        {
           // Console.WriteLine("no data found");
            return NotFound("no data found");
        }
        return Ok(item);
    }
    [HttpPost]
    public IActionResult CreateBlog(BlogModel Blog)
    {
        string query = @"INSERT INTO [dbo].[Tbl_Blog]
           ([BlogTitle]
           ,[BlogAuthor]
           ,[BlogContent])
     VALUES
           (@BlogTitle
           ,@BlogAuthor
           ,@BlogContent)";
        // using IDbConnection db = new SqlConnection(ConnectionString.SqlConnectionStringBuilder.ConnectionString);
        // int result = db.Execute(query, Blog);
        int result = _dapperService.Execute(query, Blog);

        string message = result > 0 ? "saving successful." : "saving failed.";
        return Ok(message);
    }
    [HttpPut("{id}")]
    public IActionResult UpdateBlog(int id , BlogModel Blog)
    {
        var item = FindById(id);
        if (item == null)
        {
            return NotFound("no data found");
        }
        Blog.BlogId = id;
        string query = @"UPDATE [dbo].[Tbl_Blog]
   SET [BlogTitle] = @BlogTitle
      ,[BlogAuthor] = @BlogAuthor
      ,[BlogContent] = @BlogContent
 WHERE [BlogId] = @BlogId ";
        //  using IDbConnection db = new SqlConnection(ConnectionString.SqlConnectionStringBuilder.ConnectionString);
        //  int result = db.Execute(query, Blog);
        int result = _dapperService.Execute(query, Blog);

        string message = result > 0 ? "updating successful." : "updating failed.";
        return Ok(message);
    }
    [HttpPatch("{id}")]
    public IActionResult PatchBlog(int id, BlogModel Blog)
    {
        var item = FindById(id);
        if (item == null)
        {
            return NotFound("no data found");
        }
        string conditions = string.Empty;
        if(!string.IsNullOrEmpty(Blog.BlogTitle))
        {
            conditions += "[BlogTitle] = @BlogTitle, ";
        }
        if (!string.IsNullOrEmpty(Blog.BlogAuthor))
        {
            conditions += "[BlogAuthor] = @BlogAuthor, ";
        }
        if (!string.IsNullOrEmpty(Blog.BlogContent))
        {
            conditions += "[BlogContent] = @BlogContent, ";
        }
        if (conditions.Length == 0) {
            return NotFound("no data to update");
        }
        conditions = conditions.Substring(0, conditions.Length - 2);
        Blog.BlogId = id;
        string query = $@"UPDATE [dbo].[Tbl_Blog]
   SET {conditions}
 WHERE [BlogId] = @BlogId ";
        //using IDbConnection db = new SqlConnection(ConnectionString.SqlConnectionStringBuilder.ConnectionString);
        // int result = db.Execute(query, Blog);
        int result = _dapperService.Execute(query, Blog);

        string message = result > 0 ? "updating successful." : "updating failed.";
        return Ok(message);
    }
    [HttpDelete("{id}")]
    public IActionResult DeleteBlog(int id)

    {
        var item = FindById(id);
        if (item == null)
        {
            return NotFound("no data found");
        }
        string query = @"Delete From [dbo].[Tbl_Blog]
 WHERE [BlogId] = @BlogId ";
          using IDbConnection db = new SqlConnection(ConnectionString.SqlConnectionStringBuilder.ConnectionString);
         int result = db.Execute(query, new BlogModel
       
        { BlogId= id });
        string message = result > 0 ? "deleting successful." : "deleting failed.";
        return Ok(message);
    }
    private BlogModel FindById(int id)
    {
        string query = "select * from tbl_blog where blogod = @blogid";
        //   using IDbConnection db = new SqlConnection(ConnectionString.SqlConnectionStringBuilder.ConnectionString);
        //  var item = db.Query<BlogModel>(query, new BlogModel { BlogId = id }).FirstOrDefault();
        var item = _dapperService.QueryFirstOrDefault <BlogModel> (query, new BlogModel { BlogId = id});
        return item;
    }

}
