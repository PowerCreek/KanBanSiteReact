using Microsoft.AspNetCore.Mvc;

namespace KanBanApi.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SetStarBoard()
        {
            return Ok();
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

        [HttpPost("/test")]
        public async Task<IActionResult> Test([FromBody] IDictionary<string, object> body, CancellationToken token)
        {
            try
            {
                return Ok(new[]{
                    new {
                        Name = "Super Project 1",
                    },
                    new {
                        Name = "Side Project B",
                    }
                });
            }catch(Exception e)
            {
                return BadRequest(500);
            }
        }

        [HttpGet]
        public IActionResult GetBoardList()
        {
            return Ok();
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
            }.Select((board, UUID) => new {
                UUID,
                board.Name,
                board.Updates,
                board.Workspace
            }));
        }
    }
}
