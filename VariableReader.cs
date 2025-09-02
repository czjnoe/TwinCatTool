using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TwinCAT.Ads;

namespace TwinCatTool
{
    public class VariableReader
    {
        private readonly AdsClient adsClient;

        public VariableReader(AdsClient adsClient)
        {
            this.adsClient = adsClient;
        }

        public async Task<string> ReadVariableValueAsync(string variableName, string dataType)
        {
            try
            {
                // 使用基本的读取方法
                switch (dataType.ToLower())
                {
                    case "bool":
                    case "boolean":
                        var boolValue = adsClient.ReadValue<bool>(variableName);
                        return boolValue.ToString();
                    case "int":
                    case "int32":
                        var intValue = adsClient.ReadValue<int>(variableName);
                        return intValue.ToString();
                    case "uint":
                    case "uint32":
                        var uintValue = adsClient.ReadValue<UInt32>(variableName);
                        return uintValue.ToString();
                    case "short":
                    case "int16":
                        var shortValue = adsClient.ReadValue<short>(variableName);
                        return shortValue.ToString();
                    case "ushort":
                    case "uint16":
                        var ushortValue = adsClient.ReadValue<UInt16>(variableName);
                        return ushortValue.ToString();
                    case "long":
                    case "int64":
                        var longValue = adsClient.ReadValue<long>(variableName);
                        return longValue.ToString();
                    case "ulong":
                    case "uint64":
                        var ulongValue = adsClient.ReadValue<ulong>(variableName);
                        return ulongValue.ToString();
                    case "float":
                    case "real":
                        var floatValue = adsClient.ReadValue<float>(variableName);
                        return floatValue.ToString("F3");
                    case "double":
                    case "lreal":
                        var doubleValue = adsClient.ReadValue<double>(variableName);
                        return doubleValue.ToString("F6");
                    case "string":
                        var stringValue = adsClient.ReadValue<string>(variableName);
                        return stringValue ?? "";
                    default:
                        // 尝试读取为字节数组并转换为字符串
                        try
                        {
                            var bytes = adsClient.ReadValue<byte[]>(variableName);
                            return BitConverter.ToString(bytes);
                        }
                        catch
                        {
                            return "未知类型";
                        }
                }
            }
            catch (Exception ex)
            {
                return $"错误: {ex.Message}";
            }
        }

        public async Task<List<VariableInfo>> ReadMultipleVariablesAsync(List<VariableInfo> variables)
        {
            var tasks = new List<Task>();

            foreach (var variable in variables)
            {
                var task = Task.Run(async () =>
                {
                    variable.Value = await ReadVariableValueAsync(variable.Name, variable.DataType);
                });
                tasks.Add(task);
            }

            await Task.WhenAll(tasks);
            return variables;
        }

        // 简化版本，暂时不实现通知功能
        public void SubscribeToVariable(string variableName, Action<string> valueChangedCallback)
        {
        }

        public void UnsubscribeFromVariable(string variableName)
        {
           
        }

        public void UnsubscribeAll()
        {
            
        }
    }
}
