using Core.Entities;
using Core.Interfaces;
using Core.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public class OpinionService
    {
        public IOpinionRepository OpinionRepository { get; set; }
        public ITopicRepository TopicRepository { get; set; }

        public OpinionService(IOpinionRepository opinionRepository, ITopicRepository topicRepository)
        {
            OpinionRepository = opinionRepository;
            TopicRepository = topicRepository;
        }

        public Result<Opinion> AddOpinion(string opinionText, string topicName)
        {
            var topicResult = TopicRepository.GetTopic(topicName);

            if (topicResult.Success)
            {
                var randomOpinionResult = OpinionRepository.GetRandomOpinion(topicName);

                Topic topic = topicResult.Value;
                Opinion opinion = new Opinion(opinionText, topic);

                var addOpinionResult = OpinionRepository.Add(opinion);

                if (!addOpinionResult.Success)
                {
                    return new Result<Opinion>(false, addOpinionResult.ErrorMessages, null);
                }

                if (!randomOpinionResult.Success)
                {
                    return new Result<Opinion>(true, new string[] { "Message.NoOpinionsForTopic" }, null);
                }
                else
                {
                    return new Result<Opinion>(true, new string[] { }, randomOpinionResult.Value);
                }

            }
            else
            {
                return new Result<Opinion>(false, topicResult.ErrorMessages, null);
            }
        }

        public Result<Topic> GetTopic(string topicName)
        {
            return TopicRepository.GetTopic(topicName);
        }
    }
}
