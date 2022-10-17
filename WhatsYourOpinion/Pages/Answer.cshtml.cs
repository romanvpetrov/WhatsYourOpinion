using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using WhatsYourOpinion.Contexts;
using WhatsYourOpinion.Models;

namespace WhatsYourOpinion.Pages
{
    public class AnswerModel : PageModel
    {
        OpinionContext Context { get; set; }

        [BindProperty]
        public Opinion Opinion { get; set; }
        [BindProperty]
        public Topic Topic { get; set; }

        public AnswerModel(OpinionContext context)
        {
            Context = context;
            Opinion = new Opinion();
            Topic = new Topic();
        }

        public void OnGet(string topic)
        {
            Opinion.Topic = Context.Topics.Where(x => x.Title == topic).FirstOrDefault();
        }

        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                Context.Add(Opinion);
                Context.SaveChanges();
                Opinion = Context.Opinions.OrderByDescending(x => x.Id).Include(x => x.Topic).FirstOrDefault();
                return RedirectToPage("./Response", new { topic = Opinion.Topic.Title });
            }

            return RedirectToPage("./Index");
        }
    }
}
