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
    
    public partial class GrammarianRecord
    {
        public int RecordID { get; set; }
        public int MeetingID { get; set; }
        public int UsageType { get; set; }
        public string Explanation { get; set; }
    
        public virtual Meeting Meeting { get; set; }
    }
}
