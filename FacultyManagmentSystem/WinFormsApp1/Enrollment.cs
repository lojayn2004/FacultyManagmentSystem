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
    public partial class Enrollment : Form
    {
        private void fillAllStudentsGrid()
        {
            SqlConnection sql = DatabaseManager.GetSqlConnection();
            sql.Open();
            string query = "select firstName, student.userID , deptID\r\n from student , " +
                "users\r\n where student.userID = users.userID";
            SqlDataAdapter adp = new SqlDataAdapter(query, sql);
            DataTable tble = new DataTable(); adp.Fill(tble);
            dataGridView1.DataSource = tble;
            sql.Close();
        }
        public Enrollment()
        {
            InitializeComponent();
            fillAllStudentsGrid();

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            SqlConnection connection = DatabaseManager.GetSqlConnection();
            connection.Open(); // Open the connection
            int result; // Declare an integer variable to store the result
            int num = 0;
            using (SqlCommand pre_cmd = new
                SqlCommand("SELECT COUNT(*) FROM prerequisite,enrollment WHERE enrollment.userID = @UserID AND prerequisite.course_code = @code AND enrollment.course_code = prerequisite.pre_course_code", connection))
            {
                pre_cmd.Parameters.AddWithValue("@UserID", int.Parse(textBox1.Text));
                pre_cmd.Parameters.AddWithValue("@code", textBox2.Text);
                // Execute the SQL command
                pre_cmd.ExecuteNonQuery();
                // Execute the command and store the result in the variable
                result = (int)pre_cmd.ExecuteScalar();
            }


            if (result == 0)
            {
                using (SqlCommand pre_cmd = new SqlCommand("SELECT COUNT(*) FROM prerequisite WHERE  prerequisite.course_code = @code", connection))
                {
                    pre_cmd.Parameters.AddWithValue("@code", textBox2.Text);
                    // Execute the SQL command
                    pre_cmd.ExecuteNonQuery();
                    // Execute the command and store the result in the variable
                    num = (int)pre_cmd.ExecuteScalar();
                }
            }
            if (num == 0 || result == 1)
            {
                // Create a new SqlCommand --> Enroll 
                SqlCommand cmd = new SqlCommand("insert into enrollment values (@userID, @course_code, @grade, @years, @semester)", connection);
                cmd.Parameters.AddWithValue("@userID", int.Parse(textBox1.Text));
                cmd.Parameters.AddWithValue("@course_code", textBox2.Text);
                if(textBox3.Text.Trim() == "")
                    cmd.Parameters.AddWithValue("@grade",0);
                else
                   cmd.Parameters.AddWithValue("@grade", int.Parse(textBox3.Text));
                cmd.Parameters.AddWithValue("@years", int.Parse(textBox4.Text));
                cmd.Parameters.AddWithValue("@semester", textBox5.Text);

                // Execute the SQL command
                cmd.ExecuteNonQuery();

                // Update credit_hours
                SqlCommand course_hours = new SqlCommand("select credit_hours from course where course_code = @code", connection);
                course_hours.Parameters.AddWithValue("@code", textBox2.Text);
                SqlDataReader reader = course_hours.ExecuteReader();
                int c_hours = 0;
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        c_hours = int.Parse(reader["credit_hours"].ToString());

                    }
                }

                reader.Close();
                SqlCommand hours = new SqlCommand("update student set total_credit_hours = total_credit_hours + @hours where userID = @userID  ", connection);
                hours.Parameters.AddWithValue("@hours", c_hours);
                hours.Parameters.AddWithValue("@userID", int.Parse(textBox1.Text));
                hours.ExecuteNonQuery();


                // calc GPA 
                SqlCommand myCourse = new SqlCommand("select course.credit_hours, enrollment.grade from course inner join enrollment on enrollment.userID =  @userID ", connection);
                myCourse.Parameters.AddWithValue("@userID", int.Parse(textBox1.Text));
                SqlDataReader courses = myCourse.ExecuteReader();
                float count_hours = 0, all_grades = 0;
                if (courses.HasRows)
                {
                    while (courses.Read())
                    {
                        float h = float.Parse(courses["credit_hours"].ToString());
                        float g = float.Parse(courses["grade"].ToString());
                        count_hours += h;
                        float n = g * 4 * h / 100;
                        all_grades += n;
                    }
                }
                courses.Close();
                float myGpa = all_grades / count_hours;
                SqlCommand gpa = new SqlCommand("update student set gpa = @gpa where userID = @id", connection);
                gpa.Parameters.AddWithValue("@gpa", myGpa);
                gpa.Parameters.AddWithValue("@id", int.Parse(textBox1.Text));
                gpa.ExecuteNonQuery();
                updateStudentInfo();



            }
            else
            {

                // Display an alert box with a message
                MessageBox.Show("This course has Prerequisite Course not Enrolled to student.", "Alert");

            }

            // Close the connection after executing the command
            connection.Close();



        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnection connection = DatabaseManager.GetSqlConnection();
            connection.Open(); // Open the connection


            SqlCommand cmd = new SqlCommand("UPDATE student SET deptID = @DeptID WHERE userID = @UserID", connection);
            cmd.Parameters.AddWithValue("@DeptID", textBox6.Text);
            cmd.Parameters.AddWithValue("@UserID", int.Parse(textBox1.Text));
            // Execute the SQL command
            cmd.ExecuteNonQuery();

            // Close the connection after executing the command
            connection.Close();
            updateStudentInfo();

        }

        private void updateStudentInfo()
        {
            SqlConnection connection = DatabaseManager.GetSqlConnection();
            connection.Open(); // Open the connection

            SqlCommand cmd = new SqlCommand("select * from student where userID = @userID", connection);
            cmd.Parameters.AddWithValue("@UserID", int.Parse(textBox1.Text));

            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            dataGridView2.DataSource = dt;

            connection.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            SqlConnection connection = DatabaseManager.GetSqlConnection();
            connection.Open(); // Open the connection

            SqlCommand cmd = new SqlCommand("select * from student where userID = @userID", connection);
            cmd.Parameters.AddWithValue("@UserID", int.Parse(textBox1.Text));

            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            dataGridView2.DataSource = dt;

            connection.Close();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
