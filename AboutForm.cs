using CZJ.Extension;
using System;
using System.Drawing;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace TwinCatTool
{
    public partial class AboutForm : Form
    {
        public AboutForm()
        {
            InitializeComponent();
            Load();
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Load()
        {
            lblVersionValue.Text = Assembly.GetExecutingAssembly().GetName().Version?.ToString() ?? "Unknown";
            lblPlatformValue.Text= RuntimeInformation.RuntimeIdentifier ?? "Unknown";
            lblFrameworkValue.Text = RuntimeInformation.FrameworkDescription ?? "Unknown";
            lblReleaseDateValue.Text = File.GetLastWriteTime(Assembly.GetExecutingAssembly().Location).ToString("yyyy-MM-dd");
            lblSystemValue.Text= $"{OS} {Environment.OSVersion.Version}";
        }

        private static string OS
        {
            get
            {
                if (OperatingSystem.IsWindows())
                    return "Windows";
                if (OperatingSystem.IsMacOS())
                    return "macOS";
                if (OperatingSystem.IsLinux())
                    return "Linux";
                return "Unknown";
            }
        }
    }
}

