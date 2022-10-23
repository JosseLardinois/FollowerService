namespace FollowerService.Exceptions
{
    public class ProfileNotFoundException : Exception
    {
            public ProfileNotFoundException(Guid userid) : base($"Could not find user for userid {userid}")
            {

            }
        }
    }
