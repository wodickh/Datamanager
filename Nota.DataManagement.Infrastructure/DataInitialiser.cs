using Nota.DataManagement.Data;
using Nota.DataManagement.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nota.DataManagement.Infrastructure
{
    public class DataInitialiser
    {
        public static bool Initialize()
        {
            // Deserialize all data that is serialised 
            // e.g. Loans
            DataRepositoryHelper.Deserialize<Loan>(@"c:\temp\loanjson");
            //DataRepository<Loan> 
            return true;
        }
    }
}
