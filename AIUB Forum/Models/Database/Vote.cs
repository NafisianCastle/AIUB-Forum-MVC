//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AIUB_Forum.Models.Database
{
    using System;
    using System.Collections.Generic;
    
    public partial class Vote
    {
        public int VoteId { get; set; }
        public int PostId { get; set; }
        public int UserId { get; set; }
        public System.DateTime Date { get; set; }
        public int Value { get; set; }
    
        public virtual Post Post { get; set; }
        public virtual User User { get; set; }
    }
}
