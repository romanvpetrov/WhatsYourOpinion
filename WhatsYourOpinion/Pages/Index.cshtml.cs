using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using Core.Interfaces;
using Core.Entities;
using System.Linq;

namespace Data.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly ITopicRepository TopicRepository;

        public List<Topic> Topics { get; set; }

        public IndexModel(ILogger<IndexModel> logger, ITopicRepository topicRepository)
        {
            _logger = logger;
            TopicRepository = topicRepository;
        }

        public void OnGet()
        {
            var topicsResult = TopicRepository.GetAllTopics();
            if (topicsResult.Success)
            {
                Topics = TopicRepository.GetAllTopics().Value.ToList();
            }

        }
    }
}
