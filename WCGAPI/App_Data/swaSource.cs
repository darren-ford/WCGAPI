using System;
using System.Linq;
using System.Web.Http;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections.Generic;
using System.Reflection;
using System.Net.Http;
using System.Net;
using System.Diagnostics;
using NLog;
namespace ControllerLibrary
{
static public class DbUtil 

{
static public string message;
	static public SqlConnection GetConnection() {
		string conStr = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
		SqlConnection con = new SqlConnection(conStr);
DbUtil.message = "";
con.InfoMessage += delegate(object sender, SqlInfoMessageEventArgs e)
{
if (DbUtil.message.Length > 0) DbUtil.message +=  "\n";
DbUtil.message +=  e.Message;
};
		return con;        
	}
}

    public static class DbExtensions
    {
        public static List<T> ToListCollection<T>(this DataTable dt)
        {
            List<T> lst = new System.Collections.Generic.List<T>();
            Type tClass = typeof(T);
            PropertyInfo[] pClass = tClass.GetProperties();
            List<DataColumn> dc = dt.Columns.Cast<DataColumn>().ToList();
            T cn;
            foreach (DataRow item in dt.Rows)
            {
                cn = (T)Activator.CreateInstance(tClass);
                foreach (PropertyInfo pc in pClass)
                {
                    // Can comment try catch block. 
                    try
                    {
                        DataColumn d = dc.Find(c => c.ColumnName == pc.Name);
                        if (d != null && item[pc.Name] != DBNull.Value)
                            pc.SetValue(cn, item[pc.Name], null);
                    }
                    catch (Exception ex)
                    {
                       throw ex;
                    }
                }
                lst.Add(cn);
            }
            return lst;
        }
    }
        
}

