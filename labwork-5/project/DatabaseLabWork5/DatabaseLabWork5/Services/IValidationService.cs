using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace DatabaseLabWork5.Services
{
    public interface IValidationService<T>
    {
        Task<ValidationResult> ValidateCreateAsync(T entity);
        Task<ValidationResult> ValidateUpdateAsync(T entity);
        Task<ValidationResult> ValidateDeleteAsync(int id);
    }
}