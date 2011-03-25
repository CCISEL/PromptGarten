using System.Linq;
using NUnit.Framework;
using PromptGarten.Domain.Commands;
using PromptGarten.Domain.Exceptions;
using PromptGarten.Domain.Handlers;
using PromptGarten.Domain.Model;
using PromptGarten.Domain.Services;

namespace PromptGarten.Domain.Test.Commands
{
    [TestFixture]
    public class AddStudentCommandTests
    {
        private IRepository _rep;

        [SetUp]
        public void setup()
        {
            _rep = new InMemoryRepository()
                .WithKey<Student>(t => t.Id);
        }

        [Test]
        public void adds_a_student()
        {
            var cmd = new AddStudentCommand { Id = 64646, Name = "Pauliteiro de Mirandela" };
            var handler = new AddStudent(_rep);
            
            handler.Handle(cmd);

            var student = _rep.Query<Student>().FirstOrDefault(f => f.Id == cmd.Id);
            Assert.NotNull(student);
            Assert.AreEqual(cmd.Name, student.Name);
        }

        [Test]
        public void fails_to_add_a_duplicate_student()
        {
            _rep.Insert(new Student(64646, "Pauliteiro de Mirandela"));
            var cmd = new AddStudentCommand { Id = 64646, Name = "Pauliteiro de Mirandela" };
            var handler = new AddStudent(_rep);

            Assert.Throws<DuplicateAggregateException>(() => handler.Handle(cmd));
        }
    }
}
