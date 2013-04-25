﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BRApplication.Models;
using System.Data.SqlClient;
using System.Data;
using System.Diagnostics;

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
                    string insertCommand = "INSERT INTO " + TableName + " ( ";
                    bool first = true;
                    foreach (var column in ColumnValuePairs)
	                {
                        if (first)
                        {
                            insertCommand += column.Key;
                            first = false;
                        }
                        else
                        {
                            insertCommand += (", " + column.Key);
                        }
	                }

                    insertCommand += " ) VALUES ( ";
                    first = true;
                    foreach (var column in ColumnValuePairs)
	                {
                        if (first)
                        {
                            insertCommand += ("@" + column.Key);
                            first = false;
                        }
                        else
                        {
                            insertCommand += (", @" + column.Key);
                        }
	                }
                    insertCommand += " )";
                   
                    command.CommandText = insertCommand + " SELECT SCOPE_IDENTITY()";
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
                string selectCommand = "SELECT ";

                int i = 0;
                int numColumns = ColumnNames.Count();
                while (i < numColumns)
                {
                    selectCommand += ColumnNames[i];
                    if ((i + 1) == numColumns) break;

                    selectCommand += ", ";
                    i++;
                }

                selectCommand += " FROM {0}";
                
                if (WhereClause != "")
                {
                    selectCommand = selectCommand + " WHERE " + WhereClause;
                }
                int temp = Array.IndexOf(WhiteListOfTableNames, TableName);
                if (temp < 0)
                {
                    throw new Exception();
                }
                string ct = String.Format(selectCommand, TableName);
                cmd.CommandText = ct;
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
                string updateCommand = "UPDATE {0} SET ";
                updateCommand = string.Format(updateCommand, TableName);

                bool first = true;
                foreach (var column in newColumnValues)
                {
                    if (first)
                    {
                        updateCommand += column.Key;
                        updateCommand += "=";
                        updateCommand += column.Value;
                        first = false;
                    }
                    else
                    {
                        updateCommand += ", ";
                        updateCommand += column.Key;
                        updateCommand += "=";
                        updateCommand += column.Value;
                    }
                }

                if (WhereClause != "")
                {
                    updateCommand = updateCommand + " WHERE " + WhereClause;
                }

                command.CommandText = updateCommand;
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
