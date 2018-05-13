namespace SpyandPlaybackTestTool
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.importToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.InspectorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewLogsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.guideToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.ConsolePanelPush = new System.Windows.Forms.RichTextBox();
            this.ResultPanelPush = new System.Windows.Forms.RichTextBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Select = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Index = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AutomationId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Names = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Type = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clbTestScriptList = new System.Windows.Forms.CheckedListBox();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.TestSteps = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Index1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AutomationId1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Name1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Type1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Actions = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.InputValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CheckPoint = new System.Windows.Forms.DataGridViewButtonColumn();
            this.rtxtScript = new System.Windows.Forms.RichTextBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.redcircleTip = new System.Windows.Forms.ToolStripStatusLabel();
            this.greencircleTip = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnAttachProcess = new System.Windows.Forms.ToolStripButton();
            this.btnSpy = new System.Windows.Forms.ToolStripButton();
            this.btnAdd = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripTextBox1 = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripComboBox1 = new System.Windows.Forms.ToolStripComboBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.splitContainer4 = new System.Windows.Forms.SplitContainer();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.btnPlayScenario = new System.Windows.Forms.ToolStripButton();
            this.btnSave = new System.Windows.Forms.ToolStripButton();
            this.btnRemoveScript = new System.Windows.Forms.ToolStripButton();
            this.btnMoveUpScript = new System.Windows.Forms.ToolStripButton();
            this.btnMoveDownScript = new System.Windows.Forms.ToolStripButton();
            this.tctrlPlayback = new System.Windows.Forms.TabControl();
            this.tpgPlaybackTable = new System.Windows.Forms.TabPage();
            this.toolStrip3 = new System.Windows.Forms.ToolStrip();
            this.btnPlayTestStep = new System.Windows.Forms.ToolStripButton();
            this.btnCreateScript = new System.Windows.Forms.ToolStripButton();
            this.btnSendKeyWait = new System.Windows.Forms.ToolStripButton();
            this.btnRemoveRows = new System.Windows.Forms.ToolStripButton();
            this.btnMoveRowUp = new System.Windows.Forms.ToolStripButton();
            this.btnMoveRowDown = new System.Windows.Forms.ToolStripButton();
            this.tpgPlaybackScript = new System.Windows.Forms.TabPage();
            this.toolStrip4 = new System.Windows.Forms.ToolStrip();
            this.btnPlayTestScript = new System.Windows.Forms.ToolStripButton();
            this.btnCreateSteps = new System.Windows.Forms.ToolStripButton();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).BeginInit();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.Panel2.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer4)).BeginInit();
            this.splitContainer4.Panel1.SuspendLayout();
            this.splitContainer4.Panel2.SuspendLayout();
            this.splitContainer4.SuspendLayout();
            this.toolStrip2.SuspendLayout();
            this.tctrlPlayback.SuspendLayout();
            this.tpgPlaybackTable.SuspendLayout();
            this.toolStrip3.SuspendLayout();
            this.tpgPlaybackScript.SuspendLayout();
            this.toolStrip4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.toolsToolStripMenuItem,
            this.helpsToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.menuStrip1.Size = new System.Drawing.Size(1341, 24);
            this.menuStrip1.TabIndex = 12;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.importToolStripMenuItem,
            this.exportToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // importToolStripMenuItem
            // 
            this.importToolStripMenuItem.Name = "importToolStripMenuItem";
            this.importToolStripMenuItem.Size = new System.Drawing.Size(110, 22);
            this.importToolStripMenuItem.Text = "Import";
            this.importToolStripMenuItem.Click += new System.EventHandler(this.importToolStripMenuItem_Click_1);
            // 
            // exportToolStripMenuItem
            // 
            this.exportToolStripMenuItem.Name = "exportToolStripMenuItem";
            this.exportToolStripMenuItem.Size = new System.Drawing.Size(110, 22);
            this.exportToolStripMenuItem.Text = "Export";
            this.exportToolStripMenuItem.Click += new System.EventHandler(this.exportToolStripMenuItem_Click);
            // 
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.InspectorToolStripMenuItem,
            this.viewLogsToolStripMenuItem});
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            this.toolsToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.toolsToolStripMenuItem.Text = "Tools";
            // 
            // InspectorToolStripMenuItem
            // 
            this.InspectorToolStripMenuItem.Name = "InspectorToolStripMenuItem";
            this.InspectorToolStripMenuItem.Size = new System.Drawing.Size(127, 22);
            this.InspectorToolStripMenuItem.Text = "Inspector";
            this.InspectorToolStripMenuItem.Click += new System.EventHandler(this.InspectorToolStripMenuItem_Click);
            // 
            // viewLogsToolStripMenuItem
            // 
            this.viewLogsToolStripMenuItem.Name = "viewLogsToolStripMenuItem";
            this.viewLogsToolStripMenuItem.Size = new System.Drawing.Size(127, 22);
            this.viewLogsToolStripMenuItem.Text = "View Logs";
            this.viewLogsToolStripMenuItem.Click += new System.EventHandler(this.viewLogsToolStripMenuItem_Click);
            // 
            // helpsToolStripMenuItem
            // 
            this.helpsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem,
            this.guideToolStripMenuItem});
            this.helpsToolStripMenuItem.Name = "helpsToolStripMenuItem";
            this.helpsToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpsToolStripMenuItem.Text = "Help";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(131, 22);
            this.aboutToolStripMenuItem.Text = "About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // guideToolStripMenuItem
            // 
            this.guideToolStripMenuItem.Name = "guideToolStripMenuItem";
            this.guideToolStripMenuItem.Size = new System.Drawing.Size(131, 22);
            this.guideToolStripMenuItem.Text = "User Guide";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // ConsolePanelPush
            // 
            this.ConsolePanelPush.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ConsolePanelPush.Font = new System.Drawing.Font("Lucida Console", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ConsolePanelPush.Location = new System.Drawing.Point(3, 18);
            this.ConsolePanelPush.Name = "ConsolePanelPush";
            this.ConsolePanelPush.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedBoth;
            this.ConsolePanelPush.Size = new System.Drawing.Size(413, 172);
            this.ConsolePanelPush.TabIndex = 0;
            this.ConsolePanelPush.Text = "";
            this.ConsolePanelPush.TextChanged += new System.EventHandler(this.ConsolePanelPush_TextChanged);
            // 
            // ResultPanelPush
            // 
            this.ResultPanelPush.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ResultPanelPush.Font = new System.Drawing.Font("Lucida Console", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ResultPanelPush.Location = new System.Drawing.Point(3, 18);
            this.ResultPanelPush.Name = "ResultPanelPush";
            this.ResultPanelPush.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedBoth;
            this.ResultPanelPush.Size = new System.Drawing.Size(912, 172);
            this.ResultPanelPush.TabIndex = 0;
            this.ResultPanelPush.Text = "";
            this.ResultPanelPush.TextChanged += new System.EventHandler(this.ResultPanelPush_TextChanged_1);
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Select,
            this.Index,
            this.AutomationId,
            this.Names,
            this.Type});
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.GridColor = System.Drawing.SystemColors.AppWorkspace;
            this.dataGridView1.Location = new System.Drawing.Point(0, 33);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(419, 526);
            this.dataGridView1.TabIndex = 1;
            this.dataGridView1.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellDoubleClick);
            this.dataGridView1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dataGridView1_KeyDown);
            // 
            // Select
            // 
            this.Select.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.Select.HeaderText = "Select";
            this.Select.Name = "Select";
            this.Select.ReadOnly = true;
            this.Select.Width = 43;
            // 
            // Index
            // 
            this.Index.FillWeight = 59.71831F;
            this.Index.HeaderText = "Index";
            this.Index.Name = "Index";
            this.Index.ReadOnly = true;
            this.Index.Width = 53;
            // 
            // AutomationId
            // 
            this.AutomationId.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.AutomationId.FillWeight = 110.0698F;
            this.AutomationId.HeaderText = "AutomationId";
            this.AutomationId.Name = "AutomationId";
            this.AutomationId.ReadOnly = true;
            // 
            // Names
            // 
            this.Names.FillWeight = 138.0625F;
            this.Names.HeaderText = "Name";
            this.Names.Name = "Names";
            this.Names.ReadOnly = true;
            this.Names.Width = 122;
            // 
            // Type
            // 
            this.Type.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Type.FillWeight = 92.14939F;
            this.Type.HeaderText = "Type";
            this.Type.Name = "Type";
            this.Type.ReadOnly = true;
            // 
            // clbTestScriptList
            // 
            this.clbTestScriptList.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.clbTestScriptList.CheckOnClick = true;
            this.clbTestScriptList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.clbTestScriptList.FormattingEnabled = true;
            this.clbTestScriptList.HorizontalScrollbar = true;
            this.clbTestScriptList.Location = new System.Drawing.Point(0, 33);
            this.clbTestScriptList.Margin = new System.Windows.Forms.Padding(0);
            this.clbTestScriptList.Name = "clbTestScriptList";
            this.clbTestScriptList.Size = new System.Drawing.Size(154, 494);
            this.clbTestScriptList.TabIndex = 27;
            this.clbTestScriptList.ThreeDCheckBoxes = true;
            this.clbTestScriptList.SelectedIndexChanged += new System.EventHandler(this.clbTestScriptList_SelectedIndexChanged_1);
            this.clbTestScriptList.KeyDown += new System.Windows.Forms.KeyEventHandler(this.clbTestScriptList_KeyDown);
            // 
            // dataGridView2
            // 
            this.dataGridView2.AllowDrop = true;
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.TestSteps,
            this.Index1,
            this.AutomationId1,
            this.Name1,
            this.Type1,
            this.Actions,
            this.InputValue,
            this.CheckPoint});
            this.dataGridView2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView2.Location = new System.Drawing.Point(3, 36);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView2.Size = new System.Drawing.Size(732, 462);
            this.dataGridView2.TabIndex = 1;
            this.dataGridView2.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView2_CellClick);
            this.dataGridView2.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView2_CellContentClick);
            this.dataGridView2.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.dataGridView2_CellValidating);
            this.dataGridView2.SelectionChanged += new System.EventHandler(this.dataGridView2_SelectionChanged);
            this.dataGridView2.UserDeletedRow += new System.Windows.Forms.DataGridViewRowEventHandler(this.dataGridView2_UserDeletedRow);
            this.dataGridView2.DragDrop += new System.Windows.Forms.DragEventHandler(this.dataGridView2_DragDrop);
            this.dataGridView2.DragOver += new System.Windows.Forms.DragEventHandler(this.dataGridView2_DragOver);
            this.dataGridView2.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dataGridView2_KeyDown);
            this.dataGridView2.MouseDown += new System.Windows.Forms.MouseEventHandler(this.dataGridView2_MouseDown);
            this.dataGridView2.MouseMove += new System.Windows.Forms.MouseEventHandler(this.dataGridView2_MouseMove);
            // 
            // TestSteps
            // 
            this.TestSteps.HeaderText = "Test Step";
            this.TestSteps.Name = "TestSteps";
            // 
            // Index1
            // 
            this.Index1.HeaderText = "Index";
            this.Index1.Name = "Index1";
            this.Index1.ReadOnly = true;
            // 
            // AutomationId1
            // 
            this.AutomationId1.HeaderText = "AutomationId";
            this.AutomationId1.Name = "AutomationId1";
            this.AutomationId1.ReadOnly = true;
            // 
            // Name1
            // 
            this.Name1.HeaderText = "Name";
            this.Name1.Name = "Name1";
            this.Name1.ReadOnly = true;
            // 
            // Type1
            // 
            this.Type1.HeaderText = "Type";
            this.Type1.Name = "Type1";
            this.Type1.ReadOnly = true;
            // 
            // Actions
            // 
            this.Actions.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Actions.HeaderText = "Action";
            this.Actions.Name = "Actions";
            // 
            // InputValue
            // 
            this.InputValue.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.InputValue.HeaderText = "Input Value";
            this.InputValue.Name = "InputValue";
            // 
            // CheckPoint
            // 
            this.CheckPoint.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.CheckPoint.HeaderText = "CheckPoint";
            this.CheckPoint.MinimumWidth = 50;
            this.CheckPoint.Name = "CheckPoint";
            this.CheckPoint.Text = "";
            this.CheckPoint.Width = 71;
            // 
            // rtxtScript
            // 
            this.rtxtScript.AcceptsTab = true;
            this.rtxtScript.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.rtxtScript.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtxtScript.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rtxtScript.Location = new System.Drawing.Point(3, 36);
            this.rtxtScript.Name = "rtxtScript";
            this.rtxtScript.Size = new System.Drawing.Size(792, 462);
            this.rtxtScript.TabIndex = 0;
            this.rtxtScript.Text = "";
            this.rtxtScript.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.rtxtScript_KeyPress);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.redcircleTip,
            this.greencircleTip,
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 780);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1341, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            this.statusStrip1.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.statusStrip1_ItemClicked);
            // 
            // redcircleTip
            // 
            this.redcircleTip.Image = ((System.Drawing.Image)(resources.GetObject("redcircleTip.Image")));
            this.redcircleTip.Name = "redcircleTip";
            this.redcircleTip.Size = new System.Drawing.Size(67, 17);
            this.redcircleTip.Text = "NO AUT";
            // 
            // greencircleTip
            // 
            this.greencircleTip.Image = ((System.Drawing.Image)(resources.GetObject("greencircleTip.Image")));
            this.greencircleTip.Name = "greencircleTip";
            this.greencircleTip.Size = new System.Drawing.Size(49, 17);
            this.greencircleTip.Text = "AUT:";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(118, 17);
            this.toolStripStatusLabel1.Text = "toolStripStatusLabel1";
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 24);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.splitContainer3);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(1341, 756);
            this.splitContainer1.SplitterDistance = 559;
            this.splitContainer1.TabIndex = 14;
            // 
            // splitContainer3
            // 
            this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer3.Location = new System.Drawing.Point(0, 0);
            this.splitContainer3.Name = "splitContainer3";
            // 
            // splitContainer3.Panel1
            // 
            this.splitContainer3.Panel1.Controls.Add(this.dataGridView1);
            this.splitContainer3.Panel1.Controls.Add(this.toolStrip1);
            // 
            // splitContainer3.Panel2
            // 
            this.splitContainer3.Panel2.Controls.Add(this.tabControl1);
            this.splitContainer3.Size = new System.Drawing.Size(1341, 559);
            this.splitContainer3.SplitterDistance = 419;
            this.splitContainer3.TabIndex = 0;
            // 
            // toolStrip1
            // 
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnAttachProcess,
            this.btnSpy,
            this.btnAdd,
            this.toolStripSeparator1,
            this.toolStripLabel1,
            this.toolStripTextBox1,
            this.toolStripSeparator2,
            this.toolStripLabel2,
            this.toolStripComboBox1});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(419, 33);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // btnAttachProcess
            // 
            this.btnAttachProcess.AutoSize = false;
            this.btnAttachProcess.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnAttachProcess.Image = ((System.Drawing.Image)(resources.GetObject("btnAttachProcess.Image")));
            this.btnAttachProcess.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnAttachProcess.Name = "btnAttachProcess";
            this.btnAttachProcess.Size = new System.Drawing.Size(30, 30);
            this.btnAttachProcess.Text = "toolStripButton1";
            this.btnAttachProcess.Click += new System.EventHandler(this.btnAttachProcess_Click);
            // 
            // btnSpy
            // 
            this.btnSpy.AutoSize = false;
            this.btnSpy.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnSpy.Image = ((System.Drawing.Image)(resources.GetObject("btnSpy.Image")));
            this.btnSpy.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSpy.Name = "btnSpy";
            this.btnSpy.Size = new System.Drawing.Size(30, 30);
            this.btnSpy.Text = "toolStripButton2";
            this.btnSpy.Click += new System.EventHandler(this.btnSpy_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.AutoSize = false;
            this.btnAdd.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnAdd.Image = ((System.Drawing.Image)(resources.GetObject("btnAdd.Image")));
            this.btnAdd.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(30, 30);
            this.btnAdd.Text = "toolStripButton3";
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 33);
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(42, 30);
            this.toolStripLabel1.Text = "Search";
            // 
            // toolStripTextBox1
            // 
            this.toolStripTextBox1.Name = "toolStripTextBox1";
            this.toolStripTextBox1.Size = new System.Drawing.Size(100, 33);
            this.toolStripTextBox1.TextChanged += new System.EventHandler(this.toolStripTextBox1_TextChanged);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 33);
            // 
            // toolStripLabel2
            // 
            this.toolStripLabel2.Name = "toolStripLabel2";
            this.toolStripLabel2.Size = new System.Drawing.Size(33, 30);
            this.toolStripLabel2.Text = "Type";
            // 
            // toolStripComboBox1
            // 
            this.toolStripComboBox1.Items.AddRange(new object[] {
            "All",
            "Interactive Controls",
            "TextBox",
            "CheckBox",
            "TabItem",
            "RichTextBox",
            "Button",
            "RadioButton",
            "ComboBox",
            "ComboBoxEdit",
            "DataGrid"});
            this.toolStripComboBox1.Name = "toolStripComboBox1";
            this.toolStripComboBox1.Size = new System.Drawing.Size(121, 23);
            this.toolStripComboBox1.SelectedIndexChanged += new System.EventHandler(this.toolStripComboBox1_SelectedIndexChanged);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(918, 559);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.splitContainer4);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(910, 533);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Playback Table";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // splitContainer4
            // 
            this.splitContainer4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer4.Location = new System.Drawing.Point(3, 3);
            this.splitContainer4.Name = "splitContainer4";
            // 
            // splitContainer4.Panel1
            // 
            this.splitContainer4.Panel1.Controls.Add(this.clbTestScriptList);
            this.splitContainer4.Panel1.Controls.Add(this.toolStrip2);
            // 
            // splitContainer4.Panel2
            // 
            this.splitContainer4.Panel2.Controls.Add(this.tctrlPlayback);
            this.splitContainer4.Size = new System.Drawing.Size(904, 527);
            this.splitContainer4.SplitterDistance = 154;
            this.splitContainer4.TabIndex = 0;
            // 
            // toolStrip2
            // 
            this.toolStrip2.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnPlayScenario,
            this.btnSave,
            this.btnRemoveScript,
            this.btnMoveUpScript,
            this.btnMoveDownScript});
            this.toolStrip2.Location = new System.Drawing.Point(0, 0);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Size = new System.Drawing.Size(154, 33);
            this.toolStrip2.TabIndex = 0;
            this.toolStrip2.Text = "toolStrip2";
            // 
            // btnPlayScenario
            // 
            this.btnPlayScenario.AutoSize = false;
            this.btnPlayScenario.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnPlayScenario.Image = ((System.Drawing.Image)(resources.GetObject("btnPlayScenario.Image")));
            this.btnPlayScenario.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnPlayScenario.Name = "btnPlayScenario";
            this.btnPlayScenario.Size = new System.Drawing.Size(30, 30);
            this.btnPlayScenario.Text = "toolStripButton4";
            this.btnPlayScenario.Click += new System.EventHandler(this.btnPlayScenario_Click);
            // 
            // btnSave
            // 
            this.btnSave.AutoSize = false;
            this.btnSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnSave.Image = ((System.Drawing.Image)(resources.GetObject("btnSave.Image")));
            this.btnSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(30, 30);
            this.btnSave.Text = "toolStripButton5";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnRemoveScript
            // 
            this.btnRemoveScript.AutoSize = false;
            this.btnRemoveScript.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnRemoveScript.Image = ((System.Drawing.Image)(resources.GetObject("btnRemoveScript.Image")));
            this.btnRemoveScript.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnRemoveScript.Name = "btnRemoveScript";
            this.btnRemoveScript.Size = new System.Drawing.Size(30, 30);
            this.btnRemoveScript.Text = "toolStripButton6";
            this.btnRemoveScript.Click += new System.EventHandler(this.btnRemoveScript_Click);
            // 
            // btnMoveUpScript
            // 
            this.btnMoveUpScript.AutoSize = false;
            this.btnMoveUpScript.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnMoveUpScript.Image = ((System.Drawing.Image)(resources.GetObject("btnMoveUpScript.Image")));
            this.btnMoveUpScript.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnMoveUpScript.Name = "btnMoveUpScript";
            this.btnMoveUpScript.Size = new System.Drawing.Size(30, 30);
            this.btnMoveUpScript.Text = "toolStripButton7";
            this.btnMoveUpScript.Click += new System.EventHandler(this.btnMoveUpScript_Click);
            // 
            // btnMoveDownScript
            // 
            this.btnMoveDownScript.AutoSize = false;
            this.btnMoveDownScript.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnMoveDownScript.Image = ((System.Drawing.Image)(resources.GetObject("btnMoveDownScript.Image")));
            this.btnMoveDownScript.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnMoveDownScript.Name = "btnMoveDownScript";
            this.btnMoveDownScript.Size = new System.Drawing.Size(30, 30);
            this.btnMoveDownScript.Text = "toolStripButton8";
            this.btnMoveDownScript.Click += new System.EventHandler(this.btnMoveDownScript_Click);
            // 
            // tctrlPlayback
            // 
            this.tctrlPlayback.Controls.Add(this.tpgPlaybackTable);
            this.tctrlPlayback.Controls.Add(this.tpgPlaybackScript);
            this.tctrlPlayback.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tctrlPlayback.Location = new System.Drawing.Point(0, 0);
            this.tctrlPlayback.Name = "tctrlPlayback";
            this.tctrlPlayback.SelectedIndex = 0;
            this.tctrlPlayback.Size = new System.Drawing.Size(746, 527);
            this.tctrlPlayback.TabIndex = 0;
            // 
            // tpgPlaybackTable
            // 
            this.tpgPlaybackTable.Controls.Add(this.dataGridView2);
            this.tpgPlaybackTable.Controls.Add(this.toolStrip3);
            this.tpgPlaybackTable.Location = new System.Drawing.Point(4, 22);
            this.tpgPlaybackTable.Name = "tpgPlaybackTable";
            this.tpgPlaybackTable.Padding = new System.Windows.Forms.Padding(3);
            this.tpgPlaybackTable.Size = new System.Drawing.Size(738, 501);
            this.tpgPlaybackTable.TabIndex = 0;
            this.tpgPlaybackTable.Text = "Test Steps ";
            this.tpgPlaybackTable.UseVisualStyleBackColor = true;
            // 
            // toolStrip3
            // 
            this.toolStrip3.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnPlayTestStep,
            this.btnCreateScript,
            this.btnSendKeyWait,
            this.btnRemoveRows,
            this.btnMoveRowUp,
            this.btnMoveRowDown});
            this.toolStrip3.Location = new System.Drawing.Point(3, 3);
            this.toolStrip3.Name = "toolStrip3";
            this.toolStrip3.Size = new System.Drawing.Size(732, 33);
            this.toolStrip3.Stretch = true;
            this.toolStrip3.TabIndex = 0;
            this.toolStrip3.Text = "toolStrip3";
            // 
            // btnPlayTestStep
            // 
            this.btnPlayTestStep.AutoSize = false;
            this.btnPlayTestStep.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnPlayTestStep.Image = ((System.Drawing.Image)(resources.GetObject("btnPlayTestStep.Image")));
            this.btnPlayTestStep.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnPlayTestStep.Name = "btnPlayTestStep";
            this.btnPlayTestStep.Size = new System.Drawing.Size(30, 30);
            this.btnPlayTestStep.Text = "toolStripButton9";
            this.btnPlayTestStep.Click += new System.EventHandler(this.btnPlayBackTestStep_Click);
            // 
            // btnCreateScript
            // 
            this.btnCreateScript.AutoSize = false;
            this.btnCreateScript.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnCreateScript.Image = ((System.Drawing.Image)(resources.GetObject("btnCreateScript.Image")));
            this.btnCreateScript.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnCreateScript.Name = "btnCreateScript";
            this.btnCreateScript.Size = new System.Drawing.Size(30, 30);
            this.btnCreateScript.Text = "toolStripButton10";
            this.btnCreateScript.Click += new System.EventHandler(this.btnCreateScript_Click);
            // 
            // btnSendKeyWait
            // 
            this.btnSendKeyWait.AutoSize = false;
            this.btnSendKeyWait.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnSendKeyWait.Image = ((System.Drawing.Image)(resources.GetObject("btnSendKeyWait.Image")));
            this.btnSendKeyWait.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSendKeyWait.Name = "btnSendKeyWait";
            this.btnSendKeyWait.Size = new System.Drawing.Size(30, 30);
            this.btnSendKeyWait.Text = "toolStripButton11";
            this.btnSendKeyWait.Click += new System.EventHandler(this.btnSendKeyWait_Click);
            // 
            // btnRemoveRows
            // 
            this.btnRemoveRows.AutoSize = false;
            this.btnRemoveRows.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnRemoveRows.Image = ((System.Drawing.Image)(resources.GetObject("btnRemoveRows.Image")));
            this.btnRemoveRows.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnRemoveRows.Name = "btnRemoveRows";
            this.btnRemoveRows.Size = new System.Drawing.Size(30, 30);
            this.btnRemoveRows.Text = "toolStripButton12";
            this.btnRemoveRows.Click += new System.EventHandler(this.btnRemoveRows_Click);
            // 
            // btnMoveRowUp
            // 
            this.btnMoveRowUp.AutoSize = false;
            this.btnMoveRowUp.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnMoveRowUp.Image = ((System.Drawing.Image)(resources.GetObject("btnMoveRowUp.Image")));
            this.btnMoveRowUp.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnMoveRowUp.Name = "btnMoveRowUp";
            this.btnMoveRowUp.Size = new System.Drawing.Size(30, 30);
            this.btnMoveRowUp.Text = "toolStripButton13";
            this.btnMoveRowUp.Click += new System.EventHandler(this.btnMoveRowUp_Click);
            // 
            // btnMoveRowDown
            // 
            this.btnMoveRowDown.AutoSize = false;
            this.btnMoveRowDown.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnMoveRowDown.Image = ((System.Drawing.Image)(resources.GetObject("btnMoveRowDown.Image")));
            this.btnMoveRowDown.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnMoveRowDown.Name = "btnMoveRowDown";
            this.btnMoveRowDown.Size = new System.Drawing.Size(30, 30);
            this.btnMoveRowDown.Text = "toolStripButton9";
            this.btnMoveRowDown.Click += new System.EventHandler(this.btnMoveRowDown_Click);
            // 
            // tpgPlaybackScript
            // 
            this.tpgPlaybackScript.Controls.Add(this.rtxtScript);
            this.tpgPlaybackScript.Controls.Add(this.toolStrip4);
            this.tpgPlaybackScript.Location = new System.Drawing.Point(4, 22);
            this.tpgPlaybackScript.Name = "tpgPlaybackScript";
            this.tpgPlaybackScript.Padding = new System.Windows.Forms.Padding(3);
            this.tpgPlaybackScript.Size = new System.Drawing.Size(798, 501);
            this.tpgPlaybackScript.TabIndex = 1;
            this.tpgPlaybackScript.Text = "Test Script ";
            this.tpgPlaybackScript.UseVisualStyleBackColor = true;
            // 
            // toolStrip4
            // 
            this.toolStrip4.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnPlayTestScript,
            this.btnCreateSteps});
            this.toolStrip4.Location = new System.Drawing.Point(3, 3);
            this.toolStrip4.Name = "toolStrip4";
            this.toolStrip4.Size = new System.Drawing.Size(792, 33);
            this.toolStrip4.TabIndex = 0;
            this.toolStrip4.Text = "toolStrip4";
            // 
            // btnPlayTestScript
            // 
            this.btnPlayTestScript.AutoSize = false;
            this.btnPlayTestScript.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnPlayTestScript.Image = ((System.Drawing.Image)(resources.GetObject("btnPlayTestScript.Image")));
            this.btnPlayTestScript.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnPlayTestScript.Name = "btnPlayTestScript";
            this.btnPlayTestScript.Size = new System.Drawing.Size(30, 30);
            this.btnPlayTestScript.Text = "toolStripButton14";
            this.btnPlayTestScript.Click += new System.EventHandler(this.btnPlayTestScript_Click);
            // 
            // btnCreateSteps
            // 
            this.btnCreateSteps.AutoSize = false;
            this.btnCreateSteps.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnCreateSteps.Image = ((System.Drawing.Image)(resources.GetObject("btnCreateSteps.Image")));
            this.btnCreateSteps.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnCreateSteps.Name = "btnCreateSteps";
            this.btnCreateSteps.Size = new System.Drawing.Size(30, 30);
            this.btnCreateSteps.Text = "toolStripButton15";
            this.btnCreateSteps.Click += new System.EventHandler(this.btnCreateSteps_Click);
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.groupBox1);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.groupBox2);
            this.splitContainer2.Size = new System.Drawing.Size(1341, 193);
            this.splitContainer2.SplitterDistance = 419;
            this.splitContainer2.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.ConsolePanelPush);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(419, 193);
            this.groupBox1.TabIndex = 15;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Console";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.ResultPanelPush);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(918, 193);
            this.groupBox2.TabIndex = 16;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Result";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1341, 802);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.Black;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Spy & Playback";
            this.Activated += new System.EventHandler(this.Form1_Activated);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer3.Panel1.ResumeLayout(false);
            this.splitContainer3.Panel1.PerformLayout();
            this.splitContainer3.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).EndInit();
            this.splitContainer3.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.splitContainer4.Panel1.ResumeLayout(false);
            this.splitContainer4.Panel1.PerformLayout();
            this.splitContainer4.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer4)).EndInit();
            this.splitContainer4.ResumeLayout(false);
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            this.tctrlPlayback.ResumeLayout(false);
            this.tpgPlaybackTable.ResumeLayout(false);
            this.tpgPlaybackTable.PerformLayout();
            this.toolStrip3.ResumeLayout(false);
            this.toolStrip3.PerformLayout();
            this.tpgPlaybackScript.ResumeLayout(false);
            this.tpgPlaybackScript.PerformLayout();
            this.toolStrip4.ResumeLayout(false);
            this.toolStrip4.PerformLayout();
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem importToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem InspectorToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.RichTextBox ConsolePanelPush;
        private System.Windows.Forms.RichTextBox ResultPanelPush;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.ToolStripMenuItem helpsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem guideToolStripMenuItem;
        private System.Windows.Forms.CheckedListBox clbTestScriptList;
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.RichTextBox rtxtScript;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel greencircleTip;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.ToolStripStatusLabel redcircleTip;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.ToolStripMenuItem viewLogsToolStripMenuItem;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Select;
        private System.Windows.Forms.DataGridViewTextBoxColumn Index;
        private System.Windows.Forms.DataGridViewTextBoxColumn AutomationId;
        private System.Windows.Forms.DataGridViewTextBoxColumn Names;
        private System.Windows.Forms.DataGridViewTextBoxColumn Type;
        private System.Windows.Forms.DataGridViewTextBoxColumn TestSteps;
        private System.Windows.Forms.DataGridViewTextBoxColumn Index1;
        private System.Windows.Forms.DataGridViewTextBoxColumn AutomationId1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Name1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Type1;
        private System.Windows.Forms.DataGridViewComboBoxColumn Actions;
        private System.Windows.Forms.DataGridViewTextBoxColumn InputValue;
        private System.Windows.Forms.DataGridViewButtonColumn CheckPoint;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.SplitContainer splitContainer3;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton btnAttachProcess;
        private System.Windows.Forms.ToolStripButton btnSpy;
        private System.Windows.Forms.ToolStripButton btnAdd;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.SplitContainer splitContainer4;
        private System.Windows.Forms.TabControl tctrlPlayback;
        private System.Windows.Forms.TabPage tpgPlaybackTable;
        private System.Windows.Forms.TabPage tpgPlaybackScript;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripTextBox toolStripTextBox1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripLabel toolStripLabel2;
        private System.Windows.Forms.ToolStripComboBox toolStripComboBox1;
        private System.Windows.Forms.ToolStrip toolStrip2;
        private System.Windows.Forms.ToolStripButton btnPlayScenario;
        private System.Windows.Forms.ToolStripButton btnSave;
        private System.Windows.Forms.ToolStripButton btnRemoveScript;
        private System.Windows.Forms.ToolStripButton btnMoveUpScript;
        private System.Windows.Forms.ToolStripButton btnMoveDownScript;
        private System.Windows.Forms.ToolStrip toolStrip3;
        private System.Windows.Forms.ToolStripButton btnPlayTestStep;
        private System.Windows.Forms.ToolStripButton btnCreateScript;
        private System.Windows.Forms.ToolStripButton btnSendKeyWait;
        private System.Windows.Forms.ToolStripButton btnRemoveRows;
        private System.Windows.Forms.ToolStripButton btnMoveRowUp;
        private System.Windows.Forms.ToolStrip toolStrip4;
        private System.Windows.Forms.ToolStripButton btnPlayTestScript;
        private System.Windows.Forms.ToolStripButton btnCreateSteps;
        private System.Windows.Forms.ToolStripButton btnMoveRowDown;
    }
}

