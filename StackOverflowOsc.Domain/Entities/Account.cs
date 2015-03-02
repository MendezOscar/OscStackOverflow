using System;

namespace StackOverflowOsc.Domain.Entities
{
    public class Account : IEntity
    {
        public Guid ID { get; private set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }


        public Account()
        {
            ID = Guid.NewGuid();
        }
    }
}