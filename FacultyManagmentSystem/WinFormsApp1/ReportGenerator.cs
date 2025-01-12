using DataBaseConnection;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsApp1
{
    public partial class ReportGenerator : Form
    {
        public ReportGenerator()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnection conn = DatabaseManager.GetSqlConnection();

            conn.Open();
            string query1 = "select top 10 firstName ,lastName , student.userID , " +
                "grade as TotalGrades  , gpa \r\n from student" +
                " , enrollment ,users\r\n where  student.userID = enrollment.userID and " +
                "student.userID = users.userID\r\n group by student.userID , gpa, grade, firstName" +
                " ,lastName\r\n order by gpa desc , grade desc;";
           
            SqlDataAdapter sql1 = new SqlDataAdapter(query1, conn);
            DataTable dtbl1 = new DataTable();
            sql1.Fill(dtbl1);
            dataGridView1.DataSource = dtbl1;
            string query2 = " select firstName, lastName ,enrollment.userID," +
                " count(course_code) as ToTalNumberOfEnrolledCourses , total_credit_hours" +
                " as TotalTakenHours\r\n from  enrollment , student , users\r\n where" +
                " enrollment.userID = student.userID and users.userID = student.userID\r\n" +
                " group by enrollment.userID , firstName,lastName, total_credit_hours\r\n\r\n\r\n";
            
            SqlDataAdapter sql2 = new SqlDataAdapter(query2, conn);
            DataTable dtbl2 = new DataTable();

            sql2.Fill(dtbl2);
            dataGridView2.DataSource = dtbl2;
            conn.Close();
        }
    }
}
