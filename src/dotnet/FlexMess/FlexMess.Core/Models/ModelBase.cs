namespace FlexMess.Core.Models
{
    internal class ModelBase
    {
        public ModelBase()
        {
            
        }

        public ModelBase(ModelType type)
        {
            Type = type;
        }

        public ModelType Type { get; set; }
    }
}