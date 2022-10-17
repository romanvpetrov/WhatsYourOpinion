using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Linq;
using WhatsYourOpinion.Contexts;
using WhatsYourOpinion.Models;

namespace WhatsYourOpinion.Pages
{
    public class ResponseModel : PageModel
    {
        public OpinionContext Context { get; set; }
        public Opinion RandomOpinion { get; set; }
        public Topic Topic { get; set; }

        public ResponseModel(OpinionContext context)
        {
            Context = context;
        }

        public void OnGet(string topic)
        {
            Topic = Context.Topics.Where(x => x.Title == topic).FirstOrDefault();
            var topicOpinions = Topic != null ? 
                Context
                .Opinions
                .Where(x => x.Topic.Id == Topic.Id).ToList() : null;

            if (topicOpinions != null)
            {
                Random rand = new Random();
                int toSkip = rand.Next(0, topicOpinions.Count());

                RandomOpinion = topicOpinions.Skip(toSkip).FirstOrDefault();
            }
            else
            {
                RandomOpinion = null;
            }
        }
    }
}
