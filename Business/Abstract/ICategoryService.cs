using Business.ResultPattern.Result;
using DataAccess.DTO.CategoryDtos;

namespace Business.Abstract
{
    public interface ICategoryService
    {
        Task<Result> Add(CreateCategoryDto dto);
        Task<Result> Update(UpdateCategoryDto dto);
        Task<Result> Delete(int id);
    }
}
