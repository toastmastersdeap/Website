
using System.Collections.Generic;

namespace Toast.jsonClasses
{
   public class MemberInfo
   {
      public class Item
      {
         public string Name { get; set; }
         public string Email { get; set; }
         public string Entity { get; set; }
         public string Position { get; set; }
         public string UnreadMessagesCount { get; set; }

         public string ImageLink { get; set; }
      }

      public class RootObject
      {
         public List<Item> items { get; set; }
      }
   }
}