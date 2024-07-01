using FluentValidation;

namespace Catalog.API.Products.CreateProduct.Validators
{
    public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
    {
        public CreateProductCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required");
            
            RuleFor(x => x.Category).NotEmpty().WithMessage("Category is required");
            
            RuleFor(x => x.ImageFile)
                .NotEmpty()
                .WithMessage("ImageFile is required")
                .Must(BeAValidImageFormat)
                .WithMessage("Invalid image format");


            RuleFor(x => x)
                .Must(ShouldHaveDescriptionIfPriceIsGreaterThan100)
                .WithMessage("Description is required when the price is above 100");

            RuleFor(x => x.Price).GreaterThan(0).WithMessage("Price must be greater than 0");
        }

        private bool BeAValidImageFormat(string imageFile)
        {
            var validImageFormats = new[] { ".jpg", ".jpeg", ".png", ".gif" };
            var fileExtension = Path.GetExtension(imageFile);
            return validImageFormats.Contains(fileExtension?.ToLower());
        }

        private static bool ShouldHaveDescriptionIfPriceIsGreaterThan100(CreateProductCommand command)
        {
            if (command.Price > 100)
            {
                return !string.IsNullOrWhiteSpace(command.Description);
            }

            return true;
        }
    }
}
