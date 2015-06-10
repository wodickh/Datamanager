using System;
using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DataManagementCenter.Models;

namespace DataManagementCenterTest
{
    [TestClass]
    public class RepositoryTest
    {
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
            LoanRepository repository = new LoanRepository();
            for (int i= 0;i <1000000; i++) {
                Loan loan = new Loan { Id = i, MemberId = i * 2, Notes = string.Format("Notes for loan with id: {0}", i), LibraryId = i * 1000, Path = "Loan" };
                repository.Add(loan);
            }
            repository.Serialize();
            repository.Clear();
            repository.Deserialize();
            stopwatch.Stop();
            int count = repository.Count;
        }
    }
}
