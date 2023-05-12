using System.ComponentModel.DataAnnotations;

namespace netpc.Models
{
    public class Contact
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public ContactCategoryEnum Category { get; set; }
        public ContactSubcategoryEnum? Subcategory { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public DateTime DateOfBirth { get; set; }

        public string fullName { get { return FirstName + " " + LastName; } }
    }
    public enum ContactCategoryEnum
    {
        [Display(Name = "Służbowy")]
        Business,
        [Display(Name = "Prywatny")]
        Private,
        [Display(Name = "Inny")]
        Other
    }

    public enum ContactSubcategoryEnum
    {
        [Display(Name = "Szef")]
        Boss,
        [Display(Name = "Klient")]
        Customer,
        [Display(Name = "Pracownik")]
        Employee,
        [Display(Name = "Inne")]
        Other
    }
}
