using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FollowerService.Contracts.Models
{
    public class FollowerInputModel
    {
        public Guid UserId { get; set; }
        public Guid FollowerId { get; set; }
    }
}
