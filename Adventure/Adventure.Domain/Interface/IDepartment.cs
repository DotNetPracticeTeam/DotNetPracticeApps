using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Adventure.Domain.Entities
{
    public interface IDepartment:IBaseEntity
    {
        int DepartmentID { get; set; }
        string Name { get; set; }
        string GroupName { get; set; }
    }

}
