using System;
using System.Drawing;
using System.IO;
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

        private void frmStudent_Load(object sender, EventArgs e)
        {
            // Create columns programmatically
            dgvStudent.Columns.Add("StudentNumber", "Student Number");
            dgvStudent.Columns.Add("FullName", "Full Name");
            dgvStudent.Columns.Add("Course", "Course");
            dgvStudent.Columns.Add("Age", "Age");

            // Load existing users into DataGridView
            dataGrid.LoadUsers(dgvStudent);
        }

        private void btn_Create_Click(object sender, EventArgs e)
        {
            if (ValidateInput(out int studentID, out string fname, out string courseID, out int age))
            {
                // Register new student
                handler.Register(studentID, fname, courseID, age, dgvStudent);

                // Clear input fields
                ClearInputFields();
            }
        }
    
        private void btn_Update_Click(object sender, EventArgs e)
        {
            
            if (int.TryParse(txtSID.Text, out int studentID) && studentID.ToString().Length == 6)
            {
                string fname = txtName.Text;
                string courseID = txtCourse.Text;
                int.TryParse(txtAge.Text, out int age);

                handler.Update(studentID, fname, courseID, age, dgvStudent);
            }
            else
            {
                MessageBox.Show("Please enter a valid 6-digit Student ID.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        
        }

        private void btn_Del_Click(object sender, EventArgs e)
        {
            if (ValidateStudentID(txtSID.Text, out int studentID))
            {
                // Delete student
                handler.Delete(studentID, dgvStudent);
            }
        }

        private void btn_Exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private bool ValidateInput(out int studentID, out string fname, out string courseID, out int age)
        {
            age = int.Parse(txtAge.Text);
            fname = txtName.Text;
            courseID = txtCourse.Text;

            // Validate Student ID and Age
            if (!ValidateStudentID(txtSID.Text, out studentID) || !int.TryParse(txtAge.Text, out age) || age <= 0)
            {
                MessageBox.Show("Please enter valid data. Student ID must be a 6-digit integer and age must be a positive number.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        private bool ValidateStudentID(string studentIDText, out int studentID)
        {
            studentID = 0;
            if (int.TryParse(studentIDText, out studentID) && studentIDText.Length == 6)
            {
                return true;
            }
            MessageBox.Show("Student ID must be a 6-digit integer.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return false;
        }

        private void ClearInputFields()
        {
            txtName.Clear();
            txtCourse.Clear();
            txtAge.Clear();
            txtSID.Clear();
        }

        private void btnReport_Click(object sender, EventArgs e)
        {
            // Generate summary report
            handler.GenerateSummary(dgvStudent, lblTotalStudents, lblAverageAge);
            lblAverageAge.Visible = true;
            lblTotalStudents.Visible = true;
        }

        private void txtSID_TextChanged(object sender, EventArgs e)
        {
            handler.Search(dgvStudent, studentFile, txtSID.Text);
        }
    }
}
