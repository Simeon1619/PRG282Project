using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PRG282Project
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        DataGrid frm=new DataGrid();
        DataHandler handler = new DataHandler();

        private void btn_Create_Click(object sender, EventArgs e)
        {
            //Student newstudent= new Student();
            //handler.Register(newstudent.StudentID, newstudent.Fname, newstudent.CourseID, newstudent.Age);
            string fname=txtName.Text;
            string courseID=txtCourse.Text;
            string age=txtAge.Text;
            string studentID=txtSID.Text;

            Register(fname,courseID,age,studentID);

            txtName.Clear();
            txtCourse.Clear();
            txtAge.Clear();
            txtSID.Clear();





        }

        private void btn_Update_Click(object sender, EventArgs e)
        {
            frm.ShowDialog();
        }

        private void btn_Read_Click(object sender, EventArgs e)
        {
            frm.ShowDialog();
        }

        private void btn_Del_Click(object sender, EventArgs e)
        {
            frm.ShowDialog();
        }

        private void btn_Exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
