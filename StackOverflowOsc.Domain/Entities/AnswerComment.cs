using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StackOverflowOsc.Domain.Entities
{
    class AnswerComment : IEntity
    {
        public Guid Id { get; set; }

        public AnswerComment() { Id = Guid.NewGuid(); }
        public Guid AccountId { get; set; }
        public Guid AnswerrId { get; set; }
    }
}
