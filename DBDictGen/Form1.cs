using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DBDictGen.Entity;

namespace DBDictGen
{
    public partial class Form1 : Form
    {
        private string dbhost;
        private string dbuser;
        private string dbpwd;
        private string dbport;
        private string dbtype;
        private bool IsOldVersion = false;
        private string VersionInfo = "";
        private int CountTableListed = 0;
        private int CountTableSelected = 0;
        private List<Entity.DBInfo> DBInfoList = new List<Entity.DBInfo>();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.toolStripStatusLabel1.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            this.toolStripStatusLabel2.Text = "";
            this.cmbDB.Enabled = false;
            this.btnListTable.Enabled = false;
            this.btnDisconnect.Enabled = false;
            this.btnGen.Enabled = false;
        }


        private void timer1_Tick(object sender, EventArgs e)
        {
            this.toolStripStatusLabel1.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            fbd.ShowDialog();
            if (!String.IsNullOrEmpty(fbd.SelectedPath))
                this.txtDictPath.Text = fbd.SelectedPath;
        }

        private void cmbDBType_SelectedIndexChanged(object sender, EventArgs e)
        {
            string Val = this.cmbDBType.Text.Trim().ToLower();
            switch (Val)
            {
                case "sqlite":
                    this.txtDBHost.Text = "";
                    this.txtDBUser.Text = "";
                    this.txtDBPwd.Text = "";
                    this.txtDBPort.Text = "";
                    break;
                case "mysql":
                    this.txtDBPort.Text = "3306";
                    this.txtDBUser.Text = "root";
                    break;
                case "sqlserver":
                case "sql server":
                    this.txtDBPort.Text = "1433";
                    this.txtDBUser.Text = "sa";
                    break;
                default:
                    break;

            }
        }

        /// <summary>
        /// 连接数据库并列出所有可被列出的数据库名
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnConnect_Click(object sender, EventArgs e)
        {
            this.DBInfoList.Clear();
            this.toolStripProgressBar1.Value = 10;
            this.toolStripStatusLabel2.Text = "正在连接数据库...";
            this.dbhost = this.txtDBHost.Text.Trim();
            this.dbport = this.txtDBPort.Text.Trim();
            this.dbuser = this.txtDBUser.Text.Trim();
            this.dbpwd = this.txtDBPwd.Text.Trim();
            this.dbtype = this.cmbDBType.Text.Trim().ToLower();
            

            this.txtDBHost.Enabled = false;
            this.txtDBPort.Enabled = false;
            this.txtDBPwd.Enabled = false;
            this.txtDBUser.Enabled = false;
            this.cmbDBType.Enabled = false;
            this.btnConnect.Enabled = false;
            this.btnListTable.Enabled = true;
            this.btnDisconnect.Enabled = true;
            this.cmbDB.Enabled = true;
            this.cmbDB.Items.Clear();
            this.lstTable.Items.Clear();
            this.lstTableSelected.Items.Clear();
            this.toolStripProgressBar1.Value = 30;

            if (this.dbtype == "sqlite")
            {
                OpenFileDialog ofd = new OpenFileDialog();
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    string DbFile = ofd.FileName;
                    this.cmbDB.Text = DbFile;
                    this.dbhost = DbFile;
                    this.dbpwd = this.txtDBPwd.Text.Trim();
                    try
                    {
                        System.IO.FileInfo FI = new System.IO.FileInfo(DbFile);
                        DBInfo DBIsqlite = new DBInfo();
                        DBIsqlite.DBName = FI.Name;
                        this.DBInfoList.Add(DBIsqlite);
                    }
                    catch { }
                }
                return;
            }

            if (!String.IsNullOrEmpty(this.dbhost) && !String.IsNullOrEmpty(this.dbuser))
            {
                this.toolStripProgressBar1.Value = 50;
                switch (this.dbtype)
                {
                    case "mysql":
                        MySql.Data.MySqlClient.MySqlConnection MysqlConn = GetMySQLConnection();
                        try
                        {
                            MysqlConn.Open();
                        }
                        catch (Exception exmysqlconn)
                        {
                            this.toolStripProgressBar1.Value = 0;
                            this.toolStripStatusLabel2.Text = "连接失败";
                            btnDisconnect_Click(sender, e);
                            MessageBox.Show(exmysqlconn.Message, "连接错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        string MySqlListDb = "select * from information_schema.schemata";
                        MySql.Data.MySqlClient.MySqlCommand MySqlCmd = new MySql.Data.MySqlClient.MySqlCommand(MySqlListDb, MysqlConn);
                        MySql.Data.MySqlClient.MySqlDataReader Dr1 = MySqlCmd.ExecuteReader();
                        while (Dr1.Read())
                        {
                            DBInfo DBI2 = new DBInfo();
                            DBI2.DBCategory = Dr1["CATALOG_NAME"].ToString();
                            DBI2.DBCharSet = Dr1["DEFAULT_CHARACTER_SET_NAME"].ToString();
                            DBI2.DBCmptlevel = "";
                            DBI2.DBCollationName = Dr1["DEFAULT_COLLATION_NAME"].ToString();
                            DBI2.DBCRDate = "";
                            DBI2.DBFilename = "";
                            DBI2.DBId = 0;
                            DBI2.DBMode = "";
                            DBI2.DBName = Dr1["SCHEMA_NAME"].ToString();
                            DBI2.DBReserved = "";
                            DBI2.DBSid = "";
                            DBI2.DBStatus = "";
                            DBI2.DBStatus2 = "";
                            DBI2.DBVersion = "";
                            DBInfoList.Add(DBI2);
                        }
                        Dr1.Dispose();
                        Dr1.Close();
                        MySqlCmd.CommandText = "select version()";
                        Dr1 = MySqlCmd.ExecuteReader();
                        if (Dr1.Read())
                        {
                            this.VersionInfo = "mysql " + Dr1[0];
                        }
                        Dr1.Dispose();
                        Dr1.Close();
                        MysqlConn.Dispose();
                        MysqlConn.Close();
                        break;
                    case "sql server":
                    case "sqlserver":
                        System.Data.SqlClient.SqlConnection Conn = GetSqlConnection();
                        try
                        {
                            Conn.Open();
                        }
                        catch (Exception exconnsql)
                        {
                            btnDisconnect_Click(sender, e);
                            this.toolStripStatusLabel2.Text = "连接失败";
                            this.toolStripProgressBar1.Value = 0;
                            MessageBox.Show(exconnsql.Message, "连接错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        string SqlListDB = "SELECT * FROM master.dbo.sysdatabases ORDER BY name ASC";
                        System.Data.SqlClient.SqlCommand Cmd = new System.Data.SqlClient.SqlCommand(SqlListDB, Conn);
                        System.Data.SqlClient.SqlDataReader Dr0 = Cmd.ExecuteReader();

                        while (Dr0.Read())
                        {
                            DBInfo DBI1 = new DBInfo();
                            DBI1.DBName = Dr0["name"].ToString();
                            DBI1.DBCategory = Dr0["category"].ToString();
                            DBI1.DBCharSet = "";
                            DBI1.DBCmptlevel = Dr0["cmptlevel"].ToString();
                            DBI1.DBCollationName = "";
                            DBI1.DBCRDate = Dr0["crdate"].ToString();
                            DBI1.DBFilename = Dr0["filename"].ToString();
                            try
                            {
                                DBI1.DBId = Convert.ToInt32(Dr0["dbid"].ToString());
                            }
                            catch { }
                            DBI1.DBMode = Dr0["mode"].ToString();
                            DBI1.DBReserved = Dr0["reserved"].ToString();
                            DBI1.DBSid = Dr0["sid"].ToString();
                            DBI1.DBStatus = Dr0["status"].ToString();
                            DBI1.DBStatus2 = Dr0["status2"].ToString();
                            DBI1.DBVersion = Dr0["version"].ToString();
                            DBInfoList.Add(DBI1);
                        }
                        Dr0.Dispose();
                        Dr0.Close();
                        string SQLVersion = "select @@version";
                        Cmd = new System.Data.SqlClient.SqlCommand(SQLVersion, Conn);
                        System.Data.SqlClient.SqlDataReader SQLDr1 = Cmd.ExecuteReader();
                        while (SQLDr1.Read())
                        {
                            string SQLVer = SQLDr1[0].ToString();
                            if (!String.IsNullOrEmpty(SQLVer))
                            {
                                if (SQLVer.Contains("SQL Server  2000"))
                                {
                                    IsOldVersion = true;
                                }
                                try
                                {
                                    VersionInfo = SQLVer.Split('-')[0];
                                    this.toolStripStatusLabel2.Text = VersionInfo;
                                }
                                catch { }
                            }
                        }
                        Conn.Dispose();
                        Conn.Close();
                        break;
                    default:
                        MessageBox.Show("不确定的数据库类型" + this.dbtype, "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        this.btnDisconnect_Click(sender, e);
                        break;
                }
                this.toolStripProgressBar1.Value = 75;
                foreach (DBInfo DBI2 in DBInfoList)
                {
                    this.cmbDB.Items.Add(DBI2.DBName);
                }
                this.toolStripProgressBar1.Value = 100;
                this.toolStripProgressBar1.Value = 0;
                this.toolStripStatusLabel2.Text = VersionInfo + ", 数据库数量:" + DBInfoList.Count;
            }
        }

        /// <summary>
        /// 列出所有表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnListTable_Click(object sender, EventArgs e)
        {
            this.btnGen.Enabled = true;
            this.lstTable.Items.Clear();
            this.lstTableSelected.Items.Clear();
            string DBName = this.cmbDB.Text.Trim();
            int TableCount = 0;
            if (this.dbtype == "sqlite")
            {
                try
                {
                    System.IO.FileInfo FInf = new System.IO.FileInfo(DBName);
                    DBName = FInf.Name;
                }
                catch { }
            }
            try
            {
                DBInfoList.Find(d => d.DBName == DBName).TableList.Clear();
            }
            catch { }

            List<TableInfo> TList = new List<TableInfo>();
            this.toolStripProgressBar1.Value = 10;
            switch (this.dbtype)
            {
                case "sqlite":
                    Mono.Data.Sqlite.SqliteConnection SqliteConn = GetSqliteConnection();
                    try
                    {
                        //SqliteConn.Open();
                        Builder.Sqlite SQLite = new Builder.Sqlite();
                        TList = SQLite.GetTables(SqliteConn);
                        if (TList != null)
                        {
                            foreach (TableInfo TI in TList)
                            {
                                if (!String.IsNullOrEmpty(TI.TableName))
                                {
                                    //this.lstTable.Items.Add(TI.TableName);
                                    TableCount++;
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    break;
                case "mysql":
                    MySql.Data.MySqlClient.MySqlConnection MySqlConn = GetMySQLConnection();
                    MySqlConn.Open();
                    string MySqlListTable = " select * from  information_schema.`TABLES` where TABLE_SCHEMA='" + DBName + "'";
                    MySql.Data.MySqlClient.MySqlCommand MySqlCmd = new MySql.Data.MySqlClient.MySqlCommand(MySqlListTable, MySqlConn);
                    MySql.Data.MySqlClient.MySqlDataReader Dr1 = MySqlCmd.ExecuteReader();
                    while (Dr1.Read())
                    {
                        TableInfo TI1 = new TableInfo();
                        TI1.DBName = DBName;
                        TI1.TableCollation = Dr1["TABLE_COLLATION"].ToString();
                        TI1.TableCreateTime = Dr1["CREATE_TIME"].ToString();
                        TI1.TableEngine = Dr1["ENGINE"].ToString();
                        TI1.TableId = 0;
                        TI1.TableKeyField = "";
                        TI1.TableName = Dr1["TABLE_NAME"].ToString();
                        TI1.TableNote = Dr1["TABLE_COMMENT"].ToString();
                        TI1.TableType = Dr1["TABLE_TYPE"].ToString();
                        TI1.TableUpdateTime = Dr1["UPDATE_TIME"].ToString();
                        if (!String.IsNullOrEmpty(TI1.TableName))
                        {
                            TList.Add(TI1);
                            TableCount++;
                        }
                    }
                    Dr1.Close();
                    MySqlCmd.Dispose();
                    MySqlConn.Close();
                    break;
                case "sql server":
                case "sqlserver":
                    string sql0 = "use " + DBName + ";select * from sysobjects where xtype='U' and [name]<>'dtproperties' order by [name]";
                    string sql1 = "use " + DBName + ";select * from sysobjects where xtype='V' and [name]<>'syssegments' and [name]<>'sysconstraints' order by [name]";
                    string sql2 = "use " + DBName + ";select * from sysobjects where xtype='P' and [name]<>'dtproperties' order by [name]";
                    System.Data.SqlClient.SqlConnection SqlConn = GetSqlConnection();
                    SqlConn.Open();
                    System.Data.SqlClient.SqlCommand SqlCmd = SqlConn.CreateCommand();
                    SqlCmd.CommandText = sql0;
                    System.Data.SqlClient.SqlDataReader Dr0 = SqlCmd.ExecuteReader();
                    while (Dr0.Read())
                    {
                        TableInfo TISQL0 = new TableInfo();
                        TISQL0.DBName = DBName;
                        TISQL0.TableCollation = "";
                        TISQL0.TableCreateTime = Dr0["crdate"].ToString();
                        TISQL0.TableEngine = "";
                        TISQL0.TableId = Convert.ToInt32(Dr0["id"].ToString());
                        TISQL0.TableKeyField = "";
                        TISQL0.TableName = Dr0["name"].ToString();
                        TISQL0.TableNote = "";
                        TISQL0.TableType = Dr0["type"].ToString();
                        TISQL0.TableUpdateTime = Dr0["refdate"].ToString();
                        if (!String.IsNullOrEmpty(TISQL0.TableName))
                        {
                            TList.Add(TISQL0);
                            TableCount++;
                        }
                    }
                    Dr0.Dispose();
                    SqlCmd.CommandText = sql1;
                    Dr0 = SqlCmd.ExecuteReader();
                    // 视图
                    while (Dr0.Read())
                    {
                        TableInfo TISQL1 = new TableInfo();
                        TList.Add(TISQL1);
                    }
                    Dr0.Dispose();
                    SqlCmd.CommandText = sql2;
                    Dr0 = SqlCmd.ExecuteReader();
                    // 存储过程
                    while (Dr0.Read())
                    {
                        TableInfo TISQL2 = new TableInfo();
                        //TList.Add(TISQL2);
                    }
                    Dr0.Dispose();
                    Dr0.Close();
                    SqlCmd.Dispose();
                    SqlConn.Close();

                    break;
                default:
                    break;
            }
            this.toolStripProgressBar1.Value = 70;
            foreach (TableInfo TInfo in TList)
            {
                if (!String.IsNullOrEmpty(TInfo.TableName))
                {
                    this.lstTable.Items.Add(TInfo.TableName);
                }
            }
            this.toolStripProgressBar1.Value = 100;
            this.toolStripProgressBar1.Value = 0;
            try
            {
                DBInfoList.Find(d => d.DBName == DBName).TableList = TList;
            }
            catch { }
            this.CountTableListed = TableCount;
            this.toolStripStatusLabel2.Text = VersionInfo + ",表数量:" + TableCount;
        }

        /// <summary>
        /// 断开连接，释放资源
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDisconnect_Click(object sender, EventArgs e)
        {
            this.toolStripProgressBar1.Value = 10;
            this.dbhost = "";
            this.dbport = "";
            this.dbpwd = "";
            this.dbtype = "";
            this.dbuser = "";
            this.VersionInfo = "";

            this.CountTableListed = 0;
            this.CountTableSelected = 0;
            this.toolStripProgressBar1.Value = 50;
            this.txtDBHost.Enabled = true;
            this.txtDBPort.Enabled = true;
            this.txtDBPwd.Enabled = true;
            this.txtDBUser.Enabled = true;
            this.cmbDBType.Enabled = true;
            this.btnConnect.Enabled = true;
            this.btnDisconnect.Enabled = false;
            this.btnListTable.Enabled = false;
            this.cmbDB.Items.Clear();
            this.cmbDB.Text = "";
            this.cmbDB.Enabled = false;
            this.btnGen.Enabled = false;
            this.lstTable.Items.Clear();
            this.lstTableSelected.Items.Clear();
            this.toolStripStatusLabel2.Text = "";
            this.toolStripProgressBar1.Value = 100;
            this.DBInfoList.Clear();
            this.toolStripProgressBar1.Value = 0;
        }

        /// <summary>
        /// 生成字典
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnGen_Click(object sender, EventArgs e)
        {
            string DBName = this.cmbDB.Text.Trim();
            if (this.dbtype == "sqlite")
            {
                try
                {
                    System.IO.FileInfo FI = new System.IO.FileInfo(DBName);
                    DBName = FI.Name;
                }
                catch { }
            }
            //DBInfoList.Find(p1 => p1.DBName == DBName).TableList = new List<TableInfo>();
            this.lstTableSelected.Sorted = true;
            ListBox.ObjectCollection OC0 = this.lstTableSelected.Items;
            string dictcontent = "<!DOCTYPE html><html><head><meta http-equiv='content-type' content='text/html;charset=utf-8' /><title>DB Dict:" + DBName + "</title><style type='text/css'>table{width:100%;border-collapse:collapse;} td{border:1px solid #cecece;} th{background:#efefef;border:1px solid #cecece;}.t{margin-top:10px;} .t1{margin-top:10px;}</style></head><body><div style='width:90%;margin:30px auto;'><div>DB Name:" + DBName + "<br/>Table Count:[TABLECOUNT]<br/>List Count:[TABLELISTCOUNT]<br/>Generate on:" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "</div><div>[DBDICT]</div></div></body></html>";
            string dictcontent1 = "";
            string tablecontent = "";
            
            if (OC0 != null)
            {
                switch (this.dbtype)
                {
                    case "sqlite":

                        foreach (string SI in OC0)
                        {
                            tablecontent += "\r\n" + SI;
                            dictcontent1 += "\r\n<div class='t'>Table:" + SI + "</div>\r\n";
                            dictcontent1 += "<div class='t1'><table>\r\n";
                            dictcontent1 += "<th>列名</th>";
                            dictcontent1 += "<th>类型</th>";
                            dictcontent1 += "<th>主键</th>";
                            dictcontent1 += "<th>自增</th>";
                            dictcontent1 += "<th>空</th>";
                            dictcontent1 += "<th>备注</th>";
                            TableInfo TI = DBInfoList.Where(a => a.DBName == DBName).First().TableList.Where(b => b.TableName == SI).First();
                            if (TI != null)
                            {
                                foreach (FieldInfo FI in TI.FieldList)
                                {
                                    dictcontent1 += "<tr>";
                                    dictcontent1 += "<td>" + FI.FieldName + "</td>";
                                    dictcontent1 += "<td>" + FI.FieldType + "</td>";
                                    dictcontent1 += "<td>" + ((FI.IsPrimaryKey) ? "√" : "") + "</td>";
                                    dictcontent1 += "<td>" + ((FI.IsPrimaryKey) ? "√" : "") + "</td>";
                                    dictcontent1 += "<td>" + ((FI.IsNullable) ? "√" : "") + "</td>";
                                    dictcontent1 += "<td></td>";
                                    dictcontent1 += "</tr>";
                                }
                            }
                            dictcontent1 += "</table></div>\r\n";
                        }
                        break;
                    case "mysql":
                        MySql.Data.MySqlClient.MySqlConnection MySqlConn = GetMySQLConnection();
                        MySqlConn.Open();
                        MySql.Data.MySqlClient.MySqlCommand MySqlCmd = MySqlConn.CreateCommand();
                        foreach (string SI in OC0)
                        {
                            tablecontent += "\r\n" + SI;
                            DBInfoList.Find(p1 => p1.DBName == DBName).TableList.Find(p2 => p2.TableName == SI).FieldList = new List<FieldInfo>();

                            dictcontent1 += "\r\n<div class='t'>Table:" + SI + "</div>\r\n";
                            dictcontent1 += "<div class='t1'><table>\r\n";
                            dictcontent1 += "<tr>";
                            dictcontent1 += "<th>列名</th>";
                            dictcontent1 += "<th>类型</th>";
                            dictcontent1 += "<th>自增</th>";
                            dictcontent1 += "<th>主键</th>";
                            dictcontent1 += "<th>空</th>";
                            dictcontent1 += "<th>默认值</th>";
                            dictcontent1 += "<th>字符集</th>";
                            dictcontent1 += "<th>排序规则</th>";
                            dictcontent1 += "<th>备注</th>";
                            dictcontent1 += "</tr>\r\n";
                            string sqlm0 = "select * from information_schema.`COLUMNS` where TABLE_SCHEMA='" + DBName + "' and TABLE_NAME='" + SI + "'";
                            MySqlCmd.CommandText = sqlm0;
                            MySql.Data.MySqlClient.MySqlDataReader Dr1 = MySqlCmd.ExecuteReader();
                            while (Dr1.Read())
                            {
                                if (Dr1["EXTRA"].ToString() == "auto_increment")
                                {
                                    DBInfoList.Find(p1 => p1.DBName == DBName).TableList.Find(p2 => p2.TableName == SI).TableKeyField = Dr1["COLUMN_NAME"].ToString();
                                }
                                FieldInfo FI1 = new FieldInfo();
                                FI1.FieldLength = "";
                                FI1.FieldName = Dr1["COLUMN_NAME"].ToString();
                                FI1.FieldNote = Dr1["COLUMN_COMMENT"].ToString();
                                FI1.FieldType = Dr1["DATA_TYPE"].ToString();
                                //FI1.FieldTypeSharp = TypeSharpConvert.Convert(Dr1["DATA_TYPE"].ToString()); // C#实体生成器用
                                FI1.FieldDefaultValue = Dr1["COLUMN_DEFAULT"].ToString();
                                DBInfoList.Find(p1 => p1.DBName == DBName).TableList.Find(p2 => p2.TableName == SI).FieldList.Add(FI1);

                                dictcontent1 += "<tr>";
                                dictcontent1 += "<td>" + Dr1["COLUMN_NAME"].ToString() + "</td>";
                                dictcontent1 += "<td>" + Dr1["COLUMN_TYPE"].ToString() + "</td>";
                                dictcontent1 += "<td>" + (Dr1["EXTRA"].ToString().Contains("auto_increment") ? "√" : "") + "</td>";
                                dictcontent1 += "<td>" + (Dr1["COLUMN_KEY"].ToString().Contains("PRI") ? "√" : "") + "</td>";
                                dictcontent1 += "<td>" + Dr1["IS_NULLABLE"].ToString().Replace("YES", "√").Replace("NO", "") + "</td>";
                                dictcontent1 += "<td>" + Dr1["COLUMN_DEFAULT"].ToString() + "</td>";
                                dictcontent1 += "<td>" + Dr1["CHARACTER_SET_NAME"].ToString() + "</td>";
                                dictcontent1 += "<td>" + Dr1["COLLATION_NAME"].ToString() + "</td>";
                                dictcontent1 += "<td>" + Dr1["COLUMN_COMMENT"].ToString() + "</td>";
                                dictcontent1 += "</tr>\r\n";
                            }
                            dictcontent1 += "</table></div>";
                            Dr1.Dispose();
                        }
                        MySqlCmd.Dispose();
                        MySqlConn.Close();
                        break;
                    case "sql server":
                    case "sqlserver":
                        System.Data.SqlClient.SqlConnection sqlconn = GetSqlConnection();
                        sqlconn.Open();
                        System.Data.SqlClient.SqlCommand sqlcmd = sqlconn.CreateCommand();

                        if (IsOldVersion)
                        {
                            // sql 2000 的处理方式
                            foreach (string si in OC0)
                            {
                                tablecontent += "\r\n" + si;
                                DBInfoList.Find(p1 => p1.DBName == DBName).TableList.Find(p2 => p2.TableName == si).FieldList = new List<FieldInfo>();
                                dictcontent1 += "\r\n<div class='t'>Table:" + si + "</div>\r\n";
                                dictcontent1 += "<div class='t1'><table>\r\n";
                                dictcontent1 += "<tr>";
                                dictcontent1 += "<th>列名</th>";
                                dictcontent1 += "<th>类型</th>";
                                dictcontent1 += "<th>自增</th>";
                                dictcontent1 += "<th>主键</th>";
                                //dictcontent1 += "<th>索引</th>";
                                //dictcontent1 += "<th>排序</th>";
                                dictcontent1 += "<th>空</th>";
                                dictcontent1 += "<th>默认值</th>";
                                dictcontent1 += "<th>备注</th>";
                                dictcontent1 += "</tr>\r\n";
                                string sqlt1 = "use " + DBName + ";";

                                sqlt1 += "SELECT TOP 100 PERCENT a.colorder, d.name AS TableName, a.name AS ColumnName, ";
                                sqlt1 += "CASE WHEN COLUMNPROPERTY(a.id, a.name, 'IsIdentity') = 1 THEN '√' ELSE '' END AS IsIdentity,  ";
                                sqlt1 += "CASE WHEN EXISTS (SELECT 1 FROM dbo.sysindexes si  ";
                                sqlt1 += "INNER JOIN dbo.sysindexkeys sik ON si.id = sik.id AND si.indid = sik.indid INNER JOIN ";
                                sqlt1 += "dbo.syscolumns sc ON sc.id = sik.id AND sc.colid = sik.colid INNER JOIN ";
                                sqlt1 += "dbo.sysobjects so ON so.name = si.name AND so.xtype = 'PK' ";
                                sqlt1 += "WHERE sc.id = a.id AND sc.colid = a.colid) THEN '√' ELSE '' END AS isPK,  ";
                                sqlt1 += "b.name AS TypeName,  [Length]=CASE WHEN b.name='nvarchar' THEN a.length/2 ELSE a.length END, ";
                                sqlt1 += "COLUMNPROPERTY(a.id, a.name, 'PRECISION')  ";
                                sqlt1 += "AS Preci, ISNULL(COLUMNPROPERTY(a.id, a.name, 'Scale'), 0) AS Scale,  ";
                                sqlt1 += "CASE WHEN a.isnullable = 1 THEN '√' ELSE '' END AS cisNull, ISNULL(e.text, '') AS defaultVal,  ";
                                sqlt1 += "ISNULL(g.[value], '') AS deText, d.crdate AS Create_Date,  ";
                                sqlt1 += "CASE WHEN a.colorder = 1 THEN d.refdate ELSE NULL END AS Modify_Date ";
                                sqlt1 += "FROM dbo.syscolumns a LEFT OUTER JOIN ";
                                sqlt1 += "dbo.systypes b ON a.xtype = b.xusertype INNER JOIN ";
                                sqlt1 += "dbo.sysobjects d ON a.id = d.id AND d.xtype = 'U' AND  ";
                                sqlt1 += "d.status >= 0 LEFT OUTER JOIN ";
                                sqlt1 += " dbo.syscomments e ON a.cdefault = e.id LEFT OUTER JOIN ";
                                sqlt1 += " dbo.sysproperties g ON a.id = g.id AND a.colid = g.smallid AND  ";
                                sqlt1 += " g.name = 'MS_Description' LEFT OUTER JOIN ";
                                sqlt1 += " dbo.sysproperties f ON d.id = f.id AND f.smallid = 0 AND  ";
                                sqlt1 += " f.name = 'MS_Description' ";
                                sqlt1 += " where d.name=N'" + si + "' ";
                                sqlt1 += " ORDER BY d.name, a.colorder ";

                                sqlcmd.CommandText = sqlt1;
                                System.Data.SqlClient.SqlDataReader dr = sqlcmd.ExecuteReader();
                                while (dr.Read())
                                {
                                    if (dr["isPK"].ToString() != "")
                                    {
                                        DBInfoList.Find(p1 => p1.DBName == DBName).TableList.Find(p2 => p2.TableName == si).TableKeyField = dr["ColumnName"].ToString();
                                    }
                                    FieldInfo FI1 = new FieldInfo();
                                    FI1.FieldLength = dr["Length"].ToString();
                                    FI1.FieldScale = dr["Scale"].ToString();
                                    FI1.FieldName = dr["ColumnName"].ToString();
                                    FI1.FieldNote = dr["deText"].ToString();
                                    FI1.FieldType = dr["TypeName"].ToString();
                                    //FI1.FieldTypeSharp = TypeSharpConvert.Convert(dr["TypeName"].ToString());
                                    FI1.FieldDefaultValue = dr["defaultVal"].ToString();
                                    if (!String.IsNullOrEmpty(FI1.FieldName))
                                    {
                                        DBInfoList.Find(p1 => p1.DBName == DBName).TableList.Find(p2 => p2.TableName == si).FieldList.Add(FI1);
                                    }
                                    dictcontent1 += "<tr>";
                                    dictcontent1 += "<td>" + dr["ColumnName"].ToString() + "</td>";
                                    dictcontent1 += "<td>" + dr["TypeName"].ToString() + "(" + dr["Length"].ToString();
                                    if (dr["TypeName"].ToString().ToLower() == "decimal")
                                    {
                                        dictcontent1 += "," + dr["Scale"].ToString();
                                    }
                                    dictcontent1 += ")" + "</td>";
                                    dictcontent1 += "<td>" + dr["IsIdentity"].ToString() + "</td>";
                                    dictcontent1 += "<td>" + dr["isPK"].ToString() + "</td>";
                                    //dictcontent1 += "<td>" + dr["IndexName"].ToString() + "</td>";
                                    //dictcontent1 += "<td>" + dr["IndexSort"].ToString() + "</td>";
                                    dictcontent1 += "<td>" + dr["cisNull"].ToString() + "</td>";
                                    dictcontent1 += "<td>" + dr["defaultVal"].ToString() + "</td>";
                                    dictcontent1 += "<td>" + dr["deText"].ToString() + "</td>";
                                    dictcontent1 += "</tr>\r\n";
                                }
                                dr.Dispose();
                                dr.Close();
                                dictcontent1 += "</table></div>";
                            }
                        }
                        else
                        {
                            foreach (string si in OC0)
                            {
                                tablecontent += "\r\n" + si;
                                DBInfoList.Find(p1 => p1.DBName == DBName).TableList.Find(p2 => p2.TableName == si).FieldList = new List<FieldInfo>();
                                dictcontent1 += "\r\n<div class='t'>Table:" + si + "</div>\r\n";
                                dictcontent1 += "<div class='t1'><table>\r\n";
                                dictcontent1 += "<tr>";
                                dictcontent1 += "<th>列名</th>";
                                dictcontent1 += "<th>类型</th>";
                                dictcontent1 += "<th>自增</th>";
                                dictcontent1 += "<th>主键</th>";
                                //dictcontent1 += "<th>索引</th>";
                                dictcontent1 += "<th>排序</th>";
                                dictcontent1 += "<th>空</th>";
                                dictcontent1 += "<th>默认值</th>";
                                dictcontent1 += "<th>备注</th>";
                                dictcontent1 += "</tr>\r\n";
                                string sqlt1 = "use " + DBName + ";SELECT colorder=C.column_id,ColumnName=C.name,TypeName=T.name, Length=CASE WHEN T.name='nchar'";
                                sqlt1 += "THEN C.max_length/2 WHEN T.name='nvarchar' THEN C.max_length/2 ELSE C.max_length END,Preci=C.precision, ";
                                sqlt1 += "Scale=C.scale, IsIdentity=CASE WHEN C.is_identity=1 THEN N'√'ELSE N'' END,isPK=ISNULL(IDX.PrimaryKey,N''),";
                                sqlt1 += "Computed=CASE WHEN C.is_computed=1 THEN N'√'ELSE N'' END, IndexName=ISNULL(IDX.IndexName,N''), ";
                                sqlt1 += "IndexSort=ISNULL(IDX.Sort,N''), Create_Date=O.Create_Date, Modify_Date=O.Modify_date, cisNull=CASE WHEN C.is_nullable=1 THEN N'√'ELSE N'' END, ";
                                sqlt1 += " defaultVal=ISNULL(D.definition,N''), deText=ISNULL(PFD.[value],N'') ";
                                sqlt1 += " FROM sys.columns C INNER JOIN sys.objects O ON C.[object_id]=O.[object_id] AND (O.type='U' or O.type='V') AND O.is_ms_shipped=0 INNER JOIN sys.types T ON C.user_type_id=T.user_type_id LEFT JOIN ";
                                sqlt1 += "sys.default_constraints D ON C.[object_id]=D.parent_object_id AND C.column_id=D.parent_column_id AND C.default_object_id=D.[object_id] LEFT JOIN sys.extended_properties PFD ON PFD.class=1  AND ";
                                sqlt1 += "C.[object_id]=PFD.major_id  AND C.column_id=PFD.minor_id LEFT JOIN sys.extended_properties PTB ON PTB.class=1 AND PTB.minor_id=0  AND C.[object_id]=PTB.major_id LEFT JOIN ( SELECT  IDXC.[object_id], ";
                                sqlt1 += "IDXC.column_id, Sort=CASE INDEXKEY_PROPERTY(IDXC.[object_id],IDXC.index_id,IDXC.index_column_id,'IsDescending') WHEN 1 THEN 'DESC' WHEN 0 THEN 'ASC' ELSE '' END, PrimaryKey=CASE WHEN IDX.is_primary_key=1 THEN ";
                                sqlt1 += "N'√'ELSE N'' END, IndexName=IDX.Name FROM sys.indexes IDX INNER JOIN sys.index_columns IDXC ON IDX.[object_id]=IDXC.[object_id] AND IDX.index_id=IDXC.index_id LEFT JOIN sys.key_constraints KC ON ";
                                sqlt1 += "IDX.[object_id]=KC.[parent_object_id] AND IDX.index_id=KC.unique_index_id INNER JOIN  ( SELECT [object_id], Column_id, index_id=MIN(index_id) FROM sys.index_columns GROUP BY [object_id], Column_id ) IDXCUQ ON ";
                                sqlt1 += "IDXC.[object_id]=IDXCUQ.[object_id] AND IDXC.Column_id=IDXCUQ.Column_id AND IDXC.index_id=IDXCUQ.index_id ) IDX ON C.[object_id]=IDX.[object_id] AND C.column_id=IDX.column_id  WHERE O.name=N'" + si + "' ";
                                sqlt1 += "ORDER BY O.name,C.column_id  ";

                                sqlcmd.CommandText = sqlt1;
                                System.Data.SqlClient.SqlDataReader dr = sqlcmd.ExecuteReader();
                                while (dr.Read())
                                {
                                    if (dr["isPK"].ToString() != "")
                                    {
                                        DBInfoList.Find(p1 => p1.DBName == DBName).TableList.Find(p2 => p2.TableName == si).TableKeyField = dr["ColumnName"].ToString();
                                    }
                                    FieldInfo FI1 = new FieldInfo();
                                    FI1.FieldLength = dr["Length"].ToString();
                                    FI1.FieldScale = dr["Scale"].ToString();
                                    FI1.FieldName = dr["ColumnName"].ToString();
                                    FI1.FieldNote = dr["deText"].ToString();
                                    FI1.FieldType = dr["TypeName"].ToString();
                                    //FI1.FieldTypeSharp = TypeSharpConvert.Convert(dr["TypeName"].ToString());
                                    FI1.FieldDefaultValue = dr["defaultVal"].ToString();
                                    if (!String.IsNullOrEmpty(FI1.FieldName))
                                    {
                                        DBInfoList.Find(p1 => p1.DBName == DBName).TableList.Find(p2 => p2.TableName == si).FieldList.Add(FI1);
                                    }
                                    dictcontent1 += "<tr>";
                                    dictcontent1 += "<td>" + dr["ColumnName"].ToString() + "</td>";
                                    dictcontent1 += "<td>" + dr["TypeName"].ToString() + "(" + dr["Length"].ToString();
                                    if (dr["TypeName"].ToString().ToLower() == "decimal")
                                    {
                                        dictcontent1 += "," + dr["Scale"].ToString();
                                    }
                                    dictcontent1 += ")" + "</td>";
                                    dictcontent1 += "<td>" + dr["IsIdentity"].ToString() + "</td>";
                                    dictcontent1 += "<td>" + dr["isPK"].ToString() + "</td>";
                                    //dictcontent1 += "<td>" + dr["IndexName"].ToString() + "</td>";
                                    dictcontent1 += "<td>" + dr["IndexSort"].ToString() + "</td>";
                                    dictcontent1 += "<td>" + dr["cisNull"].ToString() + "</td>";
                                    dictcontent1 += "<td>" + dr["defaultVal"].ToString() + "</td>";
                                    dictcontent1 += "<td>" + dr["deText"].ToString() + "</td>";
                                    dictcontent1 += "</tr>\r\n";
                                }
                                dr.Dispose();
                                dr.Close();
                                dictcontent1 += "</table></div>";
                            }
                        }
                        sqlcmd.Dispose();
                        sqlconn.Close();
                        break;
                    default:
                        MessageBox.Show("不确定的数据库类型", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        break;
                }
                if (this.dbtype == "sqlite")
                {
                }
                else
                {
                    
                }
                dictcontent = dictcontent.Replace("[DBDICT]", dictcontent1);
                dictcontent = dictcontent.Replace("[TABLECOUNT]", DBInfoList.Find(p1 => p1.DBName == DBName).TableList.Count.ToString());
                dictcontent = dictcontent.Replace("[TABLELISTCOUNT]", this.CountTableSelected.ToString());

                string SavePath = this.txtDictPath.Text.ToString();
                SavePath = String.IsNullOrEmpty(SavePath) ? "./dict/" : SavePath;
                FileUtil.CreateFile(SavePath + DBName + ".html", dictcontent);
                FileUtil.CreateFile(SavePath + DBName + "_tables.txt", tablecontent);

                //List<TableInfo> TIList2 = DBInfoList.Find(p1 => p1.DBName == DBName).TableList;
                
                /*
                 * // 实体生成处理
                foreach (TableInfo TI2 in TIList2)
                {
                    if (!String.IsNullOrEmpty(TI2.TableName))
                    {
                        //Builder.BuildPHP1.Build(txtPath.Text.Trim() + "php1/" + DBName + "/", TI2);
                    }
                }
                */
            }
        }

        /// <summary>
        /// >> 增加选择全部
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            this.lstTableSelected.Items.AddRange(this.lstTable.Items);
            this.lstTable.Items.Clear();
            this.CountTableSelected = this.lstTableSelected.Items.Count;
            this.toolStripStatusLabel2.Text=VersionInfo+",表数量:" + this.CountTableListed+", 已选择:" + this.CountTableSelected;
        }

        /// <summary>
        /// 增加选择一个
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            if (this.lstTable.SelectedItems.Count > 0)
            {
                this.lstTableSelected.Items.Add(this.lstTable.SelectedItem);
                this.lstTable.Items.RemoveAt(this.lstTable.SelectedIndex);
                this.CountTableSelected = this.lstTableSelected.Items.Count;
                this.toolStripStatusLabel2.Text = VersionInfo + ",表数量:" + this.CountTableListed + ", 已选择:" + this.CountTableSelected;
            }
            this.lstTable.Sorted = true;
            this.lstTableSelected.Sorted = true;
        }

        /// <summary>
        /// 取消选择一个
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click(object sender, EventArgs e)
        {
            if (this.lstTableSelected.SelectedItems.Count > 0)
            {
                this.lstTable.Items.Add(this.lstTableSelected.SelectedItem);
                this.lstTableSelected.Items.RemoveAt(this.lstTableSelected.SelectedIndex);
                this.CountTableSelected = this.lstTableSelected.Items.Count;
                this.toolStripStatusLabel2.Text = VersionInfo + ",表数量:" + this.CountTableListed + ", 已选择:" + this.CountTableSelected;
            }
            this.lstTable.Sorted = true;
            this.lstTableSelected.Sorted = true;
        }

        /// <summary>
        /// 全部取消选择
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button4_Click(object sender, EventArgs e)
        {
            this.lstTable.Items.AddRange(this.lstTableSelected.Items);
            this.lstTableSelected.Items.Clear();
            this.CountTableSelected = this.lstTableSelected.Items.Count;
            this.toolStripStatusLabel2.Text = VersionInfo + ",表数量:" + this.CountTableListed + ", 已选择:" + this.CountTableSelected;
        }


        private System.Data.SqlClient.SqlConnection GetSqlConnection()
        {
            System.Data.SqlClient.SqlConnection Conn = new System.Data.SqlClient.SqlConnection();
            if (this.dbhost.Contains("\\"))
            {
                // 指定实例的，可能为非默认实例，由服务器指定端口。
                Conn.ConnectionString = "Data Source=" + this.dbhost + ";User ID=" + this.dbuser + ";Password=" + this.dbpwd;
            }
            else
            {
                Conn.ConnectionString = "Data Source=" + this.dbhost + "," + this.dbport + ";User ID=" + this.dbuser + ";Password=" + this.dbpwd;
            }
            try
            {
                // Conn.Open();
            }
            catch
            {
                this.btnDisconnect_Click(null, null);
                return null;
            }
            return Conn;
        }

        private MySql.Data.MySqlClient.MySqlConnection GetMySQLConnection()
        {
            MySql.Data.MySqlClient.MySqlConnection Conn = new MySql.Data.MySqlClient.MySqlConnection();
            Conn.ConnectionString = String.Format("server={0};uid={1};pwd={2};port={3}", this.dbhost, this.dbuser, this.dbpwd, this.dbport);
            try
            {
                //Conn.Open();
            }
            catch
            {
                this.btnDisconnect_Click(null, null);
                return null;
            }
            return Conn;
        }

        private Mono.Data.Sqlite.SqliteConnection GetSqliteConnection()
        {
            Mono.Data.Sqlite.SqliteConnection Conn = new Mono.Data.Sqlite.SqliteConnection();
            Conn.ConnectionString = String.Format("Data Source={0}", this.dbhost);
            if (!String.IsNullOrEmpty(this.dbpwd))
            {
                Conn.SetPassword(this.dbpwd);
            }
            return Conn;
        }

    }
}
