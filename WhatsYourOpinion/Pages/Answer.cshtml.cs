using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Core.Interfaces;
using Core.Entities;
using Core.Services;

namespace Data.Pages
{
    public class AnswerModel : PageModel
    {
        OpinionService OpinionService { get; set; }

        [BindProperty]
        public string Opinion { get; set; }
        [BindProperty]
        public string TopicName { get; set; }

        public AnswerModel(OpinionService opinionService)
        {
            OpinionService = opinionService;
        }

        public void OnGet(string topic)
        {
            var result = OpinionService.GetTopic(topic);
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
                var addOpinionResult = OpinionService.AddOpinion(Opinion, TopicName);

                if (addOpinionResult.Success && addOpinionResult.ErrorMessages.Length > 0) {
                    return RedirectToPage("./Response", new { topic = TopicName });
                }
                else if (addOpinionResult.Success)
                {
                    return RedirectToPage("./Response", new { topic = TopicName, randomOpinion = addOpinionResult.Value.Text });
                }
            }

            return RedirectToPage("./Index");
        }
    }
}
