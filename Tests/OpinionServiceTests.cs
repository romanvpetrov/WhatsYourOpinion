using Core.Entities;
using Core.Interfaces;
using Core.Responses;
using Core.Services;
using System.ComponentModel.DataAnnotations;

namespace Tests
{
    public class OpinionServiceTests
    {
        [Fact]
        public void first_opinion_in_topic()
        {
            IOpinionRepository opinionRepository = new FakeOpinionRepository();
            ITopicRepository topicRepository = new FakeTopicRepository();

            topicRepository.AddTopic("Politics");

            OpinionService opinionService = new OpinionService(opinionRepository, topicRepository);

            var randomOpinionResult = opinionService.AddOpinion("This is my opinion", "Politics");

            Assert.True(randomOpinionResult.Success);
            Assert.Equal("Message.NoOpinionsForTopic", randomOpinionResult.ErrorMessages[0]);
        }

        [Fact]
        public void second_opinion_in_topic()
        {
            IOpinionRepository opinionRepository = new FakeOpinionRepository();
            ITopicRepository topicRepository = new FakeTopicRepository();

            topicRepository.AddTopic("Politics");
            var topic = topicRepository.GetTopic("Politics").Value;
            opinionRepository.Add(new Opinion(2, "I agree.", topic));

            OpinionService opinionService = new OpinionService(opinionRepository, topicRepository);

            var randomOpinionResult = opinionService.AddOpinion("This is my opinion", "Politics");

            Assert.True(randomOpinionResult.Success);
            Assert.Equal("I agree.", randomOpinionResult.Value.Text);
        }


        [Fact]
        public void first_opinion_in_topic_but_not_in_database()
        {
            IOpinionRepository opinionRepository = new FakeOpinionRepository();
            ITopicRepository topicRepository = new FakeTopicRepository();

            topicRepository.AddTopic("Politics");
            topicRepository.AddTopic("Pop Music");
            var topic = topicRepository.GetTopic("Pop Music").Value;
            opinionRepository.Add(new Opinion(2, "I like it", topic));

            OpinionService opinionService = new OpinionService(opinionRepository, topicRepository);

            var randomOpinionResult = opinionService.AddOpinion("This is my opinion", "Politics");

            Assert.True(randomOpinionResult.Success);
            Assert.Equal("Message.NoOpinionsForTopic", randomOpinionResult.ErrorMessages[0]);
        }

        [Fact]
        public void add_opinion_to_topic_that_does_not_exist()
        {
            IOpinionRepository opinionRepository = new FakeOpinionRepository();
            ITopicRepository topicRepository = new FakeTopicRepository();

            OpinionService opinionService = new OpinionService(opinionRepository, topicRepository);

            var randomOpinionResult = opinionService.AddOpinion("This is my opinion", "Politics");

            Assert.False(randomOpinionResult.Success);
            Assert.Equal("Message.TopicNotFound", randomOpinionResult.ErrorMessages[0]);
        }

        [Fact]
        public void get_topic()
        {
            IOpinionRepository opinionRepository = new FakeOpinionRepository();
            ITopicRepository topicRepository = new FakeTopicRepository();

            topicRepository.AddTopic("Politics");

            OpinionService opinionService = new OpinionService(opinionRepository, topicRepository);

            var randomOpinionResult = opinionService.GetTopic("Politics");

            Assert.True(randomOpinionResult.Success);
        }
    }

    public class FakeOpinionRepository : IOpinionRepository
    {
        public List<Opinion> Opinions { get; set; }

        public FakeOpinionRepository()
        {
            Opinions = new List<Opinion>();
        }

        public Result Add(Opinion opinion)
        {
            Opinions.Add(opinion);
            return new Result(true, null);
        }

        public Result<Opinion> Get(int opinionId)
        {
            var opinion = Opinions.FirstOrDefault(x => x.Id == opinionId);
            
            return opinion == null ? 
                new Result<Opinion>(false, new string[] { "Error.NoOpinions" }, null) : 
                new Result<Opinion>(true, null, opinion);
        }

        public Result<Opinion> GetRandomOpinion(string topicName)
        {
            var opinion = Opinions.FirstOrDefault(x => x.Topic.Name == topicName);

            return opinion == null ? 
                new Result<Opinion>(false, new string[] { "Error.NoOpinions" }, null) : 
                new Result<Opinion>(true, null, opinion);
        }

        public Result Remove(int opinionId)
        {
            Opinions.Remove(Opinions.FirstOrDefault(x => x.Id == opinionId));
            return new Result(true, null);
        }
    }

    public class FakeTopicRepository : ITopicRepository
    {
        public List<Topic> Topics { get; set; }
        public FakeTopicRepository()
        {
            Topics = new List<Topic>();
        }

        public Result AddTopic(string topicName)
        {
            Topics.Add(new Topic(1, topicName));
            return new Result(true, null);
        }

        public Result<IEnumerable<Topic>> GetAllTopics()
        {
            throw new NotImplementedException();
        }

        public Result<Topic> GetTopic(string topicName)
        {
            var topic = Topics.FirstOrDefault(x => x.Name == topicName);

            return topic == null ?
                new Result<Topic>(false, new string[] { "Message.TopicNotFound" }, null) :
                new Result<Topic>(true, null, topic);
        }
    }
}