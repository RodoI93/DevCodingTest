using DevCodingTest.Models;

namespace DevCodingTest.Services
{
    public interface IStoryService
    {
        Task<List<int>> GetBestSories(int storiesNumber, string path);

        Task<List<StoryResponse>> GetStoriesDetail(List<int> stories, string path);
    }
}
