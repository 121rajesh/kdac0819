//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ExaminationPortal.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class T_Result
    {
        public int ResId { get; set; }
        public int UserId { get; set; }
        public int SubId { get; set; }
        public int CntCorrectAns { get; set; }
    
        public virtual T_Subject T_Subject { get; set; }
        public virtual T_Users T_Users { get; set; }
    }
}