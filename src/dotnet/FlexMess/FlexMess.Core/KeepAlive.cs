using FlexMess.Core.Models;

namespace FlexMess.Core
{
    public class KeepAlive
    {
        internal KeepAlive(KeepAliveModel keepAliveModel)
        {
            ProfilePic = keepAliveModel.ProfilePic;
            Sender = new User
            {
                Username = keepAliveModel.Name,
                UserId = keepAliveModel.UserId
            };
        }

        public string ProfilePic { get; }

        public User Sender { get; }
    }
}