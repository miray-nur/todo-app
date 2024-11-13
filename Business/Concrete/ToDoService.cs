using Business.Abstract;
using Business.ResultPattern.Result;
using Business.ValidationServices;
using DataAccess.Abstract;
using DataAccess.DTO.Mappers;
using DataAccess.DTO.ToDoDtos;

namespace Business.Concrete
{
    public class ToDoService : IToDoService
    {
        #region Members

        private readonly ToDoValidatorService _validatorService;
        private readonly IToDoRepository _toDoRepository;
        private readonly IUnitOfWork _unitOfWork;

        #endregion

        #region Constructor
        public ToDoService(IToDoRepository toDoRepository, IUnitOfWork unitOfWork, ToDoValidatorService toDoValidatorService)
        {
            _validatorService = toDoValidatorService;
            _toDoRepository = toDoRepository;
            _unitOfWork = unitOfWork;
        }

        #endregion

        #region Methods

        public async Task<Result> Add(CreateToDoDto dto)
        {
            var errorMessages = _validatorService.ValidateToDo(dto);
            if (errorMessages.Any())
            {
                return Result.Fail(string.Join(",", errorMessages));
            }
            else
            {
                var toDo = ToDoMapper.MapToEntity(dto);
                toDo.CreatedDate = DateTime.Now;

                await _toDoRepository.Add(toDo);
                await _unitOfWork.Save();

                return Result.Ok("ToDo added");
            }
        }

        public async Task<Result> Update(UpdateToDoDto dto)
        {
            var existingToDo = await _toDoRepository.GetById(dto.Id);
            var errorMessages = _validatorService.ValidateToDo(dto);

            if (errorMessages.Any())
            {
                return Result.Fail(string.Join(", ", errorMessages));
            }

            if (existingToDo != null)
            {
                existingToDo.Description = dto.Description;
                existingToDo.Status = dto.Status;
                existingToDo.Priority = dto.Priority;

                await _toDoRepository.Update(existingToDo);
                await _unitOfWork.Save();
                return Result.Ok("ToDo updated");
            }

            return Result.Fail("ToDo update failed");
        }

        public async Task<Result> Delete(int id)
        {
            if (id <= 0)
            {
                return Result.Fail("invalid ToDo id");
            }

            await _toDoRepository.Delete(id);
            await _unitOfWork.Save();
            return Result.Ok("ToDo deleted");
        }

        #endregion

    }
}
