using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DBDictGen.Entity;
using System.Text.RegularExpressions;

namespace DBDictGen.Builder
{
    public class Sqlite
    {
        public List<TableInfo> GetTables(Mono.Data.Sqlite.SqliteConnection Conn)
        {
            List<TableInfo> TList = new List<TableInfo>();
            try
            {
                Conn.Open();
                Mono.Data.Sqlite.SqliteCommand Cmd = Conn.CreateCommand();
                Cmd.CommandText = "select  * from sqlite_master where type='table' and name<>'sqlite_sequence'";
                Mono.Data.Sqlite.SqliteDataReader Dr = Cmd.ExecuteReader();
                while (Dr.Read())
                {
                    string StrSql = Dr["sql"].ToString().Replace("\r", "").Replace("\n", "").Replace("\r\n", "");
                    System.Diagnostics.Debug.WriteLine(StrSql);
                    if (!String.IsNullOrEmpty(StrSql))
                    {
                        string Pattern1 = "create\\s+table\\s+\\\"(?<tablename>.+?)\\\"\\s+\\((?<fields>.+?)\\)$";
                       // Pattern1 = @"create\s+table\s+\""(?<tablename>.+?)\""\s+\((?<fields>.+?)\)";
                        //System.Diagnostics.Debug.WriteLine(Pattern1);
                        Regex Reg1 = new Regex(Pattern1, RegexOptions.IgnoreCase | RegexOptions.Multiline);
                        Match M1 = Reg1.Match(StrSql.ToLower());
                        if (M1.Success)
                        {
                            string TName = M1.Groups["tablename"].Value;
                            string Fields = M1.Groups["fields"].Value;

                            TableInfo TInfo = new TableInfo();
                            TInfo.TableName = Dr["name"].ToString();
                            
                            List<FieldInfo> FList = new List<FieldInfo>();
                            string[] FStrArr = Fields.Split(',');
                            // 遍历字段描述语句，提出字段名，判断是为主键
                            foreach (string FStr in FStrArr)
                            {
                                string Pattern2 = "\\\"(?<fieldname>.+?)\\\"\\s+(?<desc>.+?)$";
                                //Pattern2 = @"\""(?<fieldname>.+?)\""\s+(?<desc>.+?)";
                                Regex Reg2 = new Regex(Pattern2);
                                Match M2 = Reg2.Match(FStr);
                                if (M2.Success)
                                {
                                    string FieldName = M2.Groups["fieldname"].Value.Trim();
                                    string FieldDesc = M2.Groups["desc"].Value.Trim().Replace(",", "");

                                    if (!String.IsNullOrEmpty(FieldName))
                                    {
                                        FieldInfo FInfo = new FieldInfo();
                                        FInfo.FieldName = FieldName;

                                        if (FieldDesc.ToLower().Contains(" primary key "))
                                        {
                                            TInfo.TableKeyField = FieldName;
                                            FInfo.IsPrimaryKey = true;
                                        }
                                        else
                                        {
                                            FInfo.IsPrimaryKey = false;
                                        }
                                        if (FieldDesc.ToLower().Contains("autoincrement"))
                                        {
                                            FInfo.IsAutoIncrement = true;
                                        }
                                        else
                                        {
                                            FInfo.IsAutoIncrement = false;
                                        }
                                        if (FieldDesc.ToLower().Contains("not null"))
                                        {
                                            FInfo.IsNullable = false;
                                        }
                                        else
                                        {
                                            FInfo.IsNullable = true;
                                        }
                                        if (!String.IsNullOrEmpty(FieldDesc))
                                        {
                                            string[] FDescArr = FieldDesc.Split(' ');
                                            if (FDescArr.Length > 1)
                                            {
                                                // 有多个描述，第一个为字段类型
                                            }
                                            FInfo.FieldType = FDescArr[0];
                                        }
                                        FList.Add(FInfo);
                                    }
                                }
                            }
                            TInfo.FieldList = FList;
                            TList.Add(TInfo);
                        }
                    }
                }
            }
            catch { }
            return TList;
        }
    }
}
