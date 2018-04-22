using Gu.Wpf.UiAutomation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using SpyandPlaybackTestTool.SpyPlaybackObjects;
using SpyandPlaybackTestTool.Ultils;
using SpyandPlaybackTestTool.Actions;
using System.Windows.Automation;
using System.IO;
using Newtonsoft.Json;
using System.Reflection;
using log4net;
using System.Threading;
using System.Threading.Tasks;
using System.Text;
using Newtonsoft.Json.Linq;
using SpyandPlaybackTestTool.Views;
namespace SpyandPlaybackTestTool.Views
{
    public partial class CheckPointForm : Form
    {
        public string ControlType { get; set; }
        public bool cpIsEmpty { get; set; }
        public bool cpIsReadOnly { get; set; }
        public bool cpIsEnabled { get; set; }
        public bool cpIsEqual { get; set; }
       public string expectedVal { get; set; }
        public CheckPointForm()
        {
            InitializeComponent();
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.AllowUserToResizeRows = false;
            foreach (DataGridViewColumn col in dataGridView1.Columns)
                col.SortMode = DataGridViewColumnSortMode.NotSortable;
            (dataGridView1.Columns[0] as DataGridViewCheckBoxColumn).TrueValue = true;
            (dataGridView1.Columns[0] as DataGridViewCheckBoxColumn).FalseValue = false;
        }
        public void textBoxCheckPoint()
        {
            
            string[] a = { "IsEmpty", "IsReadOnly", "IsEqual", "IsEnabled" };

            for (int i=0;i<4;i++)
            {
                DataGridViewRow row = (DataGridViewRow)dataGridView1.Rows[0].Clone();
                row.Cells[1].Value = a[i];
     
                dataGridView1.Rows.Add(row);
            }
            dataGridView1.Rows[0].Cells[2].ReadOnly = true;
            dataGridView1.Rows[1].Cells[2].ReadOnly = true;
            dataGridView1.Rows[3].Cells[2].ReadOnly = true;
        }

        private void CheckPointForm_Load(object sender, EventArgs e)
        {
            switch (ControlType)
            {
                case "TextBox":
                    {
                        if (dataGridView1.RowCount <= 1)
                        {
                            cpIsEmpty = false;
                            cpIsReadOnly = false;
                            cpIsEqual = false;
                            cpIsEnabled = false;
                            expectedVal = "";
                            textBoxCheckPoint();
                            dataGridView1.AllowUserToAddRows = false;
                        }
                        else
                        {
                            for (int i = 0; i < 4; i++)
                            {
                                if (cpIsEmpty == true)
                                {
                                    ((DataGridViewCheckBoxCell)dataGridView1.Rows[i].Cells[0]).Value = ((DataGridViewCheckBoxCell)dataGridView1.Rows[i].Cells[0]).TrueValue;
                                }
                                if (cpIsReadOnly == true)
                                {
                                    ((DataGridViewCheckBoxCell)dataGridView1.Rows[i].Cells[0]).Value = ((DataGridViewCheckBoxCell)dataGridView1.Rows[i].Cells[0]).TrueValue;
                                }
                                if (cpIsEqual == true)
                                {
                                    ((DataGridViewCheckBoxCell)dataGridView1.Rows[i].Cells[0]).Value = ((DataGridViewCheckBoxCell)dataGridView1.Rows[i].Cells[0]).TrueValue;
                                    if (expectedVal != null)
                                        dataGridView1.Rows[i].Cells[2].Value = expectedVal;
                                }
                                if (cpIsEnabled == true)
                                {
                                    ((DataGridViewCheckBoxCell)dataGridView1.Rows[i].Cells[0]).Value = ((DataGridViewCheckBoxCell)dataGridView1.Rows[i].Cells[0]).TrueValue;
                                }
                            }
                        }
                    }
                    break;
                default:
                    break;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 4; i++)
            {
                if (((DataGridViewCheckBoxCell)dataGridView1.Rows[i].Cells[0]).Value == ((DataGridViewCheckBoxCell)dataGridView1.Rows[i].Cells[0]).TrueValue)
                {
                    System.Windows.Forms.MessageBox.Show(dataGridView1.Rows[i].Cells[1].Value.ToString());
                    if (i == 0)
                    {
                        cpIsEmpty = true;
                    }
                    if (i == 1)
                    {
                        cpIsReadOnly = true;
                    }
                    if (i == 2)
                    {
                        cpIsEqual = true;
                        if(dataGridView1.Rows[i].Cells[2].Value!=null)
                        expectedVal = dataGridView1.Rows[i].Cells[2].Value.ToString();
                    }
                    if (i == 3)
                    {
                        cpIsEnabled = true;
                    }
                }

            }
            this.Close();
          
        }
    }
}
