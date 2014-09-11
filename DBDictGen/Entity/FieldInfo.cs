using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DBDictGen.Entity
{
    public class FieldInfo
    {
        public string FieldName { get; set; }
        public string FieldType { get; set; }
        public string FieldTypeSharp { get; set; }
        public string FieldLength { get; set; }
        public string FieldScale { get; set; }
        public string FieldNote { get; set; }
        public string FieldDefaultValue { get; set; }
        public bool IsPrimaryKey { get; set; }
        public bool IsAutoIncrement { get; set; }
        public bool IsNullable { get; set; }
    }
}
