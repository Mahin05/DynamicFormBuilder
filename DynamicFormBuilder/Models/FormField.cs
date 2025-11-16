using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DynamicFormBuilder.Models
{
    public class FormField
    {
        public long Id { get; set; }
        [Required]
        public long FormId { get; set; }
        [ForeignKey("FormId")]
        public Form Form { get; set; }
        [Required]
        public string Label { get; set; }
        public bool IsRequired { get; set; }
        public string SelectedOption { get; set; }

        public int Position { get; set; }
    }
}
