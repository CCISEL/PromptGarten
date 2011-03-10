using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using PromptGarten.Domain.Model;

namespace PromptGarten.Domain.Test.Model
{
    [TestFixture]
    class TeacherTests
    {

        [Test]
        public void silly_test()
        {
            var t = new Teacher(1207, "Pedro Félix");
            Assert.AreEqual(1207,t.Id);
        }

    }
}
