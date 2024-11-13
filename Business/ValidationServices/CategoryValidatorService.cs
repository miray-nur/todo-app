using DataAccess.Abstract;
using DataAccess.DTO.CategoryDtos;

namespace Business.ValidationServices
{
    public class CategoryValidatorService
    {
        #region Members

        private readonly ICategoryRepository _categoryRepository;

        #endregion

        #region Constructor
        public CategoryValidatorService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        #endregion

        #region Methods

        public async Task<List<string>> ValidateCategory(CreateCategoryDto categoryDto)
        {
            List<string> errors = new List<string>();

            var categories = await _categoryRepository.GetAll();
            if (categories.Any(c => c.Name.Equals(categoryDto.Name, StringComparison.OrdinalIgnoreCase)))
            {
                errors.Add("There is another category with the same name.");
            }

            if (string.IsNullOrWhiteSpace(categoryDto.Name))
            {
                errors.Add("Category name cannot be empty.");
            }

            if (string.IsNullOrWhiteSpace(categoryDto.Description))
            {
                errors.Add("Category description cannot be empty.");
            }

            return errors;
        }

        public List<string> ValidateCategory(UpdateCategoryDto categoryDto)
        {
            List<string> errors = new List<string>();

            if (string.IsNullOrWhiteSpace(categoryDto.Name))
            {
                errors.Add("Category name cannot be empty.");
            }
            if (string.IsNullOrWhiteSpace(categoryDto.Description))
            {
                errors.Add("Category description cannot be empty.");
            }

            return errors;
        }

        #endregion
    }
}
