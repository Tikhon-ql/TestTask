namespace TestTask.Logic.Models
{
    /// <summary>
    /// Class to transfer object from Logic layer to DataProvider.
    /// </summary>
    public class ContactDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string MobilePhone { get; set; }
        public string? JobTitle { get; set; }
        public DateTime? BirthDate { get; set; }
    }
}
