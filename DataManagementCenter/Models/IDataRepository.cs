using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataManagementCenter.Models
{
    public interface IDataRepository
    {
        IEnumerable<Loan> GetAll();
        Loan Get(int id);
        Loan Add(Loan data);
        void Remove(Loan data);
        bool Update(Loan data);
        int GetLastestRevision();
    }
}
