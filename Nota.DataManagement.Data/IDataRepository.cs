using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nota.DataManagement.Data
{
    public interface IDataRepository<T>
    {
        IEnumerable<T> GetAll();
        T Get(int id);
        T Add(T data);
        void Remove(T data);
        bool Update(T data);
        int GetLastestRevision();
        int Count();
        IDictionary<int, T> GetData();
        void SetData(IDictionary<int, T> data);
    }
}
