using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StackOverflowOsc.Domain.Entities
{
    public class Question : IEntity
    {
        public Guid ID { get; private set; }

        public Question()
        {
            CreationDate = DateTime.Now;
            ID = Guid.NewGuid();
        }

        public Account Owner { get; set; }
        public string Description { get; set; }
        public int Votes { get; set; }
        public string Tittle { get; set; }
        public DateTime CreationDate { get; set; }


    }
}
