using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Core.Interfaces;
using Core.Entities;

namespace Data.Pages
{
    public class AnswerModel : PageModel
    {
        IOpinionRepository Repository { get; set; }
        ITopicRepository TopicRepository { get; set; }

        [BindProperty]
        public string Opinion { get; set; }
        [BindProperty]
        public string TopicName { get; set; }

        public AnswerModel(IOpinionRepository repository, ITopicRepository topicRepository)
        {
            Repository = repository;
            TopicRepository = topicRepository;
        }

        public void OnGet(string topic)
        {
            var result = TopicRepository.GetTopic(topic);
            if (result.Success)
            {
                TopicName = result.Value.Name;
                Opinion = "";
            }
            else
            {
                //TODO: need to figure out how to show to user
            }
        }

        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                var topicResult = TopicRepository.GetTopic(TopicName);

                if (topicResult.Success) {
                    Topic topic = topicResult.Value;
                    var opinion = new Opinion(Opinion, topic);

                    Repository.Add(opinion);
                    return RedirectToPage("./Response", new { topic = topic.Name });
                }

                
                
            }

            return RedirectToPage("./Index");
        }
    }
}
