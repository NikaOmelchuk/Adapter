using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Adapter
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SaveToBinaryAdapter adapter = new SaveToBinaryAdapter(dataGridView1);
            adapter.SaveTo("data.bin");
            MessageBox.Show("Сохранено!");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            BinaryAdapter adapter = new BinaryAdapter();
            adapter.SaveToGrid("data.bin", dataGridView1);
        }

        void hh()
        {
            if (dataGridView1.RowCount > 0 && dataGridView1[0, 0].Value != null)
            {
                button1.Enabled = true;
                button2.Enabled = true;
                button5.Enabled = true;
            }
            else
            {
                button1.Enabled = false;
                button2.Enabled = false;
                button5.Enabled = false;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            hh();
        }

        private void dataGridView1_CurrentCellChanged(object sender, EventArgs e)
        {
            hh();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SaveToTextAdapter adapter = new SaveToTextAdapter(dataGridView1);
            adapter.SaveTo("data.txt");
            MessageBox.Show("Сохранено!");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            TextAdapter adapter = new TextAdapter();
            adapter.SaveToGrid("data.txt", dataGridView1);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
