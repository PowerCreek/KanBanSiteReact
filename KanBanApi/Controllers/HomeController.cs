using Microsoft.AspNetCore.Mvc;

namespace KanBanApi.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("/test")]
        public IActionResult Test(string test)
        {
            return Ok(new[]{
                new {
                    Name = "Super Project 1",
                },
                new {
                    Name = "Side Project B",
                }
            });
        }

        [HttpGet("/getboards")]
        public IActionResult GetBoards()
        {
            return Ok(new[]{
                new {
                    Workspace = "A",
                    Name = "Super Project 1",
                    Updates = 0,
                },
                new {
                    Workspace = "A",
                    Name = "Side Project B",
                    Updates = 0,
                },
                new {
                    Workspace = "A",
                    Name = "Side Project B",
                    Updates = 0,
                },
                new {
                    Workspace = "A",
                    Name = "Side Project B",
                    Updates = 0,
                },
                new {
                    Workspace = "A",
                    Name = "Side Project B",
                    Updates = 0,
                },
                new {
                    Workspace = "A",
                    Name = "Side Project B",
                    Updates = 0,
                },
                new {
                    Workspace = "A",
                    Name = "Side Project B",
                    Updates = 0,
                },
                new {
                    Workspace = "B",
                    Name = "Side Project B",
                    Updates = 0,
                }
            });
        }
    }
}
