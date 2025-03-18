using System.Data;
using NUnit.Framework;
using UnityEngine;

namespace BetterReality.Framework.Editor.Tests
{
    [TestFixture]
    public class BetterTasksTest
    {
        DataTable _fileDataTable;
        // todo: put this inside the framework package as test will fail in isolation - filepaths differ...
        private readonly string _inputFile = $"{Application.dataPath}/Config/BetterTasks.csv";

        [SetUp]
        public void Setup()
        {
            _fileDataTable = new DataTable();
            _fileDataTable.Columns.Add("id", typeof(int));
            _fileDataTable.Columns.Add("scene", typeof(string));
            _fileDataTable.Columns.Add("parameters", typeof(string));
            _fileDataTable.Columns.Add("organisation", typeof(string));
            _fileDataTable.Columns.Add("name", typeof(string));
            _fileDataTable.Columns.Add("description", typeof(string));

            // Add some example rows
            _fileDataTable.Rows.Add(1,"Scene1","", "Org1", "Task1", "Description1");
            _fileDataTable.Rows.Add(2, "Scene2","tests=1,2,5", "Org2", "Task2", "Description2");
            _fileDataTable.Rows.Add(3, "Scene3", "","Org3", "Task3", "Description3");
        }

//  [TearDown]

        [Test]
        public void BetterTasks_Task()
        {
            // Access Rows property
            BetterTasks.BetterTask task = new BetterTasks.BetterTask(_fileDataTable.Rows[0]);
            Assert.AreEqual(task.id, 1, "task extracted from datarow");
            Assert.AreEqual(task.scene, "Scene1", "scene extracted from datarow");
            Assert.AreEqual(task.parameters, "", "parameters extracted from datarow");
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
            Assert.AreEqual("Whack-a-mole",task.name, "loaded task from file");
            Assert.AreEqual( "Whackamole",task.scene, "loaded scene name from file");
        }
    }
}