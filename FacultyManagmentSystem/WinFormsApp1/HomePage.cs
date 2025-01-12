using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsApp1
{
    public partial class HomePage : Form
    {
        public HomePage()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            studentForm stud = new studentForm();
            stud.Show();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form2 loginPage = new Form2();
            loginPage.Show();

        }

        private void HomePage_Load(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            ReportGenerator reportGenerator = new ReportGenerator();
            reportGenerator.Show();
        }
    }
}
