using System.ComponentModel.DataAnnotations;

namespace TranslationsAdmin.Models
{
    public class LanguageModel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Locale { get; set; }
        public DateTime CreatedAt { get; internal set; }
        public DateTime UpdatedAt { get; internal set; }
    }
}
