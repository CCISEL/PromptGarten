using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using PromptGarten.Domain.Commands;
using PromptGarten.Domain.Exceptions;
using PromptGarten.Domain.Model;
using PromptGarten.Domain.Services;

namespace PromptGarten.Domain.Test.Commands
{
    [TestFixture]
    public class AddTeacherCommandTests
    {
        private IRepository _rep;

        [SetUp]
        public void setup()
        {
            _rep = new InMemoryRepository()
                .WithKey<Teacher>(t => t.Id);
        }

        [Test]
        public void adds_a_teacher()
        {
            var cmd = new AddTeacherCommand { Id = 1619, Name = "Miguel Carvalho" };
            var handler = new AddTeacher(_rep);
            
            handler.Handle(cmd);

            var teacher = _rep.Query<Teacher>().FirstOrDefault(f => f.Id == cmd.Id);
            Assert.NotNull(teacher);
            Assert.AreEqual(cmd.Name, teacher.Name);
        }

        [Test]
        public void fails_to_add_a_duplicate_teacher()
        {
            _rep.Insert(new Teacher(1619, "Miguel Carvalho"));
            var cmd = new AddTeacherCommand { Id = 1619, Name = "Miguel Carvalho" };
            var handler = new AddTeacher(_rep);

            Assert.Throws<DuplicateAggregateException>(() => handler.Handle(cmd));
        }
    }
}
