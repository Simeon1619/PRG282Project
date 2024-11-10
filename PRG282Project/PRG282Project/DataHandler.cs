using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using System.Data;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.LinkLabel;

namespace PRG282Project
{
    public class DataHandler
    {
        public DataHandler()
        {
            

        }
        string studentFile = @"students.txt";

        public void Register(int studentID, string fname, string courseID, int age, DataGridView dgvStudent)
        {
            
            // Add to DataGridView
            dgvStudent.Rows.Add(studentID, fname, courseID, age);

            // Update the text file
            UpdateFileFromDataGrid(dgvStudent);
           

        }

        public void Update(int studentID, string fname, string courseID, int age, DataGridView dgvStudent)
        {
            bool found = false;

            foreach (DataGridViewRow row in dgvStudent.Rows)
            {
                if (row.Cells["StudentNumber"].Value != null && int.TryParse(row.Cells["StudentNumber"].Value.ToString(), out int studentNumber))
                {
                    if (studentNumber == studentID)
                    {
                        row.Selected = true;
                        dgvStudent.FirstDisplayedScrollingRowIndex = row.Index;
                        found = true;

                        if (!string.IsNullOrEmpty(fname))
                        {
                            row.Cells["FullName"].Value = fname;
                            
                        }

                        if (age != 0)
                        {
                            row.Cells["Age"].Value = age;
                        }

                        if (!string.IsNullOrEmpty(courseID))
                        {
                            row.Cells["Course"].Value = courseID;
                        }

                       
                        UpdateFileFromDataGrid(dgvStudent);

                        MessageBox.Show("Student record updated successfully.", "Update Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        break;
                    }
                }
            }

            if (!found)
            {
                MessageBox.Show("Student ID not found.", "Search Result", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void UpdateFileFromDataGrid(DataGridView dgvStudent)
        {
            List<string> lines = new List<string>();

            foreach (DataGridViewRow row in dgvStudent.Rows)
            {
                if (!row.IsNewRow)
                {
                    string studentNumber = row.Cells["StudentNumber"].Value.ToString();
                    string fullName = row.Cells["FullName"].Value.ToString();
                    string course = row.Cells["Course"].Value.ToString();
                    string age = row.Cells["Age"].Value.ToString();
                    lines.Add($"{studentNumber},{fullName},{course},{age}");
                }
            }

            File.WriteAllLines("students.txt", lines);
        }

        public void Delete(int studentID, DataGridView dgvStudent)
        {
            
            bool found = false;

            foreach (DataGridViewRow row in dgvStudent.Rows)
            {
                if (row.Cells["StudentNumber"].Value != null && int.TryParse(row.Cells["StudentNumber"].Value.ToString(), out int studentNumber))
                {
                    if (studentNumber == studentID)
                    {
                        dgvStudent.Rows.Remove(row);
                        found = true;

                  
                        UpdateFileFromDataGrid(dgvStudent);

                        MessageBox.Show("Student record deleted successfully.", "Delete Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        break;
                    }
                }
            }

            if (!found)
            {
                MessageBox.Show("Student ID not found.", "Delete Result", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            

        }
        public void GenerateSummary(DataGridView dgvStudent, Label lblTotalStudents, Label lblAverageAge)
        {
            int totalStudents = dgvStudent.Rows.Count - 1; // Excluding the new row
            int sumAge = 0;
            int studentCount = 0;

            foreach (DataGridViewRow row in dgvStudent.Rows)
            {
                if (!row.IsNewRow && int.TryParse(row.Cells["Age"].Value.ToString(), out int age))
                {
                    sumAge += age;
                    studentCount++;
                }
            }

            double averageAge = (double)sumAge / studentCount;

            // Display results on the form
            lblTotalStudents.Text = $"Total Students: {totalStudents}";
            lblAverageAge.Text = $"Average Age: {averageAge:F2}";

            // Save to summary.txt
            string summary = $"Total Students: {totalStudents}\nAverage Age: {averageAge:F2}";
            File.WriteAllText("summary.txt", summary);

            MessageBox.Show("Summary report generated and saved to summary.txt", "Summary Report", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        public void Search(DataGridView dgvStudent, int studentID) 
        {
            //DataTable dataTable = new DataTable();
            //dgvStudent.DataSource = dataTable;
            //DataView dataView = new DataView(dataTable);
            //string sqlString = string.Format("Student Number LIKE '%{0}%'", studentID); ;
            //dataView.RowFilter = sqlString;  
        }

        
    }
}
