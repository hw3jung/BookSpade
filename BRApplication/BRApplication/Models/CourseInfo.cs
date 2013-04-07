using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BRApplication.Models
{
    public class CourseInfo
    {
        // Primary Key CourseID
        public int CourseID { get; set; }
        public string CourseName { get; set; }
        public string Description { get; set; }

        public CourseInfo(string courseName, string description) {
            this.CourseName = courseName;
            this.Description = description;
        }
    }
}