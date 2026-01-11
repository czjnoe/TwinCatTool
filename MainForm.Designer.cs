#nullable disable
using System;
using System.Drawing;
using System.Windows.Forms;

namespace TwinCatTool
{
    partial class MainForm
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
            connectionPanel = new Panel();
            btnExportTemp = new Button();
            btnCheckVariable = new Button();
            lblAmsNetId = new Label();
            txtAmsNetId = new TextBox();
            lblPort = new Label();
            txtPort = new TextBox();
            btnConnect = new Button();
            btnDisconnect = new Button();
            btnRefresh = new Button();
            lblStatus = new Label();
            searchPanel = new Panel();
            btnVariableRead = new Button();
            label3 = new Label();
            label2 = new Label();
            txtVariableName = new TextBox();
            label1 = new Label();
            txtVariableValue = new TextBox();
            btnVariableWrite = new Button();
            lblSearch = new Label();
            txtSearch = new TextBox();
            listPanel = new Panel();
            listViewVariables = new ListView();
            connectionPanel.SuspendLayout();
            searchPanel.SuspendLayout();
            listPanel.SuspendLayout();
            SuspendLayout();
            // 
            // connectionPanel
            // 
            connectionPanel.BorderStyle = BorderStyle.FixedSingle;
            connectionPanel.Controls.Add(btnExportTemp);
            connectionPanel.Controls.Add(btnCheckVariable);
            connectionPanel.Controls.Add(lblAmsNetId);
            connectionPanel.Controls.Add(txtAmsNetId);
            connectionPanel.Controls.Add(lblPort);
            connectionPanel.Controls.Add(txtPort);
            connectionPanel.Controls.Add(btnConnect);
            connectionPanel.Controls.Add(btnDisconnect);
            connectionPanel.Controls.Add(btnRefresh);
            connectionPanel.Controls.Add(lblStatus);
            connectionPanel.Dock = DockStyle.Top;
            connectionPanel.Location = new Point(0, 0);
            connectionPanel.Margin = new Padding(4, 4, 4, 4);
            connectionPanel.Name = "connectionPanel";
            connectionPanel.Size = new Size(1545, 96);
            connectionPanel.TabIndex = 0;
            // 
            // btnExportTemp
            // 
            btnExportTemp.Location = new Point(945, 8);
            btnExportTemp.Margin = new Padding(4, 4, 4, 4);
            btnExportTemp.Name = "btnExportTemp";
            btnExportTemp.Size = new Size(145, 31);
            btnExportTemp.TabIndex = 11;
            btnExportTemp.Text = "检查文件Export";
            btnExportTemp.UseVisualStyleBackColor = true;
            btnExportTemp.Click += btnExportTemp_Click;
            // 
            // btnCheckVariable
            // 
            btnCheckVariable.Location = new Point(801, 8);
            btnCheckVariable.Margin = new Padding(4, 4, 4, 4);
            btnCheckVariable.Name = "btnCheckVariable";
            btnCheckVariable.Size = new Size(136, 31);
            btnCheckVariable.TabIndex = 10;
            btnCheckVariable.Text = "检查变量Import";
            btnCheckVariable.UseVisualStyleBackColor = true;
            btnCheckVariable.Click += btnCheckVariable_Click;
            // 
            // lblAmsNetId
            // 
            lblAmsNetId.AutoSize = true;
            lblAmsNetId.Location = new Point(12, 15);
            lblAmsNetId.Margin = new Padding(4, 0, 4, 0);
            lblAmsNetId.Name = "lblAmsNetId";
            lblAmsNetId.Size = new Size(98, 20);
            lblAmsNetId.TabIndex = 0;
            lblAmsNetId.Text = "AMS Net ID:";
            // 
            // txtAmsNetId
            // 
            txtAmsNetId.Location = new Point(113, 12);
            txtAmsNetId.Margin = new Padding(4, 4, 4, 4);
            txtAmsNetId.Name = "txtAmsNetId";
            txtAmsNetId.Size = new Size(169, 27);
            txtAmsNetId.TabIndex = 1;
            txtAmsNetId.Text = "192.168.1.10.1.1";
            txtAmsNetId.MouseLeave += txtAmsNetId_MouseLeave;
            // 
            // lblPort
            // 
            lblPort.AutoSize = true;
            lblPort.Location = new Point(303, 15);
            lblPort.Margin = new Padding(4, 0, 4, 0);
            lblPort.Name = "lblPort";
            lblPort.Size = new Size(43, 20);
            lblPort.TabIndex = 2;
            lblPort.Text = "端口:";
            // 
            // txtPort
            // 
            txtPort.Location = new Point(360, 12);
            txtPort.Margin = new Padding(4, 4, 4, 4);
            txtPort.Name = "txtPort";
            txtPort.Size = new Size(67, 27);
            txtPort.TabIndex = 3;
            txtPort.Text = "851";
            txtPort.MouseLeave += txtPort_MouseLeave;
            // 
            // btnConnect
            // 
            btnConnect.Location = new Point(450, 9);
            btnConnect.Margin = new Padding(4, 4, 4, 4);
            btnConnect.Name = "btnConnect";
            btnConnect.Size = new Size(90, 31);
            btnConnect.TabIndex = 4;
            btnConnect.Text = "连接";
            btnConnect.UseVisualStyleBackColor = true;
            btnConnect.Click += BtnConnect_Click;
            // 
            // btnDisconnect
            // 
            btnDisconnect.Enabled = false;
            btnDisconnect.Location = new Point(552, 9);
            btnDisconnect.Margin = new Padding(4, 4, 4, 4);
            btnDisconnect.Name = "btnDisconnect";
            btnDisconnect.Size = new Size(90, 31);
            btnDisconnect.TabIndex = 5;
            btnDisconnect.Text = "断开";
            btnDisconnect.UseVisualStyleBackColor = true;
            btnDisconnect.Click += BtnDisconnect_Click;
            // 
            // btnRefresh
            // 
            btnRefresh.Enabled = false;
            btnRefresh.Location = new Point(653, 9);
            btnRefresh.Margin = new Padding(4, 4, 4, 4);
            btnRefresh.Name = "btnRefresh";
            btnRefresh.Size = new Size(140, 31);
            btnRefresh.TabIndex = 6;
            btnRefresh.Text = "刷新全部变量";
            btnRefresh.UseVisualStyleBackColor = true;
            btnRefresh.Click += BtnRefresh_Click;
            // 
            // lblStatus
            // 
            lblStatus.AutoSize = true;
            lblStatus.ForeColor = Color.Red;
            lblStatus.Location = new Point(12, 49);
            lblStatus.Margin = new Padding(4, 0, 4, 0);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(54, 20);
            lblStatus.TabIndex = 9;
            lblStatus.Text = "未连接";
            // 
            // searchPanel
            // 
            searchPanel.BorderStyle = BorderStyle.FixedSingle;
            searchPanel.Controls.Add(btnVariableRead);
            searchPanel.Controls.Add(label3);
            searchPanel.Controls.Add(label2);
            searchPanel.Controls.Add(txtVariableName);
            searchPanel.Controls.Add(label1);
            searchPanel.Controls.Add(txtVariableValue);
            searchPanel.Controls.Add(btnVariableWrite);
            searchPanel.Controls.Add(lblSearch);
            searchPanel.Controls.Add(txtSearch);
            searchPanel.Dock = DockStyle.Top;
            searchPanel.Location = new Point(0, 96);
            searchPanel.Margin = new Padding(4, 4, 4, 4);
            searchPanel.Name = "searchPanel";
            searchPanel.Size = new Size(1545, 60);
            searchPanel.TabIndex = 1;
            // 
            // btnVariableRead
            // 
            btnVariableRead.Location = new Point(1395, 16);
            btnVariableRead.Margin = new Padding(4, 4, 4, 4);
            btnVariableRead.Name = "btnVariableRead";
            btnVariableRead.Size = new Size(96, 27);
            btnVariableRead.TabIndex = 8;
            btnVariableRead.Text = "Read";
            btnVariableRead.UseVisualStyleBackColor = true;
            btnVariableRead.Click += btnVariableRead_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(496, 20);
            label3.Margin = new Padding(4, 0, 4, 0);
            label3.Name = "label3";
            label3.Size = new Size(113, 20);
            label3.TabIndex = 7;
            label3.Text = "Current Select";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(618, 20);
            label2.Margin = new Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.Size = new Size(67, 20);
            label2.TabIndex = 6;
            label2.Text = "Name：";
            // 
            // txtVariableName
            // 
            txtVariableName.Location = new Point(693, 16);
            txtVariableName.Margin = new Padding(4, 4, 4, 4);
            txtVariableName.Name = "txtVariableName";
            txtVariableName.ReadOnly = true;
            txtVariableName.Size = new Size(223, 27);
            txtVariableName.TabIndex = 5;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(945, 20);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(64, 20);
            label1.TabIndex = 4;
            label1.Text = "Value：";
            // 
            // txtVariableValue
            // 
            txtVariableValue.Location = new Point(1020, 16);
            txtVariableValue.Margin = new Padding(4, 4, 4, 4);
            txtVariableValue.Name = "txtVariableValue";
            txtVariableValue.Size = new Size(223, 27);
            txtVariableValue.TabIndex = 3;
            // 
            // btnVariableWrite
            // 
            btnVariableWrite.Location = new Point(1274, 16);
            btnVariableWrite.Margin = new Padding(4, 4, 4, 4);
            btnVariableWrite.Name = "btnVariableWrite";
            btnVariableWrite.Size = new Size(96, 27);
            btnVariableWrite.TabIndex = 2;
            btnVariableWrite.Text = "Write";
            btnVariableWrite.UseVisualStyleBackColor = true;
            btnVariableWrite.Click += btnVariableWrite_Click;
            // 
            // lblSearch
            // 
            lblSearch.AutoSize = true;
            lblSearch.Location = new Point(12, 20);
            lblSearch.Margin = new Padding(4, 0, 4, 0);
            lblSearch.Name = "lblSearch";
            lblSearch.Size = new Size(73, 20);
            lblSearch.TabIndex = 0;
            lblSearch.Text = "搜索变量:";
            // 
            // txtSearch
            // 
            txtSearch.Location = new Point(102, 16);
            txtSearch.Margin = new Padding(4, 4, 4, 4);
            txtSearch.Name = "txtSearch";
            txtSearch.PlaceholderText = "输入变量名进行模糊搜索...";
            txtSearch.Size = new Size(337, 27);
            txtSearch.TabIndex = 1;
            txtSearch.TextChanged += TxtSearch_TextChanged;
            // 
            // listPanel
            // 
            listPanel.Controls.Add(listViewVariables);
            listPanel.Dock = DockStyle.Fill;
            listPanel.Location = new Point(0, 156);
            listPanel.Margin = new Padding(4, 4, 4, 4);
            listPanel.Name = "listPanel";
            listPanel.Size = new Size(1545, 544);
            listPanel.TabIndex = 2;
            // 
            // listViewVariables
            // 
            listViewVariables.Dock = DockStyle.Fill;
            listViewVariables.FullRowSelect = true;
            listViewVariables.GridLines = true;
            listViewVariables.Location = new Point(0, 0);
            listViewVariables.Margin = new Padding(4, 4, 4, 4);
            listViewVariables.Name = "listViewVariables";
            listViewVariables.Size = new Size(1545, 544);
            listViewVariables.TabIndex = 0;
            listViewVariables.UseCompatibleStateImageBehavior = false;
            listViewVariables.View = View.Details;
            listViewVariables.SelectedIndexChanged += listViewVariables_SelectedIndexChanged;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(9F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1545, 700);
            Controls.Add(listPanel);
            Controls.Add(searchPanel);
            Controls.Add(connectionPanel);
            Margin = new Padding(4, 4, 4, 4);
            Name = "MainForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Beckhoff TwinCAT 变量查看器";
            connectionPanel.ResumeLayout(false);
            connectionPanel.PerformLayout();
            searchPanel.ResumeLayout(false);
            searchPanel.PerformLayout();
            listPanel.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel connectionPanel;
        private System.Windows.Forms.Label lblAmsNetId;
        private System.Windows.Forms.TextBox txtAmsNetId;
        private System.Windows.Forms.Label lblPort;
        private System.Windows.Forms.TextBox txtPort;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.Button btnDisconnect;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Panel searchPanel;
        private System.Windows.Forms.Label lblSearch;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Panel listPanel;
        private System.Windows.Forms.ListView listViewVariables;
        private Button btnCheckVariable;
        private TextBox txtVariableValue;
        private Button btnVariableWrite;
        private Label label1;
        private Label label2;
        private TextBox txtVariableName;
        private Label label3;
        private Button btnVariableRead;
        private Button btnExportTemp;
    }
}
