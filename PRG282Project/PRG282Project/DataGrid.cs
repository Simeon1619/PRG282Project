using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public void LoadUsers(DataGridView dgvStudent)
        {
            dgvStudent.Rows.Clear();

            if (File.Exists(studentFile))
            {
                var lines = File.ReadAllLines(studentFile);
                foreach (var line in lines)
                {
                    var data = line.Split(',');
                    dgvStudent.Rows.Add(data);
                }
                MessageBox.Show("gay");
            }
            else
            {
                File.Create(studentFile);
                MessageBox.Show("leesss soos kak");
            }

        }
    }
}
