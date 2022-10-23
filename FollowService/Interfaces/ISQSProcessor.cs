using FollowerService.Contracts.Models;

namespace FollowerService.Interfaces
{
    public interface ISQSProcessor
    {
        Task SQSPost(FollowerInputModel followerRequest);
        Task SQSRemove(FollowerInputModel followerRequest);

    }
}
