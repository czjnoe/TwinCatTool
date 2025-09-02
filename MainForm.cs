using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TwinCAT.Ads;
using TwinCAT.Ads.TypeSystem;

namespace TwinCatTool
{
    public partial class MainForm : Form
    {
        private AdsClient? adsClient;
        private VariableReader? variableReader;
        private VariableManager? variableManager;
        private bool isConnected = false;
        private List<VariableInfo> allVariables = new List<VariableInfo>();
        private System.Windows.Forms.Timer? refreshTimer;

        public MainForm()
        {
            InitializeComponent();
            InitializeAdsClient();
            InitializeListView();
        }

        private void InitializeAdsClient()
        {
            adsClient = new AdsClient();
            adsClient.AdsNotification += OnAdsNotification;
            
            // 初始化定时器用于定期刷新变量值
            refreshTimer = new System.Windows.Forms.Timer();
            refreshTimer.Interval = 1000; // 1秒刷新一次
            refreshTimer.Tick += RefreshTimer_Tick;
        }

        private void InitializeListView()
        {
            // 添加ListView列
            listViewVariables.Columns.Add("变量名", 200);
            listViewVariables.Columns.Add("数据类型", 100);
            listViewVariables.Columns.Add("值", 150);
            listViewVariables.Columns.Add("地址", 80);
            listViewVariables.Columns.Add("大小", 60);
            listViewVariables.Columns.Add("注释", 200);
        }

        private async void BtnConnect_Click(object sender, EventArgs e)
        {
            try
            {
                if (adsClient == null || btnConnect == null || lblStatus == null) return;

                btnConnect.Enabled = false;
                lblStatus.Text = "正在连接...";
                lblStatus.ForeColor = Color.Orange;

                var amsNetId = AmsNetId.Parse(txtAmsNetId.Text);
                var port = int.Parse(txtPort.Text);

                await Task.Run(() => adsClient.Connect(amsNetId, port));

                // 初始化变量读取器和管理器
                variableReader = new VariableReader(adsClient);
                variableManager = new VariableManager(adsClient);

                isConnected = true;
                if (btnConnect != null) btnConnect.Enabled = false;
                if (btnDisconnect != null) btnDisconnect.Enabled = true;
                if (btnRefresh != null) btnRefresh.Enabled = true;
                if (chkAutoRefresh != null) chkAutoRefresh.Enabled = true;
                if (lblStatus != null)
                {
                    lblStatus.Text = "已连接";
                    lblStatus.ForeColor = Color.Green;
                }

                // 自动刷新变量列表
                await RefreshVariables();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"连接失败: {ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                if (btnConnect != null) btnConnect.Enabled = true;
                if (lblStatus != null)
                {
                    lblStatus.Text = "连接失败";
                    lblStatus.ForeColor = Color.Red;
                }
            }
        }

        private void BtnDisconnect_Click(object sender, EventArgs e)
        {
            try
            {
                if (adsClient != null && isConnected)
                {
                    adsClient.Disconnect();
                }

                isConnected = false;
                if (btnConnect != null) btnConnect.Enabled = true;
                if (btnDisconnect != null) btnDisconnect.Enabled = false;
                if (btnRefresh != null) btnRefresh.Enabled = false;
                if (chkAutoRefresh != null)
                {
                    chkAutoRefresh.Enabled = false;
                    chkAutoRefresh.Checked = false;
                }
                if (lblStatus != null)
                {
                    lblStatus.Text = "已断开";
                    lblStatus.ForeColor = Color.Red;
                }

                // 停止自动刷新
                if (refreshTimer != null) refreshTimer.Stop();

                // 取消所有变量订阅
                if (variableReader != null)
                {
                    variableReader.UnsubscribeAll();
                }

                // 清空变量列表
                allVariables.Clear();
                if (listViewVariables != null) listViewVariables.Items.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"断开连接失败: {ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void BtnRefresh_Click(object sender, EventArgs e)
        {
            await RefreshVariables();
        }

        private async Task RefreshVariables()
        {
            if (!isConnected || adsClient == null) return;

            try
            {
                if (btnRefresh != null) btnRefresh.Enabled = false;
                if (lblStatus != null) lblStatus.Text = "正在获取变量列表...";

                allVariables.Clear();
                if (listViewVariables != null) listViewVariables.Items.Clear();

                // 获取变量列表
                if (variableManager != null)
                {
                    allVariables = variableManager.GetVariables();
                }

                // 先读取当前变量的最新值
                if (variableReader != null)
                {
                    await variableReader.ReadMultipleVariablesAsync(allVariables);

                    // 过滤掉读取失败或无值的变量（认为不可读或无访问权限）
                    allVariables = allVariables
                        .Where(v => !string.IsNullOrEmpty(v.Value) && !v.Value.StartsWith("错误:"))
                        .ToList();
                }

                // 显示所有变量（含最新值）
                DisplayVariables(allVariables);
                if (lblStatus != null) lblStatus.Text = $"已连接 - 共 {allVariables.Count} 个变量";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"获取变量列表失败: {ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                if (lblStatus != null) lblStatus.Text = "获取变量失败";
            }
            finally
            {
                if (btnRefresh != null) btnRefresh.Enabled = true;
            }
        }

        private void TxtSearch_TextChanged(object sender, EventArgs e)
        {
            if (variableManager != null && txtSearch != null)
            {
                var filteredVariables = variableManager.SearchVariables(allVariables, txtSearch.Text);
                DisplayVariables(filteredVariables);
            }
        }

        private void DisplayVariables(List<VariableInfo> variables)
        {
            if (listViewVariables == null) return;
            
            listViewVariables.Items.Clear();
            
            foreach (var variable in variables)
            {
                var item = new ListViewItem(variable.Name);
                item.SubItems.Add(variable.DataType);
                item.SubItems.Add(variable.Value ?? "");
                item.SubItems.Add($"0x{variable.Address:X}");
                item.SubItems.Add(variable.Size.ToString());
                item.SubItems.Add(variable.Comment);
                
                listViewVariables.Items.Add(item);
            }
        }

        private async void RefreshTimer_Tick(object? sender, EventArgs e)
        {
            if (!isConnected || variableReader == null) return;

            try
            {
                // 只刷新当前显示的变量值
                var currentVariables = GetCurrentDisplayedVariables();
                await variableReader.ReadMultipleVariablesAsync(currentVariables);
                
                // 更新显示
                var searchText = txtSearch.Text.ToLower();
                var filteredVariables = allVariables.Where(v => 
                    v.Name.ToLower().Contains(searchText) || 
                    v.Comment.ToLower().Contains(searchText)).ToList();
                
                DisplayVariables(filteredVariables);
            }
            catch (Exception ex)
            {
                // 静默处理错误，避免影响UI
                Console.WriteLine($"自动刷新错误: {ex.Message}");
            }
        }

        private List<VariableInfo> GetCurrentDisplayedVariables()
        {
            if (variableManager != null && txtSearch != null)
            {
                return variableManager.SearchVariables(allVariables, txtSearch.Text);
            }
            return new List<VariableInfo>();
        }

        private void ChkAutoRefresh_CheckedChanged(object? sender, EventArgs e)
        {
            if (refreshTimer == null) return;
            
            if (chkAutoRefresh?.Checked == true)
            {
                refreshTimer.Start();
            }
            else
            {
                refreshTimer.Stop();
            }
        }

        private void OnAdsNotification(object? sender, AdsNotificationEventArgs e)
        {
            // 处理变量值变化通知
            if (InvokeRequired)
            {
                Invoke(new Action(() => OnAdsNotification(sender, e)));
                return;
            }

            // 更新对应变量的值
            // 注意：新版本的API可能没有NotificationHandle属性
            // 这里可以根据需要更新UI中的变量值
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (refreshTimer != null)
            {
                refreshTimer.Stop();
                refreshTimer.Dispose();
            }

            if (variableReader != null)
            {
                variableReader.UnsubscribeAll();
            }

            if (adsClient != null)
            {
                adsClient.Dispose();
            }
            base.OnFormClosing(e);
        }
    }

    public class VariableInfo
    {
        public string Name { get; set; } = "";
        public string DataType { get; set; } = "";
        public string? Value { get; set; }
        public int Address { get; set; }
        public int Size { get; set; }
        public string Comment { get; set; } = "";
    }
}
