using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Nota.DataManagement.Data;
using Nota.DataManagement.Model;

namespace DataManagementCenterTest
{
    [TestClass]
    public class RepositoryTest
    {
        
        [TestMethod]
        public void TestReadNWriteAsync()
        {
            IDataRepository<Loan> repo = RepositoryFactory.Create<Loan>();
            Task[] tasks = new Task[2];
            tasks[0] = Task.Run(() =>
            {
                for (int i = 1; i < 10; i++)
                {
                    Loan loan = new Loan { Id = i, MemberId = i * 2, Notes = string.Format("Notes for loan with id: {0}", i), LibraryId = i * 1000, Path = "Loan" };
                    repo.Add(loan);
                }
            });

            tasks[1] = Task.Run(() =>
            {
                for (int i = 10; i < 20; i++)
                {
                    Loan loan = new Loan { Id = i, MemberId = i * 2, Notes = string.Format("from task 2: Notes for loan with id: {0}", i), LibraryId = i * 1000, Path = "Loan" };
                    repo.Add(loan);
                }
            });
            Task.WaitAll(tasks);
            Assert.IsTrue(repo.Count() > 0);
        }

        [TestMethod]
        public void TestCount()
        {
            
        }

        [TestMethod]
        public void TestClear()
        {
            
        }

        [TestMethod]
        public void TestGetAll()
        {
            
        }

        [TestMethod]
        public void TestGet()
        {
            
        }

        [TestMethod]
        public void TestAdd()
        {
            
        }

        [TestMethod]
        public void TestRemove()
        {
            
        }

        [TestMethod]
        public void TestUpdate()
        {
            
        }

        [TestMethod]
        public void TestGetLatestRevision()
        {
            
        }
        [TestMethod]
        public void TestSerialise()
        {
        }

        [TestMethod]
        public void TestDeserialise()
        {

        }

        [TestMethod]
        public void TestCreate100KOfLoans() {
            LoanRepository repository = new LoanRepository();
            for (int i = 0; i < 1000; i++)
            {
                Loan loan = new Loan { Id = i, MemberId = i * 2, Notes = string.Format("Notes for loan with id: {0}", i), LibraryId = i * 1000, Path = "Loan" };
                repository.Add(loan);
            }
            repository.Serialize();
        }

        [TestMethod]
        public void TestCreate100KOfLoansSerialiseNDeserialize()
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            IDataRepository<Loan> repository = RepositoryFactory.Create<Loan>();
            for (int i= 0;i <1000000; i++) {
                Loan loan = new Loan { Id = i, MemberId = i * 2, Notes = string.Format("Notes for loan with id: {0}", i), LibraryId = i * 1000, Path = "Loan" };
                repository.Add(loan);
            }
            DataRepositoryHelper.Serialize<Loan>(repository.GetData(),@"c:\temp\loanjsonfile.txt");
        //    repository.Clear();
        //    repository.Deserialize();
            stopwatch.Stop();
            int count = repository.Count();
        }
    }
}
