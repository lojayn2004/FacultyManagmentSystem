using DataBaseConnection;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace WinFormsApp1
{
    public partial class StudentProfile : Form
    {
        private bool fillUserDataFields()
        {
            SqlConnection connection = DatabaseManager.GetSqlConnection();
            connection.Open();
            string query = "SELECT * FROM users JOIN student " +
                           "ON users.userID = student.userID AND users.userID = @userId";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@userId", UserSession.userId);

            using (SqlDataReader reader = command.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    if (reader.Read())
                    {
                        textBox4.Text = reader.IsDBNull(reader.GetOrdinal("firstName")) ? "" : reader.GetString(reader.GetOrdinal("firstName"));
                        textBox2.Text = reader.IsDBNull(reader.GetOrdinal("lastName")) ? "" : reader.GetString(reader.GetOrdinal("lastName"));
                        textBox3.Text = reader.IsDBNull(reader.GetOrdinal("phone")) ? "" : reader.GetString(reader.GetOrdinal("phone"));
                        textBox6.Text = reader.IsDBNull(reader.GetOrdinal("email")) ? "" : reader.GetString(reader.GetOrdinal("email"));
                        textBox7.Text = reader.IsDBNull(reader.GetOrdinal("userPassword")) ? "" : reader.GetString(reader.GetOrdinal("userPassword"));
                        textBox8.Text = reader.IsDBNull(reader.GetOrdinal("gender")) ? "" : reader.GetString(reader.GetOrdinal("gender"));
                        textBox5.Text = reader.IsDBNull(reader.GetOrdinal("birthDate")) ? "" : reader.GetInt32(reader.GetOrdinal("birthDate")).ToString();
                        textBox10.Text = reader.IsDBNull(reader.GetOrdinal("total_credit_hours")) ? "0" : reader.GetInt32(reader.GetOrdinal("total_credit_hours")).ToString();
                        textBox11.Text = reader.IsDBNull(reader.GetOrdinal("deptID")) ? "unusigned" : reader.GetInt32(reader.GetOrdinal("deptID")).ToString();
                        textBox9.Text = reader.IsDBNull(reader.GetOrdinal("gpa")) ? "0.0" : reader.GetDouble(reader.GetOrdinal("gpa")).ToString();
                        return true;
                    }
                }
                else
                {
                    MessageBox.Show("No data found for the user ID: " + UserSession.userId);
                }
            }

            connection.Close();
            return false;
        }

        private void getStudentCourses()
        {
            SqlConnection connection = DatabaseManager.GetSqlConnection();
            connection.Open();
            string query = "SELECT course_code, grade, years, semester " +
                           "FROM users JOIN student " +
                           "ON users.userID = student.userID " +
                           "join enrollment on users.userId = enrollment.userId " +
                           "and users.userID = @userId ";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@userId", UserSession.userId);
            DataTable dataTable = new DataTable();
            using (SqlDataAdapter adapter = new SqlDataAdapter(command))
            {
                adapter.Fill(dataTable);
                dataGridView1.DataSource = dataTable;
            }
            connection.Close();
        }

        public StudentProfile()
        {
            InitializeComponent();
            fillUserDataFields();
            getStudentCourses();
            textBox4.ReadOnly = true;
            textBox2.ReadOnly = true;
            textBox9.ReadOnly = true;
            textBox10.ReadOnly = true;
            textBox11.ReadOnly = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnection newConnection = DatabaseManager.GetSqlConnection();
            newConnection.Open();

            // EDIT STUDENT'S PHONE NUMBER
            if (textBox3.Text != "")
            {
                string updatePhoneNumberQuery = "update users set phone = @phoneNumber where userID = @userId";
                SqlCommand executePhoneNumber = new SqlCommand(updatePhoneNumberQuery, newConnection);

                executePhoneNumber.Parameters.AddWithValue("@phoneNumber", textBox3.Text);
                executePhoneNumber.Parameters.AddWithValue("@userId", UserSession.userId);
                executePhoneNumber.ExecuteNonQuery();

            }
            else
            {
                MessageBox.Show("Please Enter Phone Number");
            }

            if(textBox6.Text != "")
            {
                string updateEmail = "update users set email = @email where userID = @userId";
                SqlCommand executeEmail = new SqlCommand(updateEmail, newConnection);

                executeEmail.Parameters.AddWithValue("@email", textBox6.Text);
                executeEmail.Parameters.AddWithValue("@userId", UserSession.userId);
                executeEmail.ExecuteNonQuery();

            }
            else
            {
                MessageBox.Show("Please Enter email");
            }


            if (textBox7.Text != "")
            {
                string updatePassword = "update users set userPassword = @password where userID = @userId";
                SqlCommand executePassword = new SqlCommand(updatePassword, newConnection);

                executePassword.Parameters.AddWithValue("@password", textBox7.Text);
                executePassword.Parameters.AddWithValue("@userId", UserSession.userId);
                executePassword.ExecuteNonQuery();

            }
            else
            {
                MessageBox.Show("Please Enter Password");
            }
            if (textBox8.Text != "")
            {
                string updateGender = "update users set gender = @gender where userID = @userId";
                SqlCommand executeGender = new SqlCommand(updateGender, newConnection);

                executeGender.Parameters.AddWithValue("@gender", textBox8.Text);
                executeGender.Parameters.AddWithValue("@userId", UserSession.userId);
                executeGender.ExecuteNonQuery();

            }
            else
            {
                MessageBox.Show("Please Enter Gender");
            }

            if (textBox5.Text != "")
            {
                string updateBirthYear = "update users set birthDate = @birthYear where userID = @userId";
                SqlCommand executeBirthYear = new SqlCommand(updateBirthYear, newConnection);

                executeBirthYear.Parameters.AddWithValue("@birthYear", textBox5.Text);
                executeBirthYear.Parameters.AddWithValue("@userId", UserSession.userId);
                executeBirthYear.ExecuteNonQuery();

            }

            else
            {
                MessageBox.Show("Please Enter Birth Year");
            }


            newConnection.Close();
            if (fillUserDataFields())
            {
                MessageBox.Show("Info Updated Sucessfully\n");
            }
            else
            {
                MessageBox.Show("Error occurred Please Try Again\n");
            }

        }
    }
}
