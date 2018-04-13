using Domain.Entity;
using Microsoft.AspNet.SignalR;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace MVCWEB.Models
{
    public class NotificationHub: Hub
    {
        IUserService userService;
        public NotificationHub()
        {

            userService = new UserService();
        }
        //public override Task OnConnected()
        //{
        //    AssignToSecurityGroup();
        //    Greet();

        //    return base.OnConnected();
        //}
        public override Task OnConnected()
        {
            var us = new Employee();
            us.userName = Context.QueryString["username"];
            us.pseudoName=Context.ConnectionId;
            return base.OnConnected();
        }
        private void AssignToSecurityGroup()
        {
            if (Context.User.Identity.IsAuthenticated)
                Groups.Add(Context.ConnectionId, "authenticated");
            else
                Groups.Add(Context.ConnectionId, "anonymous");
        }

        private void Greet()
        {
            var greetedUserName = Context.User.Identity.IsAuthenticated ?
                Context.User.Identity.Name :
                "anonymous";

            Clients.Client(Context.ConnectionId).OnMessage(
                "[server]", "Welcome to the chat room, " + greetedUserName);
        }

        //public override Task OnDisconnected()
        //{
        //    RemoveFromSecurityGroups();
        //    return base.OnDisconnected();
        //}

        private void RemoveFromSecurityGroups()
        {
            Groups.Remove(Context.ConnectionId, "authenticated");
            Groups.Remove(Context.ConnectionId, "anonymous");
        }

        [Authorize]
        public void SendMessage(string message)
        {
            if (string.IsNullOrEmpty(message))
                return;

            BroadcastMessage(message);
        }

        private void BroadcastMessage(string message)
        {
            var userName = Context.User.Identity.Name;

            Clients.Group("authenticated").OnMessage(userName, message);

            var excerpt = message.Length <= 3 ? message : message.Substring(0, 3) + "...";
            Clients.Group("anonymous").OnMessage("[someone]", excerpt);
        }
        //public override Task OnConnected()
        //{
        //    string userName = Context.User.Identity.Name;
        //    string connectionId = Context.ConnectionId;

        //    User user = userService.getBylogin(userName);

        //    //Users.GetOrAdd(userName, user);

        //    lock (user.ConnectionIds)
        //    {
        //        user.ConnectionIds.Add(connectionId);


        //        Clients.All.listUserConnected(Users.Values);

        //        string text = user.Name + " " + user.Surname + " connected!";

        //        var msg = new NotificationMessage(text, Users.Count);
        //        Clients.Client(Context.ConnectionId).addMessage(msg);
        //    }

        //    return base.OnConnected();
        //}
        public void Send(string name, string message)
        {
            // Call the broadcastMessage method to update clients.
            Clients.All.broadcastMessage(name, message);
            Clients.User("bedoukAgent").send(message);
        }
    }
}
