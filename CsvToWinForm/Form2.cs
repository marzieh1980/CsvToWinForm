using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CsvToWinForm
{
    
    public partial class Form2 : Form
    {
        public ColumnsClass obj = new ColumnsClass();

        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            //if (obj != null)
            //{
            //    string newline = textBox1.Text + ";" + textBox2.Text + ";" + textBox3.Text + ";" + textBox4.Text + ";" + textBox5.Text + ";" + textBox6.Text + ";" + textBox7.Text + ";" + textBox8 + ";" + textBox9.Text;
            //}
        }

        private void btnSave_click_Click(object sender, EventArgs e)
        {
            string path = @"c:\csv_form\template.csv";
            addRow(int.Parse(textBox1.Text),int.Parse(textBox2.Text));

            //string path = @"c:\csv_form\template.csv";
            //string newline = int.Parse(textBox1.Text)+";"+int.Parse(textBox2.Text)+";"+int.Parse(textBox3.Text)+";"+int.Parse(textBox4.Text)+";"+int.Parse(textBox5.Text)+";"+int.Parse(textBox6.Text)+";"+int.Parse(textBox7.Text)+";"+int.Parse(textBox8.Text)+";"+double.Parse(textBox9.Text);
 
        }

     
        private void addRow(int zone1, int zone2)
        {
            int[] newline = { zone1, zone2 };
           // DataGridView1.Rows.Add(newline);    //(or .datasource.)
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }
    }

    
}
