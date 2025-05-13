using System.Collections.Generic;

namespace DatabaseLabWork5.Services
{
    public class ValidationResult  // <-- The class definition is missing here.
    {
        public bool IsValid { get; set; }
        public List<string> Errors { get; set; } = new List<string>();

        public static ValidationResult Success => new ValidationResult { IsValid = true };
        public static ValidationResult Failure(IEnumerable<string> errors) => new ValidationResult { IsValid = false, Errors = new List<string>(errors) };
    }
}
