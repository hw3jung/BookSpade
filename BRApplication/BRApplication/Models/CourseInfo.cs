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
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }

        public CourseInfo(string courseName) {
            this.CourseName = courseName;
            this.IsActive = true;
            this.IsDeleted = false;
            this.CreatedDate = DateTime.Now;
            this.ModifiedDate = DateTime.Now;
        }

        public static int getCourseID(string courseName)
        {
            throw new NotImplementedException();
        }
    }
}