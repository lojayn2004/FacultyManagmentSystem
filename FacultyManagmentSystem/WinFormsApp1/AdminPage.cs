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
    public partial class AdminPage : Form
    {
        public AdminPage()
        {
            InitializeComponent();
            comboBox1.Items.Add("Number of students in department");
            comboBox1.Items.Add("Show All Faculty Courses");
            comboBox1.Items.Add("Number of students in course");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ManageCoursesForm crsForm = new ManageCoursesForm();
            crsForm.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            AddDepartmentForm deptForm = new AddDepartmentForm();
            deptForm.Show();
        }

        private void AdminPage_Load(object sender, EventArgs e)
        {

        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedValue = comboBox1.SelectedItem.ToString();
            if (selectedValue == null)
                return;
            SqlConnection sqlConnection = DatabaseManager.GetSqlConnection();
            sqlConnection.Open();
            if (selectedValue == "Show All Faculty Courses")
            {

                SqlDataAdapter sqlDa = new SqlDataAdapter("select course_name , course_code from course", sqlConnection);
                DataTable dtbl = new DataTable();
                sqlDa.Fill(dtbl);
                dataGridView1.DataSource = dtbl;


            }
            else if (selectedValue == "Number of students in department")
            {

                SqlDataAdapter sqlDa = new SqlDataAdapter("select deptName as DepartmentName ,student.deptID as DepartmentID , count(userID) as numberOfStudents from student ,department where student.deptID = department.deptID group by student.deptID , deptName", sqlConnection);
                DataTable dtbl = new DataTable();
                sqlDa.Fill(dtbl);
                dataGridView1.DataSource = dtbl;
            }
            else if (selectedValue == "Number of students in course")
            {

                SqlDataAdapter sqlDa = new SqlDataAdapter("select course_name , course.course_code , count(enrollment.userID) as numberOfStudents\r\nfrom course inner join enrollment on course.course_code = enrollment.course_code\r\ngroup by course.course_code , course.course_name", sqlConnection);
                DataTable dtbl = new DataTable();
                sqlDa.Fill(dtbl);
                dataGridView1.DataSource = dtbl;
            }



            sqlConnection.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Enrollment enrollStudent = new Enrollment();
            enrollStudent.Show();

        }

        private void button4_Click(object sender, EventArgs e)
        {
            Enrollment enrollStudent = new Enrollment();
            enrollStudent.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            showOnOrMoreCriteria showCriteria = new showOnOrMoreCriteria();
            showCriteria.Show();
        }
    }
}
