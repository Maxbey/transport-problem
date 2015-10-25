using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using transport_problem.Classes;
using transport_problem.SolutionMethods;

namespace transport_problem
{
    public partial class App : Form
    {
        private Supplier[] suppliers;
        private Сonsumer[] consumers;

        public App()
        {
            InitializeComponent();
        }

        private void CalculateButton_Click(object sender, EventArgs e)
        {
            int SuppliersCount = this.dataGridView1.Columns.Count;
            int ConsumersCount = this.dataGridView2.Columns.Count;

            this.suppliers = new Supplier[SuppliersCount];
            this.consumers = new Сonsumer[ConsumersCount];

            for (int i = 0; i < SuppliersCount; i++)
            {
                int[] rates = new int[ConsumersCount];

                int stock = Convert.ToInt32(this.dataGridView1.Rows[0].Cells[i].Value);

                for (int j = 0; j < SuppliersCount; j++)
                {
                    rates[j] = Convert.ToInt32(this.dataGridView1.Rows[j + 1].Cells[i].Value);
                }

                this.suppliers[i] = new Supplier(rates, stock);
            }

            for(int i = 0; i < ConsumersCount; i++)
            {
                int need = Convert.ToInt32(this.dataGridView2.Rows[0].Cells[i].Value);

                this.consumers[i] = new Сonsumer(need);
            }

            

            var method = new PhogelsMethod(suppliers, consumers);

            method.GetSolution();
        }


        private void initButton_Click(object sender, EventArgs e)
        {
            this.dataGridView1.ColumnCount = Convert.ToInt32(this.numericUpDown1.Value);
            this.dataGridView1.RowCount = Convert.ToInt32(this.numericUpDown2.Value) + 1;
            this.dataGridView1.AutoSize = true;
            this.dataGridView1.Visible = true;

            this.dataGridView2.ColumnCount = Convert.ToInt32(this.numericUpDown2.Value);
            this.dataGridView2.RowCount = 1;
            this.dataGridView2.AutoSize = true;
            this.dataGridView2.Visible = true;

            this.label4.Visible = true;
            this.label5.Visible = true;
            this.CalculateButton.Visible = true;
        }
    }
}
