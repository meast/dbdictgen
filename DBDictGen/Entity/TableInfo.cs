using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DBDictGen.Entity
{
    public class TableInfo
    {
        public int TableId { get; set; }
        public string TableName { get; set; }
        public string TableNote { get; set; }
        public string TableCollation { get; set; }
        public string TableCreateTime { get; set; }
        public string TableUpdateTime { get; set; }
        public string TableType { get; set; }
        public string TableEngine { get; set; }
        public string TableKeyField { get; set; }
        public string DBName { get; set; }
        public List<FieldInfo> FieldList { get; set; }
    }
}
