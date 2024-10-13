using Core.Entities;
using Core.Interfaces;
using Core.Responses;
using Data.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories
{
    public class OpinionRepository : IOpinionRepository
    {
        public OpinionContext OpinionContext { get; set; }

        public OpinionRepository(OpinionContext context)
        {
            OpinionContext = context;
        }
        public Result Add(Opinion opinion)
        {
            Data.Models.Opinion dataModel = new Models.Opinion()
            {
                Comment = opinion.Text ?? "",
                TopicId = opinion.Topic.Id,
                IP = "1.1.1.1"
            };


            OpinionContext.Add(dataModel);
            int result = OpinionContext.SaveChanges();

            if (result == 1)
                return new Result(true, new string[] { });
            else
                return new Result(false, new string[] { "Error adding opinion" });
        }

        public Result<Opinion> Get(int opinionId)
        {
            Data.Models.Opinion opinion = OpinionContext.Opinions.OrderByDescending(x => x.Id).Include(x => x.Topic)
                .Where(x => x.Id == opinionId).FirstOrDefault();

            if (opinion != null)
            {
                Opinion resposeOpinion = new Opinion(opinion.Id, opinion.Comment, new Topic(opinion.Topic.Id, opinion.Topic.Title));
                return new Result<Opinion>(true, new string[] {}, resposeOpinion);
            }

            return new Result<Opinion>(false, new string[] { "Error getting opinion" }, null);
        }

        public Result<Opinion> GetRandomOpinion(string topicName)
        {
            var topic = OpinionContext.Topics.Where(x => x.Title == topicName).FirstOrDefault();
            var topicOpinions = topic != null ?
                OpinionContext
                .Opinions
                .Where(x => x.Topic.Id == topic.Id).ToList() : null;

            if (topicOpinions != null && topicOpinions.Count() != 0)
            {
                Random rand = new Random();
                int toSkip = rand.Next(0, topicOpinions.Count());

                var opinion = topicOpinions.Skip(toSkip).FirstOrDefault();
                return new Result<Opinion>(true, new string[] { }, new Opinion(opinion.Id, opinion.Comment, new Topic(opinion.Topic.Id, opinion.Topic.Title)));
            }
            else
            {
                return new Result<Opinion>(false, new string[] { "Error retrieving topic" }, null);
            }

            //Data.Models.Opinion opinion = OpinionContext.Opinions.Include(x => x.Topic).Where(x => x.Topic.Id == topicId).FirstOrDefault();

            //if (opinion != null)
            //{
            //    Opinion resposeOpinion = new Opinion(opinion.Id, opinion.Comment, new Topic(opinion.Topic.Id, opinion.Topic.Title));
            //    return new Result<Opinion>(true, new string[] { }, resposeOpinion);
            //}

            //return new Result<Opinion>(false, new string[] { "Error getting opinion" }, null);
        }

        public Result Remove(int opinionId)
        {
            throw new NotImplementedException();
        }
    }
}
