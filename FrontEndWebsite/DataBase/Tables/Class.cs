﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace DataBase.Tables
{
    public partial class Class
    {
        public Class()
        {
            StuSelect = new HashSet<StuSelect>();
        }

        public Guid Id { get; set; }
        public Guid CourseId { get; set; }
        public Guid TeaId { get; set; }
        public DateTime? SDate { get; set; }
        public DateTime? EDate { get; set; }
        public string Location { get; set; }
        public string Description { get; set; }

        public virtual Course Course { get; set; }
        public virtual Teacher Tea { get; set; }
        public virtual ICollection<StuSelect> StuSelect { get; set; }
    }
}