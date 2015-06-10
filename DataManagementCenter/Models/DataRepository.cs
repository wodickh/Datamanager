using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace DataManagementCenter.Models
{
    public class LoanRepository : IDataRepository
    {
        private List<Loan> datas = new List<Loan>();
        private int _nextRevision = 1;

        public LoanRepository()
        {
            Add(new Loan {LibraryId=1, Id = 1, MemberId=10000004, Notes="Important notes", Path ="\\Loan"});
            Add(new Loan {LibraryId=2, Id = 2, MemberId=10000004, Notes="Another note", Path="\\Loan"});
            Add(new Loan {LibraryId=3, Id = 3, MemberId=10291, Notes="speciel note", Path = "\\Loan"});
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
            return datas;
        }

        public Loan Get(int id)
        {
            return datas.Find(l => l.Id==id);
        }

        public Loan Add(Loan data)
        {
            if (data == null) {
                throw new ArgumentNullException("data");
            }
            data.Revision = _nextRevision++;
            data.RevisionTime = new DateTime();
            datas.Add(data);
            return data;
        }

        public void Remove(int id)
        {
            Loan emptyLoan = makeTombstone(id);
            Update(emptyLoan);
            //datas.RemoveAll(l => l.Id == id);
            
        }

        public bool Update(Loan data)
        {
            if (data == null)
            {
                throw new ArgumentNullException("data");
            }
            int index = datas.FindIndex(l => l.Id == data.Id);
            if (index == -1)
            {
                return false;
            }
            datas.RemoveAt(index);
            datas.Add(data);
            return true;
        }

        private Loan makeTombstone(int id)
        {
            /// find loan and create a tombstone
            Loan tombstone = Get(id);
            tombstone.LibraryId = 0;
            tombstone.Notes = null;
            return tombstone;
        }
        public List<Loan> Deserialize()
        {
            List<Loan> list;
            JsonSerializer serialiser = new JsonSerializer();
            using (StreamReader sr = new StreamReader(@"c:\temp\loaninjson.txt"))
            using (JsonReader reader = new JsonTextReader(sr))
            {
                 list = serialiser.Deserialize<List<Loan>>(reader);
            }
            return list;
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
    }             
}