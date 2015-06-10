using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DataManagementCenter.Models
{
    public class BaseData
    {
        public int Id { get; set; }
        public int Revision { get; set; }
        public string Path { get; set; }
        public DateTime RevisionTime { get; set; } // set creationtime?
     }
}