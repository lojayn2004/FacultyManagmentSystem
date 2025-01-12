using DataBaseConnection;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace WinFormsApp1
{
    public partial class prerquisiteForm : Form
    {
        private bool isEmptyFields()
        {
            if (textBox1.Text.Trim() == "")
                return true;
            if (textBox2.Text.Trim() == "")
                return true;
            return false;
        }

        private bool isCourseFound(string crs)
        {
            SqlConnection newConnection = DatabaseManager.GetSqlConnection();
            newConnection.Open();

            string query = "SELECT * FROM course WHERE course_code = @course";
            SqlCommand command = new SqlCommand(query, newConnection);
            command.Parameters.AddWithValue("@course", crs);
            using (SqlDataReader reader = command.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    newConnection.Close();
                    reader.Close();
                    return true;
                }
                else
                {
                    newConnection.Close();
                    reader.Close();
                    return false;
                }
            }

        }

        private bool hasPreQuisite()
        {
            SqlConnection newConnection = DatabaseManager.GetSqlConnection();
            newConnection.Open();
            string query = "SELECT * FROM prerequisite WHERE course_code =" +
                " @course and pre_course_code = @precode";
            SqlCommand command = new SqlCommand(query, newConnection);
            command.Parameters.AddWithValue("@course", textBox1.Text);
            command.Parameters.AddWithValue("@precode", textBox2.Text);
            using (SqlDataReader reader = command.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    newConnection.Close();
                    reader.Close();
                    return true;
                }
                else
                {
                    newConnection.Close();
                    reader.Close();
                    return true;
                }
            }
        }


        public prerquisiteForm()
        {
            InitializeComponent();
        }

        private void prerquisiteForm_Load(object sender, EventArgs e)
        {

        }


        private void button1_Click(object sender, EventArgs e)
        {
            if (isEmptyFields() == true)
            {
                MessageBox.Show("All Fields Must be Filled");
            }
            else if (isCourseFound(textBox1.Text) == false)
            {
                MessageBox.Show("Please enter a valid course Id");
            }
            else if (isCourseFound(textBox2.Text) == false)
            {
                MessageBox.Show("Please enter a valid pre-requisite course Id");
            }
            else
            {
                try
                {
                    SqlConnection newConnection = DatabaseManager.GetSqlConnection();
                    newConnection.Open();

                    string query = "insert into prerequisite values(@precode, @crscode)";
                    SqlCommand command = new SqlCommand(query, newConnection);
                    command.Parameters.AddWithValue("@precode", textBox2.Text);
                    command.Parameters.AddWithValue("@crscode", textBox1.Text);
                    command.ExecuteNonQuery();

                    newConnection.Close();
                    MessageBox.Show("Prequisite Added successfully");

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error Occurred While Adding Prerequisite");
                }

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (isEmptyFields() == true)
            {
                MessageBox.Show("All Fields Must be Filled");
            }
            else if (isCourseFound(textBox1.Text) == false)
            {
                MessageBox.Show("Please enter a valid course Id");
            }
            else if (isCourseFound(textBox2.Text) == false)
            {
                MessageBox.Show("Please enter a valid pre-requisite course Id");
            }
            else if (hasPreQuisite() == false)
            {
                MessageBox.Show("No Such prequisite exists");
            }
            else
            {
                try
                {
                    SqlConnection newConnection = DatabaseManager.GetSqlConnection();
                    newConnection.Open();

                    string deleteQuery = "delete from prerequisite where " +
                        "pre_course_code = @precode and course_code = @code";

                    SqlCommand deleteCommand = new SqlCommand(deleteQuery, newConnection);
                    deleteCommand.Parameters.AddWithValue("@code", textBox1.Text);
                    deleteCommand.Parameters.AddWithValue("@precode", textBox2.Text);
                    deleteCommand.ExecuteNonQuery();
                    newConnection.Close();
                    MessageBox.Show("Prerequisite Deleted Successfully");

                }
                catch (Exception ex)
                {

                    MessageBox.Show("Error Occurred While deleting prerequisite");
                }
            }

        }
    }
}
