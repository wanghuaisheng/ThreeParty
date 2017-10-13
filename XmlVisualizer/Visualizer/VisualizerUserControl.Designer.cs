﻿// Xml Visualizer v.2
// by Lars Hove Christiansen (larshove@gmail.com)
// http://www.codeplex.com/XmlVisualizer

namespace XmlVisualizer
{
    public partial class VisualizerUserControl
    {
        public delegate void OnDisposeEventHandler();
        public event OnDisposeEventHandler OnDisposeEvent;

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
            if (OnDisposeEvent != null)
            {
                OnDisposeEvent();
            }

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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(VisualizerUserControl));
            this.webBrowser = new System.Windows.Forms.WebBrowser();
            this.applyXsltButton = new System.Windows.Forms.Button();
            this.inputFileLabel = new System.Windows.Forms.Label();
            this.xsltFileLabel = new System.Windows.Forms.Label();
            this.applyXmlButton = new System.Windows.Forms.Button();
            this.openXsltFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.xPathLabel = new System.Windows.Forms.Label();
            this.applyXpathButton = new System.Windows.Forms.Button();
            this.xPathTypeComboBox = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.editorControlsUserControl = new XmlVisualizer.EditorControlsUserControl();
            this.treeViewUserControl = new XmlVisualizer.TreeViewUserControl();
            this.showLabel = new System.Windows.Forms.Label();
            this.injectCheckBox = new System.Windows.Forms.CheckBox();
            this.xPathComboBox = new System.Windows.Forms.ComboBox();
            this.xsltFileComboBox = new System.Windows.Forms.ComboBox();
            this.treeViewCheckBox = new System.Windows.Forms.CheckBox();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.selectXsltFileButton = new System.Windows.Forms.Button();
            this.inputFileComboBox = new System.Windows.Forms.ComboBox();
            this.selectXmlFileButton = new System.Windows.Forms.Button();
            this.openXmlFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.newXmlFileButton = new System.Windows.Forms.Button();
            this.newXsltFileButton = new System.Windows.Forms.Button();
            this.editButton = new System.Windows.Forms.Button();
            this.openInBrowserButton = new System.Windows.Forms.Button();
            this.xPathPictureBox = new System.Windows.Forms.PictureBox();
            this.xsltFilePictureBox = new System.Windows.Forms.PictureBox();
            this.inputFilePictureBox = new System.Windows.Forms.PictureBox();
            this.toClipboardButton = new System.Windows.Forms.Button();
            this.aboutButton = new System.Windows.Forms.Button();
            this.closeButton = new System.Windows.Forms.Button();
            this.ReadOnlyLabel = new System.Windows.Forms.Label();
            this.newXsdFileButton = new System.Windows.Forms.Button();
            this.xsdFileComboBox = new System.Windows.Forms.ComboBox();
            this.xsdFilePictureBox = new System.Windows.Forms.PictureBox();
            this.selectXsdFileButton = new System.Windows.Forms.Button();
            this.xsdFileLabel = new System.Windows.Forms.Label();
            this.applyXsdButton = new System.Windows.Forms.Button();
            this.openXsdFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.groupBox1.SuspendLayout();
            this.statusStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xPathPictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xsltFilePictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.inputFilePictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xsdFilePictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // webBrowser
            // 
            this.webBrowser.AllowWebBrowserDrop = false;
            this.webBrowser.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.webBrowser.Location = new System.Drawing.Point(18, 120);
            this.webBrowser.MinimumSize = new System.Drawing.Size(20, 18);
            this.webBrowser.Name = "webBrowser";
            this.webBrowser.Size = new System.Drawing.Size(555, 146);
            this.webBrowser.TabIndex = 5;
            this.webBrowser.TabStop = false;
            this.webBrowser.Url = new System.Uri("", System.UriKind.Relative);
            // 
            // applyXsltButton
            // 
            this.applyXsltButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.applyXsltButton.Location = new System.Drawing.Point(504, 35);
            this.applyXsltButton.Name = "applyXsltButton";
            this.applyXsltButton.Size = new System.Drawing.Size(75, 21);
            this.applyXsltButton.TabIndex = 8;
            this.applyXsltButton.Text = "Apply";
            this.applyXsltButton.UseVisualStyleBackColor = true;
            this.applyXsltButton.Click += new System.EventHandler(this.applyXsltButton_Click);
            this.applyXsltButton.MouseEnter += new System.EventHandler(this.applyXsltButton_MouseEnter);
            this.applyXsltButton.MouseLeave += new System.EventHandler(this.applyXsltButton_MouseLeave);
            // 
            // inputFileLabel
            // 
            this.inputFileLabel.AutoSize = true;
            this.inputFileLabel.Location = new System.Drawing.Point(9, 15);
            this.inputFileLabel.Name = "inputFileLabel";
            this.inputFileLabel.Size = new System.Drawing.Size(65, 12);
            this.inputFileLabel.TabIndex = 6;
            this.inputFileLabel.Text = "Input file";
            // 
            // xsltFileLabel
            // 
            this.xsltFileLabel.AutoSize = true;
            this.xsltFileLabel.Location = new System.Drawing.Point(9, 39);
            this.xsltFileLabel.Name = "xsltFileLabel";
            this.xsltFileLabel.Size = new System.Drawing.Size(59, 12);
            this.xsltFileLabel.TabIndex = 7;
            this.xsltFileLabel.Text = "XSLT file";
            // 
            // applyXmlButton
            // 
            this.applyXmlButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.applyXmlButton.Location = new System.Drawing.Point(504, 11);
            this.applyXmlButton.Name = "applyXmlButton";
            this.applyXmlButton.Size = new System.Drawing.Size(75, 21);
            this.applyXmlButton.TabIndex = 4;
            this.applyXmlButton.Text = "Apply";
            this.applyXmlButton.UseVisualStyleBackColor = true;
            this.applyXmlButton.Click += new System.EventHandler(this.applyXmlButton_Click);
            this.applyXmlButton.MouseEnter += new System.EventHandler(this.applyXmlButton_MouseEnter);
            this.applyXmlButton.MouseLeave += new System.EventHandler(this.applyXmlButton_MouseLeave);
            // 
            // openXsltFileDialog
            // 
            this.openXsltFileDialog.Filter = "XSLT files|*.xslt|All files|*.*";
            this.openXsltFileDialog.FileOk += new System.ComponentModel.CancelEventHandler(this.OpenXsltFileDialog_FileOk);
            // 
            // xPathLabel
            // 
            this.xPathLabel.AutoSize = true;
            this.xPathLabel.Location = new System.Drawing.Point(9, 88);
            this.xPathLabel.Name = "xPathLabel";
            this.xPathLabel.Size = new System.Drawing.Size(35, 12);
            this.xPathLabel.TabIndex = 8;
            this.xPathLabel.Text = "XPath";
            // 
            // applyXpathButton
            // 
            this.applyXpathButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.applyXpathButton.Location = new System.Drawing.Point(504, 83);
            this.applyXpathButton.Name = "applyXpathButton";
            this.applyXpathButton.Size = new System.Drawing.Size(75, 21);
            this.applyXpathButton.TabIndex = 15;
            this.applyXpathButton.Text = "Apply";
            this.applyXpathButton.UseVisualStyleBackColor = true;
            this.applyXpathButton.Click += new System.EventHandler(this.applyXpathButton_Click);
            this.applyXpathButton.MouseEnter += new System.EventHandler(this.applyXpathButton_MouseEnter);
            this.applyXpathButton.MouseLeave += new System.EventHandler(this.applyXpathButton_MouseLeave);
            // 
            // xPathTypeComboBox
            // 
            this.xPathTypeComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.xPathTypeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.xPathTypeComboBox.FormattingEnabled = true;
            this.xPathTypeComboBox.Items.AddRange(new object[] {
            "InnerXml",
            "OuterXml",
            "Value"});
            this.xPathTypeComboBox.Location = new System.Drawing.Point(351, 84);
            this.xPathTypeComboBox.Name = "xPathTypeComboBox";
            this.xPathTypeComboBox.Size = new System.Drawing.Size(112, 20);
            this.xPathTypeComboBox.TabIndex = 14;
            this.xPathTypeComboBox.SelectedIndexChanged += new System.EventHandler(this.XPathTypeComboBox_SelectedIndexChanged);
            this.xPathTypeComboBox.MouseEnter += new System.EventHandler(this.xPathTypeComboBox_MouseEnter);
            this.xPathTypeComboBox.MouseLeave += new System.EventHandler(this.xPathTypeComboBox_MouseLeave);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.editorControlsUserControl);
            this.groupBox1.Controls.Add(this.treeViewUserControl);
            this.groupBox1.Location = new System.Drawing.Point(12, 109);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(567, 162);
            this.groupBox1.TabIndex = 14;
            this.groupBox1.TabStop = false;
            // 
            // editorControlsUserControl
            // 
            this.editorControlsUserControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.editorControlsUserControl.ChangesInEditor = false;
            this.editorControlsUserControl.Location = new System.Drawing.Point(6, 11);
            this.editorControlsUserControl.Name = "editorControlsUserControl";
            this.editorControlsUserControl.Size = new System.Drawing.Size(555, 146);
            this.editorControlsUserControl.TabIndex = 26;
            this.editorControlsUserControl.Visible = false;
            // 
            // treeViewUserControl
            // 
            this.treeViewUserControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.treeViewUserControl.Location = new System.Drawing.Point(6, 11);
            this.treeViewUserControl.Name = "treeViewUserControl";
            this.treeViewUserControl.Size = new System.Drawing.Size(556, 146);
            this.treeViewUserControl.TabIndex = 25;
            this.treeViewUserControl.Visible = false;
            // 
            // showLabel
            // 
            this.showLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.showLabel.AutoSize = true;
            this.showLabel.Location = new System.Drawing.Point(311, 88);
            this.showLabel.Name = "showLabel";
            this.showLabel.Size = new System.Drawing.Size(29, 12);
            this.showLabel.TabIndex = 15;
            this.showLabel.Text = "Show";
            // 
            // injectCheckBox
            // 
            this.injectCheckBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.injectCheckBox.AutoSize = true;
            this.injectCheckBox.Checked = true;
            this.injectCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.injectCheckBox.Location = new System.Drawing.Point(414, 286);
            this.injectCheckBox.Name = "injectCheckBox";
            this.injectCheckBox.Size = new System.Drawing.Size(84, 16);
            this.injectCheckBox.TabIndex = 21;
            this.injectCheckBox.Text = "Inject Xml";
            this.injectCheckBox.UseVisualStyleBackColor = true;
            this.injectCheckBox.Visible = false;
            this.injectCheckBox.CheckedChanged += new System.EventHandler(this.injectCheckBox_CheckedChanged);
            this.injectCheckBox.MouseEnter += new System.EventHandler(this.injectCheckBox_MouseEnter);
            this.injectCheckBox.MouseLeave += new System.EventHandler(this.injectCheckBox_MouseLeave);
            // 
            // xPathComboBox
            // 
            this.xPathComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.xPathComboBox.FormattingEnabled = true;
            this.xPathComboBox.Location = new System.Drawing.Point(65, 84);
            this.xPathComboBox.Name = "xPathComboBox";
            this.xPathComboBox.Size = new System.Drawing.Size(240, 20);
            this.xPathComboBox.TabIndex = 13;
            this.xPathComboBox.SelectedIndexChanged += new System.EventHandler(this.XPathComboBox_SelectedIndexChanged);
            this.xPathComboBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.XPathComboBox_KeyDown);
            this.xPathComboBox.MouseEnter += new System.EventHandler(this.xPathComboBox_MouseEnter);
            this.xPathComboBox.MouseLeave += new System.EventHandler(this.xPathComboBox_MouseLeave);
            // 
            // xsltFileComboBox
            // 
            this.xsltFileComboBox.AllowDrop = true;
            this.xsltFileComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.xsltFileComboBox.FormattingEnabled = true;
            this.xsltFileComboBox.Location = new System.Drawing.Point(65, 36);
            this.xsltFileComboBox.Name = "xsltFileComboBox";
            this.xsltFileComboBox.Size = new System.Drawing.Size(338, 20);
            this.xsltFileComboBox.TabIndex = 5;
            this.xsltFileComboBox.SelectionChangeCommitted += new System.EventHandler(this.XsltFileComboBox_SelectionChangeCommitted);
            this.xsltFileComboBox.DragDrop += new System.Windows.Forms.DragEventHandler(this.xsltFileComboBox_DragDrop);
            this.xsltFileComboBox.DragEnter += new System.Windows.Forms.DragEventHandler(this.xsltFileComboBox_DragEnter);
            this.xsltFileComboBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.XsltFileComboBox_KeyDown);
            this.xsltFileComboBox.MouseEnter += new System.EventHandler(this.xsltFileComboBox_MouseEnter);
            this.xsltFileComboBox.MouseLeave += new System.EventHandler(this.xsltFileComboBox_MouseLeave);
            // 
            // treeViewCheckBox
            // 
            this.treeViewCheckBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.treeViewCheckBox.AutoSize = true;
            this.treeViewCheckBox.Location = new System.Drawing.Point(213, 285);
            this.treeViewCheckBox.Name = "treeViewCheckBox";
            this.treeViewCheckBox.Size = new System.Drawing.Size(96, 16);
            this.treeViewCheckBox.TabIndex = 20;
            this.treeViewCheckBox.Text = "Show as Tree";
            this.treeViewCheckBox.UseVisualStyleBackColor = true;
            this.treeViewCheckBox.CheckedChanged += new System.EventHandler(this.TreeViewCheckBox_CheckedChanged);
            this.treeViewCheckBox.MouseEnter += new System.EventHandler(this.treeViewCheckBox_MouseEnter);
            this.treeViewCheckBox.MouseLeave += new System.EventHandler(this.treeViewCheckBox_MouseLeave);
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel});
            this.statusStrip.Location = new System.Drawing.Point(0, 313);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(592, 22);
            this.statusStrip.TabIndex = 16;
            // 
            // toolStripStatusLabel
            // 
            this.toolStripStatusLabel.Name = "toolStripStatusLabel";
            this.toolStripStatusLabel.Size = new System.Drawing.Size(0, 17);
            // 
            // selectXsltFileButton
            // 
            this.selectXsltFileButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.selectXsltFileButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.selectXsltFileButton.Image = ((System.Drawing.Image)(resources.GetObject("selectXsltFileButton.Image")));
            this.selectXsltFileButton.Location = new System.Drawing.Point(409, 35);
            this.selectXsltFileButton.Name = "selectXsltFileButton";
            this.selectXsltFileButton.Size = new System.Drawing.Size(24, 21);
            this.selectXsltFileButton.TabIndex = 6;
            this.selectXsltFileButton.UseVisualStyleBackColor = true;
            this.selectXsltFileButton.Click += new System.EventHandler(this.SelectXsltFileButton_Click);
            this.selectXsltFileButton.MouseEnter += new System.EventHandler(this.selectXsltFileButton_MouseEnter);
            this.selectXsltFileButton.MouseLeave += new System.EventHandler(this.selectXsltFileButton_MouseLeave);
            // 
            // inputFileComboBox
            // 
            this.inputFileComboBox.AllowDrop = true;
            this.inputFileComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.inputFileComboBox.FormattingEnabled = true;
            this.inputFileComboBox.Location = new System.Drawing.Point(65, 12);
            this.inputFileComboBox.Name = "inputFileComboBox";
            this.inputFileComboBox.Size = new System.Drawing.Size(338, 20);
            this.inputFileComboBox.TabIndex = 1;
            this.inputFileComboBox.SelectionChangeCommitted += new System.EventHandler(this.inputFileComboBox_SelectionChangeCommitted);
            this.inputFileComboBox.DragDrop += new System.Windows.Forms.DragEventHandler(this.inputFileComboBox_DragDrop);
            this.inputFileComboBox.DragEnter += new System.Windows.Forms.DragEventHandler(this.inputFileComboBox_DragEnter);
            this.inputFileComboBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.inputFileComboBox_KeyDown);
            this.inputFileComboBox.MouseEnter += new System.EventHandler(this.inputFileComboBox_MouseEnter);
            this.inputFileComboBox.MouseLeave += new System.EventHandler(this.inputFileComboBox_MouseLeave);
            // 
            // selectXmlFileButton
            // 
            this.selectXmlFileButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.selectXmlFileButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.selectXmlFileButton.Image = ((System.Drawing.Image)(resources.GetObject("selectXmlFileButton.Image")));
            this.selectXmlFileButton.Location = new System.Drawing.Point(409, 11);
            this.selectXmlFileButton.Name = "selectXmlFileButton";
            this.selectXmlFileButton.Size = new System.Drawing.Size(24, 21);
            this.selectXmlFileButton.TabIndex = 2;
            this.selectXmlFileButton.UseVisualStyleBackColor = true;
            this.selectXmlFileButton.Click += new System.EventHandler(this.SelectXmlFileButton_Click);
            this.selectXmlFileButton.MouseEnter += new System.EventHandler(this.selectXmlFileButton_MouseEnter);
            this.selectXmlFileButton.MouseLeave += new System.EventHandler(this.selectXmlFileButton_MouseLeave);
            // 
            // openXmlFileDialog
            // 
            this.openXmlFileDialog.Filter = "Xml files|*.xml|All files|*.*";
            this.openXmlFileDialog.FileOk += new System.ComponentModel.CancelEventHandler(this.OpenXmlFileDialog_FileOk);
            // 
            // newXmlFileButton
            // 
            this.newXmlFileButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.newXmlFileButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.newXmlFileButton.Image = ((System.Drawing.Image)(resources.GetObject("newXmlFileButton.Image")));
            this.newXmlFileButton.ImageAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.newXmlFileButton.Location = new System.Drawing.Point(439, 11);
            this.newXmlFileButton.Name = "newXmlFileButton";
            this.newXmlFileButton.Size = new System.Drawing.Size(24, 21);
            this.newXmlFileButton.TabIndex = 3;
            this.newXmlFileButton.UseVisualStyleBackColor = true;
            this.newXmlFileButton.Click += new System.EventHandler(this.newXmlFileButton_Click);
            this.newXmlFileButton.MouseEnter += new System.EventHandler(this.newXmlFileButton_MouseEnter);
            this.newXmlFileButton.MouseLeave += new System.EventHandler(this.newXmlFileButton_MouseLeave);
            // 
            // newXsltFileButton
            // 
            this.newXsltFileButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.newXsltFileButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.newXsltFileButton.Image = ((System.Drawing.Image)(resources.GetObject("newXsltFileButton.Image")));
            this.newXsltFileButton.ImageAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.newXsltFileButton.Location = new System.Drawing.Point(439, 35);
            this.newXsltFileButton.Name = "newXsltFileButton";
            this.newXsltFileButton.Size = new System.Drawing.Size(24, 21);
            this.newXsltFileButton.TabIndex = 7;
            this.newXsltFileButton.UseVisualStyleBackColor = true;
            this.newXsltFileButton.Click += new System.EventHandler(this.NewXsltFileButton_Click);
            this.newXsltFileButton.MouseEnter += new System.EventHandler(this.newXsltFileButton_MouseEnter);
            this.newXsltFileButton.MouseLeave += new System.EventHandler(this.newXsltFileButton_MouseLeave);
            // 
            // editButton
            // 
            this.editButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.editButton.Image = ((System.Drawing.Image)(resources.GetObject("editButton.Image")));
            this.editButton.ImageAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.editButton.Location = new System.Drawing.Point(213, 282);
            this.editButton.Name = "editButton";
            this.editButton.Size = new System.Drawing.Size(75, 21);
            this.editButton.TabIndex = 19;
            this.editButton.Text = "Edit";
            this.editButton.UseVisualStyleBackColor = true;
            this.editButton.Visible = false;
            this.editButton.Click += new System.EventHandler(this.EditButton_Click);
            this.editButton.MouseEnter += new System.EventHandler(this.editButton_MouseEnter);
            this.editButton.MouseLeave += new System.EventHandler(this.editButton_MouseLeave);
            // 
            // openInBrowserButton
            // 
            this.openInBrowserButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.openInBrowserButton.Image = ((System.Drawing.Image)(resources.GetObject("openInBrowserButton.Image")));
            this.openInBrowserButton.ImageAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.openInBrowserButton.Location = new System.Drawing.Point(51, 282);
            this.openInBrowserButton.Name = "openInBrowserButton";
            this.openInBrowserButton.Size = new System.Drawing.Size(75, 21);
            this.openInBrowserButton.TabIndex = 17;
            this.openInBrowserButton.Text = "Open";
            this.openInBrowserButton.UseVisualStyleBackColor = true;
            this.openInBrowserButton.Click += new System.EventHandler(this.OpenInBrowserButton_Click);
            this.openInBrowserButton.MouseEnter += new System.EventHandler(this.openInBrowserButton_MouseEnter);
            this.openInBrowserButton.MouseLeave += new System.EventHandler(this.openInBrowserButton_MouseLeave);
            // 
            // xPathPictureBox
            // 
            this.xPathPictureBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.xPathPictureBox.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("xPathPictureBox.BackgroundImage")));
            this.xPathPictureBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.xPathPictureBox.ErrorImage = null;
            this.xPathPictureBox.InitialImage = null;
            this.xPathPictureBox.Location = new System.Drawing.Point(469, 84);
            this.xPathPictureBox.Name = "xPathPictureBox";
            this.xPathPictureBox.Size = new System.Drawing.Size(29, 20);
            this.xPathPictureBox.TabIndex = 13;
            this.xPathPictureBox.TabStop = false;
            this.xPathPictureBox.Visible = false;
            // 
            // xsltFilePictureBox
            // 
            this.xsltFilePictureBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.xsltFilePictureBox.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("xsltFilePictureBox.BackgroundImage")));
            this.xsltFilePictureBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.xsltFilePictureBox.ErrorImage = null;
            this.xsltFilePictureBox.InitialImage = null;
            this.xsltFilePictureBox.Location = new System.Drawing.Point(469, 36);
            this.xsltFilePictureBox.Name = "xsltFilePictureBox";
            this.xsltFilePictureBox.Size = new System.Drawing.Size(29, 20);
            this.xsltFilePictureBox.TabIndex = 12;
            this.xsltFilePictureBox.TabStop = false;
            this.xsltFilePictureBox.Visible = false;
            // 
            // inputFilePictureBox
            // 
            this.inputFilePictureBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.inputFilePictureBox.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("inputFilePictureBox.BackgroundImage")));
            this.inputFilePictureBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.inputFilePictureBox.ErrorImage = null;
            this.inputFilePictureBox.InitialImage = null;
            this.inputFilePictureBox.Location = new System.Drawing.Point(469, 12);
            this.inputFilePictureBox.Name = "inputFilePictureBox";
            this.inputFilePictureBox.Size = new System.Drawing.Size(29, 20);
            this.inputFilePictureBox.TabIndex = 11;
            this.inputFilePictureBox.TabStop = false;
            this.inputFilePictureBox.Visible = false;
            // 
            // toClipboardButton
            // 
            this.toClipboardButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.toClipboardButton.Image = ((System.Drawing.Image)(resources.GetObject("toClipboardButton.Image")));
            this.toClipboardButton.ImageAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.toClipboardButton.Location = new System.Drawing.Point(132, 282);
            this.toClipboardButton.Name = "toClipboardButton";
            this.toClipboardButton.Size = new System.Drawing.Size(75, 21);
            this.toClipboardButton.TabIndex = 18;
            this.toClipboardButton.Text = "Copy";
            this.toClipboardButton.UseVisualStyleBackColor = true;
            this.toClipboardButton.Click += new System.EventHandler(this.ToClipboardButton_Click);
            this.toClipboardButton.MouseEnter += new System.EventHandler(this.toClipboardButton_MouseEnter);
            this.toClipboardButton.MouseLeave += new System.EventHandler(this.toClipboardButton_MouseLeave);
            // 
            // aboutButton
            // 
            this.aboutButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.aboutButton.Image = ((System.Drawing.Image)(resources.GetObject("aboutButton.Image")));
            this.aboutButton.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.aboutButton.Location = new System.Drawing.Point(13, 282);
            this.aboutButton.Name = "aboutButton";
            this.aboutButton.Size = new System.Drawing.Size(32, 21);
            this.aboutButton.TabIndex = 16;
            this.aboutButton.UseVisualStyleBackColor = true;
            this.aboutButton.Click += new System.EventHandler(this.AboutButton_Click);
            this.aboutButton.MouseEnter += new System.EventHandler(this.aboutButton_MouseEnter);
            this.aboutButton.MouseLeave += new System.EventHandler(this.aboutButton_MouseLeave);
            // 
            // closeButton
            // 
            this.closeButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.closeButton.Image = ((System.Drawing.Image)(resources.GetObject("closeButton.Image")));
            this.closeButton.ImageAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.closeButton.Location = new System.Drawing.Point(504, 282);
            this.closeButton.Name = "closeButton";
            this.closeButton.Size = new System.Drawing.Size(75, 21);
            this.closeButton.TabIndex = 22;
            this.closeButton.Text = "Ok";
            this.closeButton.UseVisualStyleBackColor = true;
            this.closeButton.Click += new System.EventHandler(this.CloseButton_Click);
            this.closeButton.MouseEnter += new System.EventHandler(this.closeButton_MouseEnter);
            this.closeButton.MouseLeave += new System.EventHandler(this.closeButton_MouseLeave);
            // 
            // ReadOnlyLabel
            // 
            this.ReadOnlyLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.ReadOnlyLabel.AutoSize = true;
            this.ReadOnlyLabel.Location = new System.Drawing.Point(404, 287);
            this.ReadOnlyLabel.Name = "ReadOnlyLabel";
            this.ReadOnlyLabel.Size = new System.Drawing.Size(119, 12);
            this.ReadOnlyLabel.TabIndex = 19;
            this.ReadOnlyLabel.Text = "Object is read only";
            this.ReadOnlyLabel.Visible = false;
            // 
            // newXsdFileButton
            // 
            this.newXsdFileButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.newXsdFileButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.newXsdFileButton.Image = ((System.Drawing.Image)(resources.GetObject("newXsdFileButton.Image")));
            this.newXsdFileButton.ImageAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.newXsdFileButton.Location = new System.Drawing.Point(439, 59);
            this.newXsdFileButton.Name = "newXsdFileButton";
            this.newXsdFileButton.Size = new System.Drawing.Size(24, 21);
            this.newXsdFileButton.TabIndex = 11;
            this.newXsdFileButton.UseVisualStyleBackColor = true;
            this.newXsdFileButton.Click += new System.EventHandler(this.newXsdFileButton_Click);
            this.newXsdFileButton.MouseEnter += new System.EventHandler(this.newXsdFileButton_MouseEnter);
            this.newXsdFileButton.MouseLeave += new System.EventHandler(this.newXsdFileButton_MouseLeave);
            // 
            // xsdFileComboBox
            // 
            this.xsdFileComboBox.AllowDrop = true;
            this.xsdFileComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.xsdFileComboBox.FormattingEnabled = true;
            this.xsdFileComboBox.Location = new System.Drawing.Point(65, 60);
            this.xsdFileComboBox.Name = "xsdFileComboBox";
            this.xsdFileComboBox.Size = new System.Drawing.Size(338, 20);
            this.xsdFileComboBox.TabIndex = 9;
            this.xsdFileComboBox.SelectionChangeCommitted += new System.EventHandler(this.xsdFileComboBox_SelectionChangeCommitted);
            this.xsdFileComboBox.DragDrop += new System.Windows.Forms.DragEventHandler(this.xsdFileComboBox_DragDrop);
            this.xsdFileComboBox.DragEnter += new System.Windows.Forms.DragEventHandler(this.xsdFileComboBox_DragEnter);
            this.xsdFileComboBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.xsdFileComboBox_KeyDown);
            this.xsdFileComboBox.MouseEnter += new System.EventHandler(this.xsdFileComboBox_MouseEnter);
            this.xsdFileComboBox.MouseLeave += new System.EventHandler(this.xsdFileComboBox_MouseLeave);
            // 
            // xsdFilePictureBox
            // 
            this.xsdFilePictureBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.xsdFilePictureBox.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("xsdFilePictureBox.BackgroundImage")));
            this.xsdFilePictureBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.xsdFilePictureBox.ErrorImage = null;
            this.xsdFilePictureBox.InitialImage = null;
            this.xsdFilePictureBox.Location = new System.Drawing.Point(469, 60);
            this.xsdFilePictureBox.Name = "xsdFilePictureBox";
            this.xsdFilePictureBox.Size = new System.Drawing.Size(29, 20);
            this.xsdFilePictureBox.TabIndex = 25;
            this.xsdFilePictureBox.TabStop = false;
            this.xsdFilePictureBox.Visible = false;
            // 
            // selectXsdFileButton
            // 
            this.selectXsdFileButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.selectXsdFileButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.selectXsdFileButton.Image = ((System.Drawing.Image)(resources.GetObject("selectXsdFileButton.Image")));
            this.selectXsdFileButton.Location = new System.Drawing.Point(409, 59);
            this.selectXsdFileButton.Name = "selectXsdFileButton";
            this.selectXsdFileButton.Size = new System.Drawing.Size(24, 21);
            this.selectXsdFileButton.TabIndex = 10;
            this.selectXsdFileButton.UseVisualStyleBackColor = true;
            this.selectXsdFileButton.Click += new System.EventHandler(this.selectXsdFileButton_Click);
            this.selectXsdFileButton.MouseEnter += new System.EventHandler(this.selectXsdFileButton_MouseEnter);
            this.selectXsdFileButton.MouseLeave += new System.EventHandler(this.selectXsdFileButton_MouseLeave);
            // 
            // xsdFileLabel
            // 
            this.xsdFileLabel.AutoSize = true;
            this.xsdFileLabel.Location = new System.Drawing.Point(9, 63);
            this.xsdFileLabel.Name = "xsdFileLabel";
            this.xsdFileLabel.Size = new System.Drawing.Size(53, 12);
            this.xsdFileLabel.TabIndex = 23;
            this.xsdFileLabel.Text = "XSD file";
            // 
            // applyXsdButton
            // 
            this.applyXsdButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.applyXsdButton.Location = new System.Drawing.Point(504, 59);
            this.applyXsdButton.Name = "applyXsdButton";
            this.applyXsdButton.Size = new System.Drawing.Size(75, 21);
            this.applyXsdButton.TabIndex = 12;
            this.applyXsdButton.Text = "Apply";
            this.applyXsdButton.UseVisualStyleBackColor = true;
            this.applyXsdButton.Click += new System.EventHandler(this.applyXsdButton_Click);
            this.applyXsdButton.MouseEnter += new System.EventHandler(this.applyXsdButton_MouseEnter);
            this.applyXsdButton.MouseLeave += new System.EventHandler(this.applyXsdButton_MouseLeave);
            // 
            // openXsdFileDialog
            // 
            this.openXsdFileDialog.Filter = "XSD files|*.xsd|All files|*.*";
            this.openXsdFileDialog.FileOk += new System.ComponentModel.CancelEventHandler(this.openXsdFileDialog_FileOk);
            // 
            // VisualizerUserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.newXsdFileButton);
            this.Controls.Add(this.xsdFileComboBox);
            this.Controls.Add(this.xsdFilePictureBox);
            this.Controls.Add(this.selectXsdFileButton);
            this.Controls.Add(this.xsdFileLabel);
            this.Controls.Add(this.applyXsdButton);
            this.Controls.Add(this.ReadOnlyLabel);
            this.Controls.Add(this.newXmlFileButton);
            this.Controls.Add(this.selectXmlFileButton);
            this.Controls.Add(this.inputFileComboBox);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.treeViewCheckBox);
            this.Controls.Add(this.newXsltFileButton);
            this.Controls.Add(this.xsltFileComboBox);
            this.Controls.Add(this.xPathComboBox);
            this.Controls.Add(this.webBrowser);
            this.Controls.Add(this.editButton);
            this.Controls.Add(this.injectCheckBox);
            this.Controls.Add(this.openInBrowserButton);
            this.Controls.Add(this.showLabel);
            this.Controls.Add(this.xPathPictureBox);
            this.Controls.Add(this.xsltFilePictureBox);
            this.Controls.Add(this.inputFilePictureBox);
            this.Controls.Add(this.toClipboardButton);
            this.Controls.Add(this.xPathTypeComboBox);
            this.Controls.Add(this.applyXpathButton);
            this.Controls.Add(this.xPathLabel);
            this.Controls.Add(this.selectXsltFileButton);
            this.Controls.Add(this.applyXmlButton);
            this.Controls.Add(this.xsltFileLabel);
            this.Controls.Add(this.inputFileLabel);
            this.Controls.Add(this.applyXsltButton);
            this.Controls.Add(this.aboutButton);
            this.Controls.Add(this.closeButton);
            this.Controls.Add(this.groupBox1);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.MinimumSize = new System.Drawing.Size(592, 277);
            this.Name = "VisualizerUserControl";
            this.Size = new System.Drawing.Size(592, 335);
            this.groupBox1.ResumeLayout(false);
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xPathPictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xsltFilePictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.inputFilePictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xsdFilePictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button closeButton;
        private System.Windows.Forms.Button aboutButton;
        private System.Windows.Forms.Button applyXsltButton;
        private System.Windows.Forms.Label inputFileLabel;
        private System.Windows.Forms.Label xsltFileLabel;
        private System.Windows.Forms.Button applyXmlButton;
        private System.Windows.Forms.OpenFileDialog openXsltFileDialog;
        private System.Windows.Forms.Button selectXsltFileButton;
        private System.Windows.Forms.Label xPathLabel;
        private System.Windows.Forms.Button applyXpathButton;
        private System.Windows.Forms.ComboBox xPathTypeComboBox;
        private System.Windows.Forms.Button toClipboardButton;
        private System.Windows.Forms.PictureBox inputFilePictureBox;
        private System.Windows.Forms.PictureBox xsltFilePictureBox;
        private System.Windows.Forms.PictureBox xPathPictureBox;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label showLabel;
        private System.Windows.Forms.Button openInBrowserButton;
        private System.Windows.Forms.CheckBox injectCheckBox;
        private System.Windows.Forms.Button editButton;
        private System.Windows.Forms.ComboBox xPathComboBox;
        private System.Windows.Forms.ComboBox xsltFileComboBox;
        private System.Windows.Forms.Button newXsltFileButton;
        private System.Windows.Forms.CheckBox treeViewCheckBox;
        private System.Windows.Forms.StatusStrip statusStrip;
        private TreeViewUserControl treeViewUserControl;
        private EditorControlsUserControl editorControlsUserControl;
        private System.Windows.Forms.ComboBox inputFileComboBox;
        private System.Windows.Forms.Button newXmlFileButton;
        private System.Windows.Forms.Button selectXmlFileButton;
        private System.Windows.Forms.OpenFileDialog openXmlFileDialog;
        private System.Windows.Forms.WebBrowser webBrowser;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel;
        private System.Windows.Forms.Label ReadOnlyLabel;
        private System.Windows.Forms.Button newXsdFileButton;
        private System.Windows.Forms.ComboBox xsdFileComboBox;
        private System.Windows.Forms.PictureBox xsdFilePictureBox;
        private System.Windows.Forms.Button selectXsdFileButton;
        private System.Windows.Forms.Label xsdFileLabel;
        private System.Windows.Forms.Button applyXsdButton;
        private System.Windows.Forms.OpenFileDialog openXsdFileDialog;
    }
}
