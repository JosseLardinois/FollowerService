using FollowerService.Contracts.Models;

namespace FollowerService.Contracts.Interfaces
{
    public interface IFollowersRepository
    {
        Task<IEnumerable<Follower>> All(Guid userId);
        Task Add(FollowerInputModel entity);
        Task Remove(FollowerInputModel follower);
    }
}
