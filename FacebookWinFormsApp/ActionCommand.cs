﻿using Common.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicFacebookFeatures
{
    public class ActionCommand : ICommand
    {
        public Action Action { get; set; }
        public void Execute()
        {
            Action?.Invoke();
        }

    }
}
