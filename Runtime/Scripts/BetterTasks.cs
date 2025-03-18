using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace BetterReality.Framework
{
    public class BetterTasks : Csv
    {
        private List<string> _fieldNames;// = new List<string> { "Id", "Organisation", "Name", "Description" };
        
        /// <summary>
        /// Represents a row in a counterbalancing table.
        /// </summary>
        public struct BetterTask
        {
            public int id;
            public string organisation;
            public string name;
            public string description;

            public BetterTask (DataRow row)
            {
                id = Convert.ToInt32(row["id"]);
                organisation = row["organisation"].ToString();
                name = row["name"].ToString();
                description = row["description"].ToString();
            }
        }
        
        // constructor
        public BetterTasks(string fileName) : base(fileName)
        {
            _fieldNames = GetFieldNames<BetterTask>();
            // the base class adds on the directory which we don't want
            FilePath = fileName;
            // loads into a DataTable
            Load();
         }
        
        public List<BetterTask> Rows
        {
            get
            {
                List<BetterTask> rows = new List<BetterTask>();
                foreach (DataRow row in FileDataTable.Rows)
                {
                    rows.Add(new BetterTask(row));
                }
                return rows;
            }
        }
 
        public BetterTask GetTask(int taskId)
        { 
            DataRow row = FileDataTable.AsEnumerable().FirstOrDefault(row => row.Field<int>("Id") == taskId);
            if (row == null)
            {
                throw new Exception ($"Id:{taskId} not found in BetterTasks");
            }
            return new BetterTask(row);
        }
    }
}