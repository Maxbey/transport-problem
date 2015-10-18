using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace transport_problem
{
    public partial class App : Form
    {
        public App()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.dataGridView1.ColumnCount =  Convert.ToInt32(this.numericUpDown1.Value);
            this.dataGridView1.RowCount = Convert.ToInt32(this.numericUpDown2.Value);
            this.dataGridView1.AutoSize = true;
            this.dataGridView1.Visible = true;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
           
        }
    }
}
