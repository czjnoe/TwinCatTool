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
                variables.AddRange(GetAllVariables());
                Console.WriteLine($"获取符号信息失败: {ex.Message}");
            }

            return variables;
        }

        private List<VariableInfo> GetAllVariables()
        {
            var variables = new List<VariableInfo>();
            try
            {
                var settings = new TwinCAT.SymbolLoaderSettings(SymbolsLoadMode.Flat);// 使用 TwinCAT 符号加载器从 PLC 读取变量
                var loader = TwinCAT.Ads.TypeSystem.SymbolLoaderFactory.Create(adsClient, settings);
                foreach (TwinCAT.Ads.TypeSystem.Symbol symbol in loader.Symbols)
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
