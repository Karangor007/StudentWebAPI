using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using Newtonsoft.Json;

namespace StudentWebAPI.Models
{
    public class DBMethods
    {
    }

    public class StudentsData
    {

        #region All Students
        //Get All
        public static string getStudents()
        {
            string json = "";
            List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
            Dictionary<string, object> row;
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            SqlConnection conn = new SqlConnection();
            conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Conn"].ConnectionString);

            string query = "select * from mst_students";
            SqlDataAdapter adp = new SqlDataAdapter(query, conn);
            adp.Fill(ds);
            dt = ds.Tables[0];
            if (dt.Rows.Count <= 0)
            {
                row = new Dictionary<string, object>();
                row.Add("Message", "Records Not Found");
                rows.Add(row);
                json = JsonConvert.SerializeObject(row);
            }
            //System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();

            else
            {
                json = JsonConvert.SerializeObject(dt, Formatting.Indented);
            }

            
            
            //string jj = serializer.Serialize(rows);
            return json;
        }
        #endregion
        // Get By Id
        #region Get Student By Id
        public static string getStudentById(int id)
        {
            string json = "";
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
            Dictionary<string, object> row;
            SqlConnection conn = new SqlConnection();
            conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Conn"].ConnectionString);

            string query = "select * from mst_students where id='" + id + "'";
            SqlDataAdapter adp = new SqlDataAdapter(query, conn);
            adp.Fill(ds);
            dt = ds.Tables[0];

            if (dt.Rows.Count <= 0)
            {
                row = new Dictionary<string, object>();
                row.Add("Message", "Student Not Found");
                rows.Add(row);
                json = JsonConvert.SerializeObject(row, Formatting.Indented);
            }
            else {
                json = JsonConvert.SerializeObject(dt, Formatting.Indented);
            }
            
            return json;
        }
        #endregion
        //Post
        #region Post Students
        public static string postStudent(Student param)
        {
            Student obj = new Student();
            obj.name = param.name.Trim();
            obj.rollno = param.rollno.Trim();
            SqlConnection conn = new SqlConnection();
            conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Conn"].ConnectionString);

            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }

            string query = "insert into mst_students values('" + obj.name + "','" + obj.rollno + "')";
            SqlCommand com = new SqlCommand(query, conn);
            com.ExecuteNonQuery();
            conn.Close();

            return getStudents();


        }
        #endregion

        #region PUT Students
        public static string PutStudent(Student param)
        {

            string json = "";

            Student obj = new Student();
            obj.name = param.name.Trim();
            obj.rollno = param.rollno.Trim();
            obj.id = param.id;
            bool flag = StudentsData.studentExist(obj.id);
            if (flag)
            {
                List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
                Dictionary<string, object> row;
                row = new Dictionary<string, object>();
                row.Add("Message","Student Not Found");
                rows.Add(row);
                json = JsonConvert.SerializeObject(row);
            }
            else {
                SqlConnection conn = new SqlConnection();
                conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Conn"].ConnectionString);

                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                string query = "update mst_students set name = '" + obj.name + "',rollno = '" + obj.rollno + "' where id = '" + obj.id + "'";
                SqlCommand com = new SqlCommand(query, conn);
                com.ExecuteNonQuery();
                conn.Close();

                json = getStudents();
            }

            return json;
            
        }

        public static bool studentExist(int id)
        {
            bool flag = false;
            SqlConnection conn = new SqlConnection();
            conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Conn"].ConnectionString);
            Student obj = new Student();
            obj.id = id;

            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }

            string query = "select * from mst_students where id='"+obj.id+"'";
            SqlCommand com = new SqlCommand(query, conn);
            int count = Convert.ToInt32(com.ExecuteScalar());
            if (count <= 0)
            {
                flag = true;
            }
            conn.Close();

            return flag;
        }

        #endregion

        #region Delete Students
        public static string deleteStudent(int id)
        {
            string json = "";

            Student obj = new Student();
            
            
            obj.id = id;
            bool flag = StudentsData.studentExist(obj.id);
            if (flag)
            {
                List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
                Dictionary<string, object> row;
                row = new Dictionary<string, object>();
                row.Add("Message", "Student Not Found");
                rows.Add(row);
                json = JsonConvert.SerializeObject(row);
            }
            else
            {
                SqlConnection conn = new SqlConnection();
                conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Conn"].ConnectionString);

                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                string query = "delete mst_students where id = '"+obj.id+"'";
                SqlCommand com = new SqlCommand(query, conn);
                com.ExecuteNonQuery();
                conn.Close();

                json = getStudents();
            }

            return json;
        }
        #endregion
    }
}