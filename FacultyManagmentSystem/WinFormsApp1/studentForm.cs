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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using DataBaseConnection;

namespace WinFormsApp1
{
    public partial class studentForm : Form
    {
        public studentForm()
        {
            InitializeComponent();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            string firstName = textBox1.Text;
            string lastName = textBox4.Text;
            string phone = textBox2.Text;
            string email = textBox3.Text;
            string birthdate = textBox6.Text;
            string password = textBox5.Text;
            string gender = "";
            string userType = "";
            if (checkBox1.Checked)
                gender = checkBox1.Text;
            else if (checkBox2.Checked)
                gender = checkBox2.Text;
            if (radioButton1.Checked)
                userType = radioButton1.Text;
            else if (radioButton2.Checked)
                userType = radioButton2.Text;
            else
                userType = radioButton3.Text;
            SqlConnection connection = DatabaseManager.GetSqlConnection();
            connection.Open();
            string insertQuery = "INSERT INTO users (firstName, lastName, phone,email,birthDate,gender,userPassword,userType) " +
                "VALUES (@firstName, @lastName,@phone,@email,@birthdate,@gender,@password,@userType)";
            using (SqlCommand co2 = new SqlCommand(insertQuery, connection))
            {
                co2.Parameters.AddWithValue("@firstName", firstName);
                co2.Parameters.AddWithValue("@lastName", lastName);
                co2.Parameters.AddWithValue("@phone", phone);
                co2.Parameters.AddWithValue("@email", email);
                co2.Parameters.AddWithValue("@birthdate", birthdate);
                co2.Parameters.AddWithValue("@gender", gender);
                co2.Parameters.AddWithValue("@password", password);
                co2.Parameters.AddWithValue("@userType", userType);
                co2.ExecuteNonQuery();
            }
            if (radioButton2.Checked)
            {
                string insertQuery1 = "INSERT INTO professor (userID) select userID from users where email = @email";
                using (SqlCommand co1 = new SqlCommand(insertQuery1, connection))
                {
                    co1.Parameters.AddWithValue("@email", email);
                    co1.ExecuteNonQuery();
                }
            }
            else if (radioButton3.Checked)
            {
                string insertQuery1 = "INSERT INTO student (userID) select userID from users where email = @email";
                using (SqlCommand co1 = new SqlCommand(insertQuery1, connection))
                {
                    co1.Parameters.AddWithValue("@email", email);
                    co1.ExecuteNonQuery();
                }
            }
            this.Close();
        }


        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
