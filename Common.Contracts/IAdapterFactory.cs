﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Contracts
{
    public interface IAdapterFactory
    {
        IObject CreateAdapter(object i_ApiObject);
    }
}
