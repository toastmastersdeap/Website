using Toast.jsonClasses;
using Toast.Models;
using Toast.Utilities;
using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MlkPwgen;

namespace Toast.Hubs
{
   [Authorize]
   public class MessagingHub : Hub
   {
      private readonly DBQuery _dbQuery = new DBQuery();
      private const int TryCount        = 3;

      public void DeleteThread(string viewEmail)
      {
         var userEmail               = Context.User.Identity.Name;
         var userId                  = _dbQuery.GetUserId(userEmail);
         var userSessionParticipant1 = userId;
         var userSessionParticipant2 = _dbQuery.GetUserId(viewEmail);
         var groupName               = userEmail.Replace("@","_");

         // TODO: Set to read all messages for of this session

         using (var db = new ProfileEntities())
         {
            var messages = db.ProfileMessages
               .Where(s => (s.SenderID == userSessionParticipant1 && s.ReceiverID == userSessionParticipant2) ||
                            s.SenderID == userSessionParticipant2 && s.ReceiverID == userSessionParticipant1).OrderByDescending(s => s.ID).ToList();

            foreach (var msg in messages)
            {
               var deleteSession = db.ProfileMessageUserSessions
                  .Where(s => s.SessionID == msg.SessionID && s.UserID == userId)
                  .Select(s => s.ID).FirstOrDefault();

               var connection = db.ProfileMessageUserSessions.Find(deleteSession);
               connection.Hidden = true;

               db.SaveChanges();
            }
         }

         // Update view messages
         Clients.Group(groupName).removeMessages(viewEmail);

      }

      public void GetSessions()
      {
         var userEmail = Context.User.Identity.Name;
         var userId    = _dbQuery.GetUserId(userEmail);
         var groupName = userEmail.Replace("@", "_");

         var sessions = new List<MemberInfo.RootObject>();

         using (var db = new ProfileEntities())
         {
            var userSessions = db.ProfileMessages
               .Where(s => s.ReceiverID == userId || s.SenderID == userId)
               .Distinct().Select(s => s.SessionID).ToList();

            foreach (var session in userSessions)
            {
               // This user received messages

               var receivedMessages = db.ProfileMessages.Where(s => s.SessionID == session && s.ReceiverID == userId).ToList();
               var senderEmail      = receivedMessages.Select(s => s.AspNetUser.Email).FirstOrDefault();
               var sessionCreatedBy = db.ProfileMessageSessions.FirstOrDefault(s => s.ID == session);

               if (receivedMessages.Count == 0 && sessionCreatedBy.CreatedBy == userId)
               {
                  var receiverId    = db.ProfileMessages.Where(s => s.SessionID == session && s.SenderID == userId).Select(s => s.ReceiverID).FirstOrDefault();
                  var receiverEmail = _dbQuery.GetUser(receiverId).Email;
                  var senderInfo    = _dbQuery.GetUserInfo(receiverEmail);
                  senderInfo.items[0].UnreadMessagesCount = "0";

                  if (!sessions.Any(s => s.items.Contains(senderInfo.items[0])))
                  {
                     sessions.Add(senderInfo);
                  }
               }
               else
               {
                  var receiverInfo = _dbQuery.GetUserInfo(senderEmail);

                  var userUnreadReceivedMessages = receivedMessages.Count(s => s.Unread == true).ToString();
                  receiverInfo.items[0].UnreadMessagesCount = string.IsNullOrEmpty(userUnreadReceivedMessages) ? "0" : userUnreadReceivedMessages;

                  if (!sessions.Any(s => s.items.Contains(receiverInfo.items[0])))
                  {
                     sessions.Add(receiverInfo);
                  }
               }
            }
         }

         if (sessions.Count > 0)
         {
            Clients.Group(groupName).availableMessages(sessions);
         }
      }

      public void ResetUnreadMessages(string senderEmail)
      {
         var receiverEmail = Context.User.Identity.Name;
         var groupName     = receiverEmail.Replace("@", "_");

         var senderId   = _dbQuery.GetUserId(senderEmail);
         var receiverId = _dbQuery.GetUserId(receiverEmail);

         using (var db = new ProfileEntities())
         {
            var sessionId = (from s in db.ProfileMessages
                             where s.ReceiverID == receiverId && s.SenderID == senderId
                             select s.SessionID).Distinct().ToList();

            var msgs = db.ProfileMessages.Where(m => m.SenderID == senderId && m.ReceiverID == receiverId && sessionId.Contains(m.SessionID));

            foreach (var msg in msgs)
            {
               msg.Unread = false;
            }
            db.SaveChanges();
         }

         Clients.Group(groupName).resetUnreadMessages(senderEmail);

      }

      public void SendMessage(string recvEmail, string message, bool newMessage)
      {
         if (string.IsNullOrEmpty(recvEmail) || string.IsNullOrEmpty(message)) return;

         var senderEmail     = Context.User.Identity.Name;
         var senderInfo      = _dbQuery.GetUserBasicInfo(senderEmail);
         var groupNameSender = senderEmail.Replace("@", "_");
         var session         = string.Empty;
         var senderId        = senderInfo.ID;
         var receiversEmail  = recvEmail.Split(',');

         using (var db = new ProfileEntities())
         {
            foreach (var receiver in receiversEmail)
            {
               var groupNameReceiver = receiver.Replace("@", "_");
               IList<string> groups  = new List<string> { groupNameSender, groupNameReceiver };

               var receiverId    = _dbQuery.GetUserId(receiver);
               var storedSession = db.ProfileMessages
                  .Where(s => (s.SenderID == senderId && s.ReceiverID == receiverId) ||
                              (s.SenderID == receiverId && s.ReceiverID == senderId)).OrderByDescending(s => s.ID).FirstOrDefault();

               if (storedSession != null)
               {
                  // Check if the session is hidden for the receiver
                  var sessionHidden = db.ProfileMessageUserSessions
                     .Where(s => s.SessionID == storedSession.SessionID && s.UserID == receiverId)
                     .Select(s => s.Hidden).FirstOrDefault();

                  if (!sessionHidden)
                     session = storedSession.SessionID;
               }

               if (string.IsNullOrEmpty(session) || newMessage)
               {
                  var tempSession = session;

                  // Generate a new session because does not exists
                  session = PasswordGenerator.Generate(length: 16);

                  // Create entry
                  var dbSession = db.ProfileMessageSessions.FirstOrDefault(s => s.ID == session);

                  // Avoid using the same sessionID
                  if (dbSession != null)
                  {
                     // Try to get a new random number
                     for (var i = 0; i < TryCount; i++)
                     {
                        session   = PasswordGenerator.Generate(length: 16);
                        dbSession = db.ProfileMessageSessions.FirstOrDefault(s => s.ID == session);

                        if (dbSession != null) continue;
                        session = dbSession.ID;
                        break;
                     }
                  }

                  var newSession =
                     new ProfileMessageSession
                     {
                        ID          = session,
                        CreatedBy   = senderId,
                        DateCreated = DateTimeOffset.UtcNow
                     };
                  db.ProfileMessageSessions.Add(newSession);

                  var senderSession =
                     new ProfileMessageUserSession
                     {
                        SessionID = session,
                        UserID    = senderId,
                        Hidden    = false
                     };
                  db.ProfileMessageUserSessions.Add(senderSession);

                  if (!newMessage || string.IsNullOrEmpty(tempSession))
                  {
                     var receiverSession =
                        new ProfileMessageUserSession
                        {
                           SessionID = session,
                           UserID    = receiverId,
                           Hidden    = false
                        };
                     db.ProfileMessageUserSessions.Add(receiverSession);
                  }
                  db.SaveChanges();
                  // End Create Entry
               }

               // Add message to the database
               var entry =
                  new ProfileMessage
                  {
                     SessionID       = session,
                     SenderID        = senderId,
                     ReceiverID      = receiverId,
                     SenderName      = senderInfo.Name,
                     SenderImageLink = senderInfo.ImageLink,
                     Message         = message,
                     Unread          = true,
                     Date            = DateTimeOffset.UtcNow
                  };
               db.ProfileMessages.Add(entry);
               db.SaveChanges();
               // End adding message to the database

               // Pass along message delivered
               Clients.Groups(groups).receivedNewPost(recvEmail, Context.User.Identity.Name, receiverId);


               // Notify receiver
               var hitCounterContext = GlobalHost.ConnectionManager.GetHubContext<HitCounterHub>();
               hitCounterContext.Clients.Group(groupNameReceiver).unreadMessageNotification(GetUnreadMessagesNotification(receiverId).ToString());
            }
         }
      }

      public int GetUnreadMessagesNotification(string userId)
      {
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

         return unreadReceivedMessagesCount;

      }

      public async Task Join()
      {
         var userEmail = Context.User.Identity.Name;
         var userId    = _dbQuery.GetUserId(userEmail);
         var groupName = userEmail.Replace("@", "_");

         await Groups.Add(Context.ConnectionId, groupName);
         Clients.Group(groupName).receiveUserId(userId);

      }

   }
}