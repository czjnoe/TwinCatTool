using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwinCatTool
{
    public class VariableInfo
    {
        public string Name { get; set; } = "";

        public string DataType { get; set; } = "";

        public string? Value { get; set; }

        public int Address { get; set; }

        public int Size { get; set; }

        public string Comment { get; set; } = "";

        public bool IsWritable { get; set; }
    }
}
