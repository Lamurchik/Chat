using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace ChatLaba3
{

    [ServiceContract(CallbackContract = typeof(IServiceChatCallBack))]
    public interface IServiceChat
    {
        /*
        [OperationContract(IsOneWay = true)]
        void Connect(string name, string pasword);
        */
        [OperationContract]
        int Connect(string name, string pasword);

        [OperationContract]
        void Disconnect(int id);

        [OperationContract]
        List<string> GetChatHistory(int id);

        [OperationContract(IsOneWay = true)]
        void SendMessage(string message, int id);



    }

    public interface IServiceChatCallBack
    {
        [OperationContract(IsOneWay = true)]
        void MsgCallBack(string message);
    }
}
