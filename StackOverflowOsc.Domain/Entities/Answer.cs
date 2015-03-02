using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StackOverflowOsc.Domain.Entities
{
    public class Answer : IEntity
    {
        public Guid ID { get; private set; }

        public Answer()
        {
            CreationDate = DateTime.Now;
            ID = Guid.NewGuid();
        }
        public String Description { get; set; }
        public int Votes { get; set; }
        public string OwnerName { get; set; }
        public DateTime CreationDate { get; set; }
        private bool State { get; set; }
    }
}
