//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Toast.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class MeetingAward
    {
        public int MeetingAwardID { get; set; }
        public int MeetingID { get; set; }
        public int ID { get; set; }
        public int AwardType { get; set; }
    
        public virtual Meeting Meeting { get; set; }
        public virtual Member Member { get; set; }
    }
}
