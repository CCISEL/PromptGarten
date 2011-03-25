using PromptGarten.Domain.Commands;
using PromptGarten.Domain.Model;
using PromptGarten.Domain.Services;

namespace PromptGarten.Domain.Handlers
{
    public class AddStudent : ICommandHandler<AddStudentCommand>
    {
        private readonly IRepository _repository;

        public AddStudent(IRepository repository)
        {
            _repository = repository;
        }

        public void Handle(AddStudentCommand cmd)
        {
            var student = new Student(cmd.Id, cmd.Name);
            _repository.Save(student);
        }
    }
}