using DataAccess.DTO.ToDoDtos;

namespace Business.ValidationServices
{
    public class ToDoValidatorService
    {
        #region Methods
        public List<string> ValidateToDo(CreateToDoDto toDoDto)
        {
            List<string> errors = new List<string>();

            if (string.IsNullOrWhiteSpace(toDoDto.Description))
            {
                errors.Add("ToDo name cannot be empty.");
            }

            if (toDoDto.Status == null)
            {
                errors.Add("ToDo status cannot be empty.");

            }

            if (toDoDto.Priority == null)
            {
                errors.Add("ToDo priority cannot be empty.");
            }
            return errors;
        }

        public List<string> ValidateToDo(UpdateToDoDto toDoDto)
        {
            List<string> errors = new List<string>();

            if (string.IsNullOrEmpty(toDoDto.Description))
            {
                errors.Add("ToDo name cannot be empty.");
            }

            if (toDoDto.Status == null)
            {
                errors.Add("ToDo status cannot be empty.");
            }

            if (toDoDto.Priority == null)
            {
                errors.Add("ToDo priority cannot be empty.");
            }

            return errors;
        }

        #endregion

    }
}
