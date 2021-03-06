﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BRApplication.Models;
using System.Data;

namespace BRApplication.Handlers
{
    public class TextbookHandler
    {
        #region insert 

        public static int insert(Textbook newBook)
        {
            int id = -1; 

            try
            {
                DataAccessLayer DAL = new DataAccessLayer();
                DataTable dt = DAL.select(String.Format("ISBN = '{0}'", newBook.ISBN), "TextBooks");

                if (dt == null || dt.Rows.Count == 0)
                {
                    int courseID = CourseInfoHandler.getCourseID(newBook.CourseName);

                    Dictionary<string, string> textbook = new Dictionary<string, string>();
                    textbook.Add("ISBN", newBook.ISBN);
                    textbook.Add("BookTitle", newBook.Title);
                    textbook.Add("Author", newBook.Author);
                    textbook.Add("CourseID", courseID.ToString());
                    textbook.Add("BookImageURL", newBook.BookImageURL);
                    textbook.Add("StorePrice", Convert.ToString(newBook.StorePrice));
                    textbook.Add("IsActive", "1");
                    textbook.Add("IsDeleted", "0");
                    textbook.Add("CreatedDate", Convert.ToString(DateTime.Now));
                    textbook.Add("ModifiedDate", Convert.ToString(DateTime.Now));
                    id = DAL.insert(textbook, "TextBooks");
                }
            }
            catch (Exception ex)
            {
                Console.Write("ERROR: An error occured in adding a new textbook --- " + ex.Message);
            }

            return id; 
        }

        #endregion

        #region getTextbookID

        public static int getTextbookID(string course, string title)
        {
            int textbookID = -1;

            try
            {
                int courseID = CourseInfoHandler.getCourseID(course);

                DataAccessLayer DAL = new DataAccessLayer();
                DataTable dt = DAL.select(String.Format("CourseID = '{0}' AND BookTitle = '{1}'", courseID, title), "TextBooks");
                
                if (dt != null && dt.Rows.Count > 0)
                {
                    textbookID = Convert.ToInt32(dt.Rows[0]["TextBookID"]);
                }
            }
            catch (Exception ex)
            {
                Console.Write("ERROR: An error occured in retrieving the textbook ID --- " + ex.Message);
            }

            return textbookID;
        }

        #endregion

        #region getTextbook

        public static Textbook getTextbook(int textbookID)
        {
            Textbook textbook = null;

            try
            {
                DataAccessLayer DAL = new DataAccessLayer();
                DataTable dt = DAL.select(String.Format("TextBookID = '{0}'", textbookID), "TextBooks");

                if (dt != null && dt.Rows.Count > 0)
                {
                    DataRow row = dt.Rows[0];

                    string isbn = Convert.ToString(row["ISBN"]);
                    string title = Convert.ToString(row["BookTitle"]);
                    string author = Convert.ToString(row["Author"]);
                    string bookImageURL = Convert.ToString(row["BookImageURL"]);

                    int courseID = Convert.ToInt32(row["CourseID"]);
                    string courseName = CourseInfoHandler.getCourseName(courseID);

                    int storePrice = Convert.ToInt32(row["StorePrice"]);

                    textbook = new Textbook(isbn, title, author, courseName, bookImageURL, storePrice);
                }
            }
            catch (Exception ex)
            {
                Console.Write("ERROR: An error occured in retrieving the textbook --- " + ex.Message);
            }

            return textbook;
        }

        #endregion

        #region getAllTextbooks

        public static IEnumerable<Textbook> getAllTextbooks()
        {
            List<Textbook> allTextbooks = new List<Textbook>();

            try
            {
                DataAccessLayer DAL = new DataAccessLayer();
                DataTable dt = DAL.select("", "TextBooks");

                foreach (DataRow row in dt.Rows)
                {
                    string isbn = Convert.ToString(row["ISBN"]);
                    string title = Convert.ToString(row["BookTitle"]);
                    string author = Convert.ToString(row["Author"]);
                    string bookImageURL = Convert.ToString(row["BookImageURL"]);

                    int courseID = Convert.ToInt32(row["CourseID"]);
                    string courseName = CourseInfoHandler.getCourseName(courseID);

                    int storePrice = Convert.ToInt32(row["StorePrice"]);

                    Textbook textbook = new Textbook(isbn, title, author, courseName, bookImageURL, storePrice);
                    allTextbooks.Add(textbook);
                }
            }
            catch (Exception ex)
            {
                Console.Write("ERROR: An error occured in retrieving all textbooks --- " + ex.Message);
            }

            return allTextbooks;
        }

        #endregion

        #region getTextbooksByISBN

        public static IEnumerable<Textbook> getTextbooksByISBN(string isbn)
        {
            List<Textbook> textbooks = new List<Textbook>();

            try
            {
                DataAccessLayer DAL = new DataAccessLayer();
                DataTable dt = DAL.select(String.Format("ISBN LIKE '%{0}%'", isbn), "TextBooks");

                foreach (DataRow row in dt.Rows)
                {
                    string full_isbn = Convert.ToString(row["ISBN"]);
                    string title = Convert.ToString(row["BookTitle"]);
                    string author = Convert.ToString(row["Author"]);
                    string bookImageURL = Convert.ToString(row["BookImageURL"]);

                    int courseID = Convert.ToInt32(row["CourseID"]);
                    string courseName = CourseInfoHandler.getCourseName(courseID);

                    int storePrice = Convert.ToInt32(row["StorePrice"]);

                    Textbook textbook = new Textbook(full_isbn, title, author, courseName, bookImageURL, storePrice);
                    textbooks.Add(textbook);
                }
            }
            catch (Exception ex)
            {
                Console.Write("ERROR: An error occured in retrieving textbooks by ISBN --- " + ex.Message);
            }

            return textbooks;
        }

        #endregion

        #region getTextbooksByCourse

        public static IEnumerable<Textbook> getTextbooksByCourse(string courseName)
        {
            List<Textbook> textbooks = new List<Textbook>();

            try
            {
                int[] courseIDs = CourseInfoHandler.getCourseIDs(courseName);

                foreach (int courseID in courseIDs)
                {
                    DataAccessLayer DAL = new DataAccessLayer();
                    DataTable dt = DAL.select(String.Format("CourseID = '{0}'", courseID), "TextBooks");

                    foreach (DataRow row in dt.Rows)
                    {
                        string isbn = Convert.ToString(row["ISBN"]);
                        string title = Convert.ToString(row["BookTitle"]);
                        string author = Convert.ToString(row["Author"]);
                        string bookImageURL = Convert.ToString(row["BookImageURL"]);

                        string full_courseName = CourseInfoHandler.getCourseName(courseID);

                        int storePrice = Convert.ToInt32(row["StorePrice"]);

                        Textbook textbook = new Textbook(isbn, title, author, full_courseName, bookImageURL, storePrice);
                        textbooks.Add(textbook);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.Write("ERROR: An error occured in retrieving textbooks by course name --- " + ex.Message);
            }

            return textbooks;
        }

        #endregion

        #region getTextbooksByTitle

        public static IEnumerable<Textbook> getTextbooksByTitle(string title)
        {
            List<Textbook> textbooks = new List<Textbook>();

            try
            {
                DataAccessLayer DAL = new DataAccessLayer();
                DataTable dt = DAL.select(String.Format("BookTitle LIKE '%{0}%'", title), "TextBooks");

                foreach (DataRow row in dt.Rows)
                {
                    string isbn = Convert.ToString(row["ISBN"]);
                    string full_title = Convert.ToString(row["BookTitle"]);
                    string author = Convert.ToString(row["Author"]);
                    string bookImageURL = Convert.ToString(row["BookImageURL"]);

                    int courseID = Convert.ToInt32(row["CourseID"]);
                    string courseName = CourseInfoHandler.getCourseName(courseID);

                    int storePrice = Convert.ToInt32(row["StorePrice"]);

                    Textbook textbook = new Textbook(isbn, full_title, author, courseName, bookImageURL, storePrice);
                    textbooks.Add(textbook);
                }
            }
            catch (Exception ex)
            {
                Console.Write("ERROR: An error occured in retrieving textbooks by book title --- " + ex.Message);
            }

            return textbooks;
        }

        #endregion

        #region getTextbookIDsByISBN

        public static int[] getTextbookIDsByISBN(string isbn)
        {
            List<int> textbookIDs = new List<int>();
            
            try
            {
                // ISBN parsing (may or may not contain hyphens)
                isbn = isbn.Replace("-", String.Empty);
                
                DataAccessLayer DAL = new DataAccessLayer();
                DataTable dt = DAL.select(String.Format("ISBN LIKE '%{0}%'", isbn), "TextBooks", new string[] { "TextBookID" });
                
                foreach (DataRow row in dt.Rows)
                {
                    int textbookID = Convert.ToInt32(row["TextBookID"]);
                    
                    textbookIDs.Add(textbookID);
                }
            }
            catch (Exception ex)
            {
                Console.Write("ERROR: An error occured in retrieving textbook IDs by ISBN --- " + ex.Message);
            }

            return textbookIDs.ToArray();
        }

        #endregion

        #region getTextbookIDsByCourse

        public static int[] getTextbookIDsByCourse(string courseName)
        {
            List<int> textbookIDs = new List<int>();

            try
            {
                int[] courseIDs = CourseInfoHandler.getCourseIDs(courseName);

                foreach (int courseID in courseIDs)
                {
                    DataAccessLayer DAL = new DataAccessLayer();
                    DataTable dt = DAL.select(String.Format("CourseID = '{0}'", courseID), "TextBooks", new string[] { "TextBookID" });

                    foreach (DataRow row in dt.Rows)
                    {
                        int textbookID = Convert.ToInt32(row["TextBookID"]);

                        textbookIDs.Add(textbookID);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.Write("ERROR: An error occured in retrieving textbook IDs by course name --- " + ex.Message);
            }

            return textbookIDs.ToArray();
        }

        #endregion

        #region getTextbookIDsByTitle

        public static int[] getTextbookIDsByTitle(string title)
        {
            List<int> textbookIDs = new List<int>();

            try
            {
                DataAccessLayer DAL = new DataAccessLayer();
                DataTable dt = DAL.select(String.Format("BookTitle LIKE '%{0}%'", title), "TextBooks", new string[] { "TextBookID" });

                foreach (DataRow row in dt.Rows)
                {
                    int textbookID = Convert.ToInt32(row["TextBookID"]);

                    textbookIDs.Add(textbookID);
                }
            }
            catch (Exception ex)
            {
                Console.Write("ERROR: An error occured in retrieving textbook IDs by book title --- " + ex.Message);
            }

            return textbookIDs.ToArray();
        }

        #endregion

        #region getCourseName

        public static string getCourseName(int textBookID)
        {
            Textbook textbook = getTextbook(textBookID);

            return textbook.CourseName;
        }

        #endregion

        #region getTextbookTitle

        public static string getTextbookTitle(int textBookID)
        {
            Textbook textbook = getTextbook(textBookID);

            return textbook.Title;
        }

        #endregion

        #region getISBN

        public static string getISBN(int textBookID)
        {
            Textbook textbook = getTextbook(textBookID);

            return textbook.ISBN;
        }

        #endregion

        #region getAuthor

        public static string getAuthor(int textBookID)
        {
            Textbook textbook = getTextbook(textBookID);

            return textbook.Author;
        }

        #endregion

    }
}