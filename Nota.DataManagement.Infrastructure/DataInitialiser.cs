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
            IDictionary<int,Loan> loans = DataRepositoryHelper.Deserialize<Loan>(@"c:\temp\loanjsonfile.txt");
            IDataRepository<Loan> repo = RepositoryFactory.Create<Loan>();
            repo.SetData(loans);
            return true;
        }
    }
}
