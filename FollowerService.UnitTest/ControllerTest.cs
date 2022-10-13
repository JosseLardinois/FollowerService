using Amazon.DynamoDBv2.DataModel;
using Castle.Components.DictionaryAdapter.Xml;
using FluentAssertions;
using FollowerService.Contracts.Interfaces;
using FollowerService.Contracts.Models;
using FollowerService.Contracts.Repositories;
using FollowerService.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Xsl;

namespace FollowerService.UnitTest
{
    public class ControllerTest
    {
        private readonly Mock<IFollowersRepository> followersRepositoryMock;
        public ControllerTest()
        {
            followersRepositoryMock = MockRepo.GetFollowerRepo();
        }
        [Fact]
        public async Task Add()
        {
            var follower = new FollowerInputModel()
            {
                FollowerId = new Guid(),
                UserId = new Guid("2b950b10-1d9e-460f-80f8-c0b2395c2793")
            };

            //Given
            var handler = new FollowersRepository();
            var result = await handler.All(follower.UserId);
            var response = Assert.IsType<Follower>(result);
            Assert.NotNull(response);


        }

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
            var follower = new Follower()
            {
                FollowerId = new Guid(),
                UserId = new Guid("2b950b10-1d9e-460f-80f8-c0b2395c2793")
            };
            var mockFollowerRepository = new Mock<IFollowersRepository>();
            var mockIConfig = new Mock<IConfiguration>();
            mockFollowerRepository.Setup(c => c.All(follower.UserId)).ReturnsAsync(_followers); //mock the results of GetAll

            //Arrange

            FollowersController service = new FollowersController(mockFollowerRepository.Object, mockIConfig.Object);

            //Act
            var result = await service.GetAllFollowers(follower.UserId);
            //var result3 = result2 as ObjectResult;
            //var result = await service.GetAllFollowers(follower.UserId) as ObjectResult;
            //var actualResult = result2;

            //Assert
            Assert.Equal(3, result.Count());
        }
    }
}
