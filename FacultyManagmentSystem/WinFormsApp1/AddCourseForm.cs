using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DataBaseConnection;

namespace WinFormsApp1
{
    public partial class AddCourseForm : Form
    {
        private bool doesNotHaveEmptyField()
        {
            if (textBox1.Text.Trim() == "")
                return false;
            if (textBox2.Text.Trim() == "")
                return false;
            if (textBox3.Text.Trim() == "")
                return false;
            if (textBox4.Text.Trim() == "")
                return false;
            if (textBox5.Text.Trim() == "")
                return false;
            
            if (richTextBox1.Text.Trim() == "")
                return false;
           
            return true;

        }

        private bool validCourseCreditHours()
        {

            if (int.TryParse(textBox3.Text, out int result))
            {
                return true;
            }
            else
            {

                return false;
            }

        }

        private bool validDepartmentId()
        {
            // if it is not an int 
            if (!int.TryParse(textBox3.Text, out int result))
            {
                return false;
            }
            else
            {
                // Check whether th department is in the universty or no 
                SqlConnection newConnection = DatabaseManager.GetSqlConnection();
                newConnection.Open();

                string query = "SELECT * FROM department WHERE deptId = @deptId";
                SqlCommand command = new SqlCommand(query, newConnection);
                command.Parameters.AddWithValue("@deptId", int.Parse(textBox6.Text));
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        newConnection.Close();
                        return true;
                    }
                    else
                    {
                        newConnection.Close();
                        return true;
                    }
                }

            }
        }
        private bool validCourseCapacity()
        {
            if (int.TryParse(textBox5.Text, out int result))
            {
                return true;
            }
            else
            {

                return false;
            }

        }

        private bool isCourseFound()
        {
            SqlConnection newConnection = DatabaseManager.GetSqlConnection();
            newConnection.Open();

            string query = "SELECT * FROM course WHERE course_code = @course";
            SqlCommand command = new SqlCommand(query, newConnection);
            command.Parameters.AddWithValue("@course", textBox1.Text);
            bool found = false;
            using (SqlDataReader reader = command.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    found = true;
                }
                reader.Close();
            }
            newConnection.Close();
            return found;
        }
        public AddCourseForm()
        {
            InitializeComponent();
        }

        private void AddCourseForm_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnection connect = DatabaseManager.GetSqlConnection();
            connect.Open();


            // VALIDATE ALL FILEDS ARE FILLED AND WITH COMPATIBLE DATATYPES 
            if (doesNotHaveEmptyField() == false)
            {
                MessageBox.Show("All Fields are required");

            }
            else if (validCourseCreditHours() == false)
            {
                MessageBox.Show("Course Credit Hours Must be a number");
            }
            else if (validCourseCapacity() == false)
            {
                MessageBox.Show("Course Capacity Must be a number");
            }
            else if (validDepartmentId() == false)
            {
                MessageBox.Show("Please Enter A valid department Id, That belongs to universty");
            }
            // Must here add a check also that professor Id is valid 
            else
            {
                //try
                {
                    // GET DATA ENTERED BY USER
                    string courseCode = textBox1.Text;
                    string courseName = textBox2.Text;
                    string creditHours = textBox3.Text;
                    string location = textBox4.Text;
                    string capacity = textBox5.Text;
                    string description = richTextBox1.Text;
                    string deptId = textBox6.Text;
                    string professorId = textBox7.Text;
                    string query = "insert into course values " +
                        "(@course_code , @course_name, @credit_hours, @locations, @capacity, @descriptions, @userId, @deptId)";

                    SqlCommand exceuteQuery = new SqlCommand(query, connect);
                    exceuteQuery.Parameters.AddWithValue("@course_code", courseCode);
                    exceuteQuery.Parameters.AddWithValue("@course_name", courseName);
                    exceuteQuery.Parameters.AddWithValue("@credit_hours", int.Parse(creditHours));
                    exceuteQuery.Parameters.AddWithValue("@locations", location);
                    exceuteQuery.Parameters.AddWithValue("@capacity", int.Parse(capacity));
                    exceuteQuery.Parameters.AddWithValue("@descriptions", description);
                    if (professorId != "")
                    {
                        exceuteQuery.Parameters.AddWithValue("@userId", int.Parse(professorId));
                    }
                    else
                    {
                        exceuteQuery.Parameters.AddWithValue("@userId", null);
                    }

                    if (deptId != "")
                    {
                        exceuteQuery.Parameters.AddWithValue("@deptId", int.Parse(deptId));
                    }
                    else
                    {
                        exceuteQuery.Parameters.AddWithValue("@deptId", null);
                    }
                    exceuteQuery.ExecuteNonQuery();
                    this.Close();
                    connect.Close();
                    MessageBox.Show("Course Registered Successfully");
                }
                //catch (Exception ex)
                {
                   // MessageBox.Show("Error Occured While regestring a course Please Try Again");
                }

            }
        }
        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (isCourseFound() == false)
            {
                MessageBox.Show("Invalid Course Code");
                return;
            }
            SqlConnection newConnection = DatabaseManager.GetSqlConnection();
            newConnection.Open();
            if (textBox2.Text.Trim() != "")
            {
                string query = "update course set course_name = @name where course_code = @code";
                SqlCommand command = new SqlCommand(query, newConnection);
                command.Parameters.AddWithValue("@name", textBox2.Text);
                command.Parameters.AddWithValue("@code", textBox1.Text);
                command.ExecuteNonQuery();

            }
            if (textBox3.Text.Trim() != "")
            {
                if (validCourseCreditHours())
                {
                    string query = "update course set credit_hours = @credit where course_code = @code";
                    SqlCommand command = new SqlCommand(query, newConnection);
                    command.Parameters.AddWithValue("@credit", textBox3.Text);
                    command.Parameters.AddWithValue("@code", textBox1.Text);
                    command.ExecuteNonQuery();

                }
                else
                {
                    MessageBox.Show("Credit Hours Must be a number");
                }
            }

            if (textBox4.Text.Trim() != "")
            {
                string query = "update course set locations = @location where course_code = @code";
                SqlCommand command = new SqlCommand(query, newConnection);
                command.Parameters.AddWithValue("@location", textBox4.Text);
                command.Parameters.AddWithValue("@code", textBox1.Text);
                command.ExecuteNonQuery();
            }
            if (textBox5.Text.Trim() != "")
            {
                if (validCourseCapacity())
                {
                    string query = "update course set capacity = @capacity where course_code = @code";
                    SqlCommand command = new SqlCommand(query, newConnection);
                    command.Parameters.AddWithValue("@capacity", textBox5.Text);
                    command.Parameters.AddWithValue("@code", textBox1.Text);
                    command.ExecuteNonQuery();

                }
                else
                {
                    MessageBox.Show("Course Capacity Must be a number");
                }
            }
            if (textBox6.Text.Trim() != "")
            {
                if (validDepartmentId())
                {
                    string query = "update course set deptID = @deptID where course_code = @code";
                    SqlCommand command = new SqlCommand(query, newConnection);
                    command.Parameters.AddWithValue("@deptID", textBox6.Text);
                    command.Parameters.AddWithValue("@code", textBox1.Text);
                    command.ExecuteNonQuery();

                }
                else
                {
                    MessageBox.Show("Please Enter A valid department Id, That belongs to universty");
                }

            }
            if (richTextBox1.Text.Trim() != "")
            {
                string query = "update course set descriptions = @description where course_code = @code";
                SqlCommand command = new SqlCommand(query, newConnection);
                command.Parameters.AddWithValue("@description", richTextBox1.Text);
                command.Parameters.AddWithValue("@code", textBox1.Text);
                command.ExecuteNonQuery();

            }
            newConnection.Close();
            this.Close();
        }
    }
}
