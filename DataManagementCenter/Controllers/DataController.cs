using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Nota.DataManagement.Model;
using Nota.DataManagement.Data;

namespace DataManagementCenter.Controllers
{
    public class DataController<T> : ApiController where T:BaseData
    {
        /*
         * Api: 
         * GET {type} - return all
         * GET {type}/{id} - return specific with id
         * GET {type}/?revision - return collection that satisfy > revision
         * HEAD {type}/?revision - 
         * POST {type} - JSON in body
         * PUT {loan}/{id} - update 
         * DELETE {loan}/{id} - delete
         */
        static readonly IDataRepository<T> repository = RepositoryFactory.Create<T>();

        
        public IEnumerable<T> GetAll()
        {
            return repository.GetAll().ToList();
        }

        public T GetById(int id) {
            T data = repository.Get(id);
            if (data == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            return data;
        }

        public HttpResponseMessage GetByRevision(string revision)
        {
            int id;
            if (int.TryParse(revision, out id))
            {
                int revisionInt = int.Parse(revision);
                IEnumerable<T> datas = repository.GetAll().Where(l => l.Revision > revisionInt).ToList();
                  HttpResponseMessage message = Request.CreateResponse(HttpStatusCode.OK, datas);
                  message.Headers.Add("revision", repository.GetLastestRevision().ToString());
                  return message;
            }
            else
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
        }       

        public HttpResponseMessage Head(string revision)
        {
            if (repository.GetLastestRevision() !=0) {
                HttpResponseMessage message = new HttpResponseMessage(HttpStatusCode.OK);
             message.Headers.Add("revision", repository.GetLastestRevision().ToString());
            return message;
            } else {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }            
        }
        
        public HttpResponseMessage Post(T data)
        {
            data = repository.Add(data);
            var response = Request.CreateResponse<T>(HttpStatusCode.Created, data);
            string uri = Url.Link("DefaultApi", new { id = data.Id });
            response.Headers.Location = new Uri(uri);
            return response;
        }

        public void Put(int id, T data) 
        {
            data.Id = id;
            if (!repository.Update(data))
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
        }

        public void Delete(int id)
        {
            T data = repository.Get(id);
            if (data == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            repository.Remove(data);
        }
    }

    #region Controllers
    public class LoanController : DataController<Loan>   { }

    public class ItemController : DataController<Item>  { }
#endregion Controllers
}
