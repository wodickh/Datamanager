using Newtonsoft.Json;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace DataManagementCenter.Models
{
    public class LoanRepository : IDataRepository
    {
     //   private List<Loan> datas = new List<Loan>();
        private static ConcurrentDictionary<int, Loan> datas = new ConcurrentDictionary<int, Loan>();
        private static int _nextRevision = 1;
        private static LoanRepository _instance = new LoanRepository();
        public static bool IsInitialized { get; set; }
        // lav til singleton / eller static
        public LoanRepository()
        {
        }

        public static LoanRepository GetInstance()
        {
            return _instance;
        }
        public static void Initialise()
        {
            IsInitialized = true;
        }
        public int Count
        {
            get
            {
                return datas.Count();
            }
        }

        public void Clear()
        {
            datas.Clear();
        }
        public IEnumerable<Loan> GetAll()
        {
            return datas.Values;
        }

        public Loan Get(int id)
        {
            Loan loan;
            if (datas.TryGetValue(id, out loan))
            {
                return loan;
            } else
            {
                return null;
            }
        }

        public Loan Add(Loan data)
        {
            return AddOrUpdate(data);
        }

        public void Remove(int id)
        {
            Loan emptyLoan = MakeTombstone(id);
            Update(emptyLoan);          
        }

        public bool Update(Loan data)
        {
            return AddOrUpdate(data) != null;
        }

        private Loan AddOrUpdate(Loan data)
        {
            if (data == null)
            {
                throw new ArgumentNullException("data");
            }
            data.Revision = _nextRevision++;
            data.RevisionTime = new DateTime();

            return datas.AddOrUpdate(data.Id, data, (key, existingValue) =>
            {
                existingValue = data;
                return existingValue;
            });  
        }

        private Loan MakeTombstone(int id)
        {
            /// find loan and create a tombstone
            Loan tombstone = Get(id);
            tombstone.LibraryId = 0;
            tombstone.Notes = null;
            return tombstone;
        }

        public Dictionary<string, Loan> Deserialize()
        {
            Dictionary<string, Loan> dictionary;
            JsonSerializer serialiser = new JsonSerializer();
            using (StreamReader sr = new StreamReader(@"c:\temp\loaninjson.txt"))
            using (JsonReader reader = new JsonTextReader(sr))
            {
                dictionary = serialiser.Deserialize<Dictionary<string, Loan>>(reader);
            }
            return dictionary;
        }

        public void Serialize()
        {
            JsonSerializer serializer = new JsonSerializer();
            serializer.NullValueHandling = NullValueHandling.Ignore;
            using (StreamWriter sw = new StreamWriter(@"c:\temp\loaninjson.txt"))
            using (JsonWriter writer = new JsonTextWriter(sw))
            {
                serializer.Serialize(writer, datas);               
            }
        }


        public void Remove(Loan data)
        {
            throw new NotImplementedException();
        }


        public int GetLastestRevision()
        {
            return _nextRevision;
        }
    }             
}