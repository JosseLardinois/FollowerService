using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2;
using FollowerService.Contracts.Interfaces;
using FollowerService.Contracts.Models;
using FollowerService.Controllers;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FollowerService.Contracts.Repositories;

namespace FollowerService.UnitTest
{
    public class RepositoryTests
    {

        [Fact]
        public async Task All()
        {
            //Given
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
            
            var mockDyanmoDBCLinet = new Mock<AmazonDynamoDBClient>();
            var mockDynamoDBContext = new Mock<DynamoDBContext>();
           // var mockFollowerRepository = new Mock<DynamoD>();
           // mockFollowerRepository.Setup(c => c.All(follower.UserId)).ReturnsAsync(_followers); //mock the results of GetAll

            //Arrange

            FollowersRepository service = new FollowersRepository();

            //Act
            //var result = await service.Add(follower);
            //var result3 = result2 as ObjectResult;
            //var result = await service.GetAllFollowers(follower.UserId) as ObjectResult;
            //var actualResult = result2;

            //Assert
           // Assert.Equal(3, result.Count());
        }

    }
}
