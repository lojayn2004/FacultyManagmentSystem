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
    public partial class AddDepartmentForm : Form
    {

        private bool isEmptyForm()
        {
            if (textBox2.Text.Trim() == "")
                return true;
            return false;
        }

        public AddDepartmentForm()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (isEmptyForm() == true)
            {
                MessageBox.Show("Deperatment Name is Required !");
            }
            else
            {
                SqlConnection connect = DatabaseManager.GetSqlConnection();
                connect.Open();


                string departmentName = textBox2.Text;

                string query = "insert into department values(@deptName)";
                SqlCommand exceuteQuery = new SqlCommand(query, connect);

                exceuteQuery.Parameters.AddWithValue("@deptName", departmentName);
                MessageBox.Show(departmentName + " added Successfully");
                exceuteQuery.ExecuteNonQuery();
                connect.Close();

            }


        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            string text = textBox2.Text;
            SqlConnection sqlConnection = DatabaseManager.GetSqlConnection();
            sqlConnection.Open();
            string query = "delete from department where deptID = ( select deptID from department where deptName = @text);";

            try
            {
                using (SqlCommand command = new SqlCommand(query, sqlConnection))
                {
                    command.Parameters.AddWithValue("@text", text);

                    command.ExecuteNonQuery();
                }
                MessageBox.Show("Department Deleted Successfully");
                sqlConnection.Close();

           }
            catch (Exception ex)
            {
                MessageBox.Show("Error Occured When deleting department Please Try Again");
            }
        }
    }
}
