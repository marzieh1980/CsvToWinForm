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
        //private int tcntExceedTemp;

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
            //dataGridView1.DataSource = LoadCSV(textBox1.Text);
            LoadCSV(textBox1.Text);
        }
        private void LoadCSV(string filePath )
        {
            try
            {
                DataTable dt = new DataTable();
                string[] lines = File.ReadAllLines(filePath);

                if (lines.Length > 0)
                {
                    //first line to create header
                    string firstLine = lines[0];
                    string[] headerLable = firstLine.Split(';');
                    foreach (string headerWord in headerLable)
                    {
                        dt.Columns.Add(new DataColumn(headerWord));
                    }
                    //for data
                    for (int i = 1; i < lines.Length; i++)
                    {
                        string[] dataWords = lines[i].Split(';');
                        DataRow dr = dt.NewRow();
                        int columnIndex = 0;
                        foreach (string headerWord in headerLable)
                        {
                            dr[headerWord] = dataWords[columnIndex++];
                        }
                        dt.Rows.Add(dr);
                    }
                }
                if (dt.Rows.Count > 0)
                {
                    dataGridView1.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                var v = ex.ToString();
            }
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
                        csv += "\r\n";        //add new line
                    }
                    string folderPath = "C:\\CSV_FORM\\";        //define the path, where we will put the file

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
            using (StreamReader reader = new StreamReader(@"C:\CSV_FORM\ExportedTemp.csv"))
            {
                string line;
                // Read line by line  
                while ((line = reader.ReadLine()) != null)
                {
                    allData += line + "\r\n";       // after read ever line add it to string and go to next line for next read 
                }
            }

            String newRecord = textBox2.Text+";"+textBox3.Text+";"+textBox4.Text+";"+textBox5.Text+";"+
                textBox6.Text+";"+textBox7.Text+";"+textBox8.Text+";"+textBox9.Text+";"+textBox10.Text+";";
           
            string radioV = "";
            bool isChecked = radioBtn1.Checked;
            if (isChecked)
                radioV = radioBtn1.Text;        // if checked is true ==>  put OK
            else
                radioV = radioBtn2.Text;

            //// check if the exceed tem has value more than 4
            //if (tcntExceedTemp > 4)
            //{
            //    radioV = radioBtn2.Text;
            //}
            //    radioV = radioBtn1.Text;

            allData += newRecord;
            allData += radioV;

           

            StreamWriter file = new StreamWriter(@"C:\CSV_FORM\ExportedTemp.csv");
            file.WriteLine(allData);
            file.Close();
            dataGridView1.Update();          // refresh the datagrid
            //dataGridView1.Refresh();
        }

        // control the input of new inserted value for temperature
        private void textBox_KeyPress(object sender, KeyPressEventArgs e)
        { 
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
                DialogResult dt = MessageBox.Show("please insert only Number!");
            }

        }

        // control the value inserted should be between 90 & 120
        private void textBox_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            int cntExceedTemp = 0;
            TextBox tb = sender as TextBox;
            if (tb != null)
            {
                int i;
                if (int.TryParse(tb.Text, out i))
                {
                    if (i >= 90 && i <= 120)
                        return;
                }
            }
            MessageBox.Show("Temperture non è in range definito, sei sicuro di voler confermare?");
            cntExceedTemp += 1;    // check the time of exceed temp- every the temp is more than range add 1 to count 
         
        }

 
    }
}


