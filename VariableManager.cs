using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using TwinCAT;
using TwinCAT.Ads;
using TwinCAT.Ads.Internal;
using TwinCAT.Ads.TypeSystem;
using TwinCAT.TypeSystem;

namespace TwinCatTool
{
    public class VariableManager
    {
        private readonly AdsClient adsClient;

        public VariableManager(AdsClient adsClient)
        {
            this.adsClient = adsClient;
        }

        public List<VariableInfo> GetVariables()
        {
            var variables = new List<VariableInfo>();
            try
            {
                variables.AddRange(GetAllVariables());
            }
            catch (Exception ex)
            {
                Console.WriteLine($"获取符号信息失败: {ex.Message}");
            }
            return variables;
        }

        private List<VariableInfo> GetAllVariables()
        {
            var variables = new List<VariableInfo>();
            try
            {
                var settings = new TwinCAT.SymbolLoaderSettings(SymbolsLoadMode.Flat);
                var loader = TwinCAT.Ads.TypeSystem.SymbolLoaderFactory.Create(adsClient, settings);

                foreach (Symbol symbol in loader.Symbols)
                {
                    bool isWritable = (symbol.Flags & AdsSymbolFlags.Persistent) != 0;
                    var dataTypeName = symbol.DataType != null ? symbol.DataType.Name : symbol.TypeName;
                    var size = symbol.Size;
                    var comment = symbol.Comment ?? string.Empty;
                    var name = symbol.InstancePath;
                    
                    variables.Add(new VariableInfo
                    {
                        Name = name,
                        DataType = dataTypeName,
                        Address = 0,
                        Size = size,
                        Comment = comment,
                        IsWritable = !isWritable,
                        IndexGroup = symbol.IndexGroup,
                        IndexOffset = symbol.IndexOffset,
                    });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"读取PLC变量失败: {ex.Message}");
            }
            return variables;
        }

        public List<VariableInfo> SearchVariables(List<VariableInfo> allVariables, string searchText)
        {
            if (string.IsNullOrEmpty(searchText))
                return allVariables;

            var lowerSearchText = searchText.ToLower();
            return allVariables.Where(v =>
                v.Name.ToLower().Contains(lowerSearchText) ||
                v.Comment.ToLower().Contains(lowerSearchText)).ToList();
        }

        /// <summary>
        /// 写入变量值（自动类型转换）
        /// </summary>
        /// <param name="variableName">变量名称</param>
        /// <param name="value">要写入的值</param>
        /// <returns>写入是否成功</returns>
        public bool WriteVariable(string variableName, object value)
        {
            try
            {
                var settings = new TwinCAT.SymbolLoaderSettings(SymbolsLoadMode.Flat);
                var loader = TwinCAT.Ads.TypeSystem.SymbolLoaderFactory.Create(adsClient, settings);
                var symbol = loader.Symbols[variableName] as Symbol;

                if (symbol == null)
                {
                    Console.WriteLine($"变量 '{variableName}' 不存在");
                    return false;
                }

                // 检查变量是否可写
                if ((symbol.Flags & AdsSymbolFlags.Persistent) != 0)
                {
                    Console.WriteLine($"变量 '{variableName}' 是只读的，无法写入");
                    return false;
                }

                // 根据数据类型转换并写入
                var dataTypeName = symbol.DataType != null ? symbol.DataType.Name : symbol.TypeName;
                object convertedValue = ConvertValue(value, dataTypeName);

                // 使用WriteValue方法写入
                adsClient.WriteValue(symbol, convertedValue);
                Console.WriteLine($"成功写入变量 '{variableName}' = {convertedValue}");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"写入变量失败: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// 批量写入变量
        /// </summary>
        public Dictionary<string, bool> WriteVariables(Dictionary<string, object> variables)
        {
            var results = new Dictionary<string, bool>();

            foreach (var kvp in variables)
            {
                results[kvp.Key] = WriteVariable(kvp.Key, kvp.Value);
            }

            return results;
        }

        /// <summary>
        /// 异步写入变量
        /// </summary>
        public async Task<bool> WriteVariableAsync(string variableName, object value)
        {
            return await Task.Run(() => WriteVariable(variableName, value));
        }

        /// <summary>
        /// 根据PLC数据类型转换值
        /// </summary>
        private object ConvertValue(object value, string dataTypeName)
        {
            if (value == null)
                throw new ArgumentNullException(nameof(value));

            string normalizedType = dataTypeName.ToUpper();

            try
            {
                switch (normalizedType)
                {
                    // 布尔类型
                    case "BOOL":
                        return Convert.ToBoolean(value);

                    // 整数类型
                    case "BYTE":
                    case "USINT":
                        return Convert.ToByte(value);

                    case "SINT":
                        return Convert.ToSByte(value);

                    case "WORD":
                    case "UINT":
                        return Convert.ToUInt16(value);

                    case "INT":
                        return Convert.ToInt16(value);

                    case "DWORD":
                    case "UDINT":
                        return Convert.ToUInt32(value);

                    case "DINT":
                        return Convert.ToInt32(value);

                    case "LWORD":
                    case "ULINT":
                        return Convert.ToUInt64(value);

                    case "LINT":
                        return Convert.ToInt64(value);

                    // 浮点类型
                    case "REAL":
                        return Convert.ToSingle(value);

                    case "LREAL":
                        return Convert.ToDouble(value);

                    // 字符串类型
                    case "STRING":
                    case "WSTRING":
                        return value.ToString();

                    // 时间类型
                    case "TIME":
                    case "TIME_OF_DAY":
                    case "TOD":
                    case "DATE":
                    case "DATE_AND_TIME":
                    case "DT":
                        if (value is TimeSpan)
                            return value;
                        if (value is DateTime dt)
                            return dt.TimeOfDay;
                        return TimeSpan.Parse(value.ToString());

                    // 默认返回原值
                    default:
                        Console.WriteLine($"警告: 未识别的数据类型 '{dataTypeName}'，尝试直接写入原值");
                        return value;
                }
            }
            catch (Exception ex)
            {
                throw new InvalidCastException($"无法将值 '{value}' 转换为类型 '{dataTypeName}': {ex.Message}");
            }
        }

        /// <summary>
        /// 读取变量值
        /// </summary>
        public T ReadVariable<T>(string variableName)
        {
            try
            {
                var settings = new TwinCAT.SymbolLoaderSettings(SymbolsLoadMode.Flat);
                var loader = TwinCAT.Ads.TypeSystem.SymbolLoaderFactory.Create(adsClient, settings);
                var symbol = loader.Symbols[variableName];

                if (symbol == null)
                {
                    throw new Exception($"变量 '{variableName}' 不存在");
                }

                return (T)adsClient.ReadValue(symbol);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"读取变量失败: {ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// 读取变量值（动态类型）
        /// </summary>
        public object ReadVariable(string variableName)
        {
            try
            {
                var settings = new TwinCAT.SymbolLoaderSettings(SymbolsLoadMode.Flat);
                var loader = TwinCAT.Ads.TypeSystem.SymbolLoaderFactory.Create(adsClient, settings);
                var symbol = loader.Symbols[variableName];

                if (symbol == null)
                {
                    throw new Exception($"变量 '{variableName}' 不存在");
                }

                return adsClient.ReadValue(symbol);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"读取变量失败: {ex.Message}");
                throw;
            }
        }
    }
}
