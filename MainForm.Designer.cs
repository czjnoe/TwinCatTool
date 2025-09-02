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
            this.connectionPanel = new System.Windows.Forms.Panel();
            this.lblAmsNetId = new System.Windows.Forms.Label();
            this.txtAmsNetId = new System.Windows.Forms.TextBox();
            this.lblPort = new System.Windows.Forms.Label();
            this.txtPort = new System.Windows.Forms.TextBox();
            this.btnConnect = new System.Windows.Forms.Button();
            this.btnDisconnect = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.chkAutoRefresh = new System.Windows.Forms.CheckBox();
            this.btnTest = new System.Windows.Forms.Button();
            this.lblStatus = new System.Windows.Forms.Label();
            this.searchPanel = new System.Windows.Forms.Panel();
            this.lblSearch = new System.Windows.Forms.Label();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.listPanel = new System.Windows.Forms.Panel();
            this.listViewVariables = new System.Windows.Forms.ListView();
            this.connectionPanel.SuspendLayout();
            this.searchPanel.SuspendLayout();
            this.listPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // connectionPanel
            // 
            this.connectionPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.connectionPanel.Controls.Add(this.lblAmsNetId);
            this.connectionPanel.Controls.Add(this.txtAmsNetId);
            this.connectionPanel.Controls.Add(this.lblPort);
            this.connectionPanel.Controls.Add(this.txtPort);
            this.connectionPanel.Controls.Add(this.btnConnect);
            this.connectionPanel.Controls.Add(this.btnDisconnect);
            this.connectionPanel.Controls.Add(this.btnRefresh);
            this.connectionPanel.Controls.Add(this.chkAutoRefresh);
            this.connectionPanel.Controls.Add(this.btnTest);
            this.connectionPanel.Controls.Add(this.lblStatus);
            this.connectionPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.connectionPanel.Location = new System.Drawing.Point(0, 0);
            this.connectionPanel.Name = "connectionPanel";
            this.connectionPanel.Size = new System.Drawing.Size(1000, 120);
            this.connectionPanel.TabIndex = 0;
            // 
            // lblAmsNetId
            // 
            this.lblAmsNetId.AutoSize = true;
            this.lblAmsNetId.Location = new System.Drawing.Point(10, 15);
            this.lblAmsNetId.Name = "lblAmsNetId";
            this.lblAmsNetId.Size = new System.Drawing.Size(80, 20);
            this.lblAmsNetId.TabIndex = 0;
            this.lblAmsNetId.Text = "AMS Net ID:";
            // 
            // txtAmsNetId
            // 
            this.txtAmsNetId.Location = new System.Drawing.Point(100, 12);
            this.txtAmsNetId.Name = "txtAmsNetId";
            this.txtAmsNetId.Size = new System.Drawing.Size(150, 27);
            this.txtAmsNetId.TabIndex = 1;
            this.txtAmsNetId.Text = "192.168.1.10.1.1";
            // 
            // lblPort
            // 
            this.lblPort.AutoSize = true;
            this.lblPort.Location = new System.Drawing.Point(270, 15);
            this.lblPort.Name = "lblPort";
            this.lblPort.Size = new System.Drawing.Size(40, 20);
            this.lblPort.TabIndex = 2;
            this.lblPort.Text = "端口:";
            // 
            // txtPort
            // 
            this.txtPort.Location = new System.Drawing.Point(320, 12);
            this.txtPort.Name = "txtPort";
            this.txtPort.Size = new System.Drawing.Size(60, 27);
            this.txtPort.TabIndex = 3;
            this.txtPort.Text = "851";
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(400, 10);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(80, 30);
            this.btnConnect.TabIndex = 4;
            this.btnConnect.Text = "连接";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.BtnConnect_Click);
            // 
            // btnDisconnect
            // 
            this.btnDisconnect.Enabled = false;
            this.btnDisconnect.Location = new System.Drawing.Point(490, 10);
            this.btnDisconnect.Name = "btnDisconnect";
            this.btnDisconnect.Size = new System.Drawing.Size(80, 30);
            this.btnDisconnect.TabIndex = 5;
            this.btnDisconnect.Text = "断开";
            this.btnDisconnect.UseVisualStyleBackColor = true;
            this.btnDisconnect.Click += new System.EventHandler(this.BtnDisconnect_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Enabled = false;
            this.btnRefresh.Location = new System.Drawing.Point(580, 10);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(80, 30);
            this.btnRefresh.TabIndex = 6;
            this.btnRefresh.Text = "刷新变量";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.BtnRefresh_Click);
            // 
            // chkAutoRefresh
            // 
            this.chkAutoRefresh.AutoSize = true;
            this.chkAutoRefresh.Enabled = false;
            this.chkAutoRefresh.Location = new System.Drawing.Point(680, 15);
            this.chkAutoRefresh.Name = "chkAutoRefresh";
            this.chkAutoRefresh.Size = new System.Drawing.Size(80, 24);
            this.chkAutoRefresh.TabIndex = 7;
            this.chkAutoRefresh.Text = "自动刷新";
            this.chkAutoRefresh.UseVisualStyleBackColor = true;
            this.chkAutoRefresh.CheckedChanged += new System.EventHandler(this.ChkAutoRefresh_CheckedChanged);
            // 
            // btnTest
            // 
            this.btnTest.Location = new System.Drawing.Point(780, 10);
            this.btnTest.Name = "btnTest";
            this.btnTest.Size = new System.Drawing.Size(80, 30);
            this.btnTest.TabIndex = 8;
            this.btnTest.Text = "测试连接";
            this.btnTest.UseVisualStyleBackColor = true;
            this.btnTest.Click += new System.EventHandler(this.BtnTest_Click);
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.ForeColor = System.Drawing.Color.Red;
            this.lblStatus.Location = new System.Drawing.Point(10, 50);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(69, 20);
            this.lblStatus.TabIndex = 9;
            this.lblStatus.Text = "未连接";
            // 
            // searchPanel
            // 
            this.searchPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.searchPanel.Controls.Add(this.lblSearch);
            this.searchPanel.Controls.Add(this.txtSearch);
            this.searchPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.searchPanel.Location = new System.Drawing.Point(0, 120);
            this.searchPanel.Name = "searchPanel";
            this.searchPanel.Size = new System.Drawing.Size(1000, 60);
            this.searchPanel.TabIndex = 1;
            // 
            // lblSearch
            // 
            this.lblSearch.AutoSize = true;
            this.lblSearch.Location = new System.Drawing.Point(10, 20);
            this.lblSearch.Name = "lblSearch";
            this.lblSearch.Size = new System.Drawing.Size(70, 20);
            this.lblSearch.TabIndex = 0;
            this.lblSearch.Text = "搜索变量:";
            // 
            // txtSearch
            // 
            this.txtSearch.Location = new System.Drawing.Point(90, 17);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.PlaceholderText = "输入变量名进行模糊搜索...";
            this.txtSearch.Size = new System.Drawing.Size(300, 27);
            this.txtSearch.TabIndex = 1;
            this.txtSearch.TextChanged += new System.EventHandler(this.TxtSearch_TextChanged);
            // 
            // listPanel
            // 
            this.listPanel.Controls.Add(this.listViewVariables);
            this.listPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listPanel.Location = new System.Drawing.Point(0, 180);
            this.listPanel.Name = "listPanel";
            this.listPanel.Size = new System.Drawing.Size(1000, 520);
            this.listPanel.TabIndex = 2;
            // 
            // listViewVariables
            // 
            this.listViewVariables.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listViewVariables.FullRowSelect = true;
            this.listViewVariables.GridLines = true;
            this.listViewVariables.Location = new System.Drawing.Point(0, 0);
            this.listViewVariables.Name = "listViewVariables";
            this.listViewVariables.Size = new System.Drawing.Size(1000, 520);
            this.listViewVariables.TabIndex = 0;
            this.listViewVariables.UseCompatibleStateImageBehavior = false;
            this.listViewVariables.View = System.Windows.Forms.View.Details;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1000, 700);
            this.Controls.Add(this.listPanel);
            this.Controls.Add(this.searchPanel);
            this.Controls.Add(this.connectionPanel);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Beckhoff TwinCAT 变量查看器";
            this.connectionPanel.ResumeLayout(false);
            this.connectionPanel.PerformLayout();
            this.searchPanel.ResumeLayout(false);
            this.searchPanel.PerformLayout();
            this.listPanel.ResumeLayout(false);
            this.ResumeLayout(false);
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
        private System.Windows.Forms.Button btnTest;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Panel searchPanel;
        private System.Windows.Forms.Label lblSearch;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Panel listPanel;
        private System.Windows.Forms.ListView listViewVariables;
    }
}
