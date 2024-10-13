using Core.Entities;
using Core.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface ITopicRepository
    {
        public Result<Topic> GetTopic(string topicName);
        public Result<IEnumerable<Topic>> GetAllTopics();
    }
}
