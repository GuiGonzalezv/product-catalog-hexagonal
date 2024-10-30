namespace ProductCatalog.Domain.Ports.Validation
{
    public class ValidationResult
    {
        public bool IsValid { get; set; }
        public List<ValidationResultErrors> Errors { get; set; } = new List<ValidationResultErrors>();

        public ValidationResult(bool isValid)
        {
            IsValid = isValid;
        }

        public ValidationResult(bool isValid, List<ValidationResultErrors> errors)
        {
            IsValid = isValid;
            Errors = errors ?? new List<ValidationResultErrors>();
        }

        public ValidationResult(bool isValid, IEnumerable<ValidationResultErrors> errors)
        {
            IsValid = isValid;
            Errors = errors.ToList() ?? new List<ValidationResultErrors>();
        }
    }
}
