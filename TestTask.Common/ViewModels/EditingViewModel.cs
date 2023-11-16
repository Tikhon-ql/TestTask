using System.ComponentModel.DataAnnotations;

namespace TestTask.Common.ViewModels
{
    public class EditingViewModel
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string MobilePhone { get; set; }
        public string? JobTitle { get; set; }
        public DateTime? Birthdate { get; set; }
    }
}
