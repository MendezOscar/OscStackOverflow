using System;
using System.Collections.Generic;

namespace StackOverflowOsc.Domain.Entities
{
    public class Account : IEntity
    {
        public Account()
        {
            Id = Guid.NewGuid();
        }
        public ICollection<Answer> Answers { get; set; }
        public ICollection<Question> Questions { get; set; }
        public Guid Id { get; private set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }

    }
}