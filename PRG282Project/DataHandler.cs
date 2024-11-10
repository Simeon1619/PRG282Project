using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace PRG282Project
{
    internal class DataHandler:DataGrid
    {
        public DataHandler()
        {

        }
        string file = @"students.txt";
        public void Register(int studentID, string fname, int age, string courseID)
        {


            try
            {
                File.AppendAllText(file, studentID, fname, age, courseID);


            }
            catch (Exception e)
            {
                MessageBox.Show($"Error: {e.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                MessageBox.Show("Please enter some text to add.", "Input Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        public void Update(int studentID, string fname, int age, string courseID)
        {

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
                var lines = File.ReadAllLines(file);
                var updatedLines = lines.Where(line => !line.StartsWith(fname + ",")).ToArray();

                
                if (lines.Length == updatedLines.Length)
                {
                    MessageBox.Show("Student not found.", "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                   
                    File.WriteAllLines(file, updatedLines);

                    
                    LoadUsers();

                    MessageBox.Show("Record deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show($"Error: {e.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }



        }

        
    }
}
