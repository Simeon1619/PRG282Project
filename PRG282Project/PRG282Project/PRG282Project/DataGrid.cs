using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace PRG282Project
{
    public class DataGrid
    {
        private string studentFile;

        public DataGrid(string studentFile)
        {
            this.studentFile = studentFile;
        }

        // Method to load users from the file into DataGridView
        public void LoadUsers(DataGridView dgvStudent)
        {
            dgvStudent.Rows.Clear();

            try
            {
                if (File.Exists(studentFile))
                {
                    var lines = ReadFileLines(studentFile);
                    PopulateDataGridView(dgvStudent, lines);
                }
                else
                {
                    CreateFile(studentFile);
                }
            }
            catch (IOException ex)
            {
                MessageBox.Show($"Error reading file: {ex.Message}", "File Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Method to read all lines from a file
        private IEnumerable<string> ReadFileLines(string filePath)
        {
            return File.ReadLines(filePath);
        }

        // Method to populate DataGridView with file data
        public void PopulateDataGridView(DataGridView dgvStudent, IEnumerable<string> lines)
        {
            foreach (var line in lines)
            {
                var data = line.Split(',');
                dgvStudent.Rows.Add(data);
            }
        }

        // Method to create a new file
        private void CreateFile(string filePath)
        {
            using (File.Create(filePath)) { }
        }
    }
}
