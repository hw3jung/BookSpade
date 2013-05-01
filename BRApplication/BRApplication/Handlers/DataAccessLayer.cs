using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BRApplication.Models;
using System.Data.SqlClient;
using System.Data;
using System.Diagnostics;
using System.Text; 

namespace BRApplication.Handlers
{
    public class DataAccessLayer
    {
        // White list of valid table names to prevent SQL injection
        String[] WhiteListOfTableNames = new String[] { "Posts", "CourseInfo", "TextBooks", "Transactions", "TransactionStatus", "UserProfile", "Bids" };

        #region insert 

        public int insert(Dictionary<string, string> ColumnValuePairs, string TableName)
        {
            string connString = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["BookRack"].ToString();
            SqlConnection conn = new SqlConnection(connString);
            int newId = -1; 
            using (SqlCommand command = conn.CreateCommand())
            {
                try
                {
                    StringBuilder insertCommand = new StringBuilder("INSERT INTO ");
                    insertCommand.Append(TableName).Append(" ( "); //faster than using + concatenation
                    bool first = true;
                    foreach (var column in ColumnValuePairs)
	                {
                        if (first)
                        {
                            insertCommand.Append(column.Key);
                            first = false;
                        }
                        else
                        {
                            insertCommand.Append(", ").Append(column.Key);
                        }
	                }

                    insertCommand.Append(" ) VALUES ( ");
                    first = true;
                    foreach (var column in ColumnValuePairs)
	                {
                        if (first)
                        {
                            insertCommand.Append("@").Append(column.Key);
                            first = false;
                        }
                        else
                        {
                            insertCommand.Append(", @").Append(column.Key);
                        }
	                }
                    insertCommand.Append(" )");
                   
                    command.CommandText = insertCommand.Append(" SELECT SCOPE_IDENTITY()").ToString();
                    foreach (var pair in ColumnValuePairs)
                    {
                        command.Parameters.AddWithValue("@" + pair.Key, pair.Value);
                    }
                    conn.Open();
                    newId = Convert.ToInt32(command.ExecuteScalar());
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.StackTrace);
                }
                finally
                {
                    conn.Close();
                }
            }
            return newId;
        }

        #endregion

        #region Select

        // WhereClause is either "" or of the form "Col = value AND/OR Col2 = value2..."

        public DataTable select(string WhereClause, string TableName, string[] ColumnNames = null)
        {
            ColumnNames = ColumnNames ?? new string[] { "*" };
            
            string connString = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["BookRack"].ToString();
            SqlConnection conn = new SqlConnection(connString);
            DataTable dt = new DataTable();
            SqlCommand cmd = conn.CreateCommand();
            try
            {
                StringBuilder selectCommand = new StringBuilder();
                selectCommand.Append("SELECT "); 

                int i = 0;
                int numColumns = ColumnNames.Count();
                while (i < numColumns)
                {
                    selectCommand.Append(ColumnNames[i]);
                    if ((i + 1) == numColumns) break;
                    selectCommand.Append(", ");
                    i++;
                }

                selectCommand.Append(" FROM ").Append(TableName);
                
                if (WhereClause != "")
                {
                    selectCommand.Append(" WHERE ").Append(WhereClause);
                }
                int temp = Array.IndexOf(WhiteListOfTableNames, TableName);
                if (temp < 0)
                {
                    throw new Exception();
                }
                cmd.CommandText = selectCommand.ToString();
                conn.Open();

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                da.Dispose();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }
            finally
            {
                conn.Close();
            }

            return dt;
        }

        #endregion

        #region delete
        // WhereClause is either empty or of the form column=value etc
        public void delete(string WhereClause, string TableName)
        {
            string connString = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["BookRack"].ToString();
            SqlConnection conn = new SqlConnection(connString);
            SqlCommand command = new SqlCommand();
            command = conn.CreateCommand();

            try
            {
                string deleteCommand = "DELETE FROM {0} ";
                deleteCommand = string.Format(deleteCommand, TableName);

                if (WhereClause != "")
                {
                    deleteCommand = deleteCommand + "WHERE " + WhereClause;
                }

                command.CommandText = deleteCommand;
                conn.Open();
                command.ExecuteNonQuery();

            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }
            finally
            {
                conn.Close();
            }
        }

        #endregion

        #region update
        // WhereClause is either empty or of the form column=value etc
        public void update(string TableName, string WhereClause, Dictionary<string, string> newColumnValues)
        {
            string connString = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["BookRack"].ToString();
            SqlConnection conn = new SqlConnection(connString);
            SqlCommand command = new SqlCommand();
            command = conn.CreateCommand();
            try
            {
                StringBuilder updateCommand = new StringBuilder("UPDATE ").Append(TableName).Append(" SET ");
               
                bool first = true;
                foreach (var column in newColumnValues)
                {
                    if (first)
                    {
                        updateCommand.Append(column.Key).Append("=").Append(column.Value);
                        first = false;
                    }
                    else
                    {
                        updateCommand.Append(", ").Append(column.Key).Append("=").Append(column.Value);
                    }
                }

                if (WhereClause != String.Empty)
                {
                    updateCommand = updateCommand.Append(" WHERE ").Append(WhereClause);
                }

                command.CommandText = updateCommand.ToString();
                conn.Open();
                command.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }
            finally
            {
                conn.Close();
            }
        }

        #endregion

    }
}
