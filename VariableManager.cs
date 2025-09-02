using System;
using System.Collections.Generic;
using System.Linq;
using TwinCAT;
using TwinCAT.Ads;
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
                // 尝试使用符号信息加载器 - 新版本API可能不同
                // 暂时使用示例变量，后续可以根据实际API调整
                variables.AddRange(GetSampleVariables());
            }
            catch (Exception ex)
            {
                // 如果无法获取符号信息，返回示例变量
                variables.AddRange(GetSampleVariables());
                Console.WriteLine($"获取符号信息失败: {ex.Message}");
            }

            return variables;
        }

        private List<VariableInfo> GetSampleVariables()
        {
            var variables = new List<VariableInfo>();

            try
            {
                // 使用 TwinCAT 符号加载器从 PLC 读取变量
                var settings = new TwinCAT.SymbolLoaderSettings(SymbolsLoadMode.Flat);
                var loader = TwinCAT.Ads.TypeSystem.SymbolLoaderFactory.Create(adsClient, settings);

                foreach (var symbol in loader.Symbols)
                {
                    // 保留所有变量，这里不做权限过滤，避免 SDK 兼容性问题
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
                        Comment = comment
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
    }
}
