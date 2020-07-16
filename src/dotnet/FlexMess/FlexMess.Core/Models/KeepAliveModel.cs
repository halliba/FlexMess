using System;
using Newtonsoft.Json;

namespace FlexMess.Core.Models
{
    internal class KeepAliveModel : ModelBase
    {
        public Guid UserId { get; set; }

        public string Name { get; set; }

        public string ProfilePic { get; set; }

        public KeepAliveModel() : base(ModelType.KeepAlive)
        {
        }
    }
}