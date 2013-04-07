using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text.RegularExpressions;

namespace BRApplication.Utility
{
    public static class RegexUtil
    {
        private static Regex isbn = new Regex("^[0-9]+-[0-9]+-[0-9]+-[A-Za-z0-9-]*[A-Za-z0-9]$");
        private static Regex isbn_nohyphen = new Regex("^[0-9]+[A-Za-z]?$");
        private static Regex course = new Regex("^[A-Za-z]+ ?[0-9]+$");
        private static Regex title = new Regex("^[A-Za-z0-9.:'&]+$");

        /// <summary>
        /// Returns a boolean indicating if the search string is an ISBN
        /// </summary>
        public static bool isISBN(string searchString)
        {
            return isbn.IsMatch(searchString) || isbn_nohyphen.IsMatch(searchString);
        }

        /// <summary>
        /// Returns a boolean indicating if the search string is a course name
        /// </summary>
        public static bool isCourse(string searchString)
        {
            return course.IsMatch(searchString);
        }

        /// <summary>
        /// Returns a boolean indicating if the search string is a book title
        /// </summary>
        public static bool isTitle(string searchString)
        {
            return title.IsMatch(searchString);
        }
    }
}