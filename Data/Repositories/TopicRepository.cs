using Core.Entities;
using Core.Interfaces;
using Core.Responses;
using Data.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public class TopicRepository : ITopicRepository
    {
        public OpinionContext Context;
        public TopicRepository(OpinionContext context)
        {
            Context = context;
        }

        public Result<Topic> GetTopic(string topicName)
        {
            var data = Context.Topics.Where(x => x.Title == topicName).FirstOrDefault();

            if(data != null)
            {
                return new Result<Topic>(true, new string[] { }, new Topic(data.Id, data.Title));
            }
            else
            {
                return new Result<Topic>(false, new string[] { "Error finding topic" }, null);
            }
        }

        public Result<IEnumerable<Topic>> GetAllTopics()
        {
            var dataTopics = Context.Topics.ToList();
            var topics = dataTopics.Select(x => new Topic(x.Id, x.Title));

            return new Result<IEnumerable<Topic>>(true, new string[] { }, topics);
        }
    }
}
