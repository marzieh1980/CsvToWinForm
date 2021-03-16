using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using System.IO;


namespace CsvToWinForm
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        // browse for the file which have to be a .CSV file
        private void btnBrowse_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "CSVFile(*.csv)|*.csv";
            dlg.ShowDialog();
            textBox1.Text = dlg.FileName;
        }

        // import file csv already browse
        private void btnImport_click_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = LoadCSV(textBox1.Text);          
        }

        // gain values of csv
        public List<ColumnsClass> LoadCSV(string csvFile)
        {
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


        // export again the file on a .csv file
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
                    //define the path, where we will put the file
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

        // insert new record(temperature) , save into the source 
        private void btnSaveRec_Click(object sender, EventArgs e)
        {
            string allData = "";
            using (StreamReader reader = new StreamReader(@"C:\CSV_FORM\template_noHeader.csv"))
            {
                string line;
                // Read line by line  
                while ((line = reader.ReadLine()) != null)
                {
                    allData += line + "\r\n";       //after read ever line add it to string and go to next line for next read 
                }
            }

            String newRecord = textBox2.Text+";"+textBox3.Text+";"+textBox4.Text+";"+textBox5.Text+";"+textBox6.Text+";"+textBox7.Text+";"+textBox8.Text+";"+textBox9.Text+";"+textBox10.Text+";";
           
            string radioV = "";
            bool isChecked = radioBtn1.Checked;
            if (isChecked)
                radioV = radioBtn1.Text;        //if checked is true ==>  put OK
            else
                radioV = radioBtn2.Text;

            allData += newRecord;
            allData += radioV;


            System.IO.StreamWriter file = new System.IO.StreamWriter(@"C:\CSV_FORM\template_noHeader.csv");
            file.WriteLine(allData);
            file.Close();
            //dataGridView1.Update();          //refresh the datagrid
            //dataGridView1.Refresh();
        }

        // control the input of new inserted value for temperature
        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        { 
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;                       //if not a num 
                DialogResult dt = MessageBox.Show("please insert only Number!");
            }
        }

        // control the value inserted should be between 90 & 120
        private void chechValueTemp_TextChanged(object sender, EventArgs e)
        {
            int value;
            
                if (int.TryParse(textBox2.Text, out value))
                {
                    if ((value < 90 || value > 120))
                    {
                        MessageBox.Show("Temperture non è in range definito, sei sicuro di confermare! ");
                    }
                    // int cnt = cnt +1;
                }
          
        } 
    }

}

