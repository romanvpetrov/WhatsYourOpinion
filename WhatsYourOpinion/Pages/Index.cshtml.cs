using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WhatsYourOpinion.Contexts;
using WhatsYourOpinion.Models;

namespace WhatsYourOpinion.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly OpinionContext _opinionContext;

        public List<Topic> Topics { get; set; }

        public IndexModel(ILogger<IndexModel> logger, OpinionContext context)
        {
            _logger = logger;
            _opinionContext = context;
        }

        public void OnGet()
        {
            Topics = _opinionContext.Topics.ToList();
        }
    }
}
