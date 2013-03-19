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
        public string Title { get; set; }
        public string Author { get; set; }
        public string Edition { get; set; }

        // Foreign Key CourseID
        public int CourseID { get; set; }

        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }

        public Textbook(int textBookID, 
                        string isbn, 
                        string title, 
                        string author, 
                        string edition, 
                        int courseID,
                        bool isActive, 
                        bool isDeleted, 
                        DateTime createdDate, 
                        DateTime modifiedDate)
        {
            this.TextBookID = textBookID;
            this.ISBN = isbn;
            this.Title = title;
            this.Author = author;
            this.Edition = edition;
            this.CourseID = courseID;
            this.IsActive = isActive;
            this.IsDeleted = isDeleted;
            this.CreatedDate = createdDate;
            this.ModifiedDate = modifiedDate;
        }

        internal bool insert()
        {
            throw new NotImplementedException();
        }
    }
}