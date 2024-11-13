using Business.ResultPattern.Result;
using DataAccess.DTO.ToDoDtos;

namespace Business.Abstract
{
    public interface IToDoService
    {
        Task<Result> Add(CreateToDoDto dto);
        Task<Result> Update(UpdateToDoDto dto);
        Task<Result> Delete(int id);
    }
}
