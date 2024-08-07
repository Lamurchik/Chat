using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace ChatLaba3
{

    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class ServiceChat : IServiceChat
    {
        List<ServerUser> Users = new List<ServerUser>();        
        public int Connect(string name, string pasword)
        {
            //Console.WriteLine(OperationContext.Current.RequestContext.RequestMessage.ToString());

            if(name==""||pasword=="")
            {
                throw new FaultException("Логин или пароль не может иметь такой вид");
            }

            ServerUser q;
            using (var ct = new Сontext())
            {
                var users = ct.Users.ToList();
                q = users.FirstOrDefault(u => u.Name == name && u.Password == pasword);
                
            }
            if (q!=null)
            {
                Console.WriteLine("нашёл");
                q.chatCallBack= OperationContext.Current.GetCallbackChannel<IServiceChatCallBack>(); ;
                Users.Add(q);
                /*
                Task task = Task.Run(() => { SendMessage($" присоеденился к чату", q.Id); });

                task.Wait();
                */
                //SendMessage($" присоеденился к чату", q.Id);
                Console.WriteLine(q.Id);
                
                return q.Id;
            }
            throw new FaultException("пользователь не найден");
            //return -1;
        }
        

        /*
        public void Connect(string name, string pasword)
        {
            Console.WriteLine(OperationContext.Current.RequestContext.RequestMessage.ToString());
            ServerUser q;
            using (var ct = new Сontext())
            {
                var users = ct.Users.ToList();
                q = users.FirstOrDefault(u => u.Name == name && u.Password == pasword);

            }
            if (q != null)
            {
                Console.WriteLine("нашёл");
                q.operationContext = OperationContext.Current;
                Users.Add(q);
                SendMessage($"{q.Name} присоеденился к чату", 0);
                Console.WriteLine(q.Id);
                int id = q.Id;
                return;
            }
            else
            throw new Exception("пользователь не найден");
        }
        */


        public void Disconnect(int id)
        {
            var user = Users.FirstOrDefault(u => u.Id == id);
            if (user != null)
            {
                Users.Remove(user);
                //SendMessage($"{user.Name} покинул чат", 0);
            }
        }
        public List<string> GetChatHistory(int id)
        {
            var user = Users.FirstOrDefault(u => u.Id == id);
            if(user!=null)
            {
                using(var msg =new MsgContext())
                {
                    var messags = msg.Msg.ToList();
                    messags.Sort( (x,y)=>DateTime.Compare(x.Time, y.Time)  );
                    List<string> list = new List<string>();
                    foreach(var item in messags)
                    { list.Add(item.Message); }
                    return list;
                }
            }
            return null;
            
        }



        public void SendMessage(string message, int id)
        {
            Console.WriteLine("SendMessage");
            DateTime dateTime = DateTime.Now;
            string msg = dateTime.ToShortTimeString();
            var user = Users.FirstOrDefault(u => u.Id == id);
            if (user != null)
            {
                Console.WriteLine("all good");

                msg += $": [{user.Name}]: {message}";
            }
            if (id == 0)
                msg += message;
               
            using (var c = new MsgContext())
            {
                Msg m = new Msg { Message = msg, Time = dateTime };
                c.Msg.Add(m);
                c.SaveChanges();
            }

            /*
            Console.WriteLine("MsgCallBack start");
            OperationContext.Current.GetCallbackChannel<IServiceChatCallBack>().MsgCallBack(msg);
            Console.WriteLine("MsgCallBack finish");
            */
            
            foreach (var i in Users)
            {
                Console.WriteLine("MsgCallBack start");
                i.chatCallBack.MsgCallBack(msg);
                Console.WriteLine("MsgCallBack finish");
            }
            
        }
    }
}
