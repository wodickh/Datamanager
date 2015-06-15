using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nota.DataManagement.Model;
namespace Nota.DataManagement.Data
{
    public class DataRepository<T> : IDataRepository<T> where T:BaseData
    {
        private static ConcurrentDictionary<int, T> _datas = new ConcurrentDictionary<int, T>();
        private static int _nextRevision = 1;
        
        internal DataRepository () {}
        public int Count()
        {
            return _datas.Count();            
        }

        public void Clear()
        {
            _datas.Clear();
        }

        public void Remove(T data)
        {
            Remove(data.Id);
        }

        public IEnumerable<T> GetAll()
        {
            return _datas.Values;
        }

        public T Get(int id)
        {
            T data;
            if (_datas.TryGetValue(id, out data))
            {
                return data;
            }
            else
            {
                return null;
            }
        }

        public T Add(T data)
        {
            return AddOrUpdate(data);
        }

        public void Remove(int id)
        {
            T tombStone = MakeTombstone(id);
            Update(tombStone);
        }

        public bool Update(T data)
        {
            return AddOrUpdate(data) != null;
        }

        private T AddOrUpdate(T data)
        {
            if (data == null)
            {
                throw new ArgumentNullException("data");
            }
            data.Revision = _nextRevision++;
            data.RevisionTime = new DateTime();

            return _datas.AddOrUpdate(data.Id, data, (key, existingValue) =>
            {
                existingValue = data;
                return existingValue;
            });
        }

        private T MakeTombstone(int id)
        {
            T tombstone = Get(id);
            return tombstone;
        }

        public int GetLastestRevision()
        {
            return _nextRevision;
       }

        public void SetData(IDictionary<int, T> dictionary)
        {
            if (dictionary != null)
            {
                _datas = new ConcurrentDictionary<int, T>(dictionary);
            }
        }

        public IDictionary<int, T> GetData()
        {
            return _datas;
        }
    }
}
