using PromptGarten.Domain.Commands;
using PromptGarten.Domain.Model;
using PromptGarten.Domain.Services;

namespace PromptGarten.Domain.Handlers
{
    public class AddTeacher : ICommandHandler<AddTeacherCommand>
    {
        private readonly IRepository _repository;

        public AddTeacher(IRepository repository)
        {
            _repository = repository;
        }

        public void Handle(AddTeacherCommand cmd)
        {
            var teacher = new Teacher(cmd.Id, cmd.Name);
            _repository.Save(teacher);
        }
    }
}