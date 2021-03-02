using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CsvHelper;
using System.IO;


namespace CsvToWinForm
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        
        private void btnImport_click_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = LoadCSV(textBox1.Text);
        }

        public List<ColumnsClass>LoadCSV(string csvFile)
        {
        // OpenFileDialog.Filter = "CSVFile(*.csv)|*.csv";           //To check csv ????

            var query = from l in File.ReadLines(csvFile)
                        let data = l.Split(';')
                        select new ColumnsClass
                        {
                            Zone1 = int.Parse(data[0]),
                            Zone2 = int.Parse(data[1]),
                            Zone3 = int.Parse(data[2]),
                            Zone4 = int.Parse(data[3]),
                            Zone5 = int.Parse(data[4]),
                            Zone6 = int.Parse(data[5]),
                            Zone7 = int.Parse(data[6]),
                            Zone8 = int.Parse(data[7]),
                            MediaTemperature = double.Parse(data[8]),
                            ControlloTemperature = data[9]
                        };

            return query.ToList();
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.ShowDialog();
            textBox1.Text = dlg.FileName;
        }

       
        private void btnExport_Click(object sender, EventArgs e)
        {
            {
                try
                {
                    // build csv file data as a comma seperated string
                    string csv = string.Empty;

                    //Add the Header row for csv file
                    foreach (DataGridViewColumn column in dataGridView1.Columns)
                    {
                        csv += column.HeaderText + ';';
                    }
                    //add new line
                    csv += "\r\n";

                    //add the rows
                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        foreach (DataGridViewCell cell in row.Cells)
                        {
                            if (cell.Value != null)
                            {
                                //add the data rows
                                csv += cell.Value.ToString().Replace(";", ",") + ';';
                            }
                            // break if null
                        }
                        //add new line
                        csv += "\r\n";
                    }
                    //Exporting to csv 
                    string folderPath = "C:\\CSV_FORM\\";
                    
                    File.WriteAllText(folderPath + "ExportedTemp.csv", csv);
                    MessageBox.Show("export done");
                }
                catch (Exception ex)
                {
                    var v = ex.ToString();
                }
            }
        }

        private void btnSaveRec_Click(object sender, EventArgs e)
        {
            string allData = "";
            using (StreamReader reader = new StreamReader(@"C:\CSV_FORM\template_noHeader.csv"))
            {
                string line;
                // Read line by line  
                while ((line = reader.ReadLine()) != null)
                {
                    allData += line + "\r\n";
                }
            }

            String newRecord = textBox2.Text+";"+textBox3.Text+";"+textBox4.Text+";"+textBox5.Text+";"+textBox6.Text+";"+textBox7.Text+";"+textBox8.Text+";"+textBox9.Text+";"+textBox10.Text+";";
            string radioV = "";
            bool isChecked = radioBtn1.Checked;
            if (isChecked)
                radioV = radioBtn1.Text;
            else
                radioV = radioBtn2.Text;

            allData += newRecord;
            allData += radioV;

            System.IO.StreamWriter file = new System.IO.StreamWriter(@"C:\CSV_FORM\template_noHeader.csv");
            file.WriteLine(allData);
           
            file.Close();
            //dataGridView1.Update();
            //dataGridView1.Refresh();
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
                DialogResult dt = MessageBox.Show("please insert only Number!");
            }
        }
    }

}
