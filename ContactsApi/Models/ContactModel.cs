using System.ComponentModel.DataAnnotations;

namespace ContactsApi.Models
{
    public class ContactModel
    {
        public int Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        [EmailAddress]
        public string Mail { get; set; }
        [Required]
        public string Password { get; set; }
        public string DateOfBirth { get; set; }
        [Required]
        public string Category { get; set; }
        public string Subcategory { get; set; }


    }
}
