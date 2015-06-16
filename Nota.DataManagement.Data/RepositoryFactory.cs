using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nota.DataManagement.Model;

namespace Nota.DataManagement.Data
{
    public class RepositoryFactory
    {
        public static IDataRepository<T> Create<T>() where T : BaseData
        {
          return new DataRepository<T>();
        }

    }
}
