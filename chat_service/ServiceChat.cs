using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace chat_service
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerSession)]
    public class ServiceChat : IServiceChat
    {
        List<ServerUser> users = new List<ServerUser>();
        int id = 1;
        public int Connect(string username)
        {
            ServerUser user = new ServerUser() { 
                ID = id, 
                Name = username, 
                OperationContext = OperationContext.Current 
            };
            id++;

            SendMessage($"{username} подключился к чату!", 0);
            users.Add(user);

            return user.ID;
        }

        public void Disconnect(int id)
        {
            var user = users.FirstOrDefault(x => x.ID == id);
            if (user != null)
            {
                users.Remove(user);
                SendMessage($"{user.Name} покинул чат!", 0);
            }
        }

        public void SendMessage(string msg, int id)
        {
            foreach (var user in users)
            {
                string answer = DateTime.Now.ToShortTimeString();

                var u = users.FirstOrDefault(x => x.ID == id);
                if (u != null)
                {
                    answer += $" : {user.Name} ";
                }

                answer += msg;

                user.OperationContext.GetCallbackChannel<IServiceChatCallback>().MessageCallback(answer);
            }
        }
    }
}
