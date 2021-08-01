using System;
using System.ComponentModel.DataAnnotations;

namespace Api.Payloads
{
    public class ArticleDto
    {
        [Required]
        public string Title { get; set; }
        
        [Required]
        public string Author { get; set; }

        [Required]
        public DateTime PublicationDate { get; set; }
    }
}
