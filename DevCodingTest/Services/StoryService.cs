using DevCodingTest.Models;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Text.Json;

namespace DevCodingTest.Services
{
    public class StoryService : IStoryService
    {
        public StoryService()
        {

        }

        public async Task<List<int>> GetBestSories(int storiesNumber, string path)
        {
            List<int>? bestStories = new List<int>();

            HttpClientService httpClienteService = new HttpClientService();
            var uriBestStories = path;
            
            var storiesResponse = await httpClienteService.GetData(uriBestStories);
            if (storiesResponse.IsSuccessStatusCode)
            {
                bestStories = JsonSerializer.Deserialize<List<int>>(storiesResponse.Content.ReadAsStream());
            }

            var rangedBestStories = bestStories != null ? bestStories.GetRange(0, storiesNumber) : new List<int>();

            return rangedBestStories;
        }

        public async Task<List<StoryResponse>> GetStoriesDetail(List<int> stories, string path)
        {
            List<StoryResponse> storiesDetail = new List<StoryResponse>();
            HttpClientService httpClienteService = new HttpClientService();
            var uriStoryDetail = path;

            while (stories.Count > 0)
            {
                var storyResponse = await httpClienteService.GetData($"{uriStoryDetail}/{stories[0]}.json");
                if (storyResponse.IsSuccessStatusCode)
                {
                    var storyDetail = JsonSerializer.Deserialize<Story>(storyResponse.Content.ReadAsStream());

                    if (storyDetail != null)
                        storiesDetail.Add(ConvertToStoryResponse(storyDetail));
                }

                stories.Remove(stories[0]);
            }

            return storiesDetail; 
        }

        public StoryResponse ConvertToStoryResponse(Story story)
        {
            return new StoryResponse
            {
                title = story.title,
                uri = story.url,
                postedBy = story.by,
                time = story.time,
                score = story.score,
                commentCount = story.kids?.Count
            };
        }
    }
}
