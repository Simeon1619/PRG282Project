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

namespace PRG282Project
{
    public class DataHandler
    {
        public DataHandler()
        {
            

        }
        string studentFile = @"students.txt";

        public void Register(int studentID, string fname, int age, string courseID)
        {
            
            File.AppendAllText(studentFile, Environment.NewLine + studentID + "," + fname + "," + courseID + "," + age);
            
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

        public void Delete(int studentID, string fname, int age, string courseID)
        {
            if (string.IsNullOrEmpty(fname))
            {
                MessageBox.Show("Please enter a student record to remove","Please input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
            }
            try
            {
                var lines = File.ReadAllLines(studentFile);
                var updatedLines = lines.Where(line => !line.StartsWith(fname + ",")).ToArray();

                
                if (lines.Length == updatedLines.Length)
                {
                    MessageBox.Show("Student not found.", "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                   
                    //File.WriteAllLines(file, updatedLines);

                    
                    //LoadUsers();

                    MessageBox.Show("Record deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show($"Error: {e.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
