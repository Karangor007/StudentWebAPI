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

        #region Student


        #region All Students
        //Get All
        public static List<Dictionary<string, object>> getStudents()
        {

            List<Student> studentList = new List<Student>();
            List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
            Dictionary<string, object> row;
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            SqlConnection conn = new SqlConnection();
            conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Conn"].ConnectionString);

            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }

            string query = "select * from mst_students";

            SqlDataAdapter adp = new SqlDataAdapter(query, conn);
            adp.Fill(ds);

            dt = ds.Tables[0];
            if (dt.Rows.Count <= 0)
            {
                row = new Dictionary<string, object>();
                row.Add("Message", "Records Not Found");
                rows.Add(row);

            }
            //System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();

            else
            {
                foreach (DataRow dr in dt.Rows)
                {
                    row = new Dictionary<string, object>();
                    foreach (DataColumn col in dt.Columns)
                    {
                        row.Add(col.ColumnName, dr[col]);
                    }
                    rows.Add(row);
                }

            }



            //string jj = serializer.Serialize(rows);
            return rows;
        }
        #endregion
        // Get By Id
        #region Get Student By Id
        public static List<Dictionary<string, object>> getStudentById(Student param)
        {
            Student obj = new Student();
            obj.id = param.id;
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
            Dictionary<string, object> row;
            SqlConnection conn = new SqlConnection();
            conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Conn"].ConnectionString);

            string query = "select * from mst_students where id='" + obj.id + "'";
            SqlDataAdapter adp = new SqlDataAdapter(query, conn);
            adp.Fill(ds);
            dt = ds.Tables[0];

            if (dt.Rows.Count <= 0)
            {
                row = new Dictionary<string, object>();
                row.Add("Message", "Student Not Found");
                rows.Add(row);

            }
            else
            {
                foreach (DataRow dr in dt.Rows)
                {
                    row = new Dictionary<string, object>();
                    foreach (DataColumn col in dt.Columns)
                    {
                        row.Add(col.ColumnName, dr[col]);
                    }
                    rows.Add(row);
                }
            }

            return rows;
        }
        #endregion
        //Post
        #region Post Students
        public static List<Dictionary<string, object>> postStudent(Student param)
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
        public static List<Dictionary<string, object>> PutStudent(Student param)
        {
            List<Dictionary<string, object>> rows;
            Student obj = new Student();
            obj.name = param.name.Trim();
            obj.rollno = param.rollno.Trim();
            obj.id = param.id;
            bool flag = StudentsData.studentExist(obj.id);
            if (flag)
            {
                rows = new List<Dictionary<string, object>>();
                Dictionary<string, object> row;
                row = new Dictionary<string, object>();
                row.Add("Message", "Student Not Found");
                rows.Add(row);

            }
            else
            {
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
                rows = getStudents();

            }

            return rows;

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

            string query = "select * from mst_students where id='" + obj.id + "'";
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
        public static List<Dictionary<string, object>> deleteStudent(Student param)
        {


            Student obj = new Student();
            List<Dictionary<string, object>> rows;


            obj.id = param.id;
            bool flag = StudentsData.studentExist(obj.id);
            if (flag)
            {
                rows = new List<Dictionary<string, object>>();
                Dictionary<string, object> row;
                row = new Dictionary<string, object>();
                row.Add("Message", "Student Not Found");
                rows.Add(row);

            }
            else
            {
                SqlConnection conn = new SqlConnection();
                conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Conn"].ConnectionString);

                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                string query = "delete mst_students where id = '" + obj.id + "'";
                SqlCommand com = new SqlCommand(query, conn);
                com.ExecuteNonQuery();
                conn.Close();

                rows = getStudents();
            }

            return rows;
        }
        #endregion

        #endregion

        

    }

    public class StudentMarks
    {
        #region Marks
        #region Get All Marks
        public static List<Dictionary<string, object>> getMarks()
        {
            List<Student> studentList = new List<Student>();
            List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
            Dictionary<string, object> row;
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            SqlConnection conn = new SqlConnection();
            conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Conn"].ConnectionString);

            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }

            string query = "select * from mst_marks";

            SqlDataAdapter adp = new SqlDataAdapter(query, conn);
            adp.Fill(ds);

            dt = ds.Tables[0];
            if (dt.Rows.Count <= 0)
            {
                row = new Dictionary<string, object>();
                row.Add("Message", "Records Not Found");
                rows.Add(row);

            }
            //System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();

            else
            {
                foreach (DataRow dr in dt.Rows)
                {
                    row = new Dictionary<string, object>();
                    foreach (DataColumn col in dt.Columns)
                    {
                        row.Add(col.ColumnName, dr[col]);
                    }
                    rows.Add(row);
                }

            }



            //string jj = serializer.Serialize(rows);
            return rows;
        }

        #endregion
        #region Get Marks By Student Id
        public static List<Dictionary<string, object>> getMarksByStudentId(Marks param)
        {
            Marks obj = new Marks();
            obj.studentId = param.studentId;
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
            Dictionary<string, object> row;
            SqlConnection conn = new SqlConnection();
            conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Conn"].ConnectionString);

            string query = "select * from mst_marks where student_id='" + obj.studentId + "'";
            SqlDataAdapter adp = new SqlDataAdapter(query, conn);
            adp.Fill(ds);
            dt = ds.Tables[0];

            if (dt.Rows.Count <= 0)
            {
                row = new Dictionary<string, object>();
                row.Add("Message", "Student Not Found");
                rows.Add(row);

            }
            else
            {
                foreach (DataRow dr in dt.Rows)
                {
                    row = new Dictionary<string, object>();
                    foreach (DataColumn col in dt.Columns)
                    {
                        row.Add(col.ColumnName, dr[col]);
                    }
                    rows.Add(row);
                }
            }

            return rows;
        }
        #endregion

        #region Post Marks
        public static List<Dictionary<string, object>> postMarks(Marks param)
        {
            Marks obj = new Marks ();
            List<Dictionary<string, object>> rows;
            obj.id = param.id;
            obj.studentId = param.studentId;
            obj.marks = param.marks;
            bool flag = studentExist(obj.studentId);
            if (flag)
            {
                rows = new List<Dictionary<string, object>>();
                Dictionary<string, object> row;
                row = new Dictionary<string, object>();
                row.Add("Message", "Student Not Found");
                rows.Add(row);
            }
            else
            {
                SqlConnection conn = new SqlConnection();
                conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Conn"].ConnectionString);

                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                string query = "insert into mst_marks values('" + obj.studentId + "','" + obj.marks + "')";
                SqlCommand com = new SqlCommand(query, conn);
                com.ExecuteNonQuery();
                conn.Close();
                rows = getMarks();
            }
           

            return rows;


        }
        #endregion

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

            string query = "select * from mst_students where id='" + obj.id + "'";
            SqlCommand com = new SqlCommand(query, conn);
            int count = Convert.ToInt32(com.ExecuteScalar());
            if (count <= 0)
            {
                flag = true;
            }
            conn.Close();

            return flag;
        }

        public static bool studentMarksExist(int id)
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

            string query = "select * from mst_marks where student_id='" + obj.id + "'";
            SqlCommand com = new SqlCommand(query, conn);
            int count = Convert.ToInt32(com.ExecuteScalar());
            if (count <= 0)
            {
                flag = true;
            }
            conn.Close();

            return flag;
        }

        public static bool MarksExist(int id)
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

            string query = "select * from mst_marks where id='" + obj.id + "'";
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

        #region PUT Marks
        public static List<Dictionary<string, object>> PutMarks(Marks param)
        {
            List<Dictionary<string, object>> rows;
            Marks obj = new Marks();
            
            obj.id = param.id;
            obj.studentId = param.studentId;
            obj.marks = param.marks;
            bool flag = studentMarksExist(obj.studentId);
            if (flag)
            {
                rows = new List<Dictionary<string, object>>();
                Dictionary<string, object> row;
                row = new Dictionary<string, object>();
                row.Add("Message", "Student Not Found");
                rows.Add(row);

            }
            else
            {
                SqlConnection conn = new SqlConnection();
                conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Conn"].ConnectionString);

                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                string query = "update mst_marks set marks = '" + obj.marks + "' where student_id = '" + obj.studentId + "'";
                SqlCommand com = new SqlCommand(query, conn);
                com.ExecuteNonQuery();
                conn.Close();
                rows = getMarks();

            }

            return rows;

        }
        #endregion

        #region Delete Marks
        public static List<Dictionary<string, object>> deleteMarks(Marks param)
        {


            Marks obj = new Marks();
            List<Dictionary<string, object>> rows;


            obj.id = param.id;
            bool flag = MarksExist(obj.id);
            if (flag)
            {
                rows = new List<Dictionary<string, object>>();
                Dictionary<string, object> row;
                row = new Dictionary<string, object>();
                row.Add("Message", "Marks Not Found");
                rows.Add(row);

            }
            else
            {
                SqlConnection conn = new SqlConnection();
                conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Conn"].ConnectionString);

                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                string query = "delete mst_marks where id = '" + obj.id + "'";
                SqlCommand com = new SqlCommand(query, conn);
                com.ExecuteNonQuery();
                conn.Close();

                rows = getMarks();
            }

            return rows;
        }
        #endregion
    }
}