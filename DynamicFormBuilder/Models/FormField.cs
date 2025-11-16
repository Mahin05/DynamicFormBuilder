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


        // Save the selected option (e.g., "Option 1")
        public string SelectedOption { get; set; }


        // position to maintain ordering
        public int Position { get; set; }
    }
}
