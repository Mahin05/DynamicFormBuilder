using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace DynamicFormBuilder.Models
{
    public class Form
    {
        public long Id { get; set; }


        [Required]
        public string Title { get; set; }


        public List<FormField> Fields { get; set; } = new List<FormField>();
    }
}
