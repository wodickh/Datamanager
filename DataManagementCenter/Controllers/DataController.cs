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
    public class DataController : ApiController
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
        static readonly IDataRepository<Loan> repository = RepositoryFactory.Create<Loan>();

        
        public IEnumerable<Loan> GetAll()
        {
            return repository.GetAll();
        }

        public Loan GetById(int id) {
            Loan loan = repository.Get(id);
            if (loan == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            return loan;
        }

        public HttpResponseMessage GetByRevision(string revision)
        {
            int id;
            if (int.TryParse(revision, out id))
            {
                IEnumerable<Loan> loans = repository.GetAll().Where(l => l.Revision > int.Parse(revision));
                  HttpResponseMessage message = Request.CreateResponse(HttpStatusCode.OK, loans);
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
        
        public HttpResponseMessage Post(Loan loan)
        {
            loan = repository.Add(loan);
            var response = Request.CreateResponse<Loan>(HttpStatusCode.Created, loan);
            string uri = Url.Link("DefaultApi", new { id = loan.Id });
            response.Headers.Location = new Uri(uri);
            return response;
        }

        public void Put(int id, Loan loan) 
        {
            loan.Id = id;
            if (!repository.Update(loan))
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
        }

        public void Delete(int id)
        {
            Loan loan = repository.Get(id);
            if (loan == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            repository.Remove(loan);
        }
    }

    #region Controllers
    public class LoanController : DataController
    {
        
    }
#endregion Controllers
}
