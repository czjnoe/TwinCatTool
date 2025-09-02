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
            lblAmsNetId = new Label();
            txtAmsNetId = new TextBox();
            lblPort = new Label();
            txtPort = new TextBox();
            btnConnect = new Button();
            btnDisconnect = new Button();
            btnRefresh = new Button();
            chkAutoRefresh = new CheckBox();
            lblStatus = new Label();
            searchPanel = new Panel();
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
            connectionPanel.Controls.Add(lblAmsNetId);
            connectionPanel.Controls.Add(txtAmsNetId);
            connectionPanel.Controls.Add(lblPort);
            connectionPanel.Controls.Add(txtPort);
            connectionPanel.Controls.Add(btnConnect);
            connectionPanel.Controls.Add(btnDisconnect);
            connectionPanel.Controls.Add(btnRefresh);
            connectionPanel.Controls.Add(chkAutoRefresh);
            connectionPanel.Controls.Add(lblStatus);
            connectionPanel.Dock = DockStyle.Top;
            connectionPanel.Location = new Point(0, 0);
            connectionPanel.Name = "connectionPanel";
            connectionPanel.Size = new Size(875, 102);
            connectionPanel.TabIndex = 0;
            // 
            // lblAmsNetId
            // 
            lblAmsNetId.AutoSize = true;
            lblAmsNetId.Location = new Point(9, 13);
            lblAmsNetId.Name = "lblAmsNetId";
            lblAmsNetId.Size = new Size(80, 17);
            lblAmsNetId.TabIndex = 0;
            lblAmsNetId.Text = "AMS Net ID:";
            // 
            // txtAmsNetId
            // 
            txtAmsNetId.Location = new Point(88, 10);
            txtAmsNetId.Name = "txtAmsNetId";
            txtAmsNetId.Size = new Size(132, 23);
            txtAmsNetId.TabIndex = 1;
            txtAmsNetId.Text = "192.168.1.10.1.1";
            // 
            // lblPort
            // 
            lblPort.AutoSize = true;
            lblPort.Location = new Point(236, 13);
            lblPort.Name = "lblPort";
            lblPort.Size = new Size(35, 17);
            lblPort.TabIndex = 2;
            lblPort.Text = "端口:";
            // 
            // txtPort
            // 
            txtPort.Location = new Point(280, 10);
            txtPort.Name = "txtPort";
            txtPort.Size = new Size(53, 23);
            txtPort.TabIndex = 3;
            txtPort.Text = "851";
            // 
            // btnConnect
            // 
            btnConnect.Location = new Point(350, 8);
            btnConnect.Name = "btnConnect";
            btnConnect.Size = new Size(70, 26);
            btnConnect.TabIndex = 4;
            btnConnect.Text = "连接";
            btnConnect.UseVisualStyleBackColor = true;
            btnConnect.Click += BtnConnect_Click;
            // 
            // btnDisconnect
            // 
            btnDisconnect.Enabled = false;
            btnDisconnect.Location = new Point(429, 8);
            btnDisconnect.Name = "btnDisconnect";
            btnDisconnect.Size = new Size(70, 26);
            btnDisconnect.TabIndex = 5;
            btnDisconnect.Text = "断开";
            btnDisconnect.UseVisualStyleBackColor = true;
            btnDisconnect.Click += BtnDisconnect_Click;
            // 
            // btnRefresh
            // 
            btnRefresh.Enabled = false;
            btnRefresh.Location = new Point(508, 8);
            btnRefresh.Name = "btnRefresh";
            btnRefresh.Size = new Size(70, 26);
            btnRefresh.TabIndex = 6;
            btnRefresh.Text = "刷新变量";
            btnRefresh.UseVisualStyleBackColor = true;
            btnRefresh.Click += BtnRefresh_Click;
            // 
            // chkAutoRefresh
            // 
            chkAutoRefresh.AutoSize = true;
            chkAutoRefresh.Enabled = false;
            chkAutoRefresh.Location = new Point(595, 13);
            chkAutoRefresh.Name = "chkAutoRefresh";
            chkAutoRefresh.Size = new Size(75, 21);
            chkAutoRefresh.TabIndex = 7;
            chkAutoRefresh.Text = "自动刷新";
            chkAutoRefresh.UseVisualStyleBackColor = true;
            chkAutoRefresh.CheckedChanged += ChkAutoRefresh_CheckedChanged;
            // 
            // lblStatus
            // 
            lblStatus.AutoSize = true;
            lblStatus.ForeColor = Color.Red;
            lblStatus.Location = new Point(9, 42);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(44, 17);
            lblStatus.TabIndex = 9;
            lblStatus.Text = "未连接";
            // 
            // searchPanel
            // 
            searchPanel.BorderStyle = BorderStyle.FixedSingle;
            searchPanel.Controls.Add(lblSearch);
            searchPanel.Controls.Add(txtSearch);
            searchPanel.Dock = DockStyle.Top;
            searchPanel.Location = new Point(0, 102);
            searchPanel.Name = "searchPanel";
            searchPanel.Size = new Size(875, 51);
            searchPanel.TabIndex = 1;
            // 
            // lblSearch
            // 
            lblSearch.AutoSize = true;
            lblSearch.Location = new Point(9, 17);
            lblSearch.Name = "lblSearch";
            lblSearch.Size = new Size(59, 17);
            lblSearch.TabIndex = 0;
            lblSearch.Text = "搜索变量:";
            // 
            // txtSearch
            // 
            txtSearch.Location = new Point(79, 14);
            txtSearch.Name = "txtSearch";
            txtSearch.PlaceholderText = "输入变量名进行模糊搜索...";
            txtSearch.Size = new Size(263, 23);
            txtSearch.TabIndex = 1;
            txtSearch.TextChanged += TxtSearch_TextChanged;
            // 
            // listPanel
            // 
            listPanel.Controls.Add(listViewVariables);
            listPanel.Dock = DockStyle.Fill;
            listPanel.Location = new Point(0, 153);
            listPanel.Name = "listPanel";
            listPanel.Size = new Size(875, 442);
            listPanel.TabIndex = 2;
            // 
            // listViewVariables
            // 
            listViewVariables.Dock = DockStyle.Fill;
            listViewVariables.FullRowSelect = true;
            listViewVariables.GridLines = true;
            listViewVariables.Location = new Point(0, 0);
            listViewVariables.Name = "listViewVariables";
            listViewVariables.Size = new Size(875, 442);
            listViewVariables.TabIndex = 0;
            listViewVariables.UseCompatibleStateImageBehavior = false;
            listViewVariables.View = View.Details;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(875, 595);
            Controls.Add(listPanel);
            Controls.Add(searchPanel);
            Controls.Add(connectionPanel);
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
        private System.Windows.Forms.CheckBox chkAutoRefresh;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Panel searchPanel;
        private System.Windows.Forms.Label lblSearch;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Panel listPanel;
        private System.Windows.Forms.ListView listViewVariables;
    }
}
