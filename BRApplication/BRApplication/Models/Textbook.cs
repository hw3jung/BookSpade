using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BRApplication.Models
{
    public class Textbook
    {
        // Primary Key TextBookID
        public int TextBookID { get; set; }

        public string ISBN { get; set; }
        public string BookTitle { get; set; }
        public string Author { get; set; }
        public string Edition { get; set; }

        // Foreign Key CourseID
        public int CourseID { get; set; }

        public bool isActive { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }

        internal bool insert()
        {
            throw new NotImplementedException();
        }
    }
}