using System.Data;
using NUnit.Framework;
using UnityEngine;

namespace BetterReality.Framework.Editor.Tests
{
    [TestFixture]
    public class BetterTasksTest
    {
        DataTable _fileDataTable;
        // should probably test with the actual file rather than a copy!
        private readonly string _inputFile = $"{Application.dataPath}/Config/BetterTasks.csv";

        [SetUp]
        public void Setup()
        {
            _fileDataTable = new DataTable();
            _fileDataTable.Columns.Add("id", typeof(int));
            _fileDataTable.Columns.Add("organisation", typeof(string));
            _fileDataTable.Columns.Add("name", typeof(string));
            _fileDataTable.Columns.Add("description", typeof(string));

            // Add some example rows
            _fileDataTable.Rows.Add(1, "Org1", "Task1", "Description1");
            _fileDataTable.Rows.Add(2, "Org2", "Task2", "Description2");
            _fileDataTable.Rows.Add(3, "Org3", "Task3", "Description3");
        }

//  [TearDown]

        [Test]
        public void BetterTasks_Task()
        {
            // Access Rows property
            BetterTasks.BetterTask task = new BetterTasks.BetterTask(_fileDataTable.Rows[0]);
            Assert.AreEqual(task.id, 1, "task extracted from datarow");
            Assert.AreEqual(task.organisation,"Org1", "organisation extracted from datarow");
            Assert.AreEqual(task.name, "Task1","name extracted from datarow");
            Assert.AreEqual(task.description, "Description1","description extracted from datarow");
        }

        [Test]
        public void BetterTasks()
        {
            BetterTasks betterTasks = new BetterTasks(_inputFile);
            Assert.AreEqual(betterTasks.Rows.Count, 1, "only one record in task library");
            BetterTasks.BetterTask task = betterTasks.GetTask(1);
            Assert.AreEqual(task.name, "Whackamole", "loaded task from file");
        }
    }
}