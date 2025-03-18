using System.Data;
using UnityEngine;
using System.IO;


namespace BetterReality.Framework
{
    // base class for input output files
    public abstract class DataFile
    {
        public string FilePath { get; set; }
        public string Name { get; set; }
        public bool IsReadOnly { get; set; }
        public DataTable FileDataTable { get; set; }
        public string Rows { get; set; }
        

        public abstract bool Load();
        public abstract bool Save();

        /// <summary>
        /// Name of the table is also filename without extension
        /// add path if required. Files are readonly by default
        /// </summary>
        /// <param name="name"></param>
        public DataFile(string tableName)
        {
            // todo: a bit of a hack, should have a constructor for filename
            var name = Path.GetFileNameWithoutExtension(tableName);
            var path = Path.GetDirectoryName(tableName);
            FileDataTable = new DataTable(name);
            IsReadOnly = true;
            // needs an extension put on it by child constructor (messy!)
            FilePath = $"{Application.dataPath}/{path}/{name}";
        }
    }
}