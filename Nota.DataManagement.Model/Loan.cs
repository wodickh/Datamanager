using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Nota.DataManagement.Model
{
    public class Loan : BaseData
    {
        public int LibraryId { get; set; }
        public int MemberId { get; set; }
        public string Notes { get; set; }
        public override void CreateTombstone()
        {
            Notes = null;
        }
    }
}