using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BRApplication.Models
{
    public class MarketPostModel
    {
        public string Title { get; set; }
        public bool IsBuy { get; set; }
        public string Course { get; set; }
        public string Condition { get; set; }
        public string PostedBy { get; set; }
        public DateTime datePosted { get; set; }
        public string ISBN { get; set; }
        public string Author { get; set; }
        public double Price { get; set; }

        public MarketPostModel(string title, bool isBuy, string course, string condition, string postedby, DateTime dateposted, string isbn, string author, double price)
        {
            this.Title = title; 
            this.IsBuy = isBuy;
            this.Course = course;
            this.Condition = condition;
            this.PostedBy = postedby;
            this.datePosted = dateposted;
            this.ISBN = isbn;
            this.Author = author;
            this.Price = price; 
        }
    }
}