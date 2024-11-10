using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PRG282Project
{
    public partial class DataGrid : Form
    {
        public string file = "student.txt";
        public DataGrid()
        {
            InitializeComponent();
            LoadUsers();
        }

        public void LoadUsers()
        {
            DataGrid.Rows.Clear();

            if (File.Exists(file))
            {
                var lines=File.ReadAllLines(file);
                foreach (var line in lines)
                {
                    var data=line.Split(',');
                    DataGrid.Rows.Add(data);
                }
            }
            else
            {
                Message.Show("User file not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }


        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
