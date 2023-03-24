using System.ComponentModel.DataAnnotations;

namespace ContactsAPI.Models
{
    public class DeleteContactsRequest
    {
        [Required]
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
    }
}
