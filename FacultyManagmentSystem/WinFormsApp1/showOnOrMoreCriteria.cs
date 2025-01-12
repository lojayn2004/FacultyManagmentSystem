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
    public partial class showOnOrMoreCriteria : Form
    {
        public showOnOrMoreCriteria()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnection sql = DatabaseManager.GetSqlConnection();
            sql.Open();
            bool t1 = false, t2 = false;
            if (textBox1.Text != "")
            {
                t1 = true;

                // Parse the GPA value from the TextBox
                float text1 = float.Parse(textBox1.Text);
                
                // Define a small tolerance value
                float tolerance = 0.01f;

                // Create the SQL query with range comparison
                string query1 = "SELECT * FROM student, users WHERE gpa >= @lowerBound AND gpa <= @upperBound AND users.userID = student.userID";

                // Create the SqlDataAdapter and set the query parameters
                SqlDataAdapter sqlDa = new SqlDataAdapter(query1, sql);
                sqlDa.SelectCommand.Parameters.AddWithValue("@lowerBound", text1 - tolerance);
                sqlDa.SelectCommand.Parameters.AddWithValue("@upperBound", text1 + tolerance);

                // Execute the query and fill the DataTable
                DataTable dtbl = new DataTable();
                sqlDa.Fill(dtbl);

                // Bind the results to the DataGridView
                dataGridView1.DataSource = dtbl;
                
            }
            if (textBox2.Text != "")
            {
                t2 = true;
                int text2 = int.Parse(textBox2.Text);
                string query1 = "select * from student , users where graduation_year = @text2 and users.userID = student.userID";
                SqlDataAdapter sqlDa = new SqlDataAdapter(query1, sql);
                sqlDa.SelectCommand.Parameters.AddWithValue("@text2", text2);
                DataTable dtbl = new DataTable();
                sqlDa.Fill(dtbl);
                dataGridView2.DataSource = dtbl;
            }
            if (t1 && t2)
            {
                float text1 = float.Parse(textBox1.Text);

                // Define a small tolerance value
                float tolerance = 0.01f;

               
                int text2 = int.Parse(textBox2.Text);
                string query1 = "select * from student , users where graduation_year = @text2 and gpa >= @lowerBound AND gpa <= @upperBound and users.userID = student.userID";
                SqlDataAdapter sqlDa = new SqlDataAdapter(query1, sql);
                
                sqlDa.SelectCommand.Parameters.AddWithValue("@text2", text2);
                sqlDa.SelectCommand.Parameters.AddWithValue("@lowerBound", text1 - tolerance);
                sqlDa.SelectCommand.Parameters.AddWithValue("@upperBound", text1 + tolerance);
                DataTable dtbl = new DataTable();
                sqlDa.Fill(dtbl);
                dataGridView3.DataSource = dtbl;
            }
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
    }
}
