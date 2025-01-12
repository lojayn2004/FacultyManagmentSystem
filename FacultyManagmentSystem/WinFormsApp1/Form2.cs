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
    public partial class Form2 : Form
    {
        // Check If The user filled all the fileds or not to login 
        private bool noEmptyFields()
        {
            if (textBox1.Text.Trim() == "")
                return false;
            if (textBox2.Text.Trim() == "")
                return false;

            return true;
        }

        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (noEmptyFields() == false)
            {
                MessageBox.Show("All Fields Are required");

            }
            else
            {
                // CONNECT TO DATABASE
                SqlConnection connect = DatabaseManager.GetSqlConnection();
                connect.Open();

                // SEARCH FOR THE USER

                string email = textBox1.Text;
                string password = textBox2.Text;

                string query = "select * from users where email = @email and userPassword = @password";
                SqlCommand exceuteQuery = new SqlCommand(query, connect);

                // Info that will be sent to sql
                exceuteQuery.Parameters.AddWithValue("@email", email);
                exceuteQuery.Parameters.AddWithValue("@password", password);
                SqlDataReader reader = exceuteQuery.ExecuteReader();

                if (reader.HasRows)
                {
                    
                    if (reader.Read())
                    {
                        // Store Info About The currently Logged user to use it later 
                        UserSession.SetUserSession(email, reader.GetString(reader.GetOrdinal("userType")));
                        UserSession.userId = reader.GetInt32(reader.GetOrdinal("userID"));
                        reader.Close();

                        
                        // If he is an Admin Direct him to the admin page
                        // Otherwise direct him to the student page
                        if (UserSession.Role.ToLower() == "admin")
                        {

                            AdminPage admin = new AdminPage();
                            admin.Show();

                        }
                        
                        else if (UserSession.Role.ToLower() == "student")
                        {
                            StudentProfile student = new StudentProfile();
                            student.Show();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Invalid email or password");
                    }
                }
                else
                {
                    
                    MessageBox.Show("Invalid email or password");
                }
                connect.Close();
            }
            
        }
    }
}
