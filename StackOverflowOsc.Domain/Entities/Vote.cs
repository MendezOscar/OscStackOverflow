﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StackOverflowOsc.Domain.Entities
{
    public class Vote : IEntity
    {
        public Vote()
        {
            Id = Guid.NewGuid();
        }
        public Guid Id { get; set; }
        public virtual Account Voter { get; set; }

        public int Value;
        public virtual Question Question { get; set; }
        public virtual Answer Answer { get; set; }
    }
}
