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

namespace WebFormsDBI18NDemo
{
    internal static class SqlResourceHelper
    {
        public static IDictionary GetResources(string virtualPath,
          string className, string cultureName,
          bool designMode, IServiceProvider serviceProvider)
        {
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

        //public static void AddResource(string resource_name,
        //       string virtualPath, string className, string cultureName)
        //{
        //    SqlConnection con =
        //      new SqlConnection(System.Configuration.ConfigurationManager.
        //      ConnectionStrings["SqlResource"].ToString());
        //    SqlCommand com = new SqlCommand();
        //    StringBuilder sb = new StringBuilder();
        //    sb.Append("insert into Resource " +
        //              "(resource_name ,resource_value," +
        //              "resource_type,culture_code ) ");
        //    sb.Append(" values (@resource_name ,@resource_value," +
        //              "@resource_object,@culture_name) ");
        //    com.CommandText = sb.ToString();
        //    com.Parameters.AddWithValue("@resource_name", resource_name);
        //    com.Parameters.AddWithValue("@resource_value", resource_name +
        //                                " * DEFAULT * " +
        //                                (String.IsNullOrEmpty(cultureName) ?
        //                                string.Empty : cultureName));
        //    com.Parameters.AddWithValue("@culture_name",
        //        (String.IsNullOrEmpty(cultureName) ? SqlString.Null : cultureName));

        //    string resource_object = "UNKNOWN **ERROR**";
        //    if (!String.IsNullOrEmpty(virtualPath))
        //    {
        //        resource_object = virtualPath;
        //    }
        //    else if (!String.IsNullOrEmpty(className))
        //    {
        //        resource_object = className;
        //    }
        //    com.Parameters.AddWithValue("@resource_object", resource_object);

        //    try
        //    {
        //        com.Connection = con;
        //        con.Open();
        //        com.ExecuteNonQuery();
        //    }
        //    catch (Exception e)
        //    {
        //        throw new Exception(e.ToString());
        //    }
        //    finally
        //    {
        //        if (con.State == ConnectionState.Open)
        //            con.Close();
        //    }
        //}
    }
}