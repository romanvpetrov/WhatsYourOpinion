using Core.Interfaces;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Data.Pages
{
    public class ResponseModel : PageModel
    {
        public IOpinionRepository OpinionRepository { get; set; }
        public ITopicRepository TopicRepository { get; set; }
        public string RandomOpinion { get; set; }
        public string Topic { get; set; }

        public ResponseModel(IOpinionRepository opinionRepository, ITopicRepository topicRepository)
        {
            OpinionRepository = opinionRepository;
            TopicRepository = topicRepository;
        }

        public void OnGet(string topic)
        {
            var opinionResponse = OpinionRepository.GetRandomOpinion(topic);

            if (opinionResponse.Success)
            {
                RandomOpinion = opinionResponse.Value.Text;
                Topic = opinionResponse.Value.Topic.Name;
            }
        }
    }
}
