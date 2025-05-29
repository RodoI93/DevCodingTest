using System.Text.Json;
using DevCodingTest.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DevCodingTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StoryController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private IStoryService _storiesService;
        public StoryController(IConfiguration configuration, IStoryService storiesService)
        {
            _configuration = configuration;
            _storiesService = storiesService;
        }

        [HttpGet("{storiesNumber}")]
        public async Task<ActionResult> GetBestStories(int storiesNumber)
        {
            try
            {
                string bestStoriesPath = _configuration.GetSection("HackerNewsAPI").GetValue("BestStories", "");
                if (string.IsNullOrEmpty(bestStoriesPath))
                    throw new Exception("Best stories API wasn't found.");

                var rangedBestStories = await _storiesService.GetBestSories(storiesNumber, bestStoriesPath);
                if (rangedBestStories.Count == 0)
                    return NotFound();

                //get sories detail
                string storyDetailPath = _configuration.GetSection("HackerNewsAPI").GetValue("ItemStory", "");
                if (string.IsNullOrEmpty(storyDetailPath))
                    throw new Exception("Story detail API wasn't found");

                var storiesDetail = await _storiesService.GetStoriesDetail(rangedBestStories, storyDetailPath);

                //order by score
                return Ok(storiesDetail.OrderByDescending(s => s.score));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Error al obtener productos", Exeption = ex.Message });
            }
        }

    }
}
