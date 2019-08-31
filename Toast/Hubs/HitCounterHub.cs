using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Toast.jsonClasses;
using Toast.Models;

namespace Toast.Hubs
{
    [System.Web.Http.Authorize]
    public class HitCounterHub : Hub
    {
        private readonly DBQuery _dbQuery = new DBQuery();

        public void GetUnreadMessagesNotification()
        {
            var userEmail = Context.User.Identity.Name;
            var userId = _dbQuery.GetUserId(userEmail);
            var groupName = userEmail.Replace("@", "_");
            var unreadReceivedMessagesCount = 0;

            using (var db = new ProfileEntities())
            {
                var userAllSessions = db.ProfileMessages
                   .Where(s => s.ReceiverID == userId)
                   .Distinct().Select(s => s.SessionID).ToList();

                var userInvalidSessions = db.ProfileMessageUserSessions
                   .Where(s => s.UserID == userId && s.Hidden)
                   .Select(s => s.SessionID).ToList();

                var userValidSessions = userAllSessions.Except(userInvalidSessions).ToList();

                foreach (var session in userValidSessions)
                {
                    // This user received messages

                    var receivedMessages = db.ProfileMessages.Where(s => s.SessionID == session && s.ReceiverID == userId).ToList();

                    unreadReceivedMessagesCount += receivedMessages.Count(s => s.Unread == true);

                }
            }

            if (unreadReceivedMessagesCount > 0)
            {
                Clients.Group(groupName).unreadMessageNotification(unreadReceivedMessagesCount.ToString());
            }
        }

        // Show the list of online users for the User's Club
        public void GetOnlineUsers()
        {
            var userTableModel = _dbQuery.GetUserInfo(Context.User.Identity.Name);
            var groupName = userTableModel.items[0].Club.Replace(" ", "_");

            using (var db = new MapUserEntities())
            {
                var usersList = (from s in db.MapConnectionUsers
                                 where s.ClubName == groupName && s.Connected
                                 select s.UserName).ToList();

                var memberInfo = new List<MemberInfo.RootObject>();

                foreach (var item in usersList)
                {
                    memberInfo.Add(_dbQuery.GetUserInfo(item));
                }

                if (usersList.Count > 0)
                {
                    Clients.Group(groupName).onlineUsers(memberInfo);
                }
            }
        }

        public async Task Join()
        {
            var userInfo = _dbQuery.GetUserInfo(Context.User.Identity.Name);
            var groupName = userInfo.items[0].Club.Replace(" ", "_");

            await Groups.Add(Context.ConnectionId, groupName);
            await Groups.Add(Context.ConnectionId, Context.User.Identity.Name.Replace("@", "_"));
            Clients.Group(groupName).addOnlinePerson(Context.User.Identity.Name);

            using (var db = new MapUserEntities())
            {
                var connection = db.MapConnectionUsers.Find(Context.ConnectionId);
                connection.ClubName = groupName;
                db.SaveChanges();
            }
        }

        public override Task OnConnected()
        {
            var name = Context.User.Identity.Name;

            using (var db = new MapUserEntities())
            {
                var user = db.MapUsers
                    //.Include(x => x.MapConnectionUsers)
                    .SingleOrDefault(x => x.UserName == name);

                if (user == null)
                {
                    user = new MapUser
                    {
                        UserName = name,
                        MapConnectionUsers = new List<MapConnectionUser>()
                    };
                    db.MapUsers.Add(user);
                }

                user.MapConnectionUsers.Add(new MapConnectionUser
                {
                    ConnectionID = Context.ConnectionId,
                    UserAgent = Context.Request.Headers["User-Agent"],
                    Connected = true
                });

                db.SaveChanges();
            }
            return base.OnConnected();
        }

        public override Task OnReconnected()
        {
            // Partial CleanUp
            using (var db = new MapUserEntities())
            {
                var connection = db.MapConnectionUsers.Where(m => m.Connected && Context.User.Identity.Name == m.UserName && Context.ConnectionId != m.ConnectionID).ToList();

                foreach (var conn in connection)
                {
                    conn.Connected = false;
                    db.SaveChanges();
                }
            }

            return base.OnReconnected();
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            using (var db = new MapUserEntities())
            {
                var connection = db.MapConnectionUsers.Find(Context.ConnectionId);

                if (connection != null)
                {
                    connection.Connected = false;
                    db.SaveChanges();
                }
            }

            return base.OnDisconnected(stopCalled);
        }
    }
}