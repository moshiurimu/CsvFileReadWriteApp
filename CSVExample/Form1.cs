using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CSVLib;

namespace CSVExample
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private string fileLocation = @"namelist.csv";
        private void saveButton_Click(object sender, EventArgs e)
        {
            FileStream FileStreamReader = new FileStream(fileLocation,FileMode.OpenOrCreate);
            CsvFileReader aReader = new CsvFileReader(FileStreamReader);
            List<string> personRecod = new List<string>();
            
                while (aReader.ReadRow(personRecod))
                {
                    if (personRecod[2]==contactTextBox.Text)
                    {
                        MessageBox.Show("Contact No is alredy exist!");
                        FileStreamReader.Close();
                        return;
                    }
                }
            
            FileStreamReader.Close();

            FileStream FileStreamWriter= new FileStream(fileLocation, FileMode.Append);
            CsvFileWriter aWriter = new CsvFileWriter(FileStreamWriter);
            List<string> personList = new List<string>();
            personList.Add(nameTextBox.Text);
            personList.Add(emailTextBox.Text);
            personList.Add(contactTextBox.Text);
            personList.Add(homeContTextBox.Text);
            personList.Add(homeAddreTextBox.Text);
            aWriter.WriteRow(personList);
            FileStreamWriter.Close();
            MessageBox.Show("Add Sucsessfuly!");
            Clear();
        }

        private void showButton_Click(object sender, EventArgs e)
        {
           FileStream aStream = new FileStream(fileLocation, FileMode.OpenOrCreate);
            CsvFileReader aReader = new CsvFileReader(aStream);
            List<string> personList = new List<string>();
            allPersonView.Items.Clear();
            while (aReader.ReadRow(personList))
            {

                ListViewItem items = new ListViewItem(personList[0]);
                items.SubItems.Add(personList[1]);
                items.SubItems.Add(personList[2]);
                items.SubItems.Add(personList[3]);
                items.SubItems.Add(personList[4]);
                allPersonView.Items.Add(items);

            }

            aStream.Close();

        }

        private void searchButton_Click(object sender, EventArgs e)
        {
            FileStream aStream = new FileStream(fileLocation, FileMode.OpenOrCreate);
            CsvFileReader aReader = new CsvFileReader(aStream);
            List<string> personList = new List<string>();
            allPersonView.Items.Clear();
            while (aReader.ReadRow(personList))
            {

                if (personList[0] == searchTextBox.Text || personList[1] == searchTextBox.Text || personList[2] == searchTextBox.Text)
                {

                    ListViewItem items = new ListViewItem(personList[0]);
                    items.SubItems.Add(personList[1]);
                    items.SubItems.Add(personList[2]);
                    items.SubItems.Add(personList[3]);
                    items.SubItems.Add(personList[4]);
                    allPersonView.Items.Add(items);
                }
            }

            if (allPersonView.Items.Count==0)
            {
                MessageBox.Show("Dosn't match!");
                searchTextBox.Text = "";
            }

            aStream.Close();
        }

        private void Clear()
        { 
            nameTextBox.Text= "";
            emailTextBox.Text= "";
            contactTextBox.Text= "";
            homeContTextBox.Text= "";
            homeAddreTextBox.Text = "";
        }

       
    }
}
