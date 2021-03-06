﻿using System;
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
        public string BookImageURL { get; set; }
        public int StorePrice { get; set; }
        public string CourseName { get; set; }

        public Textbook(string isbn, 
                        string title, 
                        string author, 
                        string courseName,
                        string bookImageURL,
                        int storePrice)
        {
            this.ISBN = isbn;
            this.Title = title;
            this.Author = author;
            this.CourseName = courseName;
            this.BookImageURL = bookImageURL;
            this.StorePrice = storePrice;
        }
    }
}