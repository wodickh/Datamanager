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
           var repository = new DataRepository<T>();
     /*       var dynamicProxy = new RepositoryProxy<IRepository<T>>(repository);
            dynamicProxy.BeforeExecute += (s, e) => NotaEventSource.Log.Debug(String.Format("Before executing {0}", e.MethodName));
            dynamicProxy.AfterExecute += (s, e) => NotaEventSource.Log.Debug(String.Format("After executing {0}", e.MethodName));
            dynamicProxy.ErrorExecuting += (s, e) => NotaEventSource.Log.Debug("Error");
            return dynamicProxy.GetTransparentProxy() as IRepository<T>;
      */
            return new DataRepository<T>();
        }

    }
}
