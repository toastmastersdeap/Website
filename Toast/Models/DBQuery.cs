using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using Toast.jsonClasses;

namespace Toast.Models
{
    public class DBQuery
    {
        //Repository of functions 
        // e.g public List<Members> GetListOfMemebers(int clubID) {} 


        public Club GetClub(int clubId)
        {
            // TODO: Change to a mapping form (when schools implements)
            using (var db = new ProfileEntities())
            {
                var info =
                   (from s in db.Clubs
                    where s.ClubID == clubId
                    select s).FirstOrDefault();

                return info;
            }
        }

        public AspNetUser GetUser(string user)
        {
            using (var db = new ProfileEntities())
            {
                var info = new AspNetUser();

                if (user.Contains("@"))
                {
                    info = (from s in db.AspNetUsers
                            where s.Email == user
                            select s).FirstOrDefault();
                }
                else
                {
                    info = (from s in db.AspNetUsers
                            where s.ID == user
                            select s).FirstOrDefault();
                }

                return info;
            }
        }

        public vw_UserBasicInfo GetUserBasicInfo(string email)
        {
            using (var db = new ProfileEntities())
            {
                var user = db.vw_UserBasicInfo.FirstOrDefault(s => s.Email == email);

                return user;
            }
        }

        public string GetUserId(string email)
        {
            using (var db = new ProfileEntities())
            {
                var info = (from s in db.AspNetUsers
                            where s.Email == email
                            select s.ID).FirstOrDefault();

                return info;
            }
        }

        public MemberInfo.RootObject GetUserInfo(string email)
        {
            var userInfo = GetUserBasicInfo(email);

            var objectToSerialize = new MemberInfo.RootObject
            {

                items = new List<MemberInfo.Item>
               {
                  new MemberInfo.Item
                  {
                     Name                = userInfo.Name,
                     Email               = email,
                     Club                = "", // TODO
                     Position            = string.IsNullOrEmpty(userInfo.Position) ? "Position" : userInfo.Position,
                     UnreadMessagesCount = string.Empty,
                     ImageLink           = string.IsNullOrEmpty(userInfo.ImageLink) ? "#" : userInfo.ImageLink.Replace("~/", "../")
                  }
               }
            };

            return objectToSerialize;
        }

    }
}