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
using SpyandPlaybackTestTool.CheckPoints;
namespace SpyandPlaybackTestTool.Views
{
    public partial class CheckPointForm : Form
    {
        public string ControlType { get; set; }
        public TextBoxCheckPoint textBoxCP = new TextBoxCheckPoint();
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
        private void CheckPointForm_Load(object sender, EventArgs e)
        {
            switch (ControlType)
            {
                case "TextBox":
                    if (dataGridView1.Rows.Count > 1)
                    {
                        dataGridView1.Rows.Clear();
                    }
                    dataGridView1.AllowUserToAddRows = true;
                    for (int i = 0; i < 4; i++)
                    {
                          
                            DataGridViewRow row = (DataGridViewRow)dataGridView1.Rows[0].Clone();
                            row.Cells[1].Value = textBoxCP.cpList[i];
                       
                            dataGridView1.Rows.Add(row);
                        DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)dataGridView1.Rows[i].Cells[0];
                        switch (textBoxCP.cpList[i])
                        {
                            case "IsEmpty":
                                dataGridView1.Rows[i].Cells[2].ReadOnly = true;
                                if (textBoxCP.cpIsEmpty == true)
                                {
                                    chk.Value = chk.TrueValue;
                                }
                                else
                                    chk.Value = chk.FalseValue;
                                break;
                            case "IsReadOnly":
                                dataGridView1.Rows[i].Cells[2].ReadOnly = true;
                                if (textBoxCP.cpIsReadOnly == true)
                                {
                                    chk.Value = chk.TrueValue;
                                }
                                else
                                    chk.Value = chk.FalseValue;
                                break;
                            case "IsEnabled":
                                dataGridView1.Rows[i].Cells[2].ReadOnly = true;
                                if (textBoxCP.cpIsEnabled == true)
                                {
                                    chk.Value = chk.TrueValue;
                                }
                                else
                                    chk.Value = chk.FalseValue;
                                break;
                            case "IsEqual":
                                dataGridView1.Rows[i].Cells[2].ReadOnly = false;
                                if (textBoxCP.cpIsEqual == true)
                                {
                                    chk.Value = chk.TrueValue;
                                    if (textBoxCP.expectedVal != null)
                                        dataGridView1.Rows[i].Cells[2].Value = textBoxCP.expectedVal;
                                }
                                else
                                {
                                    chk.Value = chk.FalseValue;
                                    dataGridView1.Rows[i].Cells[2].Value = "";
                                }   
                                break;
                            default:
                                break;
                        }
                    }
                    dataGridView1.AllowUserToAddRows = false;

                    break;
                default:
                    if (dataGridView1.Rows.Count > 1)
                        dataGridView1.Rows.Clear();
                    break;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 4; i++)
            {
                if (((DataGridViewCheckBoxCell)dataGridView1.Rows[i].Cells[0]).Value == ((DataGridViewCheckBoxCell)dataGridView1.Rows[i].Cells[0]).TrueValue)
                {
                    switch(dataGridView1.Rows[i].Cells[1].Value)
                    {
                        case "IsEmpty":
                            textBoxCP.cpIsEmpty = true;
                            break;
                        case "IsReadOnly":
                            textBoxCP.cpIsReadOnly = true;
                            break;
                        case "IsEnabled":
                            textBoxCP.cpIsEnabled = true;
                            break;
                        case "IsEqual":
                            textBoxCP.cpIsEqual = true;
                            if (dataGridView1.Rows[i].Cells[2].Value != null)
                                textBoxCP.expectedVal = dataGridView1.Rows[i].Cells[2].Value.ToString();
                            break;
                        default:
                            break;
                    }
                }
                else
                {
                    switch (dataGridView1.Rows[i].Cells[1].Value)
                    {
                        case "IsEmpty":
                            textBoxCP.cpIsEmpty = false;
                            break;
                        case "IsReadOnly":
                            textBoxCP.cpIsReadOnly = false;
                            break;
                        case "IsEnabled":
                            textBoxCP.cpIsEnabled = false;
                            break;
                        case "IsEqual":
                            textBoxCP.cpIsEqual = false;
                            textBoxCP.expectedVal = "";
                            break;
                        default:
                            break;
                    }
          
                }

            }
            this.Close(); 
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
    
        }
    }
}
