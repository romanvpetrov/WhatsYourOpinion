using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WhatsYourOpinion.Models
{
    public class Topic
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        //public string SubNames { get; set; }
        public string Category { get; set; }
        public List<Opinion> Opinions { get; }
    }
}
