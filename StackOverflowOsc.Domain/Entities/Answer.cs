using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StackOverflowOsc.Domain.Entities
{
    public class Answer : IEntity
    {
        public Answer()
        {
            CreationDate = DateTime.Now;
            Id = Guid.NewGuid();
        }
        public Guid Id { get; set; }
        public ICollection<Vote> Voters { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public string Description { get; set; }
        public int Votes { get; set; }
        public Guid AccountId { get; set; }
        public Guid QuestionId { get; set; }
        public bool Correct { get; set; }
        public int Views { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
