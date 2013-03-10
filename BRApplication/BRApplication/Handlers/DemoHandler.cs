using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Sql;
using System.Data.SqlClient;
using BRApplication.Models;
using BRApplication.Handlers;
using System.Data;
using System.Diagnostics;

namespace BRApplication.Handlers
{
    public class DemoHandler
    {
        public bool InsertStatus(DemoModel Status)
        {
            DataAccessLayer dal = new DataAccessLayer();
            bool Success = true;
            try
            {
                /* Testing Insert */
                Dictionary<string, string> dict = new Dictionary<string,string>();
                dict.Add("StatusName", Status.StatusName);
                // no need to add statusID, as that column is an identity column
                dal.insert(dict, "TransactionStatus");

                /* Testing Select */
                DataTable dt = new DataTable();
                dt = dal.select("StatusName = 'a'", "TransactionStatus");
                foreach (DataRow row in dt.Rows) // Loop over the rows.
                {
                    foreach (var col in row.ItemArray)
                    {
                        Debug.Write(col);
                    }
                    Debug.Write('\n');
                }

                /* Testing Update */
                Dictionary<string, string> d = new Dictionary<string, string>();
                d.Add("StatusName", "'asma_rox'");
                dal.update("TransactionStatus", "StatusName = 'asma_sux'", d);
                /* Manually go into SQL Server to see if the row is correctly updated */

                /* Testing Delete */
                dal.delete("StatusName = 'johnny'", "TransactionStatus");
                /* now the db should not contain an entry with StatusName = 'johnny' */

            }
            catch (Exception)
            {
                Success = false;
            }
            return Success; 
        }
    }
}