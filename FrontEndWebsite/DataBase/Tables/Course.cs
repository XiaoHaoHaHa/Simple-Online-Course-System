﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace DataBase.Tables
{
    public partial class Course
    {
        public Course()
        {
            Class = new HashSet<Class>();
        }

        public Guid CourseId { get; set; }
        public string CourseName { get; set; }
        public int? CourseHour { get; set; }

        public virtual ICollection<Class> Class { get; set; }
    }
}