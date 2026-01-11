#nullable disable
using System;
using System.Drawing;
using System.Windows.Forms;

namespace TwinCatTool
{
    partial class AboutForm
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
            panelMain = new Panel();
            lblSystem = new Label();
            lblReleaseDate = new Label();
            lblFramework = new Label();
            lblPlatform = new Label();
            lblVersion = new Label();
            lblSystemValue = new Label();
            lblReleaseDateValue = new Label();
            lblFrameworkValue = new Label();
            lblPlatformValue = new Label();
            lblVersionValue = new Label();
            btnClose = new Button();
            panelMain.SuspendLayout();
            SuspendLayout();
            // 
            // panelMain
            // 
            panelMain.Controls.Add(lblSystem);
            panelMain.Controls.Add(lblReleaseDate);
            panelMain.Controls.Add(lblFramework);
            panelMain.Controls.Add(lblPlatform);
            panelMain.Controls.Add(lblVersion);
            panelMain.Controls.Add(lblSystemValue);
            panelMain.Controls.Add(lblReleaseDateValue);
            panelMain.Controls.Add(lblFrameworkValue);
            panelMain.Controls.Add(lblPlatformValue);
            panelMain.Controls.Add(lblVersionValue);
            panelMain.Dock = DockStyle.Top;
            panelMain.Location = new Point(0, 0);
            panelMain.Margin = new Padding(4, 4, 4, 4);
            panelMain.Name = "panelMain";
            panelMain.Padding = new Padding(30, 30, 30, 30);
            panelMain.Size = new Size(500, 280);
            panelMain.TabIndex = 0;
            // 
            // lblSystem
            // 
            lblSystem.AutoSize = true;
            lblSystem.Font = new Font("Microsoft YaHei UI", 10F, FontStyle.Bold);
            lblSystem.Location = new Point(30, 230);
            lblSystem.Margin = new Padding(4, 0, 4, 0);
            lblSystem.Name = "lblSystem";
            lblSystem.Size = new Size(69, 20);
            lblSystem.TabIndex = 9;
            lblSystem.Text = "System:";
            // 
            // lblReleaseDate
            // 
            lblReleaseDate.AutoSize = true;
            lblReleaseDate.Font = new Font("Microsoft YaHei UI", 10F, FontStyle.Bold);
            lblReleaseDate.Location = new Point(30, 180);
            lblReleaseDate.Margin = new Padding(4, 0, 4, 0);
            lblReleaseDate.Name = "lblReleaseDate";
            lblReleaseDate.Size = new Size(120, 20);
            lblReleaseDate.TabIndex = 8;
            lblReleaseDate.Text = "Release Date:";
            // 
            // lblFramework
            // 
            lblFramework.AutoSize = true;
            lblFramework.Font = new Font("Microsoft YaHei UI", 10F, FontStyle.Bold);
            lblFramework.Location = new Point(30, 130);
            lblFramework.Margin = new Padding(4, 0, 4, 0);
            lblFramework.Name = "lblFramework";
            lblFramework.Size = new Size(95, 20);
            lblFramework.TabIndex = 7;
            lblFramework.Text = "Framework:";
            // 
            // lblPlatform
            // 
            lblPlatform.AutoSize = true;
            lblPlatform.Font = new Font("Microsoft YaHei UI", 10F, FontStyle.Bold);
            lblPlatform.Location = new Point(30, 80);
            lblPlatform.Margin = new Padding(4, 0, 4, 0);
            lblPlatform.Name = "lblPlatform";
            lblPlatform.Size = new Size(80, 20);
            lblPlatform.TabIndex = 6;
            lblPlatform.Text = "Platform:";
            // 
            // lblVersion
            // 
            lblVersion.AutoSize = true;
            lblVersion.Font = new Font("Microsoft YaHei UI", 10F, FontStyle.Bold);
            lblVersion.Location = new Point(30, 30);
            lblVersion.Margin = new Padding(4, 0, 4, 0);
            lblVersion.Name = "lblVersion";
            lblVersion.Size = new Size(72, 20);
            lblVersion.TabIndex = 5;
            lblVersion.Text = "Version:";
            // 
            // lblSystemValue
            // 
            lblSystemValue.AutoSize = true;
            lblSystemValue.Font = new Font("Microsoft YaHei UI", 10F);
            lblSystemValue.Location = new Point(160, 230);
            lblSystemValue.Margin = new Padding(4, 0, 4, 0);
            lblSystemValue.Name = "lblSystemValue";
            lblSystemValue.Size = new Size(195, 20);
            lblSystemValue.TabIndex = 4;
            lblSystemValue.Text = "Windows 10.0.19045.0";
            // 
            // lblReleaseDateValue
            // 
            lblReleaseDateValue.AutoSize = true;
            lblReleaseDateValue.Font = new Font("Microsoft YaHei UI", 10F);
            lblReleaseDateValue.Location = new Point(160, 180);
            lblReleaseDateValue.Margin = new Padding(4, 0, 4, 0);
            lblReleaseDateValue.Name = "lblReleaseDateValue";
            lblReleaseDateValue.Size = new Size(95, 20);
            lblReleaseDateValue.TabIndex = 3;
            lblReleaseDateValue.Text = "2026-01-11";
            // 
            // lblFrameworkValue
            // 
            lblFrameworkValue.AutoSize = true;
            lblFrameworkValue.Font = new Font("Microsoft YaHei UI", 10F);
            lblFrameworkValue.Location = new Point(160, 130);
            lblFrameworkValue.Margin = new Padding(4, 0, 4, 0);
            lblFrameworkValue.Name = "lblFrameworkValue";
            lblFrameworkValue.Size = new Size(105, 20);
            lblFrameworkValue.TabIndex = 2;
            lblFrameworkValue.Text = ".NET 8.0.22";
            // 
            // lblPlatformValue
            // 
            lblPlatformValue.AutoSize = true;
            lblPlatformValue.Font = new Font("Microsoft YaHei UI", 10F);
            lblPlatformValue.Location = new Point(160, 80);
            lblPlatformValue.Margin = new Padding(4, 0, 4, 0);
            lblPlatformValue.Name = "lblPlatformValue";
            lblPlatformValue.Size = new Size(70, 20);
            lblPlatformValue.TabIndex = 1;
            lblPlatformValue.Text = "win-x64";
            // 
            // lblVersionValue
            // 
            lblVersionValue.AutoSize = true;
            lblVersionValue.Font = new Font("Microsoft YaHei UI", 10F);
            lblVersionValue.Location = new Point(160, 30);
            lblVersionValue.Margin = new Padding(4, 0, 4, 0);
            lblVersionValue.Name = "lblVersionValue";
            lblVersionValue.Size = new Size(80, 20);
            lblVersionValue.TabIndex = 0;
            lblVersionValue.Text = "1.3.0.0";
            // 
            // btnClose
            // 
            btnClose.Location = new Point(200, 300);
            btnClose.Margin = new Padding(4, 4, 4, 4);
            btnClose.Name = "btnClose";
            btnClose.Size = new Size(100, 35);
            btnClose.TabIndex = 1;
            btnClose.Text = "关闭";
            btnClose.UseVisualStyleBackColor = true;
            btnClose.Click += BtnClose_Click;
            // 
            // AboutForm
            // 
            AutoScaleDimensions = new SizeF(9F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(500, 360);
            Controls.Add(btnClose);
            Controls.Add(panelMain);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Margin = new Padding(4, 4, 4, 4);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "AboutForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "关于";
            panelMain.ResumeLayout(false);
            panelMain.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel panelMain;
        private Label lblVersion;
        private Label lblVersionValue;
        private Label lblPlatform;
        private Label lblPlatformValue;
        private Label lblFramework;
        private Label lblFrameworkValue;
        private Label lblReleaseDate;
        private Label lblReleaseDateValue;
        private Label lblSystem;
        private Label lblSystemValue;
        private Button btnClose;
    }
}

