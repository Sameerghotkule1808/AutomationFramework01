using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;

namespace AutomationPOC
{
    public static class DataSources
    {


        public static List<Login_Credentials> GetLogin_Credentials_TD()
        {
            var result = ExcelDataMapper<Login_Credentials>("..\\..\\..\\Data\\LoginData.xlsx", "Sheet1");
            return result;
        }



        #region Helper Methods


        private static List<T> ExcelDataMapper<T>(string filePath, string sheetName) where T : new()
        {
            List<T> data = new List<T>();
            try
            {
                data = ExcelDataFetch<T>(filePath, sheetName);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return data;
        }

        private static List<T> ExcelDataFetch<T>(string path, string sheetName) where T : new()
        {
            List<T> assets = new List<T>();
            try
            {
                if (File.Exists(path))
                {
                    FileInfo fi = new FileInfo(path);
                    if (fi.Extension.ToUpper() == ".XLSX")
                    {
                        DataTable data = ExcelHelper.GetDataTableFromExcelFile(path, sheetName);

                        foreach (DataRow row in data.Rows)
                        {
                            if (row.IsEmpty())
                                continue;

                            T item = new T();

                            foreach (DataColumn c in row.Table.Columns)
                            {
                                // find the property for the column
                                PropertyInfo p = item.GetType().GetProperty(c.ColumnName);

                                // if exists, set the value
                                if (p != null && row[c] != DBNull.Value)
                                {
                                    try
                                    {
                                        p.SetValue(item, Convert.ChangeType(row[c], p.PropertyType), null);
                                    }
                                    catch (Exception ex)
                                    {
                                        Console.WriteLine(ex.Message);
                                        continue;
                                    }
                                }
                            }

                            assets.Add(item);
                        }
                    }
                    else
                    {
                        Console.WriteLine($"Un supported file format : {fi.Extension.ToUpper()}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return assets;
        }

        private static bool IsEmpty(this DataRow row)
        {
            return row == null || row.ItemArray.All(i => i.IsNullEquivalent());
        }

        private static bool IsNullEquivalent(this object value)
        {
            return value == null
                   || value is DBNull
                   || string.IsNullOrWhiteSpace(value.ToString());
        }

        public static object MapObjects(object source, object target)
        {
            try
            {
                foreach (PropertyInfo sourceProp in source.GetType().GetProperties())
                {
                    PropertyInfo targetProp = target.GetType().GetProperties().Where(p => p.Name == sourceProp.Name).FirstOrDefault();
                    if (targetProp != null && targetProp.GetType().Name == sourceProp.GetType().Name)
                    {
                        targetProp.SetValue(target, sourceProp.GetValue(source));
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return target;
        }

        #endregion Helper Methods
    }

    public class Login_Credentials
    {
        public string UserName { get; set; }
        public string Password { get; set; }

    }

}