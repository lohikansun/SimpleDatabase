using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimpleDatabase;
using static SimpleDatabase.Program;
using Moq;

namespace SimpleDatabaseTest
{
    [TestClass]
    public class DatabaseTest
    {
        [TestMethod]
        public void SetTest()
        {
            //Database testDB = new Database();
            //testDB.Set("a", 20);

            //Assert. (20, testDB.Get("a"))
            var writer = new Mock<IOutputWriter>();
            var mock = new Mock<Database>(writer.Object, new object[] { "20", true });
            mock.CallBase = true;
            var db = mock.Object;
            db.Set();
        }
    }
}
