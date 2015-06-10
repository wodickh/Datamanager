using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DataManagementCenter.Models;

namespace DataManagementCenter.Controllers
{
    public class DataController : ApiController
    {
        static readonly IDataRepository repository = new LoanRepository();

        
        public IEnumerable<Loan> GetAllLoans()
        {
            return repository.GetAll();
        }

        public Loan GetLoan(int id) {
            Loan loan = repository.Get(id);
            if (loan == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            return loan;
        }

        public IEnumerable<Loan> GetLoansByRevision(string revision) {
            int id;
            if (int.TryParse(revision, out id)) {
                return repository.GetAll().Where(l => l.Revision > int.Parse(revision));
            } else {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
        }

        public HttpResponseMessage PostLoan(Loan loan)
        {
            loan = repository.Add(loan);
            var response = Request.CreateResponse<Loan>(HttpStatusCode.Created, loan);
            string uri = Url.Link("DefaultApi", new { id = loan.Id });
            response.Headers.Location = new Uri(uri);
            return response;
        }

        public void PutLoan(int id, Loan loan) 
        {
            loan.Id = id;
            if (!repository.Update(loan))
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
        }

        public void DeleteLoan(int id)
        {
            Loan loan = repository.Get(id);
            if (loan == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            repository.Remove(loan);
        }
    }
}
