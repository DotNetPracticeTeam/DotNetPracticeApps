using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Adventure.Domain.Entities
{
    public class Master:IMaster
    {
        public int MasterID { get; set; }
        public string MasterName { get; set; }
    }
}
