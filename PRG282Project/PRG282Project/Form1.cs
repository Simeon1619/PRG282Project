using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PRG282Project
{
    public partial class frmStudent : Form
    {
        DataHandler handler = new DataHandler();
        const string studentFile = @"students.txt";
        DataGrid dataGrid = new DataGrid(studentFile);
        public frmStudent()
        {
            InitializeComponent();
        }

        private void btn_Create_Click(object sender, EventArgs e)
        {    
            //Student newstudent= new Student();
            //handler.Register(newstudent.StudentID, newstudent.Fname, newstudent.CourseID, newstudent.Age);
            string fname=txtName.Text;
            string courseID=txtCourse.Text;
            int age= int.Parse(txtAge.Text);
            int studentID= int.Parse(txtSID.Text);

            handler.Register(studentID, fname, courseID, age, dgvStudent);
            //studentID, string fname, int age, string courseID
            txtName.Clear();
            txtCourse.Clear();
            txtAge.Clear();
            txtSID.Clear();

            dataGrid.LoadUsers(dgvStudent);



        }

        private void btn_Update_Click(object sender, EventArgs e)
        {
            string fname = txtName.Text;
            string courseID = txtCourse.Text;
            int age = int.Parse(txtAge.Text);
            int studentID = int.Parse(txtSID.Text);

            handler.Update(studentID, fname, courseID, age, dgvStudent);
        }

        private void btn_Read_Click(object sender, EventArgs e)
        {
            //frm.ShowDialog();
        }

        private void btn_Del_Click(object sender, EventArgs e)
        {
            int studentID = int.Parse(txtSID.Text);
            handler.Delete(studentID, dgvStudent);
        }

        private void btn_Exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void frmStudent_Load(object sender, EventArgs e)
        {
            // creating the coulmns    Identifier       Display
            dgvStudent.Columns.Add("StudentNumber", "Student Number"); 
            dgvStudent.Columns.Add("FullName", "Full Name"); 
            dgvStudent.Columns.Add("Course", "Course"); 
            dgvStudent.Columns.Add("Age", "Age"); 
              
            dataGrid.LoadUsers(dgvStudent);          
        }

        private void txtSID_TextChanged(object sender, EventArgs e)
        {
            //handler.Search(dgvStudent, int.Parse(txtSID.Text));
        }
    }
}
