using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Web;
using log4net;

namespace WebFormsDBI18NDemo
{
    internal static class SqlResourceHelper
    {
        static ILog log = LogManager.GetLogger(typeof(SqlResourceHelper));

        public static IDictionary GetResources(string virtualPath,
          string className, string cultureName,
          bool designMode, IServiceProvider serviceProvider)
        {
            if (log.IsDebugEnabled)
            {
                log.Debug("Virtual Path: " + virtualPath + " Class Name: " + className + " Culture Name: " + cultureName);
            }
            SqlConnection con = new SqlConnection(
              System.Configuration.ConfigurationManager.
              ConnectionStrings["SqlResource"].ToString());
            SqlCommand com = new SqlCommand();
            //
            // Build correct select statement to get resource values
            //
            if (!String.IsNullOrEmpty(virtualPath))
            {
                //
                // Get Local resources
                //
                if (string.IsNullOrEmpty(cultureName))
                {
                    // default resource values (no culture defined)
                    com.CommandType = CommandType.Text;
                    com.CommandText = "select resource_name, resource_value" +
                                      " from Resource" +
                                      " where resource_type = @virtual_path" +
                                      " and culture_code is null";
                    com.Parameters.AddWithValue("@virtual_path", virtualPath);
                }
                else
                {
                    com.CommandType = CommandType.Text;
                    com.CommandText = "select resource_name, resource_value" +
                                      " from Resource " +
                                      "where resource_type = @virtual_path " +
                                      "and culture_code = @culture_name ";
                    com.Parameters.AddWithValue("@virtual_path", virtualPath);
                    com.Parameters.AddWithValue("@culture_name", cultureName);
                }
            }
            else if (!String.IsNullOrEmpty(className))
            {
                //
                // Get Global resources
                // 
                if (string.IsNullOrEmpty(cultureName))
                {
                    // default resource values (no culture defined)
                    com.CommandType = CommandType.Text;
                    com.CommandText = "select resource_name, resource_value" +
                                      " from Resource " +
                                      "where resource_type = @class_name" +
                                      " and culture_code is null";
                    com.Parameters.AddWithValue("@class_name", className);
                }
                else
                {
                    com.CommandType = CommandType.Text;
                    com.CommandText = "select resource_name, resource_value " +
                                      "from Resource where " +
                                      "resource_type = @class_name and" +
                                      " culture_code = @culture_name ";
                    com.Parameters.AddWithValue("@class_name", className);
                    com.Parameters.AddWithValue("@culture_name", cultureName);
                }
            }
            else
            {
                //
                // Neither virtualPath or className provided,
                // unknown if Local or Global resource
                //
                throw new Exception("SqlResourceHelper.GetResources()" +
                      " - virtualPath or className missing from parameters.");
            }
            ListDictionary resources = new ListDictionary();
            try
            {
                com.Connection = con;
                con.Open();
                SqlDataReader sdr = com.ExecuteReader(CommandBehavior.CloseConnection);

                while (sdr.Read())
                {
                    string rn = sdr.GetString(sdr.GetOrdinal("resource_name"));
                    string rv = sdr.GetString(sdr.GetOrdinal("resource_value"));
                    resources.Add(rn, rv);
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }

            return resources;
        }

        
    }
}