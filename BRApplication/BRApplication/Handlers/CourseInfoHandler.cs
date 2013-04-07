using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BRApplication.Models;
using System.Data;

namespace BRApplication.Handlers
{
    public class CourseInfoHandler
    {
        public static bool insert(CourseInfo newCourseInfo)
        {
            bool success = false;

            try
            {
                DataAccessLayer DAL = new DataAccessLayer();
                DataTable dt = DAL.select(String.Format("CourseName = '{0}'", newCourseInfo.CourseName), "CourseInfo");

                if (dt == null || dt.Rows.Count == 0)
                {
                    Dictionary<string, string> courseInfo = new Dictionary<string, string>();
                    courseInfo.Add("CourseName", newCourseInfo.CourseName);
                    courseInfo.Add("Description", newCourseInfo.Description);
                    courseInfo.Add("IsActive", "1");
                    courseInfo.Add("IsDeleted", "0");
                    courseInfo.Add("CreatedDate", Convert.ToString(DateTime.Now));
                    courseInfo.Add("ModifiedDate", Convert.ToString(DateTime.Now));
                    success = DAL.insert(courseInfo, "CourseInfo");
                }
            }
            catch (Exception ex)
            {
                Console.Write("ERROR: An error occured in adding a new course --- " + ex.Message);
            }

            return success; 
        }

        public static CourseInfo getCourseInfo(int courseID)
        {
            CourseInfo courseInfo = null;

            try
            {
                DataAccessLayer DAL = new DataAccessLayer();
                DataTable dt = DAL.select(String.Format("CourseID = '{0}'", courseID), "CourseInfo");

                if (dt != null && dt.Rows.Count > 0)
                {
                    DataRow row = dt.Rows[0];

                    string courseName = Convert.ToString(row["CourseName"]);
                    string description = Convert.ToString(row["Description"]);

                    courseInfo = new CourseInfo(courseName, description);
                }
            }
            catch (Exception ex)
            {
                Console.Write("ERROR: An error occured in retrieving the course information --- " + ex.Message);
            }

            return courseInfo;
        }

        public static int getCourseID(string courseName)
        {
            int courseID = -1;

            try
            {
                DataAccessLayer DAL = new DataAccessLayer();
                DataTable dt = DAL.select(String.Format("CourseName = '{0}'", courseName), "CourseInfo");

                if (dt != null && dt.Rows.Count > 0)
                {
                    courseID = Convert.ToInt32(dt.Rows[0]["CourseID"]);
                }
            }
            catch (Exception ex)
            {
                Console.Write("ERROR: An error occured in retrieving the course ID --- " + ex.Message);
            }

            return courseID;
        }

        public static int[] getCourseIDs(string courseName)
        {
            List<int> courseIDs = new List<int>();

            try
            {
                DataAccessLayer DAL = new DataAccessLayer();
                DataTable dt = DAL.select(String.Format("CourseName LIKE '%{0}%'", courseName), "CourseInfo", new string[] { "CourseID" });

                foreach (DataRow row in dt.Rows)
                {
                    int courseID = Convert.ToInt32(row["CourseID"]);

                    courseIDs.Add(courseID);
                }
            }
            catch (Exception ex)
            {
                Console.Write("ERROR: An error occured in retrieving course IDs by course name --- " + ex.Message);
            }

            return courseIDs.ToArray();
        }

        public static string getCourseName(int courseID)
        {
            CourseInfo course = getCourseInfo(courseID);

            return course.CourseName;
        }
    }
}