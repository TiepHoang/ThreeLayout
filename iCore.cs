using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThreeLayoutVer2._0.Core
{
    public class iCore
    {
        public List<string> GetNameDatabase(string nameServer)
        {
            try
            {
                var dt = new Provider().GetDataFromQuery("EXEC sp_databases");
                if (dt == null || dt.Rows.Count <= 0) return null;
                List<string> lst = new List<string>();
                foreach (DataRow item in dt.Rows)
                {
                    lst.Add((string)item["DATABASE_NAME"]);
                }
                return lst;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<string> GetTableName(string nameServer, string nameDatabase)
        {
            try
            {
                var dt = new Provider().GetDataFromQuery("SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES");
                if (dt == null || dt.Rows.Count <= 0) return null;
                List<string> lst = new List<string>();
                foreach (DataRow item in dt.Rows)
                {
                    lst.Add((string)item["TABLE_NAME"]);
                }
                return lst;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<ColumnInfo> GetInfoTable(string nameTable)
        {
            var dt = new Provider().GetDataFromQuery(string.Format(@"select COLUMN_NAME , DATA_TYPE , CHARACTER_MAXIMUM_LENGTH ,  COLUMNPROPERTY(object_id(TABLE_SCHEMA+'.'+TABLE_NAME), COLUMN_NAME, 'IsIdentity') as IsIdentity
                                                        from INFORMATION_SCHEMA.COLUMNS
                                                        where TABLE_NAME = N'{0}'", nameTable));
            if (dt == null || dt.Rows.Count <= 0) return null;
            List<ColumnInfo> lst = new List<ColumnInfo>();
            var ListKey = GetKeyOfTable(nameTable);
            foreach (DataRow item in dt.Rows)
            {
                int d = 0;
                var ob = new ColumnInfo();
                ob.Name = item["COLUMN_NAME"].ToString();
                ob.Type = item["DATA_TYPE"].ToString();
                ob.Length = int.TryParse(item["CHARACTER_MAXIMUM_LENGTH"].ToString(), out d) ? d.ToString() : null;
                ob.isIdentity = item["IsIdentity"].ToString().Equals("1");
                ob.isKey = ListKey != null && ListKey.Any(q => q.Equals(ob.Name));
                lst.Add(ob);
            }
            return lst;
        }

        public List<string> GetKeyOfTable(string nameTable)
        {
            var dt = new Provider().GetDataFromQuery(string.Format(@"SELECT Col.Column_Name from 
    INFORMATION_SCHEMA.TABLE_CONSTRAINTS Tab,
    INFORMATION_SCHEMA.CONSTRAINT_COLUMN_USAGE Col
WHERE
    Col.Constraint_Name = Tab.Constraint_Name
    AND Col.Table_Name = Tab.Table_Name
    AND Constraint_Type = 'PRIMARY KEY'
    AND Col.Table_Name = N'{0}' ", nameTable));
            if (dt == null || dt.Rows.Count <= 0) return null;
            List<string> lst = new List<string>();
            foreach (DataRow item in dt.Rows)
            {
                lst.Add(item[0].ToString());
            }
            return lst;
        }

        public string GetProcInsert(string nameTable)
        {
            var lst = GetInfoTable(nameTable);
            if (lst == null || lst.Count <= 0 || !lst.Any(d => d.isKey)) return null;
            string param = null, values = null, col = null;
            bool isFirst = true;
            foreach (var item in lst)
            {
                if (!item.isIdentity)
                {
                    param += string.Format("{3} @{0} {1}{2}", item.Name, item.Type, item.Length == null ? "" : string.Format("({0})", item.Length), isFirst ? " " : " , ");
                    values += string.Format("{1} @{0}", item.Name, isFirst ? "" : ",");
                    col += string.Format("{1}[{0}]", item.Name, isFirst ? "" : ",");
                    isFirst = false;
                }
            }
            return string.Format(@"CREATE PROC sp_{0}_INSERT
{1}
as BEGIN
    INSERT INTO [{0}]( {2} ) 
    VALUES({3})
END", nameTable, param, col, values);
        }

        public string GetProcDelete(string nameTable)
        {
            var lst = GetInfoTable(nameTable);
            if (lst == null || lst.Count <= 0 || !lst.Any(d => d.isKey)) return null;
            string param = null, values = null;
            bool isFirst = true;
            foreach (var item in lst)
            {
                if (item.isKey)
                {
                    param += string.Format("{3} @{0} {1}{2}", item.Name, item.Type, item.Length == null ? "" : string.Format("({0})", item.Length), isFirst ? " " : " , ");
                    values += string.Format(" {1} [{0}] = @{0}", item.Name, isFirst ? "" : "AND");
                    isFirst = false;
                }
            }
            return string.Format(@"CREATE PROC sp_{0}_DELETE
{1}
AS BEGIN 
DELETE [{0}] WHERE {2}
END", nameTable, param, values);
        }

        public string GetProcUpdate(string nameTable)
        {
            var lst = GetInfoTable(nameTable);
            if (lst == null || lst.Count <= 0 || !lst.Any(d => d.isKey)) return null;
            string param = null, values = null, where = null;
            bool firstValue = true, firstKey = true, firstParam = true;
            foreach (var item in lst)
            {
                param += string.Format("{3} @{0} {1}{2}", item.Name, item.Type, item.Length == null ? "" : string.Format("({0})", item.Length), firstParam ? " " : " , ");
                firstParam = false;
                if (item.isKey)
                {
                    where = string.Format(" {1} [{0}] = @{0} ", item.Name, firstKey ? "" : "AND");
                    firstKey = false;
                }
                else
                {
                    values += string.Format("{1} [{0}] = @{0} ", item.Name, firstValue ? "" : ",");
                    firstValue = false;
                }
            }
            return string.Format(@"CREATE PROC sp_{0}_UPDATE
{1}
as BEGIN
    UPDATE [{0}] SET {2}  WHERE {3}
END", nameTable, param, values, where);
        }

        public string GetProcGetAll(string nameTable)
        {
            return string.Format(@" CREATE PROC sp_{0}_GetAll AS SELECT * FROM [{0}] ", nameTable);
        }

        public Dictionary<string, string> GetProcGetBy(string nameTable)
        {
            var lst = GetInfoTable(nameTable);
            if (lst == null || lst.Count <= 0 || !lst.Any(d => d.isKey)) return null;
            string r = null;
            int countKey = 0;
            string paramForMoreKey = "", whereForMoreKey = "";
            Dictionary<string, string> dic = new Dictionary<string, string>();
            foreach (var item in lst)
            {
                if (!item.isKey) continue;
                countKey++;
                string k = string.Format("GetBy{0}", item.Name);
                r = string.Format(@"; CREATE PROC sp_{0}_GetBy{1} @{1} {2}{3} 
AS SELECT * FROM [{0}] WHERE [{1}] = @{1} ;", nameTable, item.Name, item.Type, item.Length != null ? string.Format("({0})", item.Length) : "");
                dic.Add(k, r);

                paramForMoreKey += string.Format(" {0} @{1} {2}{3} ", countKey > 1 ? "," : "", item.Name, item.Type, item.Length != null ? string.Format("({0})", item.Length) : "");
                whereForMoreKey += string.Format(" {0} [{1}] = @{1}", countKey > 1 ? " AND " : "", item.Name, item.Name);
            }
            if (countKey > 1)
            {
                r = string.Format(@"; CREATE PROC sp_{0}_GetByAllKey {1} 
AS SELECT * FROM [{0}] WHERE {2} ; go", nameTable, paramForMoreKey, whereForMoreKey);
                dic.Add("GetByAllKey", r);
            }
            return dic;
        }

        public string GetType(ColumnInfo info)
        {
            string type = "";
            switch (info.Type)
            {
                case "int":
                    type = info.isKey ? "int" : "Nullable<int>";
                    break;
                case "uniqueidentifier":
                    type = info.isKey ? "Guid" : "Nullable<Guid>";
                    break;
                case "datetime":
                    type = info.isKey ? "DateTime" : "Nullable<DateTime>";
                    break;
                case "bit":
                    type = info.isKey ? "bool" : "Nullable<bool>";
                    break;
                default:
                    type = "string";
                    break;
            }
            return type;
        }

        public string GetDTO(string nameTable)
        {
            var lst = GetInfoTable(nameTable);
            if (lst == null && lst.Count <= 0) return null;
            string r = null;
            foreach (var item in lst)
            {
                string type = null;
                switch (item.Type)
                {
                    case "int":
                        type = item.isKey ? "int" : "Nullable<int>";
                        r += string.Format(" public {0} {1} {2} get; set; {3} ", type, item.Name, "{", "}");
                        break;
                    case "uniqueidentifier":
                        type = item.isKey ? "Guid" : "Nullable<Guid>";
                        r += string.Format(" public {0} {1} {2} get; set; {3} ", type, item.Name, "{", "}");
                        break;
                    case "datetime":
                        type = item.isKey ? "DateTime" : "Nullable<DateTime>";
                        r += string.Format(" public {0} {1} {2} get; set; {3} ", type, item.Name, "{", "}");
                        break;
                    case "bit":
                        type = item.isKey ? "bool" : "Nullable<bool>";
                        r += string.Format(" public {0} {1} {2} get; set; {3} ", type, item.Name, "{", "}");
                        break;
                    default:
                        r += string.Format(" public {0} {1} {2} get; set; {3} ", "string", item.Name, "{", "}");
                        break;
                }
            }

            Setting setting = new Setting();
            string nameDto = setting.extentionDto;
            string namespaceDto = setting.namespaceDto;
            try
            {
                setting = new XmlSetting().Read();
                nameDto = setting.ExtentionDto;
                namespaceDto = setting.NamespaceDto;
            }
            catch (Exception)
            { }

            return string.Format(@"
using System;
namespace {4}
{0}
    public class {2}{5}
    {0}
        {3}
    {1}
{1}
", "{", "}", nameTable, r, namespaceDto, nameDto);
        }

        public string GetBUS(string nameTable)
        {
            string GetByKey = string.Empty, Delete = string.Empty, valueGetByKey = null, paramDelete = null, valueDelete = null;
            var lst = GetInfoTable(nameTable);

            //GetByKey
            if (lst != null && lst.Count > 0 && lst.Any(d => d.isKey))
            {
                bool isFirst = true;
                foreach (var item in lst)
                {
                    if (item.isKey)
                    {
                        switch (item.Type)
                        {
                            case "int":
                                valueGetByKey = "int";
                                break;
                            case "uniqueidentifier":
                                valueGetByKey = "Guid";
                                break;
                            case "datetime":
                                valueGetByKey = "DateTime";
                                break;
                            default:
                                valueGetByKey = "string";
                                break;
                        }
                        valueGetByKey = string.Format("{0} {1}", valueGetByKey, item.Name);
                        paramDelete += (isFirst ? "" : ",") + valueGetByKey;
                        valueDelete += (isFirst ? "" : ",") + item.Name;
                        isFirst = false;
                        GetByKey += string.Format(@"
public {2}Dto GetBy{3}({4})
{0}
    return new {2}Dal().GetBy{3}({3});
{1}
", "{", "}", nameTable, item.Name, valueGetByKey);
                    }
                }

                //Delete
                Delete += string.Format(@"public int Delete({3})
{0}
    return new {2}Dal().Delete({4});
{1}
", "{", "}", nameTable, paramDelete, valueDelete);
            }

            Setting setting = new Setting();
            string nameDto = setting.extentionDto;
            string nameBus = setting.extentionBus;
            string namespaceBus = setting.namespaceBus;
            string namespaceDal = setting.namespaceDal;
            string namespaceDto = setting.namespaceDto;
            try
            {
                setting = new XmlSetting().Read();
                nameDto = setting.ExtentionDto;
                nameBus = setting.ExtentionBus;
                namespaceBus = setting.NamespaceBus;
                namespaceDal = setting.NamespaceDal;
                namespaceDto = setting.NamespaceDto;
            }
            catch (Exception)
            { }

            //param
            string paramUpdate = null, paramInsert = null, obInsert = null, obUpdate = null;
            //GetAll
            var lstColumn = GetInfoTable(nameTable);
            bool isFirstUpdate = true, isFirstInsert = true;
            foreach (var item in lstColumn)
            {
                paramUpdate += string.Format("{0} {1} {2} ", isFirstUpdate ? "" : ",", GetType(item), item.Name);
                paramInsert += item.isIdentity ? "" : string.Format("{0} {1} {2} ", isFirstInsert ? "" : ",", GetType(item), item.Name);

                obUpdate += string.Format(@"ob.{0} = {0};
", item.Name);
                if (!item.isIdentity)
                {
                    obInsert += string.Format(@"ob.{0} = {0};
", item.Name);
                    if (isFirstInsert) isFirstInsert = false;
                }
                isFirstUpdate = false;
            }


            string obDto = string.Format("{0}{1}", nameTable, nameDto);

            return string.Format(@"
using {8};
using {9};
using System;
using System.Collections.Generic;
namespace {7}
{0}
public class {2}{6}
{0}
    public List<{5}> GetAll()
{0}
    return new {2}Dal().GetAll();
{1}

{3}

public int Insert({10})
{0}
    {5} ob = new {5}();
    {12}
    return new {2}Dal().Insert(ob);
{1}

public int Update({11})
{0}
    {5} ob = new {5}();
    {13}
    return new {2}Dal().Update(ob);
{1}

{4}

{1}
{1}
", "{", "}", nameTable, GetByKey, Delete, obDto, nameBus, namespaceBus, namespaceDal, namespaceDto, paramInsert, paramUpdate, obInsert, obUpdate);
        }

        public string GetDAL(string nameTable, string nameDatabase)
        {
            //get Name Bus user want set name
            Setting setting = new Setting();
            string nameDal = setting.extentionDal;
            string nameDto = setting.extentionDto;
            string namespaceDal = setting.namespaceDal;
            string namespaceDto = setting.namespaceDto;
            try
            {
                setting = new XmlSetting().Read();
                nameDal = setting.ExtentionDal;
                nameDto = setting.ExtentionDto;
                namespaceDal = setting.NamespaceDal;
                namespaceDto = setting.NamespaceDto;
            }
            catch
            { }

            var lstColumn = GetInfoTable(nameTable);
            if (lstColumn == null || lstColumn.Count <= 0) return null;

            //ObjectDto
            string ObjectDto = string.Format("{0}{1}", nameTable, nameDto);


            //param
            string ParamUpdate = null;
            string ParamInsert = null;
            //GetAll
            string setValue = null;
            bool isFirstUpdate = true, isFirstInsert = true;
            foreach (var item in lstColumn)
            {
                setValue += string.Format("obj.{0} = item.{0}; ", item.Name);
                ParamUpdate += string.Format("{1} obj.{0} ", item.Name, isFirstUpdate ? "" : ",");
                ParamInsert += item.isIdentity ? "" : string.Format("{1} obj.{0} ", item.Name, isFirstInsert ? "" : ",");
                if (isFirstInsert && !item.isIdentity)
                {
                    isFirstInsert = false;
                }
                isFirstUpdate = false;
            }
            string GetAll = string.Format(@"
public List<{2}> GetAll()
{0}
    var list = new {4}Entities().sp_{3}_GetAll();
    List<{2}> lst = new List<{2}>();
    foreach (var item in list)
    {0}
        {2} obj = new {2}();
        {5}
        lst.Add(obj);
    {1}
    return lst;
{1}", "{", "}", ObjectDto, nameTable, nameDatabase, setValue);


            //GetBy
            string GetBy = null;
            if (lstColumn.Any(q => q.isKey))
            {
                foreach (var item in lstColumn)
                {
                    if (!item.isKey) continue;
                    string typeColumn = null;
                    switch (item.Type)
                    {
                        case "int":
                            typeColumn = "int";
                            break;
                        case "uniqueidentifier":
                            typeColumn = "Guid";
                            break;
                        case "datetime":
                            typeColumn = "DateTime";
                            break;
                        case "bit":
                            typeColumn = "bool";
                            break;
                        default:
                            typeColumn = "string";
                            break;
                    }
                    GetBy += string.Format(@"
public {3} GetBy{4}({5} {4})
{0}
    var lst = new {6}Entities().sp_{2}_GetBy{4}({4});
    {3} obj = new {3}();
    foreach (var item in lst)
    {0}
        {7}
    {1}
    return obj;
{1}
", "{", "}", nameTable, ObjectDto, item.Name, typeColumn, nameDatabase, setValue);
                }
            }


            //Insert
            string Insert = string.Format(@"
public int Insert({3} obj)
    {0}
        return new {4}Entities().sp_{2}_INSERT({5});
    {1}
", "{", "}", nameTable, ObjectDto, nameDatabase, ParamInsert);

            //Update
            string Update = string.Format(@"
public int Update({3} obj)
    {0}
        return new {4}Entities().sp_{2}_UPDATE({5});
    {1}
", "{", "}", nameTable, ObjectDto, nameDatabase, ParamUpdate);


            //Delete
            string Delete = null;
            var LstKey = GetKeyOfTable(nameTable);
            foreach (var item in lstColumn)
            {
                if (item.isKey)
                {
                    string typeColumn = null;
                    switch (item.Type)
                    {
                        case "int":
                            typeColumn = "int";
                            break;
                        case "uniqueidentifier":
                            typeColumn = "Guid";
                            break;
                        case "datetime":
                            typeColumn = "DateTime";
                            break;
                        case "bit":
                            typeColumn = "bool";
                            break;
                        default:
                            typeColumn = "string";
                            break;
                    }
                    typeColumn += " " + item.Name;

                    Delete += string.Format(@"
public int Delete{6}({3})
    {0}
        return new {4}Entities().sp_{2}_DELETE({5});
    {1}
", "{", "}", nameTable, typeColumn, nameDatabase, item.Name, LstKey.Count > 1 ? "_" + item.Name : "");
                }
            }


            return string.Format(@"
using {10};
using System.Collections.Generic;
namespace {9}
{0}
    public class {2}{3}
    {0}
        {4}
        {5}
        {6}
        {7}
        {8}
    {1}
{1}
", "{", "}", nameTable, nameDal, GetAll, GetBy, Insert, Update, Delete, namespaceDal, namespaceDto);
        }
    }

    public class ColumnInfo
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public string Length { get; set; }
        public bool isKey { get; set; }
        public bool isIdentity { get; set; }
    }
}
