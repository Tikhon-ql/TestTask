using System.ComponentModel.DataAnnotations;

namespace TestTask.DataProvider.Models
{
    /// <summary>
    /// Base object for all entities
    /// </summary>
    public abstract class BaseEntity
    {
        [Key]
        public Guid Id { get; set; }
    }
}
