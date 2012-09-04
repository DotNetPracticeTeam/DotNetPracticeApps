﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Adventure.Data.Interface
{
    public interface IUnitOfWorkFactory
    {
        IUnitOfWork Create();
    }
}
