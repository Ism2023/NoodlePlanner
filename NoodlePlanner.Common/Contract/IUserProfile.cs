using System;

namespace NoodlePlanner.Common.Contract
{
    public interface IUserProfile
    {
        string UserName { get; }
        Guid UserId { get; }
    }
}
