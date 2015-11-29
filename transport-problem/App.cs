using System;
using System.Collections;
using System.Linq;
using System.Windows.Forms;

using transport_problem.Table;
using transport_problem.SolutionMethods;

namespace transport_problem
{
    public partial class App : Form
    {
        private ArrayList _suppliers;
        private ArrayList _consumers;

        public App()
        {
            InitializeComponent();

            ConfigureGui();
        }

        private void CalculateButton_Click(object sender, EventArgs e)
        {
            InitData();

            Balancer balancer = new Balancer(_suppliers, _consumers);

            if(!balancer.CheckBalance())
                balancer.Balance();

            //var firstlyMethod = new NorthwestCornerMethod(_suppliers.Cast<Supplier>().ToArray(), _consumers.Cast<Consumer>().ToArray());
            var firstlyMethod = new VogelsMethod(_suppliers.Cast<Supplier>().ToArray(), _consumers.Cast<Consumer>().ToArray());

            Table.Table solution = firstlyMethod.GetSolution();

            var optimizeMethod = new PotentialsMethod(solution);

            Logger logger = new Logger(solution);

            while (!optimizeMethod.IsOptimal())
            {
                logger.Log();
                optimizeMethod.Otimize();
            }

            logger.Log();

            logger.ShowLog();

            MessageBox.Show("Total: " + solution.GetTotalTransportationsPrice());
        }

        private void InitData()
        {
            _suppliers = new ArrayList();
            _consumers = new ArrayList();

            int suppliersCount = this.dataGridView1.Columns.Count;
            int consumersCount = this.dataGridView2.Columns.Count;

            for (int i = 0; i < suppliersCount; i++)
            {
                int[] rates = new int[consumersCount];

                int stock = Convert.ToInt32(this.dataGridView1.Rows[0].Cells[i].Value);

                for (int j = 0; j < consumersCount; j++)
                {
                    rates[j] = Convert.ToInt32(this.dataGridView1.Rows[j + 1].Cells[i].Value);
                }

                _suppliers.Add(new Supplier(rates, stock));
            }

            for (int i = 0; i < consumersCount; i++)
            {
                int need = Convert.ToInt32(this.dataGridView2.Rows[0].Cells[i].Value);

                _consumers.Add(new Consumer(need));
            }
        }

        private void ConfigureGui()
        {
            AutoSize = true;
            AutoSizeMode = AutoSizeMode.GrowOnly;
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
