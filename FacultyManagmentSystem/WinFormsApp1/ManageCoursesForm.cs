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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace WinFormsApp1
{
    public partial class ManageCoursesForm : Form
    {
        public ManageCoursesForm()
        {
            InitializeComponent();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Trim() == "")
            {
                MessageBox.Show("Please Enter course Code to be deleted ");
            }
            else
            {
                SqlConnection newConnection = DatabaseManager.GetSqlConnection();
                newConnection.Open();

                string query = "SELECT * FROM course WHERE course_code = @code";
                SqlCommand command = new SqlCommand(query, newConnection);
                command.Parameters.AddWithValue("@code", textBox1.Text);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (!reader.HasRows)
                    {
                        MessageBox.Show("Course Not Found!! Please enter a valid Course Name");
                        reader.Close();

                    }
                    else
                    {
                        reader.Close();
                        string deleteQuery = "delete from course where course_code = @code";
                        SqlCommand deleteCommand = new SqlCommand(deleteQuery, newConnection);
                        deleteCommand.Parameters.AddWithValue("@code", textBox1.Text);
                        deleteCommand.ExecuteNonQuery();
                    }
                }
                newConnection.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AddCourseForm addNewCourse = new AddCourseForm();
            addNewCourse.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            prerquisiteForm preQuisite = new prerquisiteForm();
            preQuisite.Show();
        }

    }
}
