using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Adventure.Domain.Entities
{
    public class dDepartment:IDepartment
    {
        public int DepartmentID
        {
            get;
            set;
        }

        public string Name
        {
            get;
            set;
        }

        public string GroupName
        {
            get;
            set;
        }

        public DateTime ModifiedDate
        {
            get;
            set;
        }
    }
}
