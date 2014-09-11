using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DBDictGen.Entity
{
    /// <summary>
    /// 数据库信息
    /// </summary>
    public class DBInfo
    {
        public int DBId { get; set; }
        public string DBName { get; set; }
        public string DBSid { get; set; }
        public string DBMode { get; set; }
        public string DBStatus { get; set; }
        public string DBStatus2 { get; set; }
        public string DBCRDate { get; set; }
        public string DBReserved { get; set; }
        public string DBCategory { get; set; }
        public string DBCmptlevel { get; set; }
        public string DBFilename { get; set; }
        public string DBVersion { get; set; }
        public string DBCharSet { get; set; }
        public string DBCollationName { get; set; }
        public List<TableInfo> TableList { get; set; }
    }
}
