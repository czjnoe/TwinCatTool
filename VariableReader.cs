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

        public string ReadVariableValue(string variableName, string dataType)
        {
            try
            {
                var normalized = (dataType ?? string.Empty).Trim().ToLowerInvariant();
                var parenIndex = normalized.IndexOf('(');
                if (parenIndex > 0)
                    normalized = normalized.Substring(0, parenIndex);

                // 常见的 TwinCAT PLC 类型名
                switch (normalized)
                {
                    case "bool":
                    case "boolean":
                        var boolValue = adsClient.ReadValue<bool>(variableName);
                        return boolValue.ToString();
                    case "sint":
                        var sintValue = adsClient.ReadValue<sbyte>(variableName);
                        return sintValue.ToString();
                    case "usint":
                    case "byte":
                        var byteValue = adsClient.ReadValue<byte>(variableName);
                        return byteValue.ToString();
                    case "int": // PLC: INT (16-bit)
                    case "int16":
                        var int16Value = adsClient.ReadValue<short>(variableName);
                        return int16Value.ToString();
                    case "uint": // PLC: UINT (16-bit)
                    case "uint16":
                    case "word":
                        var uint16Value = adsClient.ReadValue<ushort>(variableName);
                        return uint16Value.ToString();
                    case "dint":
                    case "int32":
                        var int32Value = adsClient.ReadValue<int>(variableName);
                        return int32Value.ToString();
                    case "udint":
                    case "uint32":
                    case "dword":
                        var uint32Value = adsClient.ReadValue<uint>(variableName);
                        return uint32Value.ToString();
                    case "lint":
                    case "int64":
                        var int64Value = adsClient.ReadValue<long>(variableName);
                        return int64Value.ToString();
                    case "ulint":
                    case "uint64":
                    case "qword":
                        var uint64Value = adsClient.ReadValue<ulong>(variableName);
                        return uint64Value.ToString();
                    case "real":
                    case "float":
                        var floatValue = adsClient.ReadValue<float>(variableName);
                        return floatValue.ToString("F3");
                    case "lreal":
                    case "double":
                        var doubleValue = adsClient.ReadValue<double>(variableName);
                        return doubleValue.ToString("F6");
                    case "string":
                    case "wstring":
                        var stringValue = adsClient.ReadValue<string>(variableName);
                        return stringValue ?? string.Empty;
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

        public async Task<string> ReadVariableValueAsync(string variableName, string dataType, CancellationToken cancellationToken)
        {
            try
            {
                var normalized = (dataType ?? string.Empty).Trim().ToLowerInvariant();
                var parenIndex = normalized.IndexOf('(');
                if (parenIndex > 0)
                    normalized = normalized.Substring(0, parenIndex);

                // 常见的 TwinCAT PLC 类型名
                switch (normalized)
                {
                    case "bool":
                    case "boolean":
                        var boolValue = await adsClient.ReadValueAsync<bool>(variableName, cancellationToken);
                        return boolValue.Value.ToString();
                    case "sint":
                        var sintValue = await adsClient.ReadValueAsync<sbyte>(variableName, cancellationToken);
                        return sintValue.Value.ToString();
                    case "usint":
                    case "byte":
                        var byteValue = await adsClient.ReadValueAsync<byte>(variableName, cancellationToken);
                        return byteValue.Value.ToString();
                    case "int": // PLC: INT (16-bit)
                    case "int16":
                        var int16Value = await adsClient.ReadValueAsync<short>(variableName, cancellationToken);
                        return int16Value.Value.ToString();
                    case "uint": // PLC: UINT (16-bit)
                    case "uint16":
                    case "word":
                        var uint16Value = await adsClient.ReadValueAsync<ushort>(variableName, cancellationToken);
                        return uint16Value.Value.ToString();
                    case "dint":
                    case "int32":
                        var int32Value = await adsClient.ReadValueAsync<int>(variableName, cancellationToken);
                        return int32Value.Value.ToString();
                    case "udint":
                    case "uint32":
                    case "dword":
                        var uint32Value = await adsClient.ReadValueAsync<uint>(variableName, cancellationToken);
                        return uint32Value.Value.ToString();
                    case "lint":
                    case "int64":
                        var int64Value = await adsClient.ReadValueAsync<long>(variableName, cancellationToken);
                        return int64Value.Value.ToString();
                    case "ulint":
                    case "uint64":
                    case "qword":
                        var uint64Value = await adsClient.ReadValueAsync<ulong>(variableName, cancellationToken);
                        return uint64Value.Value.ToString();
                    case "real":
                    case "float":
                        var floatValue = await adsClient.ReadValueAsync<float>(variableName, cancellationToken);
                        return floatValue.Value.ToString("F3");
                    case "lreal":
                    case "double":
                        var doubleValue = await adsClient.ReadValueAsync<double>(variableName, cancellationToken);
                        return doubleValue.Value.ToString("F6");
                    case "string":
                    case "wstring":
                        var stringValue = await adsClient.ReadValueAsync<string>(variableName, cancellationToken);
                        return stringValue?.Value ?? string.Empty;
                    default:
                        // 尝试读取为字节数组并转换为字符串
                        try
                        {
                            var bytes = await adsClient.ReadValueAsync<byte[]>(variableName, cancellationToken);
                            return BitConverter.ToString(bytes?.Value);
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
            await Parallel.ForEachAsync(variables, async (variable, cancellationToken) =>
              {
                  variable.Value = await ReadVariableValueAsync(variable.Name, variable.DataType, cancellationToken);
              });
            return variables;
        }

        public List<VariableInfo> ReadMultipleVariables(List<VariableInfo> variables)
        {
            foreach (var variable in variables)
            {
                variable.Value = ReadVariableValue(variable.Name, variable.DataType);
            }
            return variables;
        }
    }
}
