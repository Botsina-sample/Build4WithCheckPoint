﻿using Gu.Wpf.UiAutomation;
using log4net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SpyandPlaybackTestTool.Actions;
using SpyandPlaybackTestTool.SpyPlaybackObjects;
using SpyandPlaybackTestTool.Ultils;
using SpyandPlaybackTestTool.Views;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using SpyandPlaybackTestTool.CheckPoints;
using System.Dynamic;
using System.Windows.Automation;

namespace SpyandPlaybackTestTool
{
    public partial class Form1 : Form
    {
        #region Variables

        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        private IReadOnlyList<UiElement> ElementList;

        private SpyObject[] SpyObjectList;
        private List<PlaybackObject> PlaybackObjectList = new List<PlaybackObject>();
        private List<PlaybackObject> PlaybackObjectScript = new List<PlaybackObject>();

        //private ScriptFile[] scriptFiles;
        private List<ScriptFile> scriptFiles = new List<ScriptFile>();
        
        /// <summary>
        /// CheckPoint Form List
        /// </summary>
        CheckPointForm[] CpFormList;

        /// <summary>
        /// This application process
        /// </summary>
        private Process thisProc = Process.GetCurrentProcess();


        /// <summary>
        /// Application under test process
        /// </summary>
        public static Process AUTPROC;

        /// <summary>
        /// Check if AUT exited
        /// </summary>
        public bool checkExited;

        /// <summary>
        /// Playback percentage for ProgressForm
        /// </summary>
        public static int playbackprogress;

        /// <summary>
        /// Playback is on your off
        /// </summary>
        public static bool playbackstatus;

        /// <summary>
        /// Use for Stop playback while running
        /// </summary>
        public static bool stop_playback;

        /// <summary>
        /// Check if scenario playback is ON.
        /// Prevent PlaybackTestScript focuses this process when finished a test script in scenario array
        /// </summary>
        public bool ScenarioStatus;

        /// <summary>
        /// Playback percentage
        /// </summary>
        public int percentage;

        /// <summary>
        /// Scenarios percentage
        /// </summary>
        public int s_percentage;

        /// <summary>
        /// Sum all steps from test script to calculate percentage for each step
        /// </summary>
        public int SumAllSteps;

        /// <summary>
        /// Progress Form instance
        /// </summary>
        private ProgressForm pf = new ProgressForm();

        /// <summary>
        /// THREADING
        /// </summary>
        private Thread Th_SPY;

        private Thread Th_CLEARVALUE;
        private Thread Th_PBTSCRIPT;
        private Thread Th_PBTSTEP;
        private Thread Th_PBScenario;

        /// <summary>
        /// CheckPoint Form
        /// </summary>
        CheckPointForm CpForm = new CheckPointForm();
        #endregion Variables

        public Form1()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            log.Info("PROGRAM STARTED");

            this.KeyPreview = true;

            ResultPanelPush.ReadOnly = true;
            ConsolePanelPush.ReadOnly = true;

            toolStripComboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.Columns[0].Visible = false;
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.AllowUserToResizeRows = false;

            (dataGridView1.Columns[0] as DataGridViewCheckBoxColumn).TrueValue = true;
            (dataGridView1.Columns[0] as DataGridViewCheckBoxColumn).FalseValue = false;

            //dataGridView2.AllowUserToResizeRows = false;
            foreach (DataGridViewColumn col in dataGridView2.Columns)
                col.SortMode = DataGridViewColumnSortMode.NotSortable;
            dataGridView2.AllowUserToAddRows = false;
            dataGridView2.RowHeadersVisible = false;
           

            redcircleTip.Visible = true;
            greencircleTip.Visible = false;

            toolStripComboBox1.Enabled = false;
            toolStripTextBox1.Enabled = false;

            //this.MaximumSize = new Size(XX, YY);
            this.MinimumSize = new Size(1280, 720);
        }


        private void Form1_Activated(object sender, EventArgs e)
        {
            if (ProcessForm.isAttached.Equals(false))
            {
                ProcessForm.targetproc = null;
                AUTPROC = null;
                toolStripStatusLabel1.Text = "";
            }
            else if (ProcessForm.isAttached.Equals(true))
            {
                AUTPROC = WindowInteraction.GetProcess(ProcessForm.targetproc);

                toolStripStatusLabel1.Text = ProcessForm.targetproc;
                redcircleTip.Visible = false;
                greencircleTip.Visible = true;

                timer1.Start();
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                if (AUTPROC.HasExited)
                {
                    toolStripStatusLabel1.Text = "";
                    ProcessForm.targetproc = null;
                    ProcessForm.isAttached = false;
                    AUTPROC = null;
                    redcircleTip.Visible = true;
                    greencircleTip.Visible = false;

                    timer1.Stop();

                    System.Windows.Forms.MessageBox.Show("The AUT: " + ProcessForm.targetproc + " has been terminated.", "Warning!!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                    dataGridView1.Rows.Clear();
                }
            }
            catch
            {
                toolStripStatusLabel1.Text = "";
                ProcessForm.targetproc = null;
                ProcessForm.isAttached = false;
                AUTPROC = null;
                redcircleTip.Visible = true;
                greencircleTip.Visible = false;

                timer1.Stop();
            }
        }


        /// <summary>
        /// SPY FUNCTION
        /// </summary>
        /// <param name="mode">normal or respy</param>
        public void Spy(string mode)
        {
            ExceptionCode excode = new ExceptionCode();

            if (ProcessForm.targetproc == null)
            {
                System.Windows.Forms.MessageBox.Show("Please attach AUT process to execute Spy function!", "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            try
            {
                if (mode == "normal")
                {
                    log.Info("BEGIN SPY");
                    ConsolePanelPush.AppendText(DateTime.Now + " - " + "BEGIN SPY" + Environment.NewLine);
                }
                if (mode == "respy")
                {
                    log.Info("BEGIN RESPY");
                    ConsolePanelPush.AppendText(DateTime.Now + " - " + "BEGIN RESPY" + Environment.NewLine);
                }

                //ElementList = GrabAUT.SearchbyFramework("WPF");
                //foreach (UiElement a in ElementList)
                //{
                //    if (a.ClassName.Equals("ComboBox"))
                //    {
                //        a.AsComboBox().Expand();
                //    }
                //}
                ElementList = GrabAUT.SearchbyFramework("WPF");

                dataGridView1.Rows.Clear();
                dataGridView1.AllowUserToAddRows = true;

                SpyObjectList = new SpyObject[ElementList.Count];


                toolStripComboBox1.Enabled = false;
                toolStripTextBox1.Enabled = false;

                dataGridView1.Enabled = true;
                dataGridView2.Enabled = true;

                dataGridView1.Rows.Clear();
                dataGridView1.Refresh();

                for (int i = 0; i < ElementList.Count; i++)
                {
                    SpyObjectList[i] = new SpyObject();
                    SpyObjectList[i].index = i;
                    if (ElementList[i].AutomationId == "" && i - 1 > 0 && ElementList[i - 1].Name != "" && ElementList[i].Name == "")
                        SpyObjectList[i].automationId = (ElementList[i - 1].Name + "_" + ElementList[i].ClassName).Replace(" ", "_").Replace(":", "");
                    else
                        SpyObjectList[i].automationId = ElementList[i].AutomationId;
                    SpyObjectList[i].name = ElementList[i].Name;
                    SpyObjectList[i].type = ElementList[i].ClassName;
                    DataGridViewRow row = (DataGridViewRow)dataGridView1.Rows[i].Clone();
                    row.Cells[1].Value = SpyObjectList[i].index;
                    row.Cells[2].Value = SpyObjectList[i].automationId;
                    row.Cells[3].Value = SpyObjectList[i].name;
                    row.Cells[4].Value = SpyObjectList[i].type;
                    switch (SpyObjectList[i].type.ToString())
                    {
                        case "ComboBox":

                            var cbxItemList = ElementList[i].AsComboBox().Items;
                            //System.Windows.Forms.MessageBox.Show(cbxItemList.Count.ToString());
                            foreach (ComboBoxItem item in cbxItemList)
                            {


                                SpyObjectList[i].itemList.Add(item.Text);

                            }
                            row.Cells[5].Value = "View Items";
                            break;
                        case "ComboBoxEdit":
                            ExpandCollapsePattern expandCollapsePattern = ElementList[i].AutomationElement.GetCurrentPattern(ExpandCollapsePattern.Pattern) as ExpandCollapsePattern;
                            expandCollapsePattern.Expand();
                            AutomationElement a = ElementList[i].AutomationElement;
                            var cbxeItemList = a.FindAll(TreeScope.Subtree, new System.Windows.Automation.PropertyCondition(AutomationElement.ClassNameProperty, "ComboBoxEditItem"));
                            expandCollapsePattern.Collapse();
                            foreach (AutomationElement cbxeitem in cbxeItemList)
                            {


                                SpyObjectList[i].itemList.Add(cbxeitem.Current.Name);

                            }
                            row.Cells[5].Value = "View Items";
                            break;
                        case "DataGrid":
                            var dtgItemList = ElementList[i].AsDataGrid().Rows;

                            foreach (DataGridRow dtgrow in dtgItemList)
                            {
                                string allCells = "";
                                foreach (Gu.Wpf.UiAutomation.DataGridCell dtgcell in dtgrow.Cells)
                                {
                                    allCells += dtgcell.Value.ToString() + " ";
                                }
                                SpyObjectList[i].itemList.Add(allCells);
                            }
                            row.Cells[5].Value = "View Items";
                            break;
                        default:
                            row.Cells[5].Value = "No Item";
                            break;
                    }

                    dataGridView1.Rows.Add(row);
                }

                if (mode == "normal")
                {
                    log.Info("DONE SPY");
                    ConsolePanelPush.AppendText(DateTime.Now + " - " + "DONE SPY" + Environment.NewLine);
                }
                if (mode == "respy")
                {
                    log.Info("DONE RESPY");
                    ConsolePanelPush.AppendText(DateTime.Now + " - " + "DONE RESPY" + Environment.NewLine);
                }

                toolStripComboBox1.Enabled = true;
                toolStripTextBox1.Enabled = true;
                dataGridView1.AllowUserToAddRows = false;
                toolStripTextBox1.Text = "";
                toolStripComboBox1.SelectedIndex = 0;

                // Capture to File
                //CaptureToImage.DoCapture(ProcessName);
                if (mode == "normal")
                {
                    WindowInteraction.FocusWindow(thisProc);
                }
                if (mode == "respy")
                {
                    WindowInteraction.FocusWindow(AUTPROC);
                }
            }
            catch (Exception ex)
            {
                if (ex.HResult == excode.AUT_NOT_FOUND)
                {
                    log.Error(DateTime.Now + " - AUT NOT FOUND");
                    ConsolePanelPush.AppendText(DateTime.Now + " - AUT NOT FOUND");
                    ConsolePanelPush.AppendText(Environment.NewLine);
                    WindowInteraction.FocusWindow(thisProc);
                }
                else if (ex.HResult == excode.NOT_WPF_PROGRAM)
                {
                    log.Error("CANNOT SPY THIS PROGRAM");
                    ConsolePanelPush.AppendText(DateTime.Now + " - CANNOT SPY THIS PROGRAM");
                    ConsolePanelPush.AppendText(Environment.NewLine);
                    WindowInteraction.FocusWindow(thisProc);
                }
                else if (ex.HResult == excode.OBJECT_NULL)
                {
                    log.Error("AUT NOT FOUND");
                    ConsolePanelPush.AppendText(DateTime.Now + " - AUT NOT FOUND" + Environment.NewLine);
                    // ConsolePanelPush.AppendText();
                    WindowInteraction.FocusWindow(thisProc);
                }
                else
                {
                    log.Error("ERROR CODE: " + ex.HResult + "  -----  " + "detail: " + ex.Message);
                    ConsolePanelPush.AppendText("ERROR CODE: " + ex.HResult + "  -----  " + "detail: " + ex.Message);
                    ConsolePanelPush.AppendText(Environment.NewLine);
                    WindowInteraction.FocusWindow(thisProc);
                }
            }
        }

        /// <summary>
        /// PLAYBACK TEST SCRIPT FUNCTION
        /// </summary>
        public void PlaybackTestScript()
        {
            ExceptionCode excode = new ExceptionCode();
            try
            {
                if (ScenarioStatus.Equals(false))
                {
                    percentage = 100 / PlaybackObjectScript.Count();
                }

                if (ScenarioStatus.Equals(true))
                {
                    log.Info("Multiple playbackscript is on");
                }

                for (int i = 0; i < PlaybackObjectScript.Count(); i++)
                {
                    if (stop_playback.Equals(true))
                    {
                        WindowInteraction.FocusWindow(thisProc);
                        return;
                    }

                    WindowInteraction.FocusWindow(AUTPROC);
                    if (GrabAUT.MainWindow.ModalWindows.Count > 0)
                        ElementList = GrabAUT.SearchbyFramework("WPF");

                    // Check if Json type is valid
                    if (PlaybackObjectScript[i].type == "SendKey" || PlaybackObjectScript[i].type == "Button" ||
                       PlaybackObjectScript[i].type == "TextBox" || PlaybackObjectScript[i].type == "WaitEnable" ||
                       PlaybackObjectScript[i].type == "ComboBox" || PlaybackObjectScript[i].type == "ComboBoxEdit" ||
                       PlaybackObjectScript[i].type == "DataGrid" || PlaybackObjectScript[i].type == "RadioButton" || PlaybackObjectScript[i].type == "AutoCompleteCombobox" ||
                       PlaybackObjectScript[i].type == "SendKeyorWaitEnable" || PlaybackObjectScript[i].type == "CheckBox" || PlaybackObjectScript[i].type == "TabItem" ||
                       PlaybackObjectScript[i].type == "PasswordBox" || PlaybackObjectScript[i].type == "RichTextBox")
                    {
                        if (PlaybackObjectScript[i].action == "Click" || PlaybackObjectScript[i].action == "DoubleClick" ||
                           PlaybackObjectScript[i].action == "Select" || PlaybackObjectScript[i].action == "SetText" ||
                           PlaybackObjectScript[i].action == "WaitEnable" || PlaybackObjectScript[i].action == "Unselect" ||
                           PlaybackObjectScript[i].action == "SendKey")
                        {
                            switch (PlaybackObjectScript[i].type)
                            {
                                case "Button":
                                    AbsAction buttonaction = new ButtonAction();
                                    buttonaction.PlaybackObject = PlaybackObjectScript[i];
                                    buttonaction.UiElement = ElementList[PlaybackObjectScript[i].index];
                                    buttonaction.DoExecute();
                                    if (buttonaction.Result == true)
                                        PlaybackLogger(buttonaction.PlaybackObject.index, buttonaction.PlaybackObject.type, true);
                                    else
                                        PlaybackLogger(buttonaction.PlaybackObject.index, buttonaction.PlaybackObject.type, false);
                                    break;

                                case "TextBox":
                                    AbsAction textboxaction = new TextBoxAction();
                                    textboxaction.PlaybackObject = PlaybackObjectScript[i];
                                    textboxaction.UiElement = ElementList[PlaybackObjectScript[i].index];
                                    textboxaction.DoExecute();
                                    if (textboxaction.Result == true)
                                        PlaybackLogger(textboxaction.PlaybackObject.index, textboxaction.PlaybackObject.type, true);
                                    else
                                        PlaybackLogger(textboxaction.PlaybackObject.index, textboxaction.PlaybackObject.type, false);
                                    PlaybackObjectScript[i].CheckPoint.UiElement = textboxaction.UiElement;
                                    (PlaybackObjectScript[i].CheckPoint as TextBoxCheckPoint).DoExecuteCheckPoint();
                                    if ((PlaybackObjectScript[i].CheckPoint as TextBoxCheckPoint).cpIsEmpty == true)
                                    {
                                        log.Info("Result of CheckPoint IsEmpty: " + (PlaybackObjectScript[i].CheckPoint as TextBoxCheckPoint).cpIsEmptyResult.ToString().Trim());
                                        ResultPanelPush.AppendText(DateTime.Now + " - " + "Result of CheckPoint IsEmpty: "
                                            + (PlaybackObjectScript[i].CheckPoint as TextBoxCheckPoint).cpIsEmptyResult.ToString().Trim() + Environment.NewLine);
                                    }
                                    if ((PlaybackObjectScript[i].CheckPoint as TextBoxCheckPoint).cpIsEnabled == true)
                                    {
                                        log.Info("Result of CheckPoint IsEnabled: " + (PlaybackObjectScript[i].CheckPoint as TextBoxCheckPoint).cpIsEnabledResult.ToString().Trim());
                                        ResultPanelPush.AppendText(DateTime.Now + " - " + "Result of CheckPoint IsEnabled: "
                                            + (PlaybackObjectScript[i].CheckPoint as TextBoxCheckPoint).cpIsEnabledResult.ToString().Trim() + Environment.NewLine);
                                        //ResultPanelPush.AppendText((PlaybackObjectList[i].CheckPoint as TextBoxCheckPoint).cpIsEnabledResult.ToString());
                                    }
                                    if ((PlaybackObjectScript[i].CheckPoint as TextBoxCheckPoint).cpIsEqual == true)
                                    {
                                        log.Info("Result of CheckPoint IsEqual: " + (PlaybackObjectScript[i].CheckPoint as TextBoxCheckPoint).cpIsEqualResult.ToString().Trim());
                                        ResultPanelPush.AppendText(DateTime.Now + " - " + "Result of CheckPoint IsEqual: "
                                            + (PlaybackObjectScript[i].CheckPoint as TextBoxCheckPoint).cpIsEqualResult.ToString().Trim() + Environment.NewLine);
                                    }
                                    if ((PlaybackObjectScript[i].CheckPoint as TextBoxCheckPoint).cpIsReadOnly == true)
                                    {
                                        log.Info("Result of CheckPoint IsReadOnly: " + (PlaybackObjectScript[i].CheckPoint as TextBoxCheckPoint).cpIsReadOnlyResult.ToString().Trim());
                                        ResultPanelPush.AppendText(DateTime.Now + " - " + "Result of CheckPoint IsReadOnly: "
                                            + (PlaybackObjectScript[i].CheckPoint as TextBoxCheckPoint).cpIsReadOnlyResult.ToString().Trim() + Environment.NewLine);
                                    }
                                    break;


                                case "RichTextBox":
                                    AbsAction RichTextBoxAction = new RichTextBoxAction();
                                    RichTextBoxAction.PlaybackObject = PlaybackObjectScript[i];
                                    RichTextBoxAction.UiElement = ElementList[PlaybackObjectScript[i].index];
                                    RichTextBoxAction.DoExecute();
                                    if (RichTextBoxAction.Result == true)
                                        PlaybackLogger(RichTextBoxAction.PlaybackObject.index, RichTextBoxAction.PlaybackObject.type, true);
                                    else
                                        PlaybackLogger(RichTextBoxAction.PlaybackObject.index, RichTextBoxAction.PlaybackObject.type, false);
                                    break;

                                case "PasswordBox":
                                    AbsAction PasswordBoxAction = new PasswordBoxAction();
                                    PasswordBoxAction.PlaybackObject = PlaybackObjectScript[i];
                                    PasswordBoxAction.UiElement = ElementList[PlaybackObjectScript[i].index];
                                    PasswordBoxAction.DoExecute();
                                    if (PasswordBoxAction.Result == true)
                                        PlaybackLogger(PasswordBoxAction.PlaybackObject.index, PasswordBoxAction.PlaybackObject.type, true);
                                    else
                                        PlaybackLogger(PasswordBoxAction.PlaybackObject.index, PasswordBoxAction.PlaybackObject.type, false);
                                    break;

                                case "ComboBox":
                                    AbsAction comboboxaction = new ComboBoxAction();
                                    comboboxaction.PlaybackObject = PlaybackObjectScript[i];
                                    comboboxaction.UiElement = ElementList[PlaybackObjectScript[i].index];
                                    comboboxaction.DoExecute();
                                    if (comboboxaction.Result == true)
                                        PlaybackLogger(comboboxaction.PlaybackObject.index, comboboxaction.PlaybackObject.type, true);
                                    else
                                        PlaybackLogger(comboboxaction.PlaybackObject.index, comboboxaction.PlaybackObject.type, false);
                                    break;

                                case "AutoCompleteCombobox":
                                    AbsAction ATcomboboxaction = new ComboBoxEditAction();
                                    ATcomboboxaction.PlaybackObject = PlaybackObjectScript[i];
                                    ATcomboboxaction.UiElement = ElementList[PlaybackObjectScript[i].index];
                                    ATcomboboxaction.DoExecute();
                                    if (ATcomboboxaction.Result == true)
                                        PlaybackLogger(ATcomboboxaction.PlaybackObject.index, ATcomboboxaction.PlaybackObject.type, true);
                                    else
                                        PlaybackLogger(ATcomboboxaction.PlaybackObject.index, ATcomboboxaction.PlaybackObject.type, false);
                                    break;

                                case "ComboBoxEdit":
                                    AbsAction comboboxeditaction = new ComboBoxEditAction();
                                    comboboxeditaction.PlaybackObject = PlaybackObjectScript[i];
                                    comboboxeditaction.UiElement = ElementList[PlaybackObjectScript[i].index];
                                    comboboxeditaction.DoExecute();
                                    if (comboboxeditaction.Result == true)
                                        PlaybackLogger(comboboxeditaction.PlaybackObject.index, comboboxeditaction.PlaybackObject.type, true);
                                    else
                                        PlaybackLogger(comboboxeditaction.PlaybackObject.index, comboboxeditaction.PlaybackObject.type, false);

                                    break;

                                case "DataGrid":
                                    AbsAction datagridaction = new DataGridAction();
                                    datagridaction.PlaybackObject = PlaybackObjectScript[i];
                                    datagridaction.UiElement = ElementList[PlaybackObjectScript[i].index];
                                    datagridaction.DoExecute();
                                    if (datagridaction.Result == true)
                                        PlaybackLogger(datagridaction.PlaybackObject.index, datagridaction.PlaybackObject.type, true);
                                    else
                                        PlaybackLogger(datagridaction.PlaybackObject.index, datagridaction.PlaybackObject.type, false);
                                    break;

                                case "RadioButton":
                                    AbsAction RadioButtonAction = new RadioButtonAction();
                                    RadioButtonAction.PlaybackObject = PlaybackObjectScript[i];
                                    RadioButtonAction.UiElement = ElementList[PlaybackObjectScript[i].index];
                                    RadioButtonAction.DoExecute();
                                    if (RadioButtonAction.Result == true)
                                        PlaybackLogger(RadioButtonAction.PlaybackObject.index, RadioButtonAction.PlaybackObject.type, true);
                                    else
                                        PlaybackLogger(RadioButtonAction.PlaybackObject.index, RadioButtonAction.PlaybackObject.type, false);
                                    break;

                                case "CheckBox":
                                    AbsAction CheckBoxAction = new CheckBoxAction();
                                    CheckBoxAction.PlaybackObject = PlaybackObjectScript[i];
                                    CheckBoxAction.UiElement = ElementList[PlaybackObjectScript[i].index];
                                    CheckBoxAction.DoExecute();
                                    if (CheckBoxAction.Result == true)
                                        PlaybackLogger(CheckBoxAction.PlaybackObject.index, CheckBoxAction.PlaybackObject.type, true);
                                    else
                                        PlaybackLogger(CheckBoxAction.PlaybackObject.index, CheckBoxAction.PlaybackObject.type, false);
                                    break;

                                case "TabItem":
                                    AbsAction TabItemAction = new TabItemAction();
                                    TabItemAction.PlaybackObject = PlaybackObjectScript[i];
                                    TabItemAction.UiElement = ElementList[TabItemAction.PlaybackObject.index];

                                    TabItemAction.DoExecute();
                                    if (TabItemAction.Result == true)
                                        PlaybackLogger(TabItemAction.PlaybackObject.index, TabItemAction.PlaybackObject.type, true);
                                    else
                                        PlaybackLogger(TabItemAction.PlaybackObject.index, TabItemAction.PlaybackObject.type, false);
                                    break;

                                case "SendKeyorWaitEnable":
                                    if (PlaybackObjectScript[i].action == "SendKey")
                                    {
                                        AbsAction SendKeyAction = new SpyandPlaybackTestTool.Actions.SendKey();
                                        SendKeyAction.PlaybackObject = PlaybackObjectScript[i];
                                        SendKeyAction.DoExecute();

                                        log.Info("Key " + PlaybackObjectScript[i].text + " HAS BEEN SENT");
                                        ResultPanelPush.AppendText(DateTime.Now + " - " + "Key " + PlaybackObjectScript[i].text + " HAS BEEN SENT");
                                        ResultPanelPush.AppendText(Environment.NewLine);
                                    }
                                    else if (PlaybackObjectScript[i].action == "WaitEnable")
                                    {
                                        AbsAction WaitEnable = new WaitEnable();
                                        WaitEnable.PlaybackObject = PlaybackObjectScript[i];
                                        WaitEnable.DoExecute();

                                        log.Info("WATIED FOR " + int.Parse(WaitEnable.PlaybackObject.text) / 1000 + "s");
                                        ResultPanelPush.AppendText(DateTime.Now + " - " + "WATIED FOR " + int.Parse(WaitEnable.PlaybackObject.text) / 1000 + "s");
                                        ResultPanelPush.AppendText(Environment.NewLine);
                                    }
                                    break;

                                default:
                                    break;
                            }
                        }
                        else
                        {
                            log.Fatal("FAILED at " + "JSON INDEX " + PlaybackObjectScript[i].index + " - " + "HAS AN INVALID ACTION: " + PlaybackObjectScript[i].action);
                            ResultPanelPush.AppendText(DateTime.Now + " - " + "FAILED at " + "JSON INDEX " + PlaybackObjectScript[i].index + " - " + "HAS AN INVALID ACTION: " + PlaybackObjectScript[i].action);
                            ResultPanelPush.AppendText(Environment.NewLine);
                        }
                    }
                    else
                    {
                        log.Fatal("FAILED at " + "JSON INDEX " + PlaybackObjectScript[i].index + " - " + "HAS AN INVALID TYPE: " + PlaybackObjectScript[i].type);
                        ResultPanelPush.AppendText(DateTime.Now + " - " + "FAILED at " + "JSON INDEX " + PlaybackObjectScript[i].index + " - " + "HAS AN INVALID TYPE: " + PlaybackObjectScript[i].type);
                        ResultPanelPush.AppendText(Environment.NewLine);
                    }
                    if (GrabAUT.MainWindow.ModalWindows.Count > 0)
                    {
                        ElementList = GrabAUT.SearchbyFramework("WPF");
                    }

                    if (ScenarioStatus.Equals(false))
                    {
                        playbackprogress += percentage;
                    }

                    if (ScenarioStatus.Equals(true))
                    {
                        playbackprogress += s_percentage;
                        log.Info(s_percentage);
                    }
                }

                if (ScenarioStatus.Equals(true) && playbackstatus.Equals(false))
                {
                    //playbackprogress = 100;
                }
                else if (ScenarioStatus.Equals(false) && playbackstatus.Equals(true))
                {

                    playbackstatus = false;
                    playbackprogress = 100;
                    WindowInteraction.FocusWindow(thisProc);
                }

                this.pf.Dispose();
            }
            catch (Exception ex)
            {
                if (ex.HResult == excode.AUT_NOT_FOUND)
                {
                    log.Error("AUT NOT FOUND");
                    ConsolePanelPush.AppendText(DateTime.Now + " - " + "AUT NOT FOUND");
                    ConsolePanelPush.AppendText(Environment.NewLine);
                }
                else if (ex.HResult == excode.CANNOT_FOCUS_ON_AUT)
                {
                    log.Error("CANNOT FOCUS ON AUT OR INPUT WAS NOT ENABLE");
                    ConsolePanelPush.AppendText(DateTime.Now + " - " + "CANNOT FOCUS ON AUT OR INPUT WAS NOT ENABLE");
                    ConsolePanelPush.AppendText(Environment.NewLine);
                }
                else if (ex.HResult == excode.INVALID_SCRIPT)
                {
                    log.Error("CANNOT USE THE CURRENT SCRIPT ON THIS SCREEN");
                    ConsolePanelPush.AppendText(DateTime.Now + " - " + "CANNOT USE THE CURRENT SCRIPT ON THIS SCREEN");
                    ConsolePanelPush.AppendText(Environment.NewLine);
                }
                else if (ex.HResult == excode.SCRIPT_ERROR)
                {
                    log.Error("SCRIPT FORMAT IS INVALID");
                    ConsolePanelPush.AppendText(DateTime.Now + " - " + "SCRIPT FORMAT IS INVALID");
                    ConsolePanelPush.AppendText(Environment.NewLine);
                }
                else if (ex.HResult == excode.AUT_QUIT_DURING_OP)
                {
                    log.Error("AUT QUIT DURING PLAYBACK OPERATION");
                    ConsolePanelPush.AppendText(DateTime.Now + " - " + "AUT QUIT DURING PLAYBACK OPERATION");
                    ConsolePanelPush.AppendText(Environment.NewLine);
                }
                else if (ex.HResult == excode.INVALID_INDEX)
                {
                    log.Error("INPUT VALUE WAS OUT OF RANGE");
                    ConsolePanelPush.AppendText(DateTime.Now + " - " + "INPUT VALUE WAS OUT OF RANGE");
                    ConsolePanelPush.AppendText(Environment.NewLine);
                }
                else if (ex.HResult == excode.OBJECT_NULL)
                {
                    log.Error("CANNOT FOUND PLAYBACK SCRIPT");
                    ConsolePanelPush.AppendText(DateTime.Now + " - " + "CANNOT FOUND PLAYBACK SCRIPT");
                    ConsolePanelPush.AppendText(Environment.NewLine);
                }
                else
                {
                    log.Error(ex.Message);
                    ConsolePanelPush.AppendText(DateTime.Now + " - " + ex.HResult + " --- " + ex.Message);
                    ConsolePanelPush.AppendText(Environment.NewLine);
                }
                WindowInteraction.FocusWindow(thisProc);
            }
        }


        /// <summary>
        /// PLAYBACK TEST STEPS
        /// </summary>
        public void PlaybackTestSteps()
        {
            ExceptionCode excode = new ExceptionCode();

            ResultPanelPush.Clear();
            ConsolePanelPush.AppendText(DateTime.Now + " - BEGIN PLAYBACK" + Environment.NewLine);

            Th_CLEARVALUE = new Thread(() => ClearTextBox.ClearValue(ProcessForm.targetproc));
            Th_CLEARVALUE.Start();
            Th_CLEARVALUE.Join();

            try
            {
                // Open ProgressBAR

                int pbindex = 0;

                //  PlaybackObjectList = new PlaybackObject[dataGridView2.Rows.Count];

                //ElementList = GrabAUT.SearchbyFramework("WPF");
                //foreach (UiElement a in ElementList)
                //{
                //    if (a.ClassName.Equals("ComboBox"))
                //    {
                //        a.AsComboBox().Expand();
                //    }
                //}
                ElementList = GrabAUT.SearchbyFramework("WPF");

                foreach (DataGridViewRow row in dataGridView2.Rows)
                {
                    PlaybackObjectList[pbindex].action = (string)row.Cells[5].Value;
                    if (PlaybackObjectList[pbindex].action == "Select" || PlaybackObjectList[pbindex].action == "Unselect")
                    {
                        PlaybackObjectList[pbindex].text = null;
                        if (PlaybackObjectList[pbindex].type != "CheckBox")
                        {
                            if (PlaybackObjectList[pbindex].type == "ComboBox" && PlaybackObjectList[pbindex].action.Equals("SetText"))
                            {
                                PlaybackObjectList[pbindex].text = row.Cells[6].Value.ToString();
                            }
                            else if (PlaybackObjectList[pbindex].type == "ComboBox" && PlaybackObjectList[pbindex].action.Equals("Select"))
                            {
                                if (row.Cells[6].Value == null)
                                {
                                    PlaybackObjectList[pbindex].itemIndex = 0;
                                }
                                else if (!Regex.IsMatch(row.Cells[6].Value.ToString(), @"^\d+$"))
                                {
                                    PlaybackObjectList[pbindex].itemIndex = 0;
                                }
                                else
                                {
                                    PlaybackObjectList[pbindex].itemIndex = int.Parse(row.Cells[6].Value.ToString());
                                }
                            }
                            else
                            {
                                PlaybackObjectList[pbindex].itemIndex = int.Parse(row.Cells[6].Value.ToString());
                            }

                        }
                    }
                    else if (PlaybackObjectList[pbindex].action == "SetText" ||
                        PlaybackObjectList[pbindex].action == "WaitEnable" ||
                        PlaybackObjectList[pbindex].action == "SendKey")
                    {
                        PlaybackObjectList[pbindex].text = (string)row.Cells[6].Value; ;
                        PlaybackObjectList[pbindex].itemIndex = -1;
                    }

                    pbindex++;
                }

                int percentage = 100 / PlaybackObjectList.Count();

                for (int i = 0; i < PlaybackObjectList.Count(); i++)
                {
                    // Call ElemenList again to spy another screen before playback
                    //ElementList = DoSpy.SearchbyFramework("WPF");

                    if (stop_playback.Equals(true))
                    {
                        pf.Hide();
                        WindowInteraction.FocusWindow(thisProc);

                        return;
                    }

                    int flag = 0;
                    if (GrabAUT.MainWindow.ModalWindows.Count > 0)
                    {
                        flag = 1;
                        ElementList = GrabAUT.SearchbyFramework("WPF");
                    }

                    // Add more playback actions here
                    switch (PlaybackObjectList[i].type)
                    {
                        case "Button":
                            AbsAction ButtonAction = new ButtonAction();
                            ButtonAction.PlaybackObject = PlaybackObjectList[i];
                            ButtonAction.UiElement = ElementList[PlaybackObjectList[i].index];
                            ButtonAction.DoExecute();
                            if (ButtonAction.Result == true)
                            {
                                PlaybackLogger(ButtonAction.PlaybackObject.index, ButtonAction.PlaybackObject.type, true);
                            }
                            else
                            {
                                PlaybackLogger(ButtonAction.PlaybackObject.index, ButtonAction.PlaybackObject.type, false);
                            }

                            break;

                        case "RadioButton":
                            AbsAction RadioButtonAction = new RadioButtonAction();
                            RadioButtonAction.PlaybackObject = PlaybackObjectList[i];
                            RadioButtonAction.UiElement = ElementList[PlaybackObjectList[i].index];
                            RadioButtonAction.DoExecute();
                            if (RadioButtonAction.Result == true)
                                PlaybackLogger(RadioButtonAction.PlaybackObject.index, RadioButtonAction.PlaybackObject.type, true);
                            else
                                PlaybackLogger(RadioButtonAction.PlaybackObject.index, RadioButtonAction.PlaybackObject.type, false);
                            break;

                        case "TextBox":
                            AbsAction TextBoxAction = new TextBoxAction();
                            TextBoxAction.PlaybackObject = PlaybackObjectList[i];
                            TextBoxAction.UiElement = ElementList[PlaybackObjectList[i].index];
                            TextBoxAction.DoExecute();
                            if (TextBoxAction.Result == true)
                                PlaybackLogger(TextBoxAction.PlaybackObject.index, TextBoxAction.PlaybackObject.type, true);
                            else
                                PlaybackLogger(TextBoxAction.PlaybackObject.index, TextBoxAction.PlaybackObject.type, false);
                            //Execute Checkpoint
                            PlaybackObjectList[i].CheckPoint.UiElement = TextBoxAction.UiElement;
                            (PlaybackObjectList[i].CheckPoint as TextBoxCheckPoint).DoExecuteCheckPoint();
                            if ((PlaybackObjectList[i].CheckPoint as TextBoxCheckPoint).cpIsEmpty == true)
                            {
                                log.Info("Result of CheckPoint IsEmpty: " + (PlaybackObjectList[i].CheckPoint as TextBoxCheckPoint).cpIsEmptyResult.ToString().Trim());
                                ResultPanelPush.AppendText(DateTime.Now + " - " + "Result of CheckPoint IsEmpty: "
                                    + (PlaybackObjectList[i].CheckPoint as TextBoxCheckPoint).cpIsEmptyResult.ToString().Trim() + Environment.NewLine);
                            }
                            if ((PlaybackObjectList[i].CheckPoint as TextBoxCheckPoint).cpIsEnabled == true)
                            {
                                log.Info("Result of CheckPoint IsEnabled: " + (PlaybackObjectList[i].CheckPoint as TextBoxCheckPoint).cpIsEnabledResult.ToString().Trim());
                                ResultPanelPush.AppendText(DateTime.Now + " - " + "Result of CheckPoint IsEnabled: "
                                    + (PlaybackObjectList[i].CheckPoint as TextBoxCheckPoint).cpIsEnabledResult.ToString().Trim() + Environment.NewLine);
                                //ResultPanelPush.AppendText((PlaybackObjectList[i].CheckPoint as TextBoxCheckPoint).cpIsEnabledResult.ToString());
                            }
                            if ((PlaybackObjectList[i].CheckPoint as TextBoxCheckPoint).cpIsEqual == true)
                            {
                                log.Info("Result of CheckPoint IsEqual: " + (PlaybackObjectList[i].CheckPoint as TextBoxCheckPoint).cpIsEqualResult.ToString().Trim());
                                ResultPanelPush.AppendText(DateTime.Now + " - " + "Result of CheckPoint IsEqual: "
                                    + (PlaybackObjectList[i].CheckPoint as TextBoxCheckPoint).cpIsEqualResult.ToString().Trim() + Environment.NewLine);
                            }
                            if ((PlaybackObjectList[i].CheckPoint as TextBoxCheckPoint).cpIsReadOnly == true)
                            {
                                log.Info("Result of CheckPoint IsReadOnly: " + (PlaybackObjectList[i].CheckPoint as TextBoxCheckPoint).cpIsReadOnlyResult.ToString().Trim());
                                ResultPanelPush.AppendText(DateTime.Now + " - " + "Result of CheckPoint IsReadOnly: "
                                    + (PlaybackObjectList[i].CheckPoint as TextBoxCheckPoint).cpIsReadOnlyResult.ToString().Trim() + Environment.NewLine);
                            }
                            //Testing Result: System.Windows.Forms.MessageBox.Show((PlaybackObjectList[i].CheckPoint as TextBoxCheckPoint).cpIsEqualResult.ToString());
                            break;
                        case "RichTextBox":
                            AbsAction RichTextBoxAction = new RichTextBoxAction();
                            RichTextBoxAction.PlaybackObject = PlaybackObjectList[i];
                            RichTextBoxAction.UiElement = ElementList[PlaybackObjectList[i].index];
                            RichTextBoxAction.DoExecute();
                            if (RichTextBoxAction.Result == true)
                                PlaybackLogger(RichTextBoxAction.PlaybackObject.index, RichTextBoxAction.PlaybackObject.type, true);
                            else
                                PlaybackLogger(RichTextBoxAction.PlaybackObject.index, RichTextBoxAction.PlaybackObject.type, false);
                            break;

                        case "PasswordBox":
                            AbsAction PasswordBoxAction = new PasswordBoxAction();
                            PasswordBoxAction.PlaybackObject = PlaybackObjectList[i];
                            PasswordBoxAction.UiElement = ElementList[PlaybackObjectList[i].index];
                            PasswordBoxAction.DoExecute();
                            if (PasswordBoxAction.Result == true)
                                PlaybackLogger(PasswordBoxAction.PlaybackObject.index, PasswordBoxAction.PlaybackObject.type, true);
                            else
                                PlaybackLogger(PasswordBoxAction.PlaybackObject.index, PasswordBoxAction.PlaybackObject.type, false);
                            break;

                        case "ComboBox":
                            AbsAction ComboBoxAction = new SpyandPlaybackTestTool.Actions.ComboBoxAction();
                            ComboBoxAction.PlaybackObject = PlaybackObjectList[i];
                            ComboBoxAction.UiElement = ElementList[PlaybackObjectList[i].index];
                            ComboBoxAction.DoExecute();
                            if (ComboBoxAction.Result == true)
                                PlaybackLogger(ComboBoxAction.PlaybackObject.index, ComboBoxAction.PlaybackObject.type, true);
                            else
                                PlaybackLogger(ComboBoxAction.PlaybackObject.index, ComboBoxAction.PlaybackObject.type, false);
                            break;

                        case "ComboBoxEdit":
                            AbsAction ComboBoxEditAction = new ComboBoxEditAction();
                            ComboBoxEditAction.PlaybackObject = PlaybackObjectList[i];
                            ComboBoxEditAction.UiElement = ElementList[PlaybackObjectList[i].index];
                            ComboBoxEditAction.DoExecute();
                            if (ComboBoxEditAction.Result == true)
                                PlaybackLogger(ComboBoxEditAction.PlaybackObject.index, ComboBoxEditAction.PlaybackObject.type, true);
                            else
                                PlaybackLogger(ComboBoxEditAction.PlaybackObject.index, ComboBoxEditAction.PlaybackObject.type, false);
                            break;

                        case "AutoCompleteCombobox":
                            AbsAction ATComboBoxEditAction = new ComboBoxEditAction();
                            ATComboBoxEditAction.PlaybackObject = PlaybackObjectList[i];
                            ATComboBoxEditAction.UiElement = ElementList[PlaybackObjectList[i].index];
                            ATComboBoxEditAction.DoExecute();
                            if (ATComboBoxEditAction.Result == true)
                                PlaybackLogger(ATComboBoxEditAction.PlaybackObject.index, ATComboBoxEditAction.PlaybackObject.type, true);
                            else
                                PlaybackLogger(ATComboBoxEditAction.PlaybackObject.index, ATComboBoxEditAction.PlaybackObject.type, false);
                            break;

                        case "DataGrid":
                            AbsAction DataGridAction = new DataGridAction();
                            DataGridAction.PlaybackObject = PlaybackObjectList[i];
                            DataGridAction.UiElement = ElementList[PlaybackObjectList[i].index];
                            DataGridAction.DoExecute();
                            if (DataGridAction.Result == true)
                                PlaybackLogger(DataGridAction.PlaybackObject.index, DataGridAction.PlaybackObject.type, true);
                            else
                                PlaybackLogger(DataGridAction.PlaybackObject.index, DataGridAction.PlaybackObject.type, false);
                            break;

                        case "SendKeyorWaitEnable":
                            if (PlaybackObjectList[i].action == "SendKey")
                            {
                                AbsAction SendKeyAction = new SpyandPlaybackTestTool.Actions.SendKey();
                                SendKeyAction.PlaybackObject = PlaybackObjectList[i];
                                SendKeyAction.DoExecute();

                                log.Info("Key " + PlaybackObjectList[i].text + " HAS BEEN SENT");
                                ResultPanelPush.AppendText(DateTime.Now + " - " + "Key " + PlaybackObjectList[i].text + " HAS BEEN SENT");
                                ResultPanelPush.AppendText(Environment.NewLine);
                            }
                            else if (PlaybackObjectList[i].action == "WaitEnable")
                            {
                                AbsAction WaitEnable = new WaitEnable();
                                WaitEnable.PlaybackObject = PlaybackObjectList[i];
                                WaitEnable.DoExecute();

                                log.Info("WATIED FOR " + int.Parse(WaitEnable.PlaybackObject.text) / 1000 + "s");
                                ResultPanelPush.AppendText(DateTime.Now + " - " + "WATIED FOR " + int.Parse(WaitEnable.PlaybackObject.text) / 1000 + "s");
                                ResultPanelPush.AppendText(Environment.NewLine);
                            }
                            break;

                        case "CheckBox":
                            AbsAction CheckBoxAction = new CheckBoxAction();
                            CheckBoxAction.PlaybackObject = PlaybackObjectList[i];

                            CheckBoxAction.UiElement = ElementList[CheckBoxAction.PlaybackObject.index];
                            CheckBoxAction.DoExecute();
                            if (CheckBoxAction.Result == true)
                                PlaybackLogger(CheckBoxAction.PlaybackObject.index, CheckBoxAction.PlaybackObject.type, true);
                            else
                                PlaybackLogger(CheckBoxAction.PlaybackObject.index, CheckBoxAction.PlaybackObject.type, false);
                            break;

                        case "TabItem":
                            AbsAction TabItemAction = new TabItemAction();
                            TabItemAction.PlaybackObject = PlaybackObjectList[i];
                            TabItemAction.UiElement = ElementList[TabItemAction.PlaybackObject.index];

                            TabItemAction.DoExecute();
                            if (TabItemAction.Result == true)
                                PlaybackLogger(TabItemAction.PlaybackObject.index, TabItemAction.PlaybackObject.type, true);
                            else
                                PlaybackLogger(TabItemAction.PlaybackObject.index, TabItemAction.PlaybackObject.type, false);
                            break;

                        default:
                            break;
                    }

                    if (flag == 1)
                    {
                        flag = 0;
                        ElementList = GrabAUT.SearchbyFramework("WPF");
                    }
                    playbackprogress += percentage;
                }

                playbackstatus = false;
                playbackprogress = 100;

                Th_SPY = new Thread(() => Spy("respy"));
                Th_SPY.Start();
                Th_SPY.IsBackground = true;
                Th_SPY.Join();

                ConsolePanelPush.AppendText(DateTime.Now + " - DONE PLAYBACK");
                ConsolePanelPush.AppendText(Environment.NewLine);

                WindowInteraction.FocusWindow(thisProc);

                this.pf.Dispose();

                //Th_PBTSTEP.Join();

            }
            catch (Exception ex)
            {
                if (ex.HResult == excode.AUT_NOT_FOUND)
                {
                    ConsolePanelPush.AppendText(DateTime.Now + " - " + "AUT NOT FOUND");
                    ConsolePanelPush.AppendText(Environment.NewLine);
                }
                else if (ex.HResult == excode.CANNOT_FOCUS_ON_AUT)
                {
                    ConsolePanelPush.AppendText(DateTime.Now + " - " + "CANNOT FOCUS ON AUT OR INPUT WAS NOT ENABLE");
                    ConsolePanelPush.AppendText(Environment.NewLine);
                }
                else if (ex.HResult == excode.INVALID_SCRIPT)
                {
                    ConsolePanelPush.AppendText(DateTime.Now + " - " + "CANNOT USE THE CURRENT SCRIPT ON THIS SCREEN");
                    ConsolePanelPush.AppendText(Environment.NewLine);
                }
                else if (ex.HResult == excode.SCRIPT_ERROR)
                {
                    ConsolePanelPush.AppendText(DateTime.Now + " - " + "SCRIPT FORMAT IS INVALID");
                    ConsolePanelPush.AppendText(Environment.NewLine);
                }
                else if (ex.HResult == excode.AUT_QUIT_DURING_OP)
                {
                    ConsolePanelPush.AppendText(DateTime.Now + " - " + "AUT QUIT DURING PLAYBACK OPERATION");
                    ConsolePanelPush.AppendText(Environment.NewLine);
                }
                else if (ex.HResult == excode.INVALID_INDEX)
                {
                    ConsolePanelPush.AppendText(DateTime.Now + " - " + "INPUT VALUE WAS OUT OF RANGE");
                    ConsolePanelPush.AppendText(Environment.NewLine);
                }
                else if (ex.HResult == excode.OBJECT_NULL)
                {
                    ConsolePanelPush.AppendText(DateTime.Now + " - " + "CANNOT FOUND PLAYBACK SCRIPT");
                    ConsolePanelPush.AppendText(Environment.NewLine);
                }
                else
                {
                    ConsolePanelPush.AppendText(DateTime.Now + " - " + ex.HResult + " --- " + ex.Message);
                    ConsolePanelPush.AppendText(Environment.NewLine);
                }
            }
            WindowInteraction.FocusWindow(thisProc);
        }



        /// <summary>
        /// Add test steps to the Playback table
        /// </summary>
        private void AddTestSteps()
        {
            if (dataGridView1.SelectedCells.Count <= 0)
            {
                System.Windows.Forms.MessageBox.Show("There is no selected cell!", "WARNING!!!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            dataGridView2.AllowUserToAddRows = true;
            //List<int> selectedCellList = new List<int>();
            List<int> indexList = new List<int>();
            foreach (DataGridViewCell cell in dataGridView1.SelectedCells)
            {
                //selectedCellList.Add(cell.RowIndex);
                indexList.Add(Convert.ToInt32(dataGridView1.Rows[cell.RowIndex].Cells[1].Value));
                //System.Windows.Forms.MessageBox.Show(cell.RowIndex.ToString());
            }

            var dict = new Dictionary<int, int>();
            foreach (var value in indexList)
            {
                if (dict.ContainsKey(value)) dict[value]++;
                else dict[value] = 1;
            }
            List<int> officialIndexList = new List<int>();
            foreach (var pair in dict)
            {
                officialIndexList.Add(pair.Key);
            }
            officialIndexList.Sort();

            //foreach (var item in rowIndexList)
            //{
            //    System.Windows.Forms.MessageBox.Show(item.ToString());
            //}
            int contIndex = dataGridView2.Rows.Count;
            for (int i = 0; i < officialIndexList.Count; i++)
            {
                DataGridViewRow row = (DataGridViewRow)dataGridView2.Rows[0].Clone();
                row.Cells[0].Value = contIndex;
                row.Cells[1].Value = SpyObjectList[officialIndexList[i]].index;
                row.Cells[2].Value = SpyObjectList[officialIndexList[i]].automationId;
                row.Cells[3].Value = SpyObjectList[officialIndexList[i]].name;
                row.Cells[4].Value = SpyObjectList[officialIndexList[i]].type;
                ((DataGridViewComboBoxCell)row.Cells[5]).Items.Clear();
                row.Cells[7].Value = "Set";
                PlaybackObject PlaybackObject = new PlaybackObject();
                PlaybackObject.index = SpyObjectList[officialIndexList[i]].index;
                PlaybackObject.automationId = SpyObjectList[officialIndexList[i]].automationId;
                PlaybackObject.name = SpyObjectList[officialIndexList[i]].name;
                PlaybackObject.type = SpyObjectList[officialIndexList[i]].type;
                // Add more actions here & initial checkpoint properties;
                switch ((string)row.Cells[4].Value)
                {
                    case "Button":
                        ((DataGridViewComboBoxCell)row.Cells[5]).Items.Add("Click");

                        break;

                    case "RadioButton":
                        ((DataGridViewComboBoxCell)row.Cells[5]).Items.Add("Click");

                        break;

                    case "TextBox":
                        ((DataGridViewComboBoxCell)row.Cells[5]).Items.Add("SetText");
                        //TextBox CheckPoint
                        TextBoxCheckPoint txtboxcp = new TextBoxCheckPoint();
                        txtboxcp.cpIsEmpty = false;
                        txtboxcp.cpIsEnabled = false;
                        txtboxcp.cpIsEqual = false;
                        txtboxcp.cpIsReadOnly = false;
                        txtboxcp.expectedVal = "";
                        PlaybackObject.CheckPoint = new TextBoxCheckPoint();
                        PlaybackObject.CheckPoint = txtboxcp;
                        break;

                    case "PasswordBox":
                        ((DataGridViewComboBoxCell)row.Cells[5]).Items.Add("SetText");
                        break;

                    case "ComboBox":
                        ((DataGridViewComboBoxCell)row.Cells[5]).Items.Add("SetText");
                        ((DataGridViewComboBoxCell)row.Cells[5]).Items.Add("Select");
                        break;

                    case "ComboBoxEdit":
                        ((DataGridViewComboBoxCell)row.Cells[5]).Items.Add("SetText");
                        ((DataGridViewComboBoxCell)row.Cells[5]).Items.Add("Select");
                        break;

                    case "AutoCompleteCombobox":
                        ((DataGridViewComboBoxCell)row.Cells[5]).Items.Add("SetText");
                        ((DataGridViewComboBoxCell)row.Cells[5]).Items.Add("Select");
                        break;

                    case "RichTextBox":
                        ((DataGridViewComboBoxCell)row.Cells[5]).Items.Add("SetText");
                        break;

                    case "CheckBox":
                        ((DataGridViewComboBoxCell)row.Cells[5]).Items.Add("Select");
                        ((DataGridViewComboBoxCell)row.Cells[5]).Items.Add("Unselect");

                        break;

                    case "DataGrid":
                        ((DataGridViewComboBoxCell)row.Cells[5]).Items.Add("Select");
                        ((DataGridViewComboBoxCell)row.Cells[5]).Items.Add("Unselect");
                        ((DataGridViewComboBoxCell)row.Cells[5]).Items.Add("DoubleClick");
                        break;

                    case "TabItem":
                        ((DataGridViewComboBoxCell)row.Cells[5]).Items.Add("Click");

                        break;

                    default:
                        break;
                }
                PlaybackObjectList.Add(PlaybackObject);
                dataGridView2.Rows.Add(row);
                contIndex++;
                row.Cells[0].ReadOnly = true;
                row.Cells[1].ReadOnly = true;
                row.Cells[2].ReadOnly = true;
                row.Cells[3].ReadOnly = true;
                row.Cells[4].ReadOnly = true;
            }
            dataGridView2.AllowUserToAddRows = false;
        }


        private void InspectorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ProcessForm.isAttached.Equals(false))
            {
                System.Windows.Forms.MessageBox.Show("NO AUT TO INSPECT", "Warning!!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                HighlightForm HLightForm = HighlightForm.GetInstance();
                HLightForm.ProcessName = ProcessForm.targetproc;
                HLightForm.Show();
            }
        }


        /// <summary>
        /// Create a Playback table from JSON test script.
        /// </summary>
        private void CreateTestSteps()
        {
            readJson();
            if (PlaybackObjectScript.Count > 0)
            {
                dataGridView2.AllowUserToAddRows = true;
                //PlaybackObjectList.Clear();//Clear the playback object list to get the new one from script
                PlaybackObjectList = PlaybackObjectScript;
                int count = PlaybackObjectList.Count();

                for (int i = 0; i < count; i++)
                {
                    DataGridViewRow row = (DataGridViewRow)dataGridView2.Rows[0].Clone();
                    if (i == 0)
                        dataGridView2.Rows.Clear();
                    row.Cells[0].Value = i + 1;
                    row.Cells[1].Value = PlaybackObjectList[i].index;
                    row.Cells[2].Value = PlaybackObjectList[i].automationId;
                    row.Cells[3].Value = PlaybackObjectList[i].name;
                    row.Cells[4].Value = PlaybackObjectList[i].type;
                    ((DataGridViewComboBoxCell)row.Cells[5]).Items.Clear();
                    switch (row.Cells[4].Value.ToString())
                    {
                        case "TabItem":
                            ((DataGridViewComboBoxCell)row.Cells[5]).Items.Add("Click");
                            if (PlaybackObjectList[i].action == "Click")
                                row.Cells[5].Value = PlaybackObjectList[i].action;
                            break;

                        case "Button":
                            ((DataGridViewComboBoxCell)row.Cells[5]).Items.Add("Click");
                            if (PlaybackObjectList[i].action == "Click")
                                row.Cells[5].Value = PlaybackObjectList[i].action;
                            break;

                        case "RadioButton":
                            ((DataGridViewComboBoxCell)row.Cells[5]).Items.Add("Click");
                            if (PlaybackObjectList[i].action == "Click")
                                row.Cells[5].Value = PlaybackObjectList[i].action;
                            break;

                        case "TextBox":
                            ((DataGridViewComboBoxCell)row.Cells[5]).Items.Add("SetText");
                            if (PlaybackObjectList[i].action == "SetText")
                                row.Cells[5].Value = PlaybackObjectList[i].action;

                            break;

                        case "RichTextBox":
                            ((DataGridViewComboBoxCell)row.Cells[5]).Items.Add("SetText");
                            if (PlaybackObjectList[i].action == "SetText")
                                row.Cells[5].Value = PlaybackObjectList[i].action;
                            break;

                        case "PasswordBox":
                            ((DataGridViewComboBoxCell)row.Cells[5]).Items.Add("SetText");
                            if (PlaybackObjectList[i].action == "SetText")
                                row.Cells[5].Value = PlaybackObjectList[i].action;
                            break;

                        case "ComboBox":
                            ((DataGridViewComboBoxCell)row.Cells[5]).Items.Add("SetText");
                            ((DataGridViewComboBoxCell)row.Cells[5]).Items.Add("Select");
                            switch (PlaybackObjectList[i].action)
                            {
                                case "SetText":
                                    row.Cells[5].Value = ((DataGridViewComboBoxCell)row.Cells[5]).Items[0];
                                    break;

                                case "Select":
                                    row.Cells[5].Value = ((DataGridViewComboBoxCell)row.Cells[5]).Items[1];
                                    break;

                                default:
                                    break;
                            }
                            break;

                        case "CheckBox":
                            ((DataGridViewComboBoxCell)row.Cells[5]).Items.Add("Select");
                            ((DataGridViewComboBoxCell)row.Cells[5]).Items.Add("Unselect");
                            switch (PlaybackObjectList[i].action)
                            {
                                case "Select":
                                    row.Cells[5].Value = ((DataGridViewComboBoxCell)row.Cells[5]).Items[0];
                                    break;

                                case "Unselect":
                                    row.Cells[5].Value = ((DataGridViewComboBoxCell)row.Cells[5]).Items[1];
                                    break;

                                default:
                                    break;
                            }
                            break;

                        case "ComboBoxEdit":
                            ((DataGridViewComboBoxCell)row.Cells[5]).Items.Add("SetText");
                            ((DataGridViewComboBoxCell)row.Cells[5]).Items.Add("Select");
                            switch (PlaybackObjectList[i].action)
                            {
                                case "SetText":
                                    row.Cells[5].Value = ((DataGridViewComboBoxCell)row.Cells[5]).Items[0];
                                    break;

                                case "Select":
                                    row.Cells[5].Value = ((DataGridViewComboBoxCell)row.Cells[5]).Items[1];
                                    break;

                                default:
                                    break;
                            }
                            break;

                        case "AutoCompleteCombobox":
                            ((DataGridViewComboBoxCell)row.Cells[5]).Items.Add("SetText");
                            ((DataGridViewComboBoxCell)row.Cells[5]).Items.Add("Select");
                            switch (PlaybackObjectList[i].action)
                            {
                                case "SetText":
                                    row.Cells[5].Value = ((DataGridViewComboBoxCell)row.Cells[5]).Items[0];
                                    break;

                                case "Select":
                                    row.Cells[5].Value = ((DataGridViewComboBoxCell)row.Cells[5]).Items[1];
                                    break;

                                default:
                                    break;
                            }
                            break;

                        case "SendKeyorWaitEnable":
                            ((DataGridViewComboBoxCell)row.Cells[5]).Items.Add("SendKey");
                            ((DataGridViewComboBoxCell)row.Cells[5]).Items.Add("WaitEnable");
                            switch (PlaybackObjectList[i].action)
                            {
                                case "SendKey":
                                    row.Cells[5].Value = ((DataGridViewComboBoxCell)row.Cells[5]).Items[0];
                                    break;

                                case "WaitEnable":
                                    row.Cells[5].Value = ((DataGridViewComboBoxCell)row.Cells[5]).Items[1];
                                    break;

                                default:
                                    break;
                            }
                            break;

                        case "DataGrid":
                            ((DataGridViewComboBoxCell)row.Cells[5]).Items.Add("Select");
                            ((DataGridViewComboBoxCell)row.Cells[5]).Items.Add("Unselect");
                            ((DataGridViewComboBoxCell)row.Cells[5]).Items.Add("DoubleClick");
                            switch (PlaybackObjectList[i].action)
                            {
                                case "Select":
                                    row.Cells[5].Value = ((DataGridViewComboBoxCell)row.Cells[5]).Items[0];
                                    break;

                                case "Unselect":
                                    row.Cells[5].Value = ((DataGridViewComboBoxCell)row.Cells[5]).Items[1];
                                    break;

                                case "DoubleClick":
                                    row.Cells[5].Value = ((DataGridViewComboBoxCell)row.Cells[5]).Items[2];
                                    break;

                                default:
                                    break;
                            }
                            break;

                        default:
                            break;
                    }
                    if (string.IsNullOrWhiteSpace(PlaybackObjectList[i].text) == false && PlaybackObjectList[i].itemIndex == -1)
                        row.Cells[6].Value = PlaybackObjectList[i].text;
                    else if (string.IsNullOrWhiteSpace(PlaybackObjectList[i].text) == true && PlaybackObjectList[i].itemIndex >= 0)
                        row.Cells[6].Value = Convert.ToInt32(PlaybackObjectList[i].itemIndex);
                    row.Cells[7].Value = "Set";
                    dataGridView2.Rows.Add(row);

                    row.Cells[0].ReadOnly = true;
                }
            }
            dataGridView2.AllowUserToAddRows = false;
        }


        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                var editingControl = this.dataGridView2.EditingControl as DataGridViewComboBoxEditingControl;
                if (editingControl != null)
                {
                    editingControl.DroppedDown = true;
                }

                if (dataGridView2.SelectedCells.Count > 0)
                {
                    int selectedrowindex = dataGridView2.SelectedCells[0].RowIndex;

                    DataGridViewRow selectedRow = dataGridView2.Rows[selectedrowindex];

                    dataGridView2.BeginEdit(true);
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
        }

        private void exportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.SaveFileDialog ExportDialog = new System.Windows.Forms.SaveFileDialog();
            ExportDialog.Title = "Save Test Script File";
            ExportDialog.Filter = "JSON files (*.json)|*.json";
            ExportDialog.RestoreDirectory = true;

            if (ExportDialog.ShowDialog() == DialogResult.OK)
            {
                StreamWriter writer = new StreamWriter(File.Create(ExportDialog.FileName));
                writer.WriteLine(rtxtScript.Text);
                writer.Dispose();
            }
        }

        private void importToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            ExceptionCode excode = new ExceptionCode();

            System.Windows.Forms.OpenFileDialog ImportDialog = new System.Windows.Forms.OpenFileDialog();
            ImportDialog.Title = "Open Test Script File";
            ImportDialog.Filter = "JSON files (*.json)|*.json";
            ImportDialog.Multiselect = true;
            ImportDialog.RestoreDirectory = true;

            //ImportDialog.InitialDirectory = @"C:\";
            try
            {
                if (ImportDialog.ShowDialog() == DialogResult.OK)
                {
                    //clbTestScriptList.Items.Clear();
                    //lbxScriptList.SelectionMode = SelectionMode.MultiExtended;
                    //scriptFiles = new ScriptFile[ImportDialog.FileNames.Length];
                    int length = ImportDialog.FileNames.Length;

                    //System.Windows.Forms.MessageBox.Show(ImportDialog.SafeFileNames[0].ToString());
                    string content;
                    int j = 0;
                    int k = scriptFiles.Count;
                    //System.Windows.Forms.MessageBox.Show(length.ToString());
                    //System.Windows.Forms.MessageBox.Show(k.ToString());
                    while (j < length)
                    {
                        scriptFiles.Add(new ScriptFile() { Name = ImportDialog.SafeFileNames[j], Path = ImportDialog.FileNames[j] });
                        content = File.ReadAllText(scriptFiles[k].Path);
                        if (ValidateJSON(content) == false)
                        {
                            log.Error("SCRIPT FORMAT IS INVALID");
                            System.Windows.Forms.MessageBox.Show(scriptFiles[k].Name + " : Test Script's format is invalid Json", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else
                        {
                            clbTestScriptList.Items.Add(scriptFiles[k].Name);
                            clbTestScriptList.SetItemChecked(j, true);
                        }
                        k++;
                        j++;
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
                System.Windows.Forms.MessageBox.Show(ex.Message, "Error Encountered", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public static bool ValidateJSON(string s)
        {
            try
            {
                JToken.Parse(s);
                return true;
            }
            catch (JsonReaderException ex)
            {
                Trace.WriteLine(ex);
                return false;
            }
        }

        /// <summary>
        /// The JSON test script validator
        /// </summary>
        public void readJson()
        {
            if (PlaybackObjectScript.Count > 0)
                PlaybackObjectScript.Clear();//Clear the old playback object script list to add the new one.
            string readText = rtxtScript.Text;
            dynamic controls = JsonConvert.DeserializeObject(readText);
            //try
            //{

            foreach (var control in controls)
            {
                PlaybackObject pbo = new PlaybackObject();
                pbo.index = control.Controller.index;
                pbo.automationId = control.Controller.automationId;
                pbo.name = control.Controller.name;
                pbo.type = control.Controller.type;
                pbo.action = control.Controller.action;
                pbo.text = control.Controller.text;
                pbo.itemIndex = control.Controller.itemIndex;
                switch (pbo.type)
                {
                    case "TextBox":
                        pbo.CheckPoint = new TextBoxCheckPoint();
                        if (control.Controller.cpIsEmpty == true)
                            (pbo.CheckPoint as TextBoxCheckPoint).cpIsEmpty = true;
                        else
                            (pbo.CheckPoint as TextBoxCheckPoint).cpIsEmpty = false;
                        if (control.Controller.cpIsReadOnly == true)
                            (pbo.CheckPoint as TextBoxCheckPoint).cpIsReadOnly = control.Controller.cpIsReadOnly;
                        else
                            (pbo.CheckPoint as TextBoxCheckPoint).cpIsReadOnly = false;
                        if (control.Controller.cpIsEnabled == true)
                            (pbo.CheckPoint as TextBoxCheckPoint).cpIsEnabled = control.Controller.cpIsEnabled;
                        else
                            (pbo.CheckPoint as TextBoxCheckPoint).cpIsEnabled = false;
                        if (control.Controller.cpIsEqual == true)
                        {
                            (pbo.CheckPoint as TextBoxCheckPoint).cpIsEqual = control.Controller.cpIsEqual;
                            if (control.Controller.expectedVal != null)
                                (pbo.CheckPoint as TextBoxCheckPoint).expectedVal = control.Controller.expectedVal;
                        }
                        else
                            (pbo.CheckPoint as TextBoxCheckPoint).cpIsEqual = false;
                        break;
                    default:
                        break;
                }
                PlaybackObjectScript.Add(pbo);
            }
            //}
            //catch (Exception ex)
            //{
            //    ConsolePanelPush.AppendText("ERROR CODE: " + ex.HResult + "  -----  " + "detail: " + ex.Message);
            //    ConsolePanelPush.AppendText(Environment.NewLine);
            //    WindowInteraction.FocusWindow(thisProc);
            //    System.Windows.Forms.MessageBox.Show(ex.Message, "Error Encountered", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
        }

        /// <summary>
        /// Check a specific keyword and change its color.
        /// </summary>
        /// <param name="word">String</param>
        /// <param name="color"></param>
        /// <param name="StartIndex"></param>
        public void CheckKeyWord(string word, Color color, int StartIndex)
        {
            if (this.ResultPanelPush.Text.Contains(word))
            {
                int index = -1;
                int selectStart = this.ResultPanelPush.SelectionStart;
                while ((index = this.ResultPanelPush.Text.IndexOf(word, (index + 1))) != -1)
                {
                    this.ResultPanelPush.Select((index + StartIndex), word.Length);
                    this.ResultPanelPush.SelectionColor = color;
                    this.ResultPanelPush.Select(selectStart, 0);
                    this.ResultPanelPush.SelectionColor = Color.Black;
                }
            }

            if (this.ConsolePanelPush.Text.Contains(word))
            {
                int index = -1;
                int selectStart = this.ConsolePanelPush.SelectionStart;
                while ((index = this.ConsolePanelPush.Text.IndexOf(word, (index + 1))) != -1)
                {
                    this.ConsolePanelPush.Select((index + StartIndex), word.Length);
                    this.ConsolePanelPush.SelectionColor = color;
                    this.ConsolePanelPush.Select(selectStart, 0);
                    this.ConsolePanelPush.SelectionColor = Color.Black;
                }
            }

            if (this.rtxtScript.Text.Contains(word))
            {
                int index = -1;
                int selectStart = this.rtxtScript.SelectionStart;
                while ((index = this.rtxtScript.Text.IndexOf(word, (index + 1))) != -1)
                {
                    this.rtxtScript.Select((index + StartIndex), word.Length);
                    this.rtxtScript.SelectionColor = color;
                    this.rtxtScript.Select(selectStart, 0);
                    this.rtxtScript.SelectionColor = Color.Black;
                }
            }
        }


        public void runScenario()
        {
            try
            {
                List<int> indexList = new List<int>();

                foreach (var item in clbTestScriptList.CheckedItems)
                {
                    indexList.Add(clbTestScriptList.Items.IndexOf(item));
                }

                ResultPanelPush.Clear();
                ConsolePanelPush.AppendText(DateTime.Now + " - " + "BEGIN SCENARIO PLAYBACK" + Environment.NewLine);
                log.Info("BEGIN SCENARIO PLAYBACK");

                for (int i = 0; i < indexList.Count; i++)
                {
                    clbTestScriptList.SelectedIndex = indexList[i];

                    SumAllSteps += PlaybackObjectList.Count();
                }

                //System.Windows.Forms.MessageBox.Show(indexList.Count.ToString());

                double div = 100 / (double)SumAllSteps;
                double temp = Math.Floor(div);
                double frac = div - temp;

                if (frac > 0.6)
                {
                    s_percentage = (int)Math.Ceiling(div);
                }
                else
                {
                    s_percentage = (int)Math.Floor(div);
                }

                for (int i = 0; i < indexList.Count; i++)
                {
                    clbTestScriptList.SelectedIndex = indexList[i];

                    readJson();

                    ElementList = GrabAUT.SearchbyFramework("WPF");
                    foreach (UiElement a in ElementList)
                    {
                        if (a.ClassName.Equals("ComboBox"))
                        {
                            a.AsComboBox().Expand();
                        }
                    }
                    ElementList = GrabAUT.SearchbyFramework("WPF");

                    Th_CLEARVALUE = new Thread(() => ClearTextBox.ClearValue(ProcessForm.targetproc));
                    Th_CLEARVALUE.Start();
                    Th_CLEARVALUE.Join();

                    if (stop_playback.Equals(false))
                    {
                        ResultPanelPush.AppendText(DateTime.Now + " - " + "PLAYBACK ON: " + clbTestScriptList.SelectedItem + Environment.NewLine);

                        PlaybackTestScript();

                        ResultPanelPush.AppendText(Environment.NewLine);
                    }
                    else
                    {
                        WindowInteraction.FocusWindow(thisProc);
                        ConsolePanelPush.AppendText(DateTime.Now + " - " + "CANCEL SCENARIO PLAYBACK" + Environment.NewLine);
                        return;
                    }
                }

                ConsolePanelPush.AppendText(DateTime.Now + " - " + "DONE SCENARIO PLAYBACK" + Environment.NewLine);
                log.Info("DONE SCENARIO PLAYBACK");

                if (playbackprogress > 100)
                {
                    playbackprogress = 100;
                }

                ScenarioStatus = false;

                WindowInteraction.FocusWindow(thisProc);

                pf.Hide();
            }
            catch
            {
                var a = new Exception("NO AUT TO PLAYBACK");
                ConsolePanelPush.AppendText(DateTime.Now + " - " + a + Environment.NewLine);
            }
        }

        private void ResultPanelPush_TextChanged_1(object sender, EventArgs e)
        {
            this.CheckKeyWord("PASSED", Color.Green, 0);
            this.CheckKeyWord("FAILED", Color.Red, 0);
            this.CheckKeyWord("item id", Color.OrangeRed, 0);
            this.CheckKeyWord("ClassType", Color.Blue, 0);
            this.CheckKeyWord("PLAYBACK ON", Color.Purple, 0);
        }

        private void ConsolePanelPush_TextChanged(object sender, EventArgs e)
        {
            this.CheckKeyWord("SCENARIO", Color.Green, 0);
            this.CheckKeyWord("DONE PLAYBACK", Color.Green, 0);
            this.CheckKeyWord("AUT NOT FOUND", Color.Red, 0);
            this.CheckKeyWord("INPUT VALUE WAS OUT OF RANGE", Color.Red, 0);
            this.CheckKeyWord("AUT QUIT DURING PLAYBACK OPERATION", Color.Red, 0);
            this.CheckKeyWord("CANNOT SPY THIS PROGRAM", Color.Red, 0);
        }

        private void clbTestScriptList_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            
            if (clbTestScriptList.SelectedItem == null)
            {
                return;
            }
            clbTestScriptList.CheckOnClick = false;

            int i = 0;
            //clbTestScriptList.Refresh();
            foreach (var scriptFile in scriptFiles)
            {
                if (scriptFile.Name == clbTestScriptList.SelectedItem.ToString())
                {
                    rtxtScript.Text = File.ReadAllText(scriptFiles[i].Path);
                    CreateTestSteps();
                    //readJson();
                    break;
                }
                i++;
            }
            CpFormList = new CheckPointForm[dataGridView2.Rows.Count];
            checkCheckPoint = new int[dataGridView2.Rows.Count];
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutWindow AboutW = new AboutWindow();
            AboutW.Show();
        }

        private void dataGridView2_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dataGridView2.SelectedCells.Count > 0)
                {
                    //int selectedrowindex = dataGridView2.SelectedCells[0].RowIndex;

                    //DataGridViewRow selectedRow = dataGridView2.Rows[selectedrowindex];

                    var row = dataGridView2.Rows[e.RowIndex];
                    if (null != row)
                    {
                        var cell = row.Cells[e.ColumnIndex];
                        if (null != cell)
                        {
                            object value = cell.Value;

                            if (null != value)
                            {
                                dataGridView2.BeginEdit(true);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
        }

        private void PlaybackLogger(int index, string classname, bool result)
        {
            if (result == true)
            {
                log.Info("PASSED at item id: " + index + " --- " + "ClassType: " + classname);
                ResultPanelPush.AppendText(DateTime.Now + " - " + "PASSED at item id: " + index + " --- " + "ClassType: " + classname + Environment.NewLine);
            }
            else
            {
                log.Info("FAILED at item id: " + index + " --- " + "ClassType: " + classname);
                ResultPanelPush.AppendText(DateTime.Now + " - " + "FAILED at item id: " + index + " --- " + "ClassType: " + classname + Environment.NewLine);
            }
        }

        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                AddTestSteps();
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyData)
            {
                case Keys.F1:
                    btnAttachProcess.PerformClick();
                    break;

                case Keys.F2:
                    btnSpy.PerformClick();
                    break;

                case Keys.F3:
                    btnPlayTestStep.PerformClick();
                    break;

                case Keys.F4:
                    btnPlayTestScript.PerformClick();
                    break;

                case Keys.F5:
                    btnPlayScenario.PerformClick();
                    break;

                case Keys.Escape:
                    DialogResult dialogResult = System.Windows.Forms.MessageBox.Show("Do you really want to exit this program?", "WARNING!!!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);

                    if (dialogResult == DialogResult.Yes)
                    {
                        // The user wants to exit the application. Close everything down.
                        System.Windows.Forms.Application.Exit();
                    }
                    else if (dialogResult == DialogResult.No)
                    {
                        return;
                    }
                    break;

                case Keys.Control | Keys.Alt | Keys.S:
                    btnSave.PerformClick();
                    break;

                case Keys.Alt | Keys.F4:
                    e.SuppressKeyPress = true;
                    DialogResult dialogResults = System.Windows.Forms.MessageBox.Show("Do you really want to exit this program?", "WARNING!!!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);

                    if (dialogResults == DialogResult.Yes)
                    {
                        // The user wants to exit the application. Close everything down.
                        System.Windows.Forms.Application.Exit();
                    }
                    else if (dialogResults == DialogResult.No)
                    {
                        return;
                    }
                    break;

                case Keys.Control | Keys.O:
                    importToolStripMenuItem.PerformClick();
                    break;

                case Keys.Control | Keys.S:
                    exportToolStripMenuItem.PerformClick();
                    break;

                default:
                    break;
            }
        }

        private void dataGridView2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                if (dataGridView2.Rows.Count <= 0)
                {
                    return;
                }
                DialogResult dialogResult = System.Windows.Forms.MessageBox.Show("Do you really want to delete this row?", "WARNING", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                if (dialogResult == DialogResult.Yes)
                {
                    e.SuppressKeyPress = false;
                }
                else if (dialogResult == DialogResult.No)
                {
                    e.SuppressKeyPress = true;
                }
            }
        }

        private void clbTestScriptList_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                if (clbTestScriptList.SelectedItems.Count <= 0)
                {
                    return;
                }
                DialogResult dialogResult = System.Windows.Forms.MessageBox.Show("Do you really want to delete this row?", "WARNING", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                if (dialogResult == DialogResult.Yes)
                {
                    //foreach (var item in clbTestScriptList.SelectedItems)
                    //    clbTestScriptList.Items.Remove(item);
                    //scriptFiles.Clear();
                }
            }
        }







        #region DragandDrop
        private Rectangle dragBoxFromMouseDown;
        private int rowIndexFromMouseDown;
        private int rowIndexOfItemUnderMouseToDrop;

        private void dataGridView2_MouseMove(object sender, MouseEventArgs e)
        {
            if ((e.Button & MouseButtons.Left) == MouseButtons.Left)
            {
                // If the mouse moves outside the rectangle, start the drag.
                if (dragBoxFromMouseDown != Rectangle.Empty &&
                !dragBoxFromMouseDown.Contains(e.X, e.Y))
                {
                    // Proceed with the drag and drop, passing in the list item.
                    DragDropEffects dropEffect = dataGridView2.DoDragDrop(
                          dataGridView2.Rows[rowIndexFromMouseDown],
                          DragDropEffects.Move);
                }
            }
        }

        private void dataGridView2_MouseDown(object sender, MouseEventArgs e)
        {
            // Get the index of the item the mouse is below.
            rowIndexFromMouseDown = dataGridView2.HitTest(e.X, e.Y).RowIndex;
            if (rowIndexFromMouseDown != -1)
            {
                // Remember the point where the mouse down occurred.
                // The DragSize indicates the size that the mouse can move
                // before a drag event should be started.
                Size dragSize = SystemInformation.DragSize;
                // Create a rectangle using the DragSize, with the mouse position being
                // at the center of the rectangle.
                dragBoxFromMouseDown = new Rectangle(
                          new Point(
                            e.X - (dragSize.Width / 2),
                            e.Y - (dragSize.Height / 2)),
                      dragSize);
            }
            else
                // Reset the rectangle if the mouse is not over an item in the ListBox.
                dragBoxFromMouseDown = Rectangle.Empty;
        }

        private void dataGridView2_DragOver(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;
        }

        private void dataGridView2_DragDrop(object sender, DragEventArgs e)
        {
            // The mouse locations are relative to the screen, so they must be
            // converted to client coordinates.
            Point clientPoint = dataGridView2.PointToClient(new Point(e.X, e.Y));

            // Get the row index of the item the mouse is below.
            rowIndexOfItemUnderMouseToDrop = dataGridView2.HitTest(clientPoint.X, clientPoint.Y).RowIndex;
            if (rowIndexOfItemUnderMouseToDrop == -1)
            {
                return;
            }
            // If the drag operation was a move then remove and insert the row.
            if (e.Effect == DragDropEffects.Move)
            {
                DataGridViewRow rowToMove = e.Data.GetData(typeof(DataGridViewRow)) as DataGridViewRow;
                dataGridView2.Rows.RemoveAt(rowIndexFromMouseDown);
                dataGridView2.Rows.Insert(rowIndexOfItemUnderMouseToDrop, rowToMove);
                dataGridView2.ClearSelection();
                dataGridView2.Rows[rowIndexOfItemUnderMouseToDrop].Selected = true;
            }
        }
        #endregion

        private void dataGridView2_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (e.ColumnIndex == 6)
            {
                int rowIndex = e.RowIndex;
                int colIndex = e.ColumnIndex;
                //System.Windows.Forms.MessageBox.Show(dataGridView2.Rows[rowIndex].Cells[5].FormattedValue.ToString());

                //System.Windows.Forms.MessageBox.Show("abc");
                if (e.FormattedValue.ToString().Equals(String.Empty))
                {
                    //return;
                }
                //System.Windows.Forms.MessageBox.Show(dataGridView2.Rows[rowIndex].Cells[6].Value.ToString());
                else
                {
                    if (dataGridView2.Rows[rowIndex].Cells[5].FormattedValue.ToString().Trim() == "")
                    {
                        //return;
                    }
                    else if (dataGridView2.Rows[rowIndex].Cells[5].FormattedValue.ToString().Trim() == "Select" || dataGridView2.Rows[rowIndex].Cells[5].Value.ToString().Trim() == "Unselect")
                    {
                        int checkNum = 0;
                        try
                        {
                            checkNum = Convert.ToInt32(e.FormattedValue.ToString());
                            if (checkNum >= 0)
                            {
                            }
                            else
                            {
                                System.Windows.Forms.MessageBox.Show("Value must be a number greater than or equal to 0 for Select action!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                e.Cancel = true;
                                return;
                            }
                        }
                        catch
                        {
                            System.Windows.Forms.MessageBox.Show("Value must be a number greater than or equal to 0 for Select action!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            e.Cancel = true;
                            return;
                        }
                    }
                    else if (dataGridView2.Rows[rowIndex].Cells[5].FormattedValue.ToString().Trim() == "Click")
                    {
                        if (e.FormattedValue.ToString().Equals(String.Empty))
                        {
                        }
                        else
                        {
                            System.Windows.Forms.MessageBox.Show("Value must be empty for Click action!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            e.Cancel = true;
                            return;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// View log menu strip
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void viewLogsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            var log = path + @"\Botsina\Logs\Botsina.log";

            Process.Start(log);
        }

        /// <summary>
        /// Close all thread before terminate the application
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Environment.Exit(Environment.ExitCode);
        }

        int[] checkCheckPoint;
        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 7)
            {
                    //CpFormList = new CheckPointForm[dataGridView2.Rows.Count];
                    int rowIndex = dataGridView2.SelectedRows[0].Index;
                //System.Windows.Forms.MessageBox.Show(rowIndex.ToString());
                checkCheckPoint[rowIndex] = new int();
                checkCheckPoint[rowIndex] = 1;
                    string type = dataGridView2.Rows[rowIndex].Cells[4].Value.ToString();
                    if (type == "TextBox")
                    {
                        CpForm.ControlType = type;
                        switch (type)
                        {
                            case "TextBox":
                                CpForm.textBoxCP = (PlaybackObjectList[rowIndex].CheckPoint as TextBoxCheckPoint);
                                break;
                            default:
                                break;

                        }
                        CpForm.ShowDialog();
                        //tempCpForm.textBoxCP = CpForm.textBoxCP;
                        CpFormList[rowIndex] = new CheckPointForm();
                        CpFormList[rowIndex].textBoxCP = CpForm.textBoxCP;
                        PlaybackObjectList[rowIndex].CheckPoint = CpForm.textBoxCP;
                    }
                    else
                    {
                        System.Windows.Forms.MessageBox.Show("Type of control must be TextBox!");
                    }
            }
        }
      
        private void dataGridView2_SelectionChanged(object sender, EventArgs e)
        {

            
        }


        private void dataGridView2_UserDeletedRow(object sender, DataGridViewRowEventArgs e)
        {
            //dataGridView2.UserDeletingRow += (sa, ea) =>
            //{
            //    System.Windows.Forms.MessageBox.Show(ea.Row.Index.ToString());
            // };

        }
        private void dataGridView2_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
           if(dataGridView2.Rows.Count==dataGridView2.SelectedRows.Count)
            {
                PlaybackObjectList.Clear();
            }
           else
           PlaybackObjectList.RemoveAt(e.Row.Index);
            for (int i = 0; i < dataGridView2.RowCount; i++)
                dataGridView2.Rows[i].Cells[0].Value = i + 1;
        }


        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            AddTestSteps();
        }

        private void rtxtScript_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 9)
            {
                e.Handled = false;
            }
        }

        private void toolStripTextBox1_TextChanged(object sender, EventArgs e)
        {
            string text = toolStripTextBox1.Text.ToLower();
            dataGridView1.Rows.Clear();
            dataGridView1.AllowUserToAddRows = true;
            for (int i = 0; i < SpyObjectList.Count(); i++)
            {
                string search = SpyObjectList[i].automationId.ToLower();
                string search1 = SpyObjectList[i].name.ToLower();
                if ((search.Contains(text) == true || search1.Contains(text) == true) && (toolStripComboBox1.SelectedItem.ToString() == "All" || (toolStripComboBox1.SelectedItem.ToString() == "Interactive Controls" && (SpyObjectList[i].type == "ComboBox" || SpyObjectList[i].type == "ComboBoxEdit" || SpyObjectList[i].type == "DataGrid" || SpyObjectList[i].type == "TextBox" || SpyObjectList[i].type == "Button" || SpyObjectList[i].type == "RadioButton"))) && SpyObjectList[i].automationId.Contains("PART") != true)
                {
                    int rowId = dataGridView1.Rows.Add();
                    DataGridViewRow row = dataGridView1.Rows[rowId];
                    row.Cells[1].Value = SpyObjectList[i].index;
                    row.Cells[2].Value = SpyObjectList[i].automationId;
                    row.Cells[3].Value = SpyObjectList[i].name;
                    row.Cells[4].Value = SpyObjectList[i].type;
                }
                else if ((search.Contains(text) == true || search1.Contains(text) == true) && (SpyObjectList[i].type == toolStripComboBox1.SelectedItem.ToString()) && SpyObjectList[i].automationId.Contains("PART") != true)
                {
                    int rowId = dataGridView1.Rows.Add();
                    DataGridViewRow row = dataGridView1.Rows[rowId];
                    row.Cells[1].Value = SpyObjectList[i].index;
                    row.Cells[2].Value = SpyObjectList[i].automationId;
                    row.Cells[3].Value = SpyObjectList[i].name;
                    row.Cells[4].Value = SpyObjectList[i].type;
                }
            }
            dataGridView1.AllowUserToAddRows = false;
        }

        private void toolStripComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            toolStripTextBox1.Text = "";
            dataGridView1.Rows.Clear();
            dataGridView1.AllowUserToAddRows = true;
            switch (toolStripComboBox1.SelectedItem.ToString())
            {
                case "Button":

                    for (int i = 0; i < SpyObjectList.Count(); i++)
                    {
                        if (SpyObjectList[i].type == "Button" && SpyObjectList[i].automationId.Contains("PART") != true)
                        {
                            int rowId = dataGridView1.Rows.Add();
                            DataGridViewRow row = dataGridView1.Rows[rowId];
                            row.Cells[1].Value = SpyObjectList[i].index;
                            row.Cells[2].Value = SpyObjectList[i].automationId;
                            row.Cells[3].Value = SpyObjectList[i].name;
                            row.Cells[4].Value = SpyObjectList[i].type;
                            row.Cells[5].Value = "No Item";
                        }
                    }
                    break;

                case "RadioButton":
                    for (int i = 0; i < SpyObjectList.Count(); i++)
                    {
                        if (SpyObjectList[i].type == "RadioButton")
                        {
                            int rowId = dataGridView1.Rows.Add();
                            DataGridViewRow row = dataGridView1.Rows[rowId];
                            row.Cells[1].Value = SpyObjectList[i].index;
                            row.Cells[2].Value = SpyObjectList[i].automationId;
                            row.Cells[3].Value = SpyObjectList[i].name;
                            row.Cells[4].Value = SpyObjectList[i].type;
                            row.Cells[5].Value = "No Item";
                        }
                    }
                    break;

                case "TextBox":
                    for (int i = 0; i < SpyObjectList.Count(); i++)
                    {
                        if (SpyObjectList[i].type == "TextBox" && SpyObjectList[i].automationId.Contains("PART") != true)
                        {
                            int rowId = dataGridView1.Rows.Add();
                            DataGridViewRow row = dataGridView1.Rows[rowId];
                            row.Cells[1].Value = SpyObjectList[i].index;
                            row.Cells[2].Value = SpyObjectList[i].automationId;
                            row.Cells[3].Value = SpyObjectList[i].name;
                            row.Cells[4].Value = SpyObjectList[i].type;
                            row.Cells[5].Value = "No Item";
                        }
                    }
                    break;

                case "ComboBox":
                    for (int i = 0; i < SpyObjectList.Count(); i++)
                    {
                        if (SpyObjectList[i].type == "ComboBox")
                        {
                            int rowId = dataGridView1.Rows.Add();
                            DataGridViewRow row = dataGridView1.Rows[rowId];
                            row.Cells[1].Value = SpyObjectList[i].index;
                            row.Cells[2].Value = SpyObjectList[i].automationId;
                            row.Cells[3].Value = SpyObjectList[i].name;
                            row.Cells[4].Value = SpyObjectList[i].type;
                            row.Cells[5].Value = "View Items";
                        }
                    }
                    break;

                case "ComboBoxEdit":
                    for (int i = 0; i < SpyObjectList.Count(); i++)
                    {
                        if (SpyObjectList[i].type == "ComboBoxEdit" || SpyObjectList[i].type == "AutoCompleteCombobox")
                        {
                            int rowId = dataGridView1.Rows.Add();
                            DataGridViewRow row = dataGridView1.Rows[rowId];
                            row.Cells[1].Value = SpyObjectList[i].index;
                            row.Cells[2].Value = SpyObjectList[i].automationId;
                            row.Cells[3].Value = SpyObjectList[i].name;
                            row.Cells[4].Value = SpyObjectList[i].type;
                            row.Cells[5].Value = "View Items";
                        }
                    }
                    break;

                case "DataGrid":
                    for (int i = 0; i < SpyObjectList.Count(); i++)
                    {
                        if (SpyObjectList[i].type == "DataGrid")
                        {
                            int rowId = dataGridView1.Rows.Add();
                            DataGridViewRow row = dataGridView1.Rows[rowId];
                            row.Cells[1].Value = SpyObjectList[i].index;
                            row.Cells[2].Value = SpyObjectList[i].automationId;
                            row.Cells[3].Value = SpyObjectList[i].name;
                            row.Cells[4].Value = SpyObjectList[i].type;
                            row.Cells[5].Value = "View Items";
                        }
                    }
                    break;

                case "RichTextBox":
                    for (int i = 0; i < SpyObjectList.Count(); i++)
                    {
                        if (SpyObjectList[i].type == "RichTextBox")
                        {
                            int rowId = dataGridView1.Rows.Add();
                            DataGridViewRow row = dataGridView1.Rows[rowId];
                            row.Cells[1].Value = SpyObjectList[i].index;
                            row.Cells[2].Value = SpyObjectList[i].automationId;
                            row.Cells[3].Value = SpyObjectList[i].name;
                            row.Cells[4].Value = SpyObjectList[i].type;
                            row.Cells[5].Value = "No Item";
                        }
                    }
                    break;

                case "CheckBox":
                    for (int i = 0; i < SpyObjectList.Count(); i++)
                    {
                        if (SpyObjectList[i].type == "CheckBox")
                        {
                            int rowId = dataGridView1.Rows.Add();
                            DataGridViewRow row = dataGridView1.Rows[rowId];
                            row.Cells[1].Value = SpyObjectList[i].index;
                            row.Cells[2].Value = SpyObjectList[i].automationId;
                            row.Cells[3].Value = SpyObjectList[i].name;
                            row.Cells[4].Value = SpyObjectList[i].type;
                            row.Cells[5].Value = "No Item";
                        }
                    }
                    break;

                case "TabItem":
                    for (int i = 0; i < SpyObjectList.Count(); i++)
                    {
                        if (SpyObjectList[i].type == "TabItem")
                        {
                            int rowId = dataGridView1.Rows.Add();
                            DataGridViewRow row = dataGridView1.Rows[rowId];
                            row.Cells[1].Value = SpyObjectList[i].index;
                            row.Cells[2].Value = SpyObjectList[i].automationId;
                            row.Cells[3].Value = SpyObjectList[i].name;
                            row.Cells[4].Value = SpyObjectList[i].type;
                            row.Cells[5].Value = "No Item";
                        }
                    }
                    break;

                // Add more types here
                case "Interactive Controls":

                    for (int i = 0; i < SpyObjectList.Count(); i++)
                    {
                        if ((SpyObjectList[i].type == "CheckBox" || SpyObjectList[i].type == "RichTextBox" ||
                            SpyObjectList[i].type == "ComboBox" || SpyObjectList[i].type == "ComboBoxEdit" ||
                            SpyObjectList[i].type == "DataGrid" || SpyObjectList[i].type == "TextBox" ||
                            SpyObjectList[i].type == "Button" || SpyObjectList[i].type == "RadioButton" ||
                            SpyObjectList[i].type == "AutoCompleteCombobox" || SpyObjectList[i].type == "TabItem" ||
                            SpyObjectList[i].type == "PasswordBox") && SpyObjectList[i].automationId.Contains("PART") != true)
                        {
                            int rowId = dataGridView1.Rows.Add();
                            DataGridViewRow row = dataGridView1.Rows[rowId];
                            row.Cells[1].Value = SpyObjectList[i].index;
                            row.Cells[2].Value = SpyObjectList[i].automationId;
                            row.Cells[3].Value = SpyObjectList[i].name;
                            row.Cells[4].Value = SpyObjectList[i].type;
                            if (SpyObjectList[i].type == "ComboBox" || SpyObjectList[i].type == "ComboBoxEdit" || SpyObjectList[i].type == "DataGrid")
                                row.Cells[5].Value = "View Items";
                            else
                                row.Cells[5].Value = "No Item";
                        }
                    }
                    break;

                case "All":
                    for (int i = 0; i < SpyObjectList.Count(); i++)
                    {
                        int rowId = dataGridView1.Rows.Add();
                        DataGridViewRow row = dataGridView1.Rows[rowId];
                        row.Cells[1].Value = SpyObjectList[i].index;
                        row.Cells[2].Value = SpyObjectList[i].automationId;
                        row.Cells[3].Value = SpyObjectList[i].name;
                        row.Cells[4].Value = SpyObjectList[i].type;
                        if (SpyObjectList[i].type == "ComboBox" || SpyObjectList[i].type == "ComboBoxEdit" || SpyObjectList[i].type == "DataGrid")
                            row.Cells[5].Value = "View Items";
                        else
                            row.Cells[5].Value = "No Item";
                    }
                    break;

                default:
                    break;
            }
            dataGridView1.AllowUserToAddRows = false;
        }

        private void btnAttachProcess_Click(object sender, EventArgs e)
        {
            ProcessForm PFM = ProcessForm.GetInstance();
            PFM.Show();
        }

        private void btnSpy_Click(object sender, EventArgs e)
        {
            Spy("normal");
        }

        private void btnAdder_Click(object sender, EventArgs e)
        {
            AddTestSteps();
        }


        private void btnPlayBackTestStep_Click(object sender, EventArgs e)
        {
            dataGridView2.ClearSelection();
            dataGridView2.EndEdit();
            if (ProcessForm.targetproc == null)
            {
                System.Windows.Forms.MessageBox.Show("Please attach AUT process to execute Playback Test Steps function!", "WARNING!!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            if (dataGridView2.Rows.Count <= 0)
            {
                System.Windows.Forms.MessageBox.Show("There is no data!", "WARNING!!!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }


            if (pf.Equals(null) || pf.IsDisposed)
            {
                pf = new ProgressForm();
                pf.Show();
            }

            pf.Show();

            playbackprogress = 0;
            playbackstatus = true;
            stop_playback = false;

      

            Th_PBTSTEP = new Thread(() => PlaybackTestSteps());
            Th_PBTSTEP.IsBackground = true;
            Th_PBTSTEP.Start();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            AddTestSteps();
        }

        private void btnCreateScript_Click(object sender, EventArgs e)
        {
            dataGridView2.ClearSelection();
            dataGridView2.EndEdit();
            if (dataGridView2.Rows.Count == 0)
            {
                System.Windows.Forms.MessageBox.Show("There is no data!", "WARNING!!!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            int pbindex = 0;
            foreach (DataGridViewRow row in dataGridView2.Rows)
            {
                PlaybackObjectList[pbindex].action = (string)row.Cells[5].Value;

                if (PlaybackObjectList[pbindex].action == "Select" || PlaybackObjectList[pbindex].action == "Unselect")
                {
                    PlaybackObjectList[pbindex].text = null;
                    if (PlaybackObjectList[pbindex].type != "CheckBox")
                    {
                        if (PlaybackObjectList[pbindex].type == "ComboBox" && PlaybackObjectList[pbindex].action.Equals("SetText"))
                        {
                            PlaybackObjectList[pbindex].text = row.Cells[6].Value.ToString();
                        }
                        else if (PlaybackObjectList[pbindex].type == "ComboBox" && PlaybackObjectList[pbindex].action.Equals("Select"))
                        {
                            if (row.Cells[6].Value == null)
                            {
                                PlaybackObjectList[pbindex].itemIndex = 0;
                            }
                            else if (!Regex.IsMatch(row.Cells[6].Value.ToString(), @"^\d+$"))
                            {
                                PlaybackObjectList[pbindex].itemIndex = 0;
                            }
                            else
                            {
                                PlaybackObjectList[pbindex].itemIndex = int.Parse(row.Cells[6].Value.ToString());
                            }
                        }
                        else
                        {
                            PlaybackObjectList[pbindex].itemIndex = int.Parse(row.Cells[6].Value.ToString());
                        }

                    }
                }
                else if (PlaybackObjectList[pbindex].action == "SetText" ||
                    PlaybackObjectList[pbindex].action == "WaitEnable" ||
                    PlaybackObjectList[pbindex].action == "SendKey")
                {
                    PlaybackObjectList[pbindex].text = (string)row.Cells[6].Value; ;
                    PlaybackObjectList[pbindex].itemIndex = -1;
                }

                pbindex++;
            }
            for (int i = 0; i < PlaybackObjectList.Count; i++)
            {
                dynamic TestScriptObject = new ExpandoObject();
                TestScriptObject.index = PlaybackObjectList[i].index;
                TestScriptObject.automationId = PlaybackObjectList[i].automationId;
                TestScriptObject.name = PlaybackObjectList[i].name;
                TestScriptObject.type = PlaybackObjectList[i].type;
                TestScriptObject.action = PlaybackObjectList[i].action;
                TestScriptObject.text = PlaybackObjectList[i].text;
                TestScriptObject.itemIndex = PlaybackObjectList[i].itemIndex;
                switch (PlaybackObjectList[i].type)
                {
                    case "TextBox":
                        if ((PlaybackObjectList[i].CheckPoint as TextBoxCheckPoint).cpIsEmpty == true)
                            TestScriptObject.cpIsEmpty = (PlaybackObjectList[i].CheckPoint as TextBoxCheckPoint).cpIsEmpty;
                        if ((PlaybackObjectList[i].CheckPoint as TextBoxCheckPoint).cpIsReadOnly == true)
                            TestScriptObject.cpIsReadOnly = (PlaybackObjectList[i].CheckPoint as TextBoxCheckPoint).cpIsReadOnly;
                        if ((PlaybackObjectList[i].CheckPoint as TextBoxCheckPoint).cpIsEnabled == true)
                            TestScriptObject.cpIsEnabled = (PlaybackObjectList[i].CheckPoint as TextBoxCheckPoint).cpIsEnabled;
                        if ((PlaybackObjectList[i].CheckPoint as TextBoxCheckPoint).cpIsEqual == true)
                        {
                            TestScriptObject.cpIsEqual = (PlaybackObjectList[i].CheckPoint as TextBoxCheckPoint).cpIsEqual;
                            TestScriptObject.expectedVal = (PlaybackObjectList[i].CheckPoint as TextBoxCheckPoint).expectedVal;
                        }
                        break;
                    default:
                        break;
                }
                string json = JsonConvert.SerializeObject(TestScriptObject, Formatting.Indented);
                if (PlaybackObjectList.Count == 1)
                {
                    //System.IO.File.WriteAllText(JsonPath, "[{\"Controller\":" + json + "}]");
                    rtxtScript.Text = "[{\"Controller\":" + json + "}]";
                    break;
                }
                if (i == 0)
                    //System.IO.File.WriteAllText(JsonPath, "[{\"Controller\":" + json);
                    rtxtScript.Text = "[{\"Controller\":" + json;
                else if (i == PlaybackObjectList.Count - 1)
                    //System.IO.File.AppendAllText(JsonPath, "},{\"Controller\":" + json + "}]");
                    rtxtScript.Text += "},{\"Controller\":" + json + "}]";
                else
                    //System.IO.File.AppendAllText(JsonPath, "},{\"Controller\":" + json);
                    rtxtScript.Text += "},{\"Controller\":" + json;

            }
        }

        private void btnSendKeyWait_Click(object sender, EventArgs e)
        {
            if (dataGridView2.Rows.Count <= 0)
            {
                return;
            }
            PlaybackObject pbo = new PlaybackObject();
            pbo.type = "SendKeyorWaitEnable";
            PlaybackObjectList.Add(pbo);
            DataGridViewRow row = (DataGridViewRow)dataGridView2.Rows[0].Clone();

            row.Cells[0].Value = dataGridView2.Rows.Count + 1;
            row.Cells[1].Value = -1;
            row.Cells[2].Value = "";
            row.Cells[3].Value = "";
            row.Cells[4].Value = "SendKeyorWaitEnable";
            ((DataGridViewComboBoxCell)row.Cells[5]).Items.Clear();
            ((DataGridViewComboBoxCell)row.Cells[5]).Items.Add("SendKey");
            ((DataGridViewComboBoxCell)row.Cells[5]).Items.Add("WaitEnable");
            row.Cells[6].Value = "";
            row.Cells[7].Value = "Set";
            row.Cells[0].ReadOnly = true;
            row.Cells[1].ReadOnly = true;
            row.Cells[2].ReadOnly = true;
            row.Cells[3].ReadOnly = true;
            row.Cells[4].ReadOnly = true;

            dataGridView2.Rows.Add(row);
        }

        private void btnRemoveRows_Click(object sender, EventArgs e)
        {
            if (dataGridView2.Rows.Count == 0)
                System.Windows.Forms.MessageBox.Show("There is no data!", "WARNING!!!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
            {
                DialogResult dialogResult = System.Windows.Forms.MessageBox.Show("Do you really want to remove this row?", "WARNING", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                if (dialogResult == DialogResult.Yes)
                {
                    int i = 1;
                    dataGridView2.MultiSelect = true;
                    foreach (DataGridViewCell cell in dataGridView2.SelectedCells)
                    {
                        if (cell.Selected == true)
                        {
                            //dataGridView2.Rows.Remove(row);
                            PlaybackObjectList.RemoveAt(cell.RowIndex);
                            dataGridView2.Rows.RemoveAt(cell.RowIndex);

                        }
                    }
                    dataGridView2.ClearSelection();
                    foreach (DataGridViewRow row in dataGridView2.Rows)
                    {
                        row.Cells[0].Value = i;
                        i++;
                    }
                }
                else
                    return;
            }

        }

        private void btnMoveRowUp_Click(object sender, EventArgs e)
        {
            if (dataGridView2.Rows.Count <= 0)
            {
                System.Windows.Forms.MessageBox.Show("Table doesn't have any data!");
            }
            else
            {
                DataGridView grid = dataGridView2;
                try
                {

                    int totalRows = grid.Rows.Count;
                    if (totalRows > 1)
                    {
                        PlaybackObject temp = new PlaybackObject();
                        if (grid.SelectedRows[0].Index > 0)
                        {
                            temp = PlaybackObjectList[grid.SelectedRows[0].Index];
                            PlaybackObjectList[grid.SelectedRows[0].Index] = PlaybackObjectList[grid.SelectedRows[0].Index - 1];
                            PlaybackObjectList[grid.SelectedRows[0].Index - 1] = temp;
                        }
                    }
                    int idx = grid.SelectedCells[0].OwningRow.Index;
                    if (idx == 0)
                        return;
                    int col = grid.SelectedCells[0].OwningColumn.Index;
                    DataGridViewRowCollection rows = grid.Rows;
                    DataGridViewRow row = rows[idx];
                    rows.Remove(row);
                    rows.Insert(idx - 1, row);
                    grid.ClearSelection();
                    grid.Rows[idx - 1].Cells[col].Selected = true;
                    for (int i = 0; i < dataGridView2.Rows.Count; i++)
                    {
                        dataGridView2.Rows[i].Cells[0].Value = i + 1;
                    }
                }
                catch
                {
                    throw;
                }
            }

        }

        private void btnMoveRowDown_Click(object sender, EventArgs e)
        {
            if (dataGridView2.Rows.Count <= 0)
            {
                System.Windows.Forms.MessageBox.Show("Table doesn't have any data!");
            }
            else
            {
                DataGridView grid = dataGridView2;
                try
                {
                    int totalRows = grid.Rows.Count;
                    if (totalRows > 1)
                    {
                        PlaybackObject temp = new PlaybackObject();
                        if (grid.SelectedRows[0].Index < totalRows - 1)
                        {
                            temp = PlaybackObjectList[grid.SelectedRows[0].Index];
                            PlaybackObjectList[grid.SelectedRows[0].Index] = PlaybackObjectList[grid.SelectedRows[0].Index + 1];
                            PlaybackObjectList[grid.SelectedRows[0].Index + 1] = temp;
                        }
                    }
                    int idx = grid.SelectedCells[0].OwningRow.Index;
                    if (idx == totalRows - 1)
                        return;
                    int col = grid.SelectedCells[0].OwningColumn.Index;
                    DataGridViewRowCollection rows = grid.Rows;
                    DataGridViewRow row = rows[idx];
                    rows.Remove(row);
                    rows.Insert(idx + 1, row);
                    grid.ClearSelection();
                    grid.Rows[idx + 1].Cells[col].Selected = true;
                    for (int i = 0; i < dataGridView2.Rows.Count; i++)
                    {
                        dataGridView2.Rows[i].Cells[0].Value = i + 1;
                    }
                }
                catch
                {
                    throw;
                }
            }

        }

        private void btnPlayTestScript_Click(object sender, EventArgs e)
        {
            if (ProcessForm.targetproc == null)
            {
                System.Windows.Forms.MessageBox.Show("Please attach AUT process to execute Playback Test Script function!", "WARNING!!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            if (ValidateJSON(rtxtScript.Text) == false)
            {
                System.Windows.Forms.MessageBox.Show("Test Script format is invalid Json!", "WARNING!!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            if (pf.Equals(null) || pf.IsDisposed)
            {
                pf = new ProgressForm();
                pf.Show();
            }

            pf.Show();

            playbackprogress = 0;
            playbackstatus = true;
            stop_playback = false;



            Th_CLEARVALUE = new Thread(() => ClearTextBox.ClearValue(ProcessForm.targetproc));
            Th_CLEARVALUE.Start();
            Th_CLEARVALUE.Join();

            readJson();

            //ElementList = GrabAUT.SearchbyFramework("WPF");
            //foreach (UiElement a in ElementList)
            //{
            //    if (a.ClassName.Equals("ComboBox"))
            //    {
            //        a.AsComboBox().Expand();
            //    }
            //}
            ElementList = GrabAUT.SearchbyFramework("WPF");

            //WindowInteraction.FocusWindow(targetProc);
            ResultPanelPush.Clear();
            ConsolePanelPush.AppendText(DateTime.Now + " - " + "BEGIN PLAYBACK");
            log.Info("BEGIN PLAYBACK");
            ConsolePanelPush.AppendText(Environment.NewLine);

            Th_PBTSCRIPT = new Thread(() => PlaybackTestScript());
            Th_PBTSCRIPT.Start();

            ConsolePanelPush.AppendText(DateTime.Now + " - " + "DONE PLAYBACK");
            log.Info("DONE PLAYBACK");
            ConsolePanelPush.AppendText(Environment.NewLine);
            //WindowInteraction.FocusWindow(thisProc);

        }

        private void btnCreateSteps_Click(object sender, EventArgs e)
        {
            if (ValidateJSON(rtxtScript.Text) == false)
            {
                System.Windows.Forms.MessageBox.Show("Test Script format is invalid Json!", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            CreateTestSteps();
        }

        private void btnPlayScenario_Click(object sender, EventArgs e)
        {
            if (ProcessForm.isAttached == false)
            {
                System.Windows.Forms.MessageBox.Show("Please attach AUT process to execute Scenario Playback function!", "WARNING!!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            if (clbTestScriptList.CheckedItems.Count <= 0)
            {
                System.Windows.Forms.MessageBox.Show("There is no checked Test Script file!", "WARNING!!!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (pf.Equals(null) || pf.IsDisposed)
            {
                pf = new ProgressForm();
                pf.Show();
            }

            pf.Show();

            ScenarioStatus = true;

            playbackprogress = 0;
            SumAllSteps = 0;

            stop_playback = false;

            Th_PBScenario = new Thread(() => runScenario());
            Th_PBScenario.IsBackground = true;
            Th_PBScenario.Start();

        }

        public void btnSave_Click(object sender, EventArgs e)
        {
            if (clbTestScriptList.Items.Count == 0)
            {
                System.Windows.Forms.MessageBox.Show("There is no Test Script file!", "WARNING!!!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (clbTestScriptList.SelectedItems.Count <= 0)
            {
                System.Windows.Forms.MessageBox.Show("There is no selected Test Script file!", "WARNING!!!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (tctrlPlayback.SelectedTab == tpgPlaybackTable)
            {
                if (dataGridView2.Rows.Count == 0)
                {
                    System.Windows.Forms.MessageBox.Show("There is no data!", "Lack Of Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                int i = 0;
                foreach (var scriptFile in scriptFiles)
                {
                    if (scriptFile.Name == clbTestScriptList.SelectedItem.ToString())
                    {
                        //if (PlaybackObjectList.Count > 0)
                        //{
                        //    PlaybackObjectList.Clear();
                        //}
                        //System.Windows.Forms.MessageBox.Show(PlaybackObjectList.Count.ToString());
                        //PlaybackObjectList = new PlaybackObject[dataGridView2.Rows.Count];
                        int pbindex = 0;
                        foreach (DataGridViewRow row in dataGridView2.Rows)
                        {

                            dynamic tempPlaybackObject = new ExpandoObject();
                            tempPlaybackObject.index = Convert.ToInt32(row.Cells[1].Value);
                            tempPlaybackObject.automationId = Convert.ToString(row.Cells[2].Value);
                            tempPlaybackObject.name = Convert.ToString(row.Cells[3].Value);
                            tempPlaybackObject.type = Convert.ToString(row.Cells[4].Value);
                            tempPlaybackObject.action = Convert.ToString(row.Cells[5].Value);
                            var check = dataGridView2.Rows[row.Index].Cells[7].Value as bool?;

                            if (checkCheckPoint[row.Index] == 1)
                            {
                                switch (tempPlaybackObject.type)
                                {
                                    case "TextBox":
                                        try
                                        {
                                            if (CpFormList[row.Index].textBoxCP.cpIsEmpty == true)
                                                tempPlaybackObject.cpIsEmpty = true;
                                            if (CpFormList[row.Index].textBoxCP.cpIsReadOnly == true)
                                                tempPlaybackObject.cpIsReadOnly = true;
                                            if (CpFormList[row.Index].textBoxCP.cpIsEnabled == true)
                                                tempPlaybackObject.cpIsEnabled = true;
                                            if (CpFormList[row.Index].textBoxCP.cpIsEqual == true)
                                            {
                                                tempPlaybackObject.cpIsEqual = true;
                                                if (CpFormList[row.Index].textBoxCP.expectedVal != null)
                                                    tempPlaybackObject.expectedVal = CpFormList[row.Index].textBoxCP.expectedVal;
                                            }
                                        }
                                        catch (Exception)
                                        {
                                            break;
                                            //throw;
                                        }
                                        break;
                                    default:
                                        break;
                                }
                            }
                            else
                            {
                                switch (tempPlaybackObject.type)
                                {
                                    case "TextBox":
                                        try
                                        {
                                            if ((PlaybackObjectList[row.Index].CheckPoint as TextBoxCheckPoint).cpIsEmpty == true)
                                                tempPlaybackObject.cpIsEmpty = true;
                                            if ((PlaybackObjectList[row.Index].CheckPoint as TextBoxCheckPoint).cpIsReadOnly == true)
                                                tempPlaybackObject.cpIsReadOnly = true;
                                            if ((PlaybackObjectList[row.Index].CheckPoint as TextBoxCheckPoint).cpIsEnabled == true)
                                                tempPlaybackObject.cpIsEnabled = true;
                                            if ((PlaybackObjectList[row.Index].CheckPoint as TextBoxCheckPoint).cpIsEqual == true)
                                            {
                                                tempPlaybackObject.cpIsEqual = true;
                                                if ((PlaybackObjectList[row.Index].CheckPoint as TextBoxCheckPoint).expectedVal != null)
                                                    tempPlaybackObject.expectedVal = (PlaybackObjectList[row.Index].CheckPoint as TextBoxCheckPoint).expectedVal;
                                            }
                                        }
                                        catch (Exception)
                                        {
                                            break;
                                            //throw;
                                        }
                                        break;
                                    default:
                                        break;
                                }
                            }
                            

                            if (tempPlaybackObject.type.Trim() == "DataGrid"
                                || tempPlaybackObject.type.Trim() == "ComboBox"
                                || tempPlaybackObject.type.Trim() == "ComboBoxEdit"
                                || tempPlaybackObject.type.Trim() == "AutoCompleteCombobox")
                            {
                                int n;
                                bool isNumeric = int.TryParse(Convert.ToString(row.Cells[6].Value), out n);
                                if (isNumeric)
                                {
                                    tempPlaybackObject.itemIndex = Convert.ToInt32(row.Cells[6].Value);
                                    tempPlaybackObject.text = "";
                                }
                                else
                                {
                                    tempPlaybackObject.itemIndex = -1;
                                    tempPlaybackObject.text = Convert.ToString(row.Cells[6].Value.ToString().Trim());
                                }
                            }
                            else
                            {
                                tempPlaybackObject.text = Convert.ToString(row.Cells[6].Value.ToString().Trim());
                                tempPlaybackObject.itemIndex = -1;
                            }

                            if (tempPlaybackObject.type == "SendKeyorWaitEnable" && PlaybackObjectList[pbindex].action == "WaitEnable")
                            {
                                if (string.IsNullOrEmpty(tempPlaybackObject.text))
                                {
                                    System.Windows.Forms.MessageBox.Show("Dont Leave any blank");
                                }
                                else
                                    tempPlaybackObject.text = Convert.ToString(row.Cells[6].Value);
                                tempPlaybackObject.itemIndex = -1;
                            }

                            string json = JsonConvert.SerializeObject(tempPlaybackObject, Formatting.Indented);
                            //System.Windows.Forms.MessageBox.Show(json);

                            if (dataGridView2.Rows.Count == 1)
                            {
                                System.IO.File.WriteAllText(scriptFiles[i].Path, "[{\"Controller\":" + json + "}]");
                                break;
                            }
                            if (pbindex == 0)
                                System.IO.File.WriteAllText(scriptFiles[i].Path, "[{\"Controller\":" + json);
                            else if (pbindex == dataGridView2.Rows.Count - 1)
                                System.IO.File.AppendAllText(scriptFiles[i].Path, "},{\"Controller\":" + json + "}]");
                            else
                                System.IO.File.AppendAllText(scriptFiles[i].Path, "},{\"Controller\":" + json);
                            pbindex++;
                        }
                        System.Windows.Forms.MessageBox.Show(scriptFile.Name + " is saved!", "NOTICE", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    i++;
                }
            }
            else if (tctrlPlayback.SelectedTab == tpgPlaybackScript)
            {
                int i = 0;
                foreach (var scriptFile in scriptFiles)
                {
                    if (scriptFile.Name == clbTestScriptList.SelectedItem.ToString())
                    {
                        if (ValidateJSON(rtxtScript.Text))
                        {
                            File.WriteAllText(scriptFiles[i].Path, rtxtScript.Text);
                            System.Windows.Forms.MessageBox.Show(scriptFile.Name + " is saved!", "NOTICE", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                        else
                        {
                            System.Windows.Forms.MessageBox.Show("Test Script's format is invalid Json", "WARNING!!!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }
                    i++;
                }
            }

        }

        private void btnRemoveScript_Click(object sender, EventArgs e)
        {
            if (clbTestScriptList.SelectedItems.Count <= 0)
            {
                return;
            }
            DialogResult dialogResult = System.Windows.Forms.MessageBox.Show("Do you really want to delete this Test Script?", "WARNING", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
            if (dialogResult == DialogResult.Yes)
            {
                scriptFiles.RemoveAt(clbTestScriptList.SelectedIndex);
                clbTestScriptList.Items.RemoveAt(clbTestScriptList.SelectedIndex);
            }
        }

        private void btnMoveUpScript_Click(object sender, EventArgs e)
        {
            // Checking selected item
            if (clbTestScriptList.SelectedItem == null || clbTestScriptList.SelectedIndex <= 0)
                return; // No selected item - nothing to do
            // add a duplicate item up in the listbox
            int oldIndex = clbTestScriptList.SelectedIndex + 1;
            clbTestScriptList.Items.Insert(clbTestScriptList.SelectedIndex - 1, clbTestScriptList.SelectedItem);
            // make it the current item
            clbTestScriptList.SelectedIndex = (clbTestScriptList.SelectedIndex - 2);
            if (clbTestScriptList.GetItemCheckState(oldIndex) == CheckState.Checked)
                clbTestScriptList.SetItemChecked(clbTestScriptList.SelectedIndex, true);
            // delete the old occurrence of this item
            clbTestScriptList.Items.RemoveAt(clbTestScriptList.SelectedIndex + 2);
        }

        private void btnMoveDownScript_Click(object sender, EventArgs e)
        {
            // Checking selected item
            if (clbTestScriptList.SelectedItem == null || clbTestScriptList.SelectedIndex >= clbTestScriptList.Items.Count - 1)
                return; // No selected item - nothing to do
            int IndexToRemove = clbTestScriptList.SelectedIndex;
            // add a duplicate item down in the listbox
            clbTestScriptList.Items.Insert(clbTestScriptList.SelectedIndex + 2, clbTestScriptList.SelectedItem);
            // make it the current item
            clbTestScriptList.SelectedIndex = (clbTestScriptList.SelectedIndex + 2);
            if (clbTestScriptList.GetItemCheckState(IndexToRemove) == CheckState.Checked)
                clbTestScriptList.SetItemChecked(clbTestScriptList.SelectedIndex, true);
            // delete the old occurrence of this item
            clbTestScriptList.Items.RemoveAt(IndexToRemove);
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
         
                int index = (int)dataGridView1.Rows[dataGridView1.SelectedCells[0].RowIndex].Cells[1].Value;
                if (dataGridView1.Rows[dataGridView1.SelectedCells[0].RowIndex].Cells[5].Value == "No Item")
                {
                    System.Windows.Forms.MessageBox.Show("This control contains 0 item");
                }
                else
                {
                    ItemForm itemForm = new ItemForm();
                    ItemForm.information = "AutomationID: " + SpyObjectList[index].automationId + "\nName: " + SpyObjectList[index].name + "\nType: " + SpyObjectList[index].type;
                    ItemForm.totalitem = SpyObjectList[index].itemList.Count().ToString();
                    itemForm.itemList = SpyObjectList[index].itemList;
                    itemForm.ShowDialog();
                }

        }


    }
}