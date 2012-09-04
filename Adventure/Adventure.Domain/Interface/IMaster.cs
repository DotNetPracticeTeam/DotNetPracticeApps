using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Adventure.Domain.Entities
{
    public interface IMaster
    {
        int MasterID { get; set; }
        string MasterName { get; set; }
    }
}
