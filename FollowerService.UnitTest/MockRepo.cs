using FollowerService.Contracts.Interfaces;
using FollowerService.Contracts.Models;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FollowerService.UnitTest
{
    public static class MockRepo
    {
        public static Mock<IFollowersRepository> GetFollowerRepo()
        {
            List<Follower> _followers = new List<Follower>();
            _followers = new List<Follower>()
            {
                new Follower()
                {
                    UserId = new Guid("7399dc91-f1a2-4e43-ab08-eb688538eb2b"),
                    FollowerId = new Guid("2b950b10-1d9e-460f-80f8-c0b2395c2793") //Follows the user
                },
                 new Follower()
                {
                    UserId = new Guid("ab997576-bc39-4c69-b66a-3af062018917"),
                    FollowerId = new Guid("2b950b10-1d9e-460f-80f8-c0b2395c2793")//Follows the user
                },
                  new Follower()
                {
                    UserId = new Guid("56808dfc-4c6e-4440-83a2-46c6a0e20570"),
                    FollowerId = new Guid("2b950b10-1d9e-460f-80f8-c0b2395c2793")//Followes the user
                },
            };
            var follower = new FollowerInputModel()
            {
                FollowerId = new Guid("2b950b10-1d9e-460f-80f8-c0b2395c2793"),
                UserId = new Guid("2b950b10-1d9e-460f-80f8-c0b2395c2793")
            };
            var mockRepo = new Mock<IFollowersRepository>();

            mockRepo.Setup(c => c.All(follower.UserId)).ReturnsAsync(_followers); //mock the results of GetAll


            mockRepo.Setup(c => c.Add(It.IsAny<FollowerInputModel>())).ReturnsAsync((Follower follower) =>
            {
                _followers.Add(follower);
                return follower;
            });
            Console.WriteLine(mockRepo);
            return mockRepo;
        }
    }
}
