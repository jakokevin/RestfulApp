using BlogApi.Models;
using BlogApi.Models.dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySqlConnector;

namespace BlogApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogPostController : ControllerBase
    {
        public string connectionString = "server=localhost;uid=root;password=;database=blog;";

        [HttpGet]

        public object BloggerGetinformation(int id, GetBlogInfoDTO getBlogInfoDTO)
        {
            var connection = new MySqlConnection(connectionString);

            connection.Open();

            var sql = @"SELECT `@name`,`@email` FROM `blogger` WHERE `id` = @id";

            var cmd = new MySqlCommand(sql, connection);

            cmd.Parameters.AddWithValue("@id", id);
            cmd.Parameters.AddWithValue("@name", getBlogInfoDTO.Name);
            cmd.Parameters.AddWithValue("@email", getBlogInfoDTO.Email);

            var datareader = cmd.ExecuteReader();
            object? data = null;
            if (datareader.Read() == true)
            {
                var bloggers = new GetBlogInfoDTO
                {
                    Name = datareader.GetString(0),
                    Email = datareader.GetString(1),
                };
                if (bloggers != null)
                {
                    data = new { message = "Sikeres Lekérdezés", result = bloggers };
                }
            }
            else
            {
                data = new { message = "Sikertelen Lekérés", result = "" };
            }


            connection.Close();
            return data;
        }
    }
}
