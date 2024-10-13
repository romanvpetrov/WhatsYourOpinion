using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Topic
    {
        public Topic(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public Topic()
        {
            Id = 0;
            Name = "no topic";
        }

        public int Id { get; set; }
        public string Name { get; set; }
    }
}
