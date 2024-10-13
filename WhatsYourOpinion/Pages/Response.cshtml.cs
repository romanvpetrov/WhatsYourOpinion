using Core.Interfaces;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Data.Pages
{
    public class ResponseModel : PageModel
    {
        public string RandomOpinion { get; set; }
        public string Topic { get; set; }

        public ResponseModel()
        {

        }

        public void OnGet(string topic, string randomOpinion)
        {

            RandomOpinion = randomOpinion;
            Topic = topic;
        }
    }
}
