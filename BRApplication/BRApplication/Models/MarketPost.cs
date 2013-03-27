using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BRApplication.Models
{
    public class MarketPost
    {
        public bool IsBuy { get; set; }
        public string ISBN { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Course { get; set; }
        public string Condition { get; set; }
        public string PostedBy { get; set; }
        public string BookImageURL { get; set; }
        public int Price { get; set; }
        public DateTime datePosted { get; set; }

        public MarketPost(string title, bool isBuy, string course, string condition, string postedby, DateTime dateposted, string isbn, string author, string bookImageURL, int price)
        {
            this.Title = title; 
            this.IsBuy = isBuy;
            this.Course = course;
            this.Condition = condition;
            this.PostedBy = postedby;
            this.datePosted = dateposted;
            this.ISBN = isbn;
            this.Author = author;
            this.BookImageURL = bookImageURL;
            this.Price = price; 
        }
    }
}