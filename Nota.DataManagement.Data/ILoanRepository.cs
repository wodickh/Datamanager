using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nota.DataManagement.Model;

namespace Nota.DataManagement.Data
{
    public interface ILoanRepository
    {
        IEnumerable<Loan> GetAll();
        Loan Get(int id);
        Loan Add(Loan data);
        void Remove(Loan data);
        bool Update(Loan data);
        int GetLastestRevision();
    }
}
