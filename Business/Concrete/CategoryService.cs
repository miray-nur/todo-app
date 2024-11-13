using Business.Abstract;
using Business.ResultPattern.Result;
using Business.ValidationServices;
using DataAccess.Abstract;
using DataAccess.DTO.CategoryDtos;
using DataAccess.DTO.Mappers;

namespace Business.Concrete
{
    public class CategoryService : ICategoryService
    {
        #region Members

        private readonly CategoryValidatorService _validatorService;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IUnitOfWork _unitOfWork;

        #endregion

        #region Constructor
        public CategoryService(CategoryValidatorService categoryValidatorService, ICategoryRepository categoryRepository, IUnitOfWork unitOfWork)
        {
            _validatorService = categoryValidatorService;
            _categoryRepository = categoryRepository;
            _unitOfWork = unitOfWork;
        }

        #endregion

        #region Methods

        public async Task<Result> Add(CreateCategoryDto dto)
        {
            var errorMessages = await _validatorService.ValidateCategory(dto);
            if (errorMessages.Any())
            {
                return Result.Fail(string.Join(", ", errorMessages));
            }
            else
            {
                var category = CategoryMapper.MapToEntity(dto);
                category.CreatedDate = DateTime.Now;
                await _categoryRepository.Add(category);
                await _unitOfWork.Save();

                return Result.Ok("category added");
            }
        }

        public async Task<Result> Update(UpdateCategoryDto dto)
        {

            var existingCategory = await _categoryRepository.GetById(dto.Id);
            var errorMessages = _validatorService.ValidateCategory(dto);

            if (errorMessages.Any())
            {
                return Result.Fail(string.Join(", ", errorMessages));
            }

            if (existingCategory != null)
            {
                existingCategory.Name = dto.Name;
                existingCategory.Description = dto.Description;

                await _categoryRepository.Update(existingCategory);
                await _unitOfWork.Save();
                return Result.Ok("category updated");
            }

            return Result.Fail("category update failed");
        }

        public async Task<Result> Delete(int id)
        {
            if (id <= 0)
            {
                return Result.Fail("invalid category id");
            }

            await _categoryRepository.Delete(id);
            await _unitOfWork.Save();
            return Result.Ok("category deleted");
        }

    }

    #endregion

}
