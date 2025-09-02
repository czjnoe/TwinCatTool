using System;
using System.Windows.Forms;
using TwinCAT.Ads;

namespace TwinCatTool
{
    public class SimpleTest
    {
        public static void TestConnection()
        {
            try
            {
                using (var client = new AdsClient())
                {
                    var amsNetId = AmsNetId.Parse("127.0.0.1.1.1");
                    client.Connect(amsNetId, 851);
                    
                    MessageBox.Show("连接成功！", "测试", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    
                    client.Disconnect();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"连接失败: {ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
