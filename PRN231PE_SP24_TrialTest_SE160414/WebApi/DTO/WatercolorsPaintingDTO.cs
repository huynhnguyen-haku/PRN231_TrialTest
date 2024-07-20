using System.ComponentModel.DataAnnotations;

namespace WebApi.DTO
{
    public class WatercolorsPaintingDTO
    {
        [Required]
        public string Id { get; set; }

        [Required]
        [RegularExpression(@"^([A-Z][a-z0-9]*\s?)+$", ErrorMessage = "Invalid painting name format")]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string Author { get; set; }

        [Required]
        [Range(0.00, double.MaxValue, ErrorMessage = "Price must be >= 0.")]
        public decimal Price { get; set; }

        [Required]
        [Range(1000, int.MaxValue, ErrorMessage = "PublishYear must be >= 1000.")]
        [CurrentYearValidation(ErrorMessage = "PublishYear must be less than the current year.")]
        public int PublishYear { get; set; }

        [Required]
        public string StyleId { get; set; }
    }
}

public class CurrentYearValidationAttribute : ValidationAttribute
{
    public override bool IsValid(object value)
    {
        int productionYear = (int)value;
        int currentYear = DateTime.Now.Year;

        return productionYear < currentYear;
    }
}