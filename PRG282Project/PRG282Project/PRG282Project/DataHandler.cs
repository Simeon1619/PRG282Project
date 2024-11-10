using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace PRG282Project
{
    public class DataHandler
    {
        const string studentFile = @"students.txt";
        DataGrid dataGrid = new DataGrid(studentFile);

        public DataHandler()
        {
        }

        // Method to register a new student
        public void Register(int studentID, string fname, string courseID, int age, DataGridView dgvStudent)
        {
            if (ValidateStudentID(studentID.ToString(), dgvStudent))
            {
                // Add to DataGridView
                dgvStudent.Rows.Add(studentID, fname, courseID, age);
                // Update the text file
                string newinfo = $"{studentID},{fname},{courseID},{age}";
                File.AppendAllText(studentFile, Environment.NewLine + newinfo);
            }
            else
            {
                MessageBox.Show("Invalid Student ID. It must be an integer and 6 characters long.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Method to update student details
        public void Update(int studentID, string fname, string courseID, int age, DataGridView dgvStudent)
        {
            bool found = false;

            foreach (DataGridViewRow row in dgvStudent.Rows)
            {
                if (IsMatchingStudent(row, studentID))
                {
                    found = true;

                    // Update fields only if they have valid data
                    if (!string.IsNullOrEmpty(fname))
                        row.Cells["FullName"].Value = fname;

                    if (age != 0)
                        row.Cells["Age"].Value = age;

                    if (!string.IsNullOrEmpty(courseID))
                        row.Cells["Course"].Value = courseID;

                    // Save changes to the file
                    UpdateFileFromDataGrid(dgvStudent);
                    MessageBox.Show("Student record updated successfully.", "Update Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    break;
                }
            }

            if (!found)
                MessageBox.Show("Student ID not found.", "Search Result", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // Method to delete a student
        public void Delete(int studentID, DataGridView dgvStudent)
        {
            bool found = false;
            List<string> lines = File.ReadAllLines(studentFile).ToList();

            foreach (DataGridViewRow row in dgvStudent.Rows)
            {
                if (IsMatchingStudent(row, studentID))
                {
                    dgvStudent.Rows.Remove(row);
                    found = true;

                    // Remove from file
                    lines = lines.Where(line => !line.StartsWith(studentID.ToString())).ToList();
                    File.WriteAllLines(studentFile, lines);

                    MessageBox.Show("Student record deleted successfully.", "Delete Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    break;
                }
            }

            if (!found)
                MessageBox.Show("Student ID not found.", "Delete Result", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // Method to generate a summary report
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

            double averageAge = studentCount > 0 ? (double)sumAge / studentCount : 0;

            // Display results on the form
            lblTotalStudents.Text = $"Total Students: {totalStudents}";
            lblAverageAge.Text = $"Average Age: {averageAge:F2}";

            // Save to summary.txt
            string summary = $"Total Students: {totalStudents}\nAverage Age: {averageAge:F2}";
            SaveToFile("summary.txt", summary);
        }

        // Method to validate the student ID
        private bool ValidateStudentID(string studentID, DataGridView dgvStudent)
        {
            bool found = false;
            foreach (DataGridViewRow row in dgvStudent.Rows)
            {
                if (IsMatchingStudent(row, int.Parse(studentID)))
                {
                    found = true;
                    break;
                }
            }
            return int.TryParse(studentID, out _) && studentID.Length == 6 && found == false;
        }

        // Method to check if the row matches the student ID
        private bool IsMatchingStudent(DataGridViewRow row, int studentID)
        {
            return row.Cells["StudentNumber"].Value != null && int.TryParse(row.Cells["StudentNumber"].Value.ToString(), out int studentNumber) && studentNumber == studentID;
        }

        // Method to update the text file from the DataGridView
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

            SaveToFile(studentFile, lines);
        }

        // Method to save lines to a file
        private void SaveToFile(string filePath, IEnumerable<string> lines)
        {
            try
            {
                File.WriteAllLines(filePath, lines);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating the file: {ex.Message}", "File Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Method to save a single string to a file
        private void SaveToFile(string filePath, string content)
        {
            try
            {
                File.WriteAllText(filePath, content);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error writing to summary file: {ex.Message}", "File Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Method to search a student by ID
        public void Search(DataGridView dgvStudent, string studentFile, string studentID)
        {
            dgvStudent.Rows.Clear();
            DataGrid dataGrid = new DataGrid(studentFile);
            var lines = File.ReadLines(studentFile);
            IEnumerable<string> filteredLines = lines.Where(line => line.Contains(studentID));
            dataGrid.PopulateDataGridView(dgvStudent, filteredLines);
        }
    }
}
