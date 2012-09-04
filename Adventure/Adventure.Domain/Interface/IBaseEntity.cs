using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Adventure.Domain.Entities
{
    public interface IBaseEntity
    {
        DateTime ModifiedDate { get; set; }
    }
}
