
using System;
using System.Data;
using NUnit.Framework;
using UnityEditor.VersionControl;

namespace BetterReality.Framework.Editor.Tests 
{
[TestFixture]    
public class BetterTasksTest
{
    DataTable _fileDataTable;

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

        
//        BetterTasks tasks = new BetterTasks("filename");

        // Access Rows property
        BetterTasks.BetterTask task = new BetterTasks.BetterTask(_fileDataTable.Rows[0]);
        Assert.AreEqual(task.id, 1);//, "task extracted from datarow");
        Assert.AreEqual(task.organisation, "Org1");//, "task extracted from datarow");
        Assert.AreEqual(task.name, "Task1");//, "task extracted from datarow");
        Assert.AreEqual(task.description, "Description1");//, "task extracted from datarow");

/*
        
        var betterTasks = new BetterTasks("Test_BetterTasks.csv");
        betterTasks.Load();
        Assert.IsNotNull(betterTasks,"PrintGlobalPosition method not found");
        */
    }
}

}
