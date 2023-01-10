using FollowerService.Contracts.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FakeItEasy;
using FollowerService.Interfaces;
using FollowerService.Contracts.Models;
using FollowerService.Controllers;
using Microsoft.Extensions.Configuration;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;

namespace FollowerService.UnitTest
{

    public class ControllerTestsFakeItEasy
    {
        private readonly IFollowersRepository _repository;
        private readonly IConfiguration _config;
        private readonly ISQSProcessor _processor;

        public ControllerTestsFakeItEasy()
        {
            _repository = A.Fake<IFollowersRepository>();
            _config = A.Fake<IConfiguration>();
            _processor = A.Fake<ISQSProcessor>();
        }

        [Fact]
        public async Task GetAllFollowersRightUserId()
        {
            var followers = new List<Follower>()
            {
                new Follower()
                {
                    UserId = new Guid("7399dc91-f1a2-4e43-ab08-eb688538eb2b"),
                    FollowerId = new Guid("2b950b10-1d9e-460f-80f8-c0b2395c2793") //Follows the user
                },
                 new Follower()
                {
                    UserId = new Guid("7399dc91-f1a2-4e43-ab08-eb688538eb2b"),
                    FollowerId = new Guid("2b950b10-1d9e-460f-80f8-c0b2395c2792")//Follows the user
                },
            };
            A.CallTo(() => _repository.All(new Guid("7399dc91-f1a2-4e43-ab08-eb688538eb2b"))).Returns(followers);
            var controller = new FollowersController(_repository, _config, _processor);
            var result = await controller.GetAllFollowers(new Guid("7399dc91-f1a2-4e43-ab08-eb688538eb2b"));

            Assert.Equal(result.Count(), 2);
            Assert.NotNull(result);
        }
        [Fact]
        public async Task GetAllFollowersWrongUserId()
        {
            var followers = new List<Follower>()
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
            };
            A.CallTo(() => _repository.All(new Guid("7399dc91-f1a2-4e43-ab08-eb688538eb2b"))).Returns(followers);
            var controller = new FollowersController(_repository, _config, _processor);
            var result = await controller.GetAllFollowers(new Guid("2b950b10-1d9e-460f-80f8-c0b2395c2793"));

            Assert.NotNull(result);
            Assert.Equal(result.Count(), 0);
        }

        
    }
}
