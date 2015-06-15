using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nota.DataManagement.Model
{
    public class Item : BaseData
    {
        public string Title { get; set; }
        public override void CreateTombstone()
        {
            throw new NotImplementedException();
        }
    }
}
