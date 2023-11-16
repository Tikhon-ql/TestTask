using System.ComponentModel.DataAnnotations;

namespace TestTask.DataProvider.Models
{
    /// <summary>
    /// Contact class
    /// </summary>
    public class Contact : BaseEntity
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string MobilePhone { get; set; }
        public string? JobTitle { get; set; }
        public DateTime? BirthDate { get; set; }
    }
}
