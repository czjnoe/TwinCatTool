using MiniExcelLibs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
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

        public MainForm()
        {
            InitializeComponent();
            InitializeAdsClient();
            InitializeListView();
        }

        private void InitializeAdsClient()
        {
            txtAmsNetId.Text = AppConfigHelper.GetSetting<string>("TwinCAT:AmsNetId");
            txtPort.Text = AppConfigHelper.GetSetting<string>("TwinCAT:Port");
            adsClient = new AdsClient();
            adsClient.AdsNotification += OnAdsNotification;
        }

        private void InitializeListView()
        {
            listViewVariables.Columns.Add("变量名", 300);
            listViewVariables.Columns.Add("数据类型", 150);
            listViewVariables.Columns.Add("值", 300);
            listViewVariables.Columns.Add("可写", 80);
            listViewVariables.Columns.Add("地址", 80);
            listViewVariables.Columns.Add("大小", 60);
            listViewVariables.Columns.Add("注释", 300);
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
                if (lblStatus != null)
                {
                    lblStatus.Text = "已连接";
                    lblStatus.ForeColor = Color.Green;
                }

                // 自动刷新变量列表
                await RefreshVariables(false);
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

                if (lblStatus != null)
                {
                    lblStatus.Text = "已断开";
                    lblStatus.ForeColor = Color.Red;
                }

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

        private async Task RefreshVariables(bool readVariable = true)
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
                if (variableReader != null && readVariable)
                {
                    await variableReader.ReadMultipleVariablesAsync(allVariables);

                    // 过滤掉读取失败或无值的变量（认为不可读或无访问权限）
                    allVariables = allVariables
                        .Where(v => !string.IsNullOrEmpty(v.Value) && !v.Value.StartsWith("错误:"))
                        .ToList();
                }

                DisplayVariables(allVariables);// 显示所有变量（含最新值）
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
                item.SubItems.Add(variable.IsWritable.ToString());
                item.SubItems.Add($"0x{variable.Address:X}");
                item.SubItems.Add(variable.Size.ToString());
                item.SubItems.Add(variable.Comment);

                listViewVariables.Items.Add(item);
            }
        }

        private void OnAdsNotification(object? sender, AdsNotificationEventArgs e)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => OnAdsNotification(sender, e)));
                return;
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (adsClient != null)
            {
                adsClient.Dispose();
            }
            base.OnFormClosing(e);
        }

        private void btnCheckVariable_Click(object sender, EventArgs e)
        {
            using OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "选择Excel文件";
            openFileDialog.Filter = "Excel文件(*.xls,*.xlsx)|*.xls;*.xlsx";
            openFileDialog.Multiselect = false;
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = openFileDialog.FileName;
                var rows = MiniExcel.Query<ExportVariableInfo>(filePath);
                var queryNames = rows.Select(s => s.DeviceName).ToList();
                var allVariableNames = allVariables.Select(s => s.Name).ToList();
                var data = queryNames.Except(allVariableNames).ToList();
                if (data.Any())
                    MessageBox.Show($"以下变量不存在：{string.Join("、", data)}");
            }
        }

        private void txtAmsNetId_MouseLeave(object sender, EventArgs e)
        {
            AppConfigHelper.UpdateSetting("TwinCAT:AmsNetId", txtAmsNetId.Text.Trim());
        }

        private void txtPort_MouseLeave(object sender, EventArgs e)
        {
            AppConfigHelper.UpdateSetting("TwinCAT:Port", txtPort.Text.Trim());
        }

        private void btnVariableWrite_Click(object sender, EventArgs e)
        {
            if (listViewVariables.SelectedItems.Count < 1)
            {
                MessageBox.Show($"没有选择变量", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            var dialog = MessageBox.Show("确认写入？", "确认", MessageBoxButtons.OK);
            if (dialog == DialogResult.OK)
            {
                variableManager.WriteVariable(txtVariableName.Text, txtVariableValue.Text);
            }
        }

        private void listViewVariables_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listViewVariables.SelectedItems.Count > 0)
            {
                var selectItem = listViewVariables.SelectedItems[0];
                txtVariableName.Text = selectItem.SubItems[0].Text;
                txtVariableValue.Text = selectItem.SubItems[2].Text;
            }
        }

        private void btnVariableRead_Click(object sender, EventArgs e)
        {
            if (listViewVariables.SelectedItems.Count > 0)
            {
                var selectItem = listViewVariables.SelectedItems[0];
                if (variableReader != null)
                {
                    var value = variableReader.ReadVariableValue(selectItem.SubItems[0].Text, selectItem.SubItems[1].Text);
                    txtVariableValue.Text = value;
                    selectItem.SubItems[2].Text = value;
                }
            }
        }
    }
}
