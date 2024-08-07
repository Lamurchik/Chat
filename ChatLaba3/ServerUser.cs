using System.Runtime.Serialization;
using System.ServiceModel;

namespace ChatLaba3
{
    [DataContract]
    public class ServerUser
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string Password { get; set; }

        public OperationContext operationContext;
        public IServiceChatCallBack chatCallBack;
    }
}
