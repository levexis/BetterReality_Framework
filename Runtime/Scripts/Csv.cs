using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Reflection;
using System.Text;
using UnityEngine;
using Object = System.Object;

namespace BetterReality.Framework
{
    /// <summary>
    /// Creates a datatable of strings for outputting to csv
    /// Cells need to be explicitly cast for aggregate functions etc.
    /// </summary>
    public class Csv : DataFile
    {
        private List<string> _fieldNames;

        protected Type RowType { get; set; }

        public Csv(string fileName) : base(fileName)
        {
            FilePath += ".csv";
        }

        protected string CsvLine(DataRow rowData)
        {
            StringBuilder sb = new StringBuilder();
            foreach (object item in rowData.ItemArray)
            {
                string field = item.ToString();
                // Replace double quotes with double-double quotes and wrap the field in double quotes
                string formattedField = field.Replace("\"", "\"\"");
                sb.Append("\"" + formattedField + "\",");
            }

            sb.Remove(sb.Length - 1, 1); // Remove the trailing comma
            return sb.ToString();
        }

        protected string CsvHeader()
        {
            StringBuilder sb = new StringBuilder();
            // Write column headers
            foreach (DataColumn column in FileDataTable.Columns)
            {
                sb.Append("\"" + column.ColumnName.Replace("\"", "\"\"") + "\",");
            }

            sb.Remove(sb.Length - 1, 1); // Remove the trailing comma
            return sb.ToString();
        }  

        // Write data from DataTable to CSV file
        public override bool Save()
        {
            string filePath = FilePath; // in filename
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(CsvHeader()); // Add a new line

            // Write rows
            foreach (DataRow row in FileDataTable.Rows)
            {
                sb.Append(CsvLine(row));
                sb.AppendLine(); // Add a new line
            }

            File.WriteAllText(filePath, sb.ToString(), Encoding.UTF8);
            return true;
        }

        public override bool Load()
        {
            return Load(0);
        }

        // Read data from CSV file to DataTable
        public bool Load(int ignoreLines)
        {
            try
            {
                string filePath = FilePath; //out filename
                FileDataTable = new DataTable();

                if (!File.Exists(filePath))
                {
                    throw new Exception("File not found: " + filePath);
                }

                string[] lines = File.ReadAllLines(filePath, Encoding.UTF8);

                // Parse column headers
                string[] headers = ParseCSVLine(lines[ignoreLines]);
                foreach (string header in headers)
                {
                    FileDataTable.Columns.Add(header);
                }

                // Parse rows
                for (int i = ignoreLines + 1; i < lines.Length; i++)
                {
                    string[] fields = ParseCSVLine(lines[i]);
                    FileDataTable.Rows.Add(fields);
                }

                return true;
            }
            catch (Exception e)
            {
                throw new FileLoadException("Invalid file format, is it open? Move too start new file: " + FilePath, e.Message);
            }
        }

        // Parse CSV line with support for commas inside double quotes
        private string[] ParseCSVLine(string line)
        {
            System.Collections.Generic.List<string> fields = new System.Collections.Generic.List<string>();
            bool inQuotes = false;
            int startIndex = 0;

            for (int i = 0; i < line.Length; i++)
            {
                if (line[i] == '"')
                {
                    inQuotes = !inQuotes;
                }
                else if (line[i] == ',' && !inQuotes)
                {
                    string field = line.Substring(startIndex, i - startIndex).Trim('"');
                    fields.Add(field);
                    startIndex = i + 1;
                }
            }

            // Add the last field
            string lastField = line.Substring(startIndex).Trim('"');
            fields.Add(lastField);

            return fields.ToArray();
        }

        /// <summary>
        /// Adds a row to a spreadsheet and does not save it
        /// </summary>
        /// <param name="newRow"></param>
        /// returns true if worked
        public bool AddRow(DataRow rowData)
        {
            // add a row to the table string[] newRowData = new string[] {"Value1", "Value2", "Value3"};
            if (rowData.ItemArray.Length != FileDataTable.Columns.Count)
            {
                throw new Exception($"Row data length ({rowData.ItemArray.Length} does not match " +
                                    $"column count ({FileDataTable.Columns.Count}): {rowData}");
            }

            FileDataTable.Rows.Add(rowData);
            
            //return Save();
            return true;
        }

        /// <summary>
        /// had an issue with quotes vanishing in column names in datatables, looks to be a bug. This replaces
        /// double quotes and commas to make sure nothing weird happens
        /// </summary>
        /// <param name="field"></param>
        /// <returns></returns>
        protected string SafeString(string field)
        {
            return field.Replace("\"", "").Replace(",", "");
        }

        public int RowCount()
        {
            return FileDataTable.Rows.Count;
        }

        public bool AppendRow(DataRow newRow)
        {
            using (StreamWriter sw = File.AppendText(FilePath))
            {
                sw.WriteLine(CsvLine(newRow)); // Move to the next line
            }

            return true;
        }

        protected List<string> FieldNames
        {
            get
            {
                if (_fieldNames != null)
                {
                    return _fieldNames;
                }
                else
                {
                    _fieldNames = new List<string>();
                    // Get the fields of the Row struct
                    PropertyInfo[] fields = RowType.GetProperties();

                    // Display the names and types of the fields
                    foreach (PropertyInfo field in fields)
                    {
                        _fieldNames.Add(field.Name);
                    }

                    return _fieldNames;
                }
            }
        }

        protected DataRow CreateRow(object result)
        {
            DataRow newRow = FileDataTable.NewRow();

            // Get the fields of the Row struct
            PropertyInfo[] props = RowType.GetProperties();

            foreach (PropertyInfo prop in props)
            {
                object value = prop.GetValue(result);
                if (value == null)
                {
                    // make sure a blank value is written out in spreadsheet
                    Debug.Log($"Result.{prop.Name} null writing blank string");
                    value = "";
                }

                newRow[prop.Name] = value.ToString();
            }

            return newRow;
        }

        protected  List<string> GetFieldNames<T>() where T : struct
        {
            var rowType = typeof(T);
            var fieldNames = new List<string>();

            // Get the fields of the specified struct type
            FieldInfo[] fields = rowType.GetFields();

            // Add the names of the fields to the list
            foreach (FieldInfo field in fields)
            {
                fieldNames.Add(field.Name);
            }

            return fieldNames;
        }

        
        protected void CreateCsvFile()
        {
            // Get all properties of the object
            var properties = RowType.GetProperties();

            FileDataTable = new DataTable();
            // Add columns to DataTable
            foreach (var property in properties)
            {
                // todo only if it doesn't start with _
                FileDataTable.Columns.Add(property.Name, property.PropertyType);
            }

            using (StreamWriter sw = new StreamWriter(FilePath))
            {
                // returns header for FileDataTable prop
                sw.WriteLine(CsvHeader());
            }
        }
        ///
        /// Adds a row to datatable and saves it
        ///
        public bool AddResult(Object Result)
        {
            DataRow newRow = CreateRow(Result);
            AddRow(newRow);
            return Save();
        }
    }
}