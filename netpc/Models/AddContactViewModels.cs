using System.ComponentModel.DataAnnotations;

namespace netpc.Models
{
    public class AddContactViewModel
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [EnumDataType(typeof(ContactCategoryEnum))]
        public ContactCategoryEnum Category { get; set; }
        public ContactSubcategoryEnum? Subcategory { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        [Display(Name = "Data urodzenia")]
        public DateTime DateOfBirth { get; set; }

    }
}
