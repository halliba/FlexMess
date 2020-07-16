using System;

namespace FlexMess.Core.Models
{
    internal class MessageModel : ModelBase
    {
        public string Text { get; set; }

        public string UserName { get; set; }
        
        public Guid UserId { get; set; }

        public MessageModel() : base(ModelType.Message)
        {
        }
    }
}