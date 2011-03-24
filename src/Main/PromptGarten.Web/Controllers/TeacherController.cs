using System;
using System.Web.Mvc;
using PromptGarten.Domain.Commands;
using PromptGarten.Domain.Services;
using PromptGarten.Domain.Model;
using PromptGarten.Domain.Handlers;

namespace PromptGarten.Web.Controllers
{
    public class TeacherController : Controller
    {
        private readonly IRepository _rep;
        private readonly ICommandHandler<AddTeacherCommand> _addTeacher;

        public TeacherController(IRepository rep, ICommandHandler<AddTeacherCommand> addTeacher)
        {
            _rep = rep;
            _addTeacher = addTeacher;
        }

        public ActionResult Index()
        {
            return View(_rep.Query<Teacher>());
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(AddTeacherCommand teacherCommand)
        {
            try
            {
                _addTeacher.Handle(teacherCommand);
                return RedirectToAction("Index");
            }catch(Exception e)
            {
                ModelState.AddModelError("", e.Message);
            }
            return View(teacherCommand);
        }
    }
}
