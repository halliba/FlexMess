using FlexMess.Core.Models;

namespace FlexMess.Core
{
    public class Message
    {
        internal Message(MessageModel messageModel)
        {
            Text = messageModel.Text;
            Sender = new User
            {
                UserId = messageModel.UserId,
                Username = messageModel.UserName
            };
        }

        private Message()
        {
            
        }

        public string Text { get; internal set; }

        public User Sender { get; internal set; }
    }
}