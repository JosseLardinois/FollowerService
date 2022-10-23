namespace FollowerService.Exceptions
{
    public class DuplicateProfileException : Exception
    {
            public DuplicateProfileException(Guid userid) : base($"Profile for user {userid} already exists")
            {
            }
        }
}
