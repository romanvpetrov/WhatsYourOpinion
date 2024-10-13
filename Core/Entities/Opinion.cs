using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Opinion
    {
        public Opinion(int id, string text, Topic topic)
        {
            Id = id;
            Text = text;
            Topic = topic;
        }

        public Opinion(string text, Topic topic)
        {
            Text = text;
            Topic = topic;
        }

        public Opinion(Topic topic)
        {
            Topic = topic;
        }

        public Opinion()
        {
            Topic = new Topic(0, "no topic");
        }

        public int Id { get; set; }
        public string? Text { get; set; }
        public Topic Topic { get; set; }
    }
}
