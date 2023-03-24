#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;


namespace ContactsAPI.Models
{
    public class Contacts
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
    }
}
