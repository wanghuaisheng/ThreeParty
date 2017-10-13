﻿// Xml Visualizer v.2
// by Lars Hove Christiansen (larshove@gmail.com)
// http://www.codeplex.com/XmlVisualizer

using System;
using System.ComponentModel;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Xml.XPath;

namespace XmlVisualizer
{
    public partial class VisualizerUserControl : UserControl
    {
        public delegate void CloseEventHandler();
        public event CloseEventHandler CloseEvent;
        public delegate void StatusTextEventHandler(string statusText);
        public event StatusTextEventHandler StatusTextEvent;

        public bool inject;
        public string originalXmlFile;

        private const int numberOfXsdFilesToSave = 10;
        private const int numberOfXPathQueriesToSave = 10;
        private const int numberOfXsltFilesToSave = 10;
        private const int numberOfXmlFilesToSave = 10;
        private States ActiveState;
        private States StateBeforeNewFile;
        private States StateBeforeXsltError;
        private string previousXPathQuery;
        private string previousXsltFile;
        private string previousXmlFile;
        private string previousXsdFile;
        private string previousFileFromXPath;
        private string previousFileFromXslt;
        private string appliedXsdFile;
        private string appliedXsltFile;
        private string debugString;
        private bool mainFormLoaded;
        private bool errorInXslt;
        private bool errorInXml;
        private bool editorEnabled;
        private bool treeviewEnabled;
        private bool newFile;
        private bool doNotDeleteFile;
        private bool debugMode;
        private bool standAlone;

        public VisualizerUserControl()
        {
            InitializeComponent();
            SetXPathDefaultType();
            FillXPathComboBox();
            FillXsdFileComboBox();
            FillXsltFileComboBox();
            FillInputFileComboBox();
            SetActive(States.InputFile);
            ReadInjectState();
            InitializeTreeViewEvents();
            InitializeEditorEvents();
            InitializeVisualizerEvents();

            originalXmlFile = "";
            debugString = "";

            SetNoInputFileOptions();
        }

        public void SetWebBrowserFocus()
        {
            webBrowser.Focus();
        }

        private void InitializeVisualizerEvents()
        {
            OnDisposeEvent += VisualizerUserControl_OnDisposeEvent;
        }

        private void VisualizerUserControl_OnDisposeEvent()
        {
            webBrowser.Dispose();
            Cleanup();
        }

        /// <summary>
        /// Gets or Sets the visibility of the status bar
        /// </summary>
        [Description("Gets or Sets the visibility of the status bar"), Category("Visualizer Control"), DefaultValue(true), Browsable(true)]
        public bool StatusBarVisible
        {
            get
            {
                return statusStrip.Visible;
            }
            set
            {
                statusStrip.Visible = value;
            }
        }

        public void SetCloseButtonText(string text)
        {
            closeButton.Text = text;
        }

        public void SetDebugMode()
        {
            debugMode = true;
        }

        public void SetStandAlone()
        {
            standAlone = true;
        }

        public bool AnyChangesToInputXml()
        {
            return editorControlsUserControl.AnyChangesToInject();
        }

        public void LoadXmlFromString(string objectString)
        {
            ClearSession();
            doNotDeleteFile = false;

            debugString = objectString;
            
            if (!String.IsNullOrEmpty(objectString))
            {
                SetInputFileOptions();
                InitiateWebBrowser();
            }
            else
            {
                SetNoInputFileOptions();
            }
        }

        public void LoadXmlFromString(string objectString, bool replaceable)
        {
            LoadXmlFromString(objectString);

            if (debugMode)
            {
                HandleInjectAction(replaceable);
            }
        }

        public string GetModifiedXml()
        {
            string modifiedXml = "";

            try
            {
                if (!String.IsNullOrEmpty(originalXmlFile))
                {
                    modifiedXml = File.ReadAllText(originalXmlFile);
                }
            }
            catch (Exception e)
            {
                Util.ShowMessage(string.Format("Can't get modified Xml. Please make sure that the input file is correct.\r\nError: {0}", e.Message));
            }

            return modifiedXml;
        }

        private void InitiateWebBrowser()
        {
            if ((!String.IsNullOrEmpty(debugString)) && !doNotDeleteFile)
            {
                originalXmlFile = string.Format(@"{0}{1}.xml", Path.GetTempPath(), Guid.NewGuid());
                previousXmlFile = originalXmlFile;
                inputFileComboBox.Text = originalXmlFile;
                WriteFile(originalXmlFile, debugString);
                webBrowser.Url = new Uri(originalXmlFile);
            }

            mainFormLoaded = true;
        }

        private void HandleInjectAction(bool replaceable)
        {
            if (replaceable)
            {
                injectCheckBox.Visible = true;
                ReadOnlyLabel.Visible = false;
            }
            else
            {
                injectCheckBox.Visible = false;
                ReadOnlyLabel.Visible = true;
            }
        }

        private void InitializeTreeViewEvents()
        {
            treeViewUserControl.StatusTextEvent += treeViewUserControl_StatusTextEvent;
        }

        private void InitializeEditorEvents()
        {
            editorControlsUserControl.StatusTextEvent += editorControlsUserControl_StatusTextEvent;
            editorControlsUserControl.CloseEvent += editorControlsUserControl_CloseEvent;
            editorControlsUserControl.SaveEvent += editorControlsUserControl_SaveEvent;
            editorControlsUserControl.SaveAsEvent += editorControlsUserControl_SaveAsEvent;
            editorControlsUserControl.UnformatXmlEvent += editorControlsUserControl_UnformatXmlEvent;
            editorControlsUserControl.FormatXmlEvent += editorControlsUserControl_FormatXmlEvent;
            editorControlsUserControl.CaretChangeEvent += editorControlsUserControl_CaretChangeEvent;
        }

        private void SetToolStripText(string text)
        {
            toolStripStatusLabel.Text = text;

            if (StatusTextEvent != null)
            {
                StatusTextEvent(text);
            }
        }

        private void editorControlsUserControl_CaretChangeEvent(int line, int column)
        {
            SetToolStripText(string.Format("Ln {0}, Col {1}", line, column));
        }

        private void treeViewUserControl_StatusTextEvent(string statusText)
        {
            SetToolStripText(statusText);
        }

        private void editorControlsUserControl_StatusTextEvent(string statusText)
        {
            SetToolStripText(statusText);
        }

        private void editorControlsUserControl_CloseEvent()
        {
            DisableEditorControl(false);
        }

        private void SaveBeforeUnformating(string messageBoxText, string unformattedText)
        {
            bool saved = false;

            DialogResult dr = MessageBox.Show(messageBoxText, Util.GetTitle(), MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information);

            if (dr.ToString() == "Yes")
            {
                if (newFile)
                {
                    saved = editorControlsUserControl.HandleSaveAs(false);
                }
                else
                {
                    saved = editorControlsUserControl.HandleSave(false);
                }
            }
            else if (dr.ToString() == "No")
            {
                editorControlsUserControl.ChangesInEditor = false;
                editorControlsUserControl.SetText(unformattedText);
            }
            else
            {
                editorControlsUserControl.SetDoNotHandleFormat(true);
                editorControlsUserControl.SetFormatXmlCheckBox(true);
                editorControlsUserControl.SetDoNotHandleFormat(false);
            }

            if (saved)
            {
                editorControlsUserControl.ChangesInEditor = false;
                editorControlsUserControl.useSaveAsOnSave = false;
            }
        }

        private void editorControlsUserControl_UnformatXmlEvent()
        {
            string unformattedText;

            switch (ActiveState)
            {
                case States.XsdFile:
                    if (newFile)
                    {
                        unformattedText = GetNewXsd();
                    }
                    else
                    {
                        unformattedText = File.ReadAllText(appliedXsdFile);
                    }

                    break;
                case States.XsltFile:
                    if (newFile)
                    {
                        unformattedText = GetNewXslt();
                    }
                    else
                    {
                        unformattedText = File.ReadAllText(appliedXsltFile);
                    }

                    break;
                default:
                    if (newFile)
                    {
                        unformattedText = GetNewXml();
                    }
                    else
                    {
                        unformattedText = File.ReadAllText(originalXmlFile);
                    }

                    break;
            }
            
            if (editorControlsUserControl.ChangesInEditor)
            {
                SaveBeforeUnformating("Changes need to be saved before unformating.\r\nSave changes?", unformattedText);
            }
            else
            {
                editorControlsUserControl.SetText(unformattedText);
            }
        }

        private void editorControlsUserControl_FormatXmlEvent(string textToSave)
        {
            CommitFormatting();
        }

        private void CommitFormatting()
        {
            XmlDocument xml = new XmlDocument();
            xml.LoadXml(editorControlsUserControl.GetText());

            XmlDeclaration dec = null;

            if (xml.FirstChild.NodeType == XmlNodeType.XmlDeclaration)
            {
                dec = (XmlDeclaration)xml.FirstChild;
            }

            XmlWriterSettings writerSettings = new XmlWriterSettings();

            writerSettings.OmitXmlDeclaration = true;
            writerSettings.IndentChars = "\t";
            writerSettings.Indent = true;

            StringWriter stringWriter = new StringWriter();
            XmlWriter xmlWriter = XmlWriter.Create(stringWriter, writerSettings);

            if (dec != null)
            {
                stringWriter.WriteLine(xml.CreateXmlDeclaration(dec.Version, dec.Encoding, dec.Standalone).OuterXml);
            }

            xmlWriter.Flush();
            stringWriter.Flush();

            xml.Save(xmlWriter);

            editorControlsUserControl.SetText(stringWriter.ToString());

            xmlWriter.Close();
            stringWriter.Dispose();
        }

        private void editorControlsUserControl_SaveEvent(bool applyAfterSave)
        {
            switch (ActiveState)
            {
                case States.XsltFile:
                    SaveEditorContent(xsltFileComboBox.Text, applyAfterSave);
                    break;
                case States.XsdFile:
                    SaveEditorContent(xsdFileComboBox.Text, applyAfterSave);
                    break;
                case States.InputFile:
                    SaveEditorContent(originalXmlFile, applyAfterSave);
                    break;
            }
        }
        
        private void editorControlsUserControl_SaveAsEvent(string fileName, bool applyAfterSave)
        {
            switch (ActiveState)
            {
                case States.XsltFile:
                    xsltFileComboBox.Text = fileName;
                    SaveEditorContent(fileName, applyAfterSave);
                    appliedXsltFile = xsltFileComboBox.Text;
                    break;
                case States.XsdFile:
                    xsdFileComboBox.Text = fileName;
                    SaveEditorContent(fileName, applyAfterSave);
                    appliedXsdFile = xsdFileComboBox.Text;
                    break;
                case States.InputFile:
                    inputFileComboBox.Text = fileName;

                    if (originalXmlFile != inputFileComboBox.Text)
                    {
                        if (!String.IsNullOrEmpty(originalXmlFile))
                        {
                            DeleteFile(originalXmlFile);
                        }

                        originalXmlFile = inputFileComboBox.Text;
                        doNotDeleteFile = true;

                        if (newFile)
                        {
                            if (debugString == "")
                            {
                                debugString = editorControlsUserControl.GetText();
                            }
                        }
                    }

                    SaveEditorContent(fileName, applyAfterSave);
                    break;
            }
        }

        private void SaveEditorContent(string fileName, bool applyAfterSave)
        {
            switch (ActiveState)
            {
                case States.XsltFile:
                    WriteFile(fileName, editorControlsUserControl.GetText());

                    if (applyAfterSave)
                    {
                        ApplyXsltFile();

                        if (!errorInXslt && !errorInXml)
                        {
                            DisableEditorControl(true);
                        }

                        errorInXml = false;
                    }

                    break;
                case States.XsdFile:
                    WriteFile(fileName, editorControlsUserControl.GetText());

                    if (applyAfterSave)
                    {
                        ApplyXsdFile();
                        DisableEditorControl(true);
                    }

                    break;
                case States.InputFile:
                    WriteFile(fileName, editorControlsUserControl.GetText());

                    if (applyAfterSave)
                    {
                        Util.SaveComboBoxItemToRegistry(inputFileComboBox.Text, "XmlFile", numberOfXmlFilesToSave);
                        FillInputFileComboBox();

                        SetInputFileOptions();
                        DisableEditorControl(true);
                        Reload(inputFileComboBox.Text);
                    }

                    break;
            }
        }

        private static void WriteFile(string fileName, string content)
        {
            try
            {
                File.WriteAllText(fileName, content);
            }
            catch (Exception e)
            {
                Util.ShowMessage(Util.GetDetailedErrorMessage(e));
            }
        }

        private enum States
        {
            InputFile,
            XsltFile,
            XsdFile,
            XPath
        };

        private void SetXPathDefaultType()
        {
            string xPathQueryType = Util.ReadFromRegistry("XPathQueryType");

            if (xPathQueryType == "")
            {
                xPathTypeComboBox.SelectedIndex = xPathTypeComboBox.FindString("InnerXml");
            }
            else
            {
                xPathTypeComboBox.SelectedIndex = xPathTypeComboBox.FindString(xPathQueryType);
            }

            if (xPathTypeComboBox.SelectedItem.ToString() == "Value")
            {
                treeViewCheckBox.Enabled = false;
            }
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            if (editorControlsUserControl.ChangesInEditor)
            {
                DialogResult dr = MessageBox.Show("Discard changes?", Util.GetTitle(), MessageBoxButtons.YesNo, MessageBoxIcon.Information);

                if (dr.ToString() == "No")
                {
                    return;
                }
            }

            if (injectCheckBox.Checked)
            {
                inject = true;
            }

            if (CloseEvent != null)
            {
                CloseEvent();
            }

            if (!debugMode && !standAlone)
            {
                ClearSession();
            }
        }

        private void ClearSession()
        {
            Cleanup();
            inputFileComboBox.Text = "";
            SetActive(States.InputFile);
            SetNoInputFileOptions();
            originalXmlFile = "";
            previousXmlFile = "";
            webBrowser.Url = new Uri("about:blank");
        }

        private void AboutButton_Click(object sender, EventArgs e)
        {
            using (AboutForm af = new AboutForm())
            {
                string version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
                af.ProgramVersion = version.Substring(0, version.Length - 4);
                af.Title = Util.GetTitle();
                af.ShowDialog();
            }
        }

        private void ReloadWebBrowser(string url)
        {
            if (!string.IsNullOrEmpty(url))
            {
                Uri uri = new Uri(url);
                webBrowser.Url = uri;
            }
        }

        private void ApplyXsltFile()
        {
            string previousFile = inputFileComboBox.Text;

            DeleteAutoGeneratedFiles();

            try
            {
                string outputFile = string.Format(@"{0}{1}.html", Path.GetTempPath(), Guid.NewGuid());
                CommitXslt(outputFile);
                inputFileComboBox.Text = outputFile;
                previousFileFromXslt = outputFile;
                errorInXslt = false;
                appliedXsltFile = xsltFileComboBox.Text;

                Reload(inputFileComboBox.Text);
            }
            catch (System.Xml.Xsl.XsltException e)
            {
                Util.ShowMessage(Util.GetDetailedErrorMessage(e));
                inputFileComboBox.Text = previousFile;
                HandleXsltError();
                editorControlsUserControl.SetEditorStatusTextBox(Util.GetDetailedErrorMessage(e));
                editorControlsUserControl.GotoLine(e.LineNumber, e.LinePosition);
            }
            catch (XmlException e)
            {
                Util.ShowMessage(Util.GetDetailedErrorMessage(e));
                inputFileComboBox.Text = originalXmlFile;
                errorInXml = true;
                errorInXslt = false;
                SetActive(States.InputFile);
                HandleEditor();
                editorControlsUserControl.SetEditorStatusTextBox(Util.GetDetailedErrorMessage(e));
                editorControlsUserControl.GotoLine(e.LineNumber, e.LinePosition);
            }
        }

        private void HandleXsltError()
        {
            if (!errorInXslt)
            {
                StateBeforeXsltError = ActiveState;
            }

            if (newFile)
            {
                StateBeforeXsltError = StateBeforeNewFile;
            }

            if (ActiveState != States.XsltFile)
            {
                SetActive(States.XsltFile);
            }

            errorInXslt = true;

            if (!editorEnabled)
            {
                HandleEditor();
            }
        }

        private void DeleteAutoGeneratedFiles()
        {
            if (!string.IsNullOrEmpty(previousFileFromXPath))
            {
                DeleteFile(previousFileFromXPath);
                previousFileFromXPath = "";
            }

            if (!string.IsNullOrEmpty(previousFileFromXslt))
            {
                DeleteFile(previousFileFromXslt);
                previousFileFromXslt = "";
            }
        }

        private void ApplyXPathQuery()
        {
            string previousFile = inputFileComboBox.Text;

            DeleteAutoGeneratedFiles();

            try
            {
                string fileExtension = "xml";

                if (xPathTypeComboBox.SelectedItem.ToString() == "Value")
                {
                    fileExtension = "html";
                    treeViewCheckBox.Checked = false;
                    treeViewCheckBox.Enabled = false;
                }
                else
                {
                    treeViewCheckBox.Enabled = true;
                }

                inputFileComboBox.Text = string.Format(@"{0}{1}.{2}", Path.GetTempPath(), Guid.NewGuid(), fileExtension);
                previousFileFromXPath = inputFileComboBox.Text;
                CommitXPath();

                Reload(inputFileComboBox.Text);
            }
            catch (Exception e)
            {
                inputFileComboBox.Text = previousFile;
                Util.ShowMessage(Util.GetDetailedErrorMessage(e));
            }
        }

        private void Reload(string url)
        {
            if (treeviewEnabled)
            {
                if (!treeViewUserControl.ReloadTreeView(url))
                {
                    DisableTreeView(url);
                }
            }
            else
            {
                ReloadWebBrowser(url);
            }
        }

        private void CommitXslt(string outputFile)
        {
            XPathDocument xPathDoc = new XPathDocument(originalXmlFile);
            System.Xml.Xsl.XslCompiledTransform xslTrans = new System.Xml.Xsl.XslCompiledTransform();
            xslTrans.Load(xsltFileComboBox.Text);
            XmlTextWriter writer = new XmlTextWriter(outputFile, System.Text.Encoding.Default);
            xslTrans.Transform(xPathDoc, null, writer);
            writer.Close();

            if (previousXsltFile != xsltFileComboBox.Text)
            {
                previousXsltFile = xsltFileComboBox.Text;
                Util.SaveComboBoxItemToRegistry(xsltFileComboBox.Text, "XsltFile", numberOfXsltFilesToSave);
                FillXsltFileComboBox();
            }

            if (ActiveState != States.XsltFile)
            {
                SetActive(States.XsltFile);
            }
        }

        private void CommitXPath()
        {
            XPathDocument doc = new XPathDocument(originalXmlFile);
            XPathNavigator nav = doc.CreateNavigator();

            string result = "";

            if (nav.Evaluate(xPathComboBox.Text).GetType().ToString() == "MS.Internal.Xml.XPath.XPathSelectionIterator")
            {
                XPathNodeIterator iterator = (XPathNodeIterator)nav.Evaluate(xPathComboBox.Text);

                while (iterator.MoveNext())
                {
                    string resultType = "";

                    switch (xPathTypeComboBox.SelectedItem.ToString())
                    {
                        case "InnerXml":
                            resultType = iterator.Current.InnerXml;
                            break;
                        case "OuterXml":
                            resultType = iterator.Current.OuterXml;
                            break;
                        case "Value":
                            resultType = iterator.Current.Value;
                            break;
                    }

                    result += string.Format("{0}\r\n", resultType);
                }
            }
            else
            {
                result = string.Format("{0}\r\n", nav.Evaluate(xPathComboBox.Text));
            }

            if (xPathTypeComboBox.SelectedItem.ToString() == "Value")
            {
                result = string.Format("<pre>\r\n{0}</pre>", result);
            }
            else
            {
                result = string.Format("<xml>\r\n{0}</xml>", result);
            }

            WriteFile(inputFileComboBox.Text, result);

            if (previousXPathQuery != xPathComboBox.Text)
            {
                previousXPathQuery = xPathComboBox.Text;
                Util.SaveComboBoxItemToRegistry(xPathComboBox.Text, "XPath", numberOfXPathQueriesToSave);
                FillXPathComboBox();
            }

            if (ActiveState != States.XPath)
            {
                SetActive(States.XPath);
            }
        }

        private void SetActive(States state)
        {
            switch (state)
            {
                case States.InputFile:
                    ActiveState = States.InputFile;
                    inputFileComboBox.Enabled = true;
                    applyXmlButton.Text = "Apply";
                    inputFilePictureBox.Visible = true;
                    xsdFilePictureBox.Visible = false;
                    xsltFilePictureBox.Visible = false;
                    xPathPictureBox.Visible = false;
                    editButton.Enabled = true;
                    treeViewCheckBox.Enabled = true;
                    break;
                case States.XsltFile:
                    ActiveState = States.XsltFile;
                    inputFileComboBox.Enabled = false;
                    applyXmlButton.Text = "Revert Xml";
                    inputFilePictureBox.Visible = false;
                    xsdFilePictureBox.Visible = false;
                    xsltFilePictureBox.Visible = true;
                    xPathPictureBox.Visible = false;
                    editButton.Enabled = true;
                    treeViewCheckBox.Enabled = true;
                    break;
                case States.XsdFile:
                    ActiveState = States.XsdFile;
                    inputFileComboBox.Enabled = true;
                    applyXmlButton.Text = "Apply";
                    xsdFilePictureBox.Visible = true;
                    xsltFilePictureBox.Visible = false;
                    inputFilePictureBox.Visible = false;
                    xPathPictureBox.Visible = false;
                    editButton.Enabled = true;
                    treeViewCheckBox.Enabled = true;
                    break;
                case States.XPath:
                    ActiveState = States.XPath;
                    inputFileComboBox.Enabled = false;
                    applyXmlButton.Text = "Revert Xml";
                    inputFilePictureBox.Visible = false;
                    xsdFilePictureBox.Visible = false;
                    xsltFilePictureBox.Visible = false;
                    xPathPictureBox.Visible = true;
                    editButton.Enabled = false;

                    if (xPathTypeComboBox.SelectedItem.ToString() == "Value")
                    {
                        treeViewCheckBox.Enabled = false;
                    }
                    else
                    {
                        treeViewCheckBox.Enabled = true;
                    }
                    
                    break;
            }
        }

        private void FillXsltFileComboBox()
        {
            xsltFileComboBox.Items.Clear();

            for (int i = 1; i <= numberOfXsltFilesToSave; i++)
            {
                string item = Util.ReadFromRegistry("XsltFile" + i);

                if (item != "")
                {
                    xsltFileComboBox.Items.Add(item);
                }
            }

            xsltFileComboBox.Text = Util.ReadFromRegistry("XsltFile1");
            previousXsltFile = xsltFileComboBox.Text;
            appliedXsltFile = xsltFileComboBox.Text;
        }

        private void FillXsdFileComboBox()
        {
            xsdFileComboBox.Items.Clear();

            for (int i = 1; i <= numberOfXsdFilesToSave; i++)
            {
                string item = Util.ReadFromRegistry("XsdFile" + i);

                if (item != "")
                {
                    xsdFileComboBox.Items.Add(item);
                }
            }

            xsdFileComboBox.Text = Util.ReadFromRegistry("XsdFile1");
            appliedXsdFile = xsdFileComboBox.Text;
        }

        private void FillInputFileComboBox()
        {
            inputFileComboBox.Items.Clear();

            for (int i = 1; i <= numberOfXmlFilesToSave; i++)
            {
                string item = Util.ReadFromRegistry("XmlFile" + i);

                if (item != "")
                {
                    inputFileComboBox.Items.Add(item);
                }
            }
        }

        private void FillXPathComboBox()
        {
            xPathComboBox.Items.Clear();

            for (int i = 1; i <= numberOfXPathQueriesToSave; i++)
            {
                string item = Util.ReadFromRegistry("XPath" + i);
                
                if (item != "")
                {
                    xPathComboBox.Items.Add(item);
                }
            }

            xPathComboBox.Text = Util.ReadFromRegistry("XPath1");
            previousXPathQuery = xPathComboBox.Text;
        }

        private void ToClipboardButton_Click(object sender, EventArgs e)
        {
            string sr;

            if (editorEnabled)
            {
                sr = editorControlsUserControl.GetText();
            }
            else
            {
                sr = File.ReadAllText(GetActiveDocumentUrl());

                if (xPathTypeComboBox.SelectedItem.ToString() == "Value" && ActiveState == States.XPath)
                {
                    sr = sr.Substring(7, sr.Length - 15);
                }
            }

            StringBuilder sb = new StringBuilder(sr);

            for (int i = 0; i < sb.Length; i++)
            {
                if (sb[i].ToString() == "\n")
                {
                    if (sb[i - 1].ToString() != "\r")
                    {
                        sb[i - 1] = Convert.ToChar("\r");
                    }
                }
            }

            Clipboard.SetDataObject(sb.ToString(), true);
        }

        private void SetNoInputFileOptions()
        {
            openInBrowserButton.Enabled = false;
            toClipboardButton.Enabled = false;
            editButton.Enabled = false;
            applyXsdButton.Enabled = false;
            applyXpathButton.Enabled = false;
            applyXsltButton.Enabled = false;
            treeViewCheckBox.Enabled = false;
            xPathTypeComboBox.Enabled = false;
            xsltFileComboBox.Enabled = false;
            xsdFileComboBox.Enabled = false;
            xPathComboBox.Enabled = false;
            selectXsdFileButton.Enabled = false;
            selectXsltFileButton.Enabled = false;
            newXsltFileButton.Enabled = false;
            newXsdFileButton.Enabled = false;
        }

        private void SetInputFileOptions()
        {
            openInBrowserButton.Enabled = true;
            toClipboardButton.Enabled = true;
            editButton.Enabled = true;
            applyXsdButton.Enabled = true;
            applyXpathButton.Enabled = true;
            applyXsltButton.Enabled = true;
            treeViewCheckBox.Enabled = true;
            xPathTypeComboBox.Enabled = true;
            xsltFileComboBox.Enabled = true;
            xsdFileComboBox.Enabled = true;
            xPathComboBox.Enabled = true;
            selectXsdFileButton.Enabled = true;
            selectXsltFileButton.Enabled = true;
            newXsltFileButton.Enabled = true;
            newXsdFileButton.Enabled = true;
        }

        private void OpenInBrowserButton_Click(object sender, EventArgs e)
        {
            System.Diagnostics.ProcessStartInfo psi = new System.Diagnostics.ProcessStartInfo(Util.GetDefaultWebBrowser());
            psi.Arguments = GetActiveDocumentUrl();
            System.Diagnostics.Process.Start(psi);
        }

        private void EditButton_Click(object sender, EventArgs e)
        {
            HandleEditor();
        }

        private void HandleEditor()
        {
            string fileToEdit = "";

            switch (ActiveState)
            {
                case States.XsltFile:
                    if (errorInXslt)
                    {
                        fileToEdit = xsltFileComboBox.Text;
                    }
                    else
                    {
                        fileToEdit = appliedXsltFile;
                    }
                    
                    break;
                case States.XsdFile:
                    fileToEdit = appliedXsdFile;
                    break;
                case States.InputFile:
                    fileToEdit = originalXmlFile;
                    break;
            }

            editorControlsUserControl.SetText(File.ReadAllText(fileToEdit));

            EnableEditorControl();

            if (errorInXslt)
            {
                editorControlsUserControl.ValidateDocument("XSL");
            }
        }

        private void EnableEditorControl()
        {
            if (newFile)
            {
                editorControlsUserControl.useSaveAsOnSave = true;
            }
            else
            {
                editorControlsUserControl.useSaveAsOnSave = false;
            }

            toClipboardButton.Enabled = true;
            newXmlFileButton.Enabled = false;
            newXsdFileButton.Enabled = false;
            inputFileComboBox.Enabled = false;
            newXsltFileButton.Enabled = false;
            xsltFileComboBox.Enabled = false;
            xsdFileComboBox.Enabled = false;
            xPathComboBox.Enabled = false;
            xPathTypeComboBox.Enabled = false;
            applyXmlButton.Enabled = false;
            applyXpathButton.Enabled = false;
            applyXsltButton.Enabled = false;
            applyXsdButton.Enabled = false;
            openInBrowserButton.Enabled = false;
            editButton.Enabled = false;
            selectXmlFileButton.Enabled = false;
            selectXsdFileButton.Enabled = false;
            selectXsltFileButton.Enabled = false;
            treeViewCheckBox.Enabled = false;
            closeButton.Enabled = false;

            if (treeviewEnabled)
            {
                treeViewUserControl.Visible = false;
            }
            else
            {
                webBrowser.Visible = false;
            }

            editorControlsUserControl.originalXmlFile = originalXmlFile;
            editorControlsUserControl.appliedXsdFile = appliedXsdFile;
            editorControlsUserControl.activeState = ActiveState.ToString();
            editorControlsUserControl.ReadFormatXmlState();
            editorControlsUserControl.Visible = true;
            editorEnabled = true;
            editorControlsUserControl.EnableEditor();
        }

        private void DisableEditorControl(bool applyAfterSave)
        {
            States StateAfterClose = ActiveState;

            if (newFile)
            {
                if (!applyAfterSave)
                {
                    StateAfterClose = StateBeforeNewFile;
                }

                newFile = false;
            }

            if (errorInXslt)
            {
                if (StateBeforeXsltError == States.XsltFile)
                {
                    inputFileComboBox.Text = originalXmlFile;
                    Reload(inputFileComboBox.Text);

                    StateAfterClose = States.InputFile;
                }
                else
                {
                    StateAfterClose = StateBeforeXsltError;
                }

                errorInXslt = false;
            }

            if (debugString != "")
            {
                SetActive(StateAfterClose);
            }
            else
            {
                inputFileComboBox.Enabled = true;
            }

            xsltFileComboBox.Text = appliedXsltFile;
            xsdFileComboBox.Text = appliedXsdFile;

            switch (StateAfterClose)
            {
                case States.XsltFile:
                    inputFileComboBox.Text = previousFileFromXslt;
                    break;
                case States.XPath:
                    inputFileComboBox.Text = previousFileFromXPath;
                    break;
                case States.InputFile:
                case States.XsdFile:
                    inputFileComboBox.Text = originalXmlFile;
                    break;
            }

            newXmlFileButton.Enabled = true;
            selectXmlFileButton.Enabled = true;

            if (debugString != "")
            {
                newXsltFileButton.Enabled = true;
                selectXsltFileButton.Enabled = true;
                xsltFileComboBox.Enabled = true;
                selectXsdFileButton.Enabled = true;
                xsdFileComboBox.Enabled = true;
                xPathComboBox.Enabled = true;
                xPathTypeComboBox.Enabled = true;
                applyXpathButton.Enabled = true;
                applyXsltButton.Enabled = true;
                openInBrowserButton.Enabled = true;
                xsdFileComboBox.Enabled = true;
                applyXsdButton.Enabled = true;
                newXsdFileButton.Enabled = true;
                selectXsdFileButton.Enabled = true;
            }
            else
            {
                toClipboardButton.Enabled = false;
            }

            applyXmlButton.Enabled = true;
            closeButton.Enabled = true;

            if (treeviewEnabled)
            {
                treeViewUserControl.Visible = true;
            }
            else
            {
                webBrowser.Visible = true;
            }

            editorControlsUserControl.CloseStatusBox();
            editorControlsUserControl.Visible = false;
            editorEnabled = false;
        }

        private void XPathComboBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ApplyXPathQuery();
            }
        }

        private void XPathComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (mainFormLoaded)
            {
                ApplyXPathQuery();
            }
        }

        private void XPathTypeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (mainFormLoaded)
            {
                Util.SaveToRegistry("XPathQueryType", xPathTypeComboBox.SelectedItem.ToString());
                ApplyXPathQuery();
            }
        }

        private void XsltFileComboBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                CheckForValidXsltInput();
            }
        }

        private void NewXsltFileButton_Click(object sender, EventArgs e)
        {
            editorControlsUserControl.SetText(GetNewXslt());
            xsltFileComboBox.Text = "";
            StateBeforeNewFile = ActiveState;
            SetActive(States.XsltFile);
            newFile = true;
            EnableEditorControl();
        }

        private static string GetNewXsd()
        {
            return "<?xml version=\"1.0\"?>\r\n<xs:schema elementFormDefault=\"qualified\" xmlns:xs=\"http://www.w3.org/2001/XMLSchema\">\r\n\t\r\n</xs:schema>\r\n";
        }

        private static string GetNewXslt()
        {
            return "<?xml version=\"1.0\"?>\r\n<xsl:stylesheet version=\"1.0\" xmlns:xsl=\"http://www.w3.org/1999/XSL/Transform\">\r\n\t\r\n</xsl:stylesheet>\r\n";
        }

        private static string GetNewXml()
        {
            return "<?xml version=\"1.0\"?>\r\n<root>\r\n\t\r\n</root>\r\n";
        }

        private void EnableTreeView(string url)
        {
            if (treeViewUserControl.ReloadTreeView(url))
            {
                webBrowser.Visible = false;
                treeviewEnabled = true;
                treeViewUserControl.Visible = true;
            }
            else
            {
                DisableTreeView(inputFileComboBox.Text);
            }
        }

        private void DisableTreeView(string url)
        {
            ReloadWebBrowser(url);
            webBrowser.Visible = true;
            treeViewCheckBox.Checked = false;
            treeviewEnabled = false;
            treeViewUserControl.Visible = false;
        }

        private string GetActiveDocumentUrl()
        {
            string url;

            switch (ActiveState)
            {
                case States.XsdFile:
                    url = appliedXsdFile;
                    break;
                default:
                    url = originalXmlFile;
                    break;
            }

            return url;
        }

        private void TreeViewCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (treeViewCheckBox.Checked)
            {
                EnableTreeView(GetActiveDocumentUrl());
            }
            else
            {
                DisableTreeView(GetActiveDocumentUrl());
            }
        }

        private void XsltFileComboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            xsltFileComboBox.Text = xsltFileComboBox.SelectedItem.ToString();
            CheckForValidXsltInput();
        }

        private void openInBrowserButton_MouseEnter(object sender, EventArgs e)
        {
            SetToolStripText("Open in Internet Explorer");
        }

        private void openInBrowserButton_MouseLeave(object sender, EventArgs e)
        {
            SetToolStripText("");
        }

        private void aboutButton_MouseEnter(object sender, EventArgs e)
        {
            SetToolStripText(string.Format("About {0}", Util.GetTitle()));
        }

        private void aboutButton_MouseLeave(object sender, EventArgs e)
        {
            SetToolStripText("");
        }

        private void toClipboardButton_MouseEnter(object sender, EventArgs e)
        {
            SetToolStripText("Copy content to Clipboard");
        }

        private void toClipboardButton_MouseLeave(object sender, EventArgs e)
        {
            SetToolStripText("");
        }

        private void editButton_MouseEnter(object sender, EventArgs e)
        {
            switch (ActiveState)
            {
                case States.InputFile:
                    SetToolStripText("Edit Xml document");
                    break;
                case States.XsltFile:
                    SetToolStripText("Edit XSLT document");
                    break;
            }
        }

        private void editButton_MouseLeave(object sender, EventArgs e)
        {
            SetToolStripText("");
        }

        private void treeViewCheckBox_MouseEnter(object sender, EventArgs e)
        {
            switch (ActiveState)
            {
                case States.InputFile:
                    SetToolStripText("Show Xml in Tree view");
                    break;
                case States.XPath:
                    SetToolStripText("Show XPath result in Tree view");
                    break;
            }
        }

        private void treeViewCheckBox_MouseLeave(object sender, EventArgs e)
        {
            SetToolStripText("");
        }

        private void injectCheckBox_MouseEnter(object sender, EventArgs e)
        {
            SetToolStripText("Inject Xml back into running application on Close");
        }

        private void injectCheckBox_MouseLeave(object sender, EventArgs e)
        {
            SetToolStripText("");
        }

        private void closeButton_MouseEnter(object sender, EventArgs e)
        {
            SetToolStripText(string.Format("Close {0}", Util.GetTitle()));
        }

        private void closeButton_MouseLeave(object sender, EventArgs e)
        {
            SetToolStripText("");
        }

        private void xPathTypeComboBox_MouseEnter(object sender, EventArgs e)
        {
            SetToolStripText("Change representation of XPath result");
        }

        private void xPathTypeComboBox_MouseLeave(object sender, EventArgs e)
        {
            SetToolStripText("");
        }

        private void xPathComboBox_MouseEnter(object sender, EventArgs e)
        {
            SetToolStripText("Enter XPath query or function");
        }

        private void xPathComboBox_MouseLeave(object sender, EventArgs e)
        {
            SetToolStripText("");
        }

        private void selectXsltFileButton_MouseEnter(object sender, EventArgs e)
        {
            SetToolStripText("Open XSLT document");
        }

        private void selectXsltFileButton_MouseLeave(object sender, EventArgs e)
        {
            SetToolStripText("");
        }

        private void newXsltFileButton_MouseEnter(object sender, EventArgs e)
        {
            SetToolStripText("Create new XSLT document");
        }

        private void newXsltFileButton_MouseLeave(object sender, EventArgs e)
        {
            SetToolStripText("");
        }

        private void xsltFileComboBox_MouseEnter(object sender, EventArgs e)
        {
            SetToolStripText("Active XSLT document");
        }

        private void xsltFileComboBox_MouseLeave(object sender, EventArgs e)
        {
            SetToolStripText("");
        }

        private void inputFileComboBox_MouseEnter(object sender, EventArgs e)
        {
            SetToolStripText("Active Xml document");
        }

        private void inputFileComboBox_MouseLeave(object sender, EventArgs e)
        {
            SetToolStripText("");
        }

        private void applyXsltButton_MouseEnter(object sender, EventArgs e)
        {
            SetToolStripText("Apply XSL Transformation");
        }

        private void applyXsltButton_MouseLeave(object sender, EventArgs e)
        {
            SetToolStripText("");
        }

        private void applyXpathButton_MouseEnter(object sender, EventArgs e)
        {
            SetToolStripText("Execute XPath query or function");
        }

        private void applyXpathButton_MouseLeave(object sender, EventArgs e)
        {
            SetToolStripText("");
        }

        private void newXmlFileButton_Click(object sender, EventArgs e)
        {
            editorControlsUserControl.SetText(GetNewXml());
            inputFileComboBox.Text = "";
            StateBeforeNewFile = ActiveState;
            SetActive(States.InputFile);
            newFile = true;
            EnableEditorControl();
        }

        private void inputFileComboBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                CheckForValidXmlInput();
            }
        }

        private void ApplyXmlFile()
        {
            SetInputFileOptions();
            originalXmlFile = inputFileComboBox.Text;
            SetActive(States.InputFile);

            if (previousXmlFile != inputFileComboBox.Text)
            {
                previousXmlFile = inputFileComboBox.Text;
                Util.SaveComboBoxItemToRegistry(inputFileComboBox.Text, "XmlFile", numberOfXmlFilesToSave);
                FillInputFileComboBox();
                inputFileComboBox.Text = originalXmlFile;
            }

            Reload(inputFileComboBox.Text);
        }

        private static void DeleteFile(string file)
        {
            Util.DeleteFile(file);
        }

        private void inputFileComboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            inputFileComboBox.Text = inputFileComboBox.SelectedItem.ToString();
            CheckForValidXmlInput();
        }

        private void applyXmlButton_Click(object sender, EventArgs e)
        {
            CheckForValidXmlInput();
        }

        private void CheckForValidXmlInput()
        {
            string fileToDelete = inputFileComboBox.Text;

            if (ActiveState != States.InputFile)
            {
                inputFileComboBox.Text = originalXmlFile;
            }

            inputFileComboBox.Text = inputFileComboBox.Text.Trim();

            if (inputFileComboBox.Text == "")
            {
                Util.ShowMessage("No Xml file specified.");
                inputFileComboBox.Text = originalXmlFile;
            }
            else if (!File.Exists(inputFileComboBox.Text))
            {
                Util.ShowMessage(string.Format("Xml file '{0}' not found.", inputFileComboBox.Text));
                inputFileComboBox.Text = originalXmlFile;
            }
            else
            {
                if (debugString == "")
                {
                    debugString = File.ReadAllText(inputFileComboBox.Text);
                }

                if (inputFileComboBox.Text.Trim().ToLower() != originalXmlFile.ToLower())
                {
                    if (ActiveState == States.InputFile)
                    {
                        if (!doNotDeleteFile)
                        {
                            DeleteFile(originalXmlFile);
                            doNotDeleteFile = true;
                        }
                    }
                    else
                    {
                        DeleteFile(fileToDelete);
                    }
                }

                ApplyXmlFile();
            }
        }

        private void CheckForValidXsltInput()
        {
            xsltFileComboBox.Text = xsltFileComboBox.Text.Trim();

            if (xsltFileComboBox.Text == "")
            {
                Util.ShowMessage("No XSLT file specified.");
                xsltFileComboBox.Text = appliedXsltFile;
            }
            else if (!File.Exists(xsltFileComboBox.Text))
            {
                Util.ShowMessage(string.Format("XSLT file '{0}' not found.", xsltFileComboBox.Text));
                xsltFileComboBox.Text = appliedXsltFile;
            }
            else
            {
                ApplyXsltFile();
            }
        }

        private void applyXmlButton_MouseEnter(object sender, EventArgs e)
        {
            switch (ActiveState)
            {
                case States.InputFile:
                    SetToolStripText("Load Xml document");
                    break;
                default:
                    SetToolStripText("Revert back to original or last saved Xml document");
                    break;
            }
        }

        private void applyXmlButton_MouseLeave(object sender, EventArgs e)
        {
            SetToolStripText("");
        }

        private void applyXsltButton_Click(object sender, EventArgs e)
        {
            CheckForValidXsltInput();
        }

        private void applyXpathButton_Click(object sender, EventArgs e)
        {
            xPathComboBox.Text = xPathComboBox.Text.Trim();

            if (xPathComboBox.Text == "")
            {
                Util.ShowMessage("No XPath Query specified.");
            }
            else
            {
                ApplyXPathQuery();
            }
        }

        private void SelectXsltFileButton_Click(object sender, EventArgs e)
        {
            openXsltFileDialog.Title = Util.GetTitle();
            openXsltFileDialog.InitialDirectory = Util.ReadFromRegistry("LastXsltDir");
            openXsltFileDialog.ShowDialog();
        }

        private void SelectXmlFileButton_Click(object sender, EventArgs e)
        {
            openXmlFileDialog.Title = Util.GetTitle();
            openXmlFileDialog.InitialDirectory = Util.ReadFromRegistry("LastXmlDir");
            openXmlFileDialog.ShowDialog();
        }

        private void OpenXsltFileDialog_FileOk(object sender, CancelEventArgs e)
        {
            LoadXsltFile(openXsltFileDialog.FileName);
        }

        private void LoadXsltFile(string fileName)
        {
            xsltFileComboBox.Text = fileName;

            string dir = fileName.Substring(0, fileName.LastIndexOf("\\"));
            Util.SaveToRegistry("LastXsltDir", dir);

            CheckForValidXsltInput();
        }

        private void Cleanup()
        {
            if (!doNotDeleteFile)
            {
                if (File.Exists(originalXmlFile))
                {
                    File.Delete(originalXmlFile);
                }
            }

            DeleteAutoGeneratedFiles();
        }

        public void LoadXmlFromFile(string fileName)
        {
            ClearSession();
            doNotDeleteFile = true;
            debugString = File.ReadAllText(fileName);
            originalXmlFile = fileName;
            InitiateWebBrowser();
            LoadXmlFile(fileName);
        }

        private void OpenXmlFileDialog_FileOk(object sender, CancelEventArgs e)
        {
            LoadXmlFile(openXmlFileDialog.FileName);
        }
        
        private void LoadXmlFile(string fileName)
        {
            inputFileComboBox.Text = fileName;

            string dir = fileName.Substring(0, fileName.LastIndexOf("\\"));
            Util.SaveToRegistry("LastXmlDir", dir);

            CheckForValidXmlInput();
        }

        private void newXmlFileButton_MouseEnter(object sender, EventArgs e)
        {
            SetToolStripText("Create new Xml document");
        }

        private void newXmlFileButton_MouseLeave(object sender, EventArgs e)
        {
            SetToolStripText("");
        }

        private void selectXmlFileButton_MouseEnter(object sender, EventArgs e)
        {
            SetToolStripText("Open Xml document");
        }

        private void selectXmlFileButton_MouseLeave(object sender, EventArgs e)
        {
            SetToolStripText("");
        }

        private void applyXsdButton_Click(object sender, EventArgs e)
        {
            CheckForValidXsdInput();
        }

        private void newXsdFileButton_Click(object sender, EventArgs e)
        {
            editorControlsUserControl.SetText(GetNewXsd());
            xsdFileComboBox.Text = "";
            StateBeforeNewFile = ActiveState;
            SetActive(States.XsdFile);
            newFile = true;
            EnableEditorControl();
        }

        private void selectXsdFileButton_Click(object sender, EventArgs e)
        {
            openXsdFileDialog.Title = Util.GetTitle();
            openXsdFileDialog.InitialDirectory = Util.ReadFromRegistry("LastXsdDir");
            openXsdFileDialog.ShowDialog();
        }

        private void xsdFileComboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            xsdFileComboBox.Text = xsdFileComboBox.SelectedItem.ToString();
            CheckForValidXsdInput();
        }

        private void xsdFileComboBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                CheckForValidXsdInput();
            }
        }

        private void xsdFileComboBox_MouseEnter(object sender, EventArgs e)
        {
            SetToolStripText("Active XSD document");
        }

        private void xsdFileComboBox_MouseLeave(object sender, EventArgs e)
        {
            SetToolStripText("");
        }

        private void selectXsdFileButton_MouseEnter(object sender, EventArgs e)
        {
            SetToolStripText("Open XSD document");
        }

        private void selectXsdFileButton_MouseLeave(object sender, EventArgs e)
        {
            SetToolStripText("");
        }

        private void newXsdFileButton_MouseEnter(object sender, EventArgs e)
        {
            SetToolStripText("Create new XSD document");
        }

        private void newXsdFileButton_MouseLeave(object sender, EventArgs e)
        {
            SetToolStripText("");
        }

        private void applyXsdButton_MouseEnter(object sender, EventArgs e)
        {
            SetToolStripText("Validate active input file against the active XSD document");
        }

        private void applyXsdButton_MouseLeave(object sender, EventArgs e)
        {
            SetToolStripText("");
        }

        private void CheckForValidXsdInput()
        {
            xsdFileComboBox.Text = xsdFileComboBox.Text.Trim();

            if (xsdFileComboBox.Text == "")
            {
                Util.ShowMessage("No XSD file specified.");
                xsdFileComboBox.Text = appliedXsdFile;
            }
            else if (!File.Exists(xsdFileComboBox.Text))
            {
                Util.ShowMessage(string.Format("XSD file '{0}' not found.", xsdFileComboBox.Text));
                xsdFileComboBox.Text = appliedXsdFile;
            }
            else
            {
                appliedXsdFile = xsdFileComboBox.Text;
                inputFileComboBox.Text = originalXmlFile;
                ApplyXsdFile();
            }
        }

        private void ApplyXsdFile()
        {
            if (previousXsdFile != xsdFileComboBox.Text)
            {
                previousXsdFile = xsdFileComboBox.Text;
                Util.SaveComboBoxItemToRegistry(xsdFileComboBox.Text, "XsdFile", numberOfXsdFilesToSave);
                FillXsdFileComboBox();
            }

            if (ActiveState != States.XsdFile)
            {
                SetActive(States.XsdFile);
            }

            Reload(xsdFileComboBox.Text);
        }

        private void openXsdFileDialog_FileOk(object sender, CancelEventArgs e)
        {
            LoadXsdFile(openXsdFileDialog.FileName);
        }

        private void LoadXsdFile(string fileName)
        {
            xsdFileComboBox.Text = fileName;

            string dir = fileName.Substring(0, fileName.LastIndexOf("\\"));
            Util.SaveToRegistry("LastXsdDir", dir);

            CheckForValidXsdInput();
        }

        private void inputFileComboBox_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            LoadXmlFile(files[0]);
        }

        private void inputFileComboBox_DragEnter(object sender, DragEventArgs e)
        {
            AllowFileDragEnter(e);
        }

        private void xsltFileComboBox_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            LoadXsltFile(files[0]);
        }

        private void xsltFileComboBox_DragEnter(object sender, DragEventArgs e)
        {
            AllowFileDragEnter(e);
        }

        private static void AllowFileDragEnter(DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop, false))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);

                if (files.Length == 1)
                {
                    e.Effect = DragDropEffects.All;
                }
            }
        }

        private void xsdFileComboBox_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            LoadXsdFile(files[0]);
        }

        private void xsdFileComboBox_DragEnter(object sender, DragEventArgs e)
        {
            AllowFileDragEnter(e);
        }

        private void injectCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            Util.SaveToRegistry("Inject", injectCheckBox.Checked.ToString());
        }

        private void ReadInjectState()
        {
            string readInjectXml = Util.ReadFromRegistry("Inject");
            bool injectXml = false;

            if (readInjectXml == "True")
            {
                injectXml = true;
            }

            injectCheckBox.Checked = injectXml;
        }
    }
}
