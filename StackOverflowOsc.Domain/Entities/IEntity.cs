﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace StackOverflowOsc.Domain.Entities
{
    public interface IEntity
    {
         Guid Id { get; }

    }
}
