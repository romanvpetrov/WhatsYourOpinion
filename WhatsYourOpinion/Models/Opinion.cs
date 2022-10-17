using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WhatsYourOpinion.Models
{
    public class Opinion
    {
        [Key]
        public int Id { get; set; }
        public string Comment { get; set; }
        public int? TopicId { get; set; }
        public Topic Topic { get; set; }
        public string IP { get; set; }
    }
}
