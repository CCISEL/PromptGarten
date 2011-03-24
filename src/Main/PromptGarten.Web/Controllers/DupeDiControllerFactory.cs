using System.Collections.Generic;
using System.Web.Mvc;
using System.Linq;
using System.Linq.Expressions;
using PromptGarten.Domain.Commands;
using PromptGarten.Domain.Handlers;
using PromptGarten.Domain.Services;
using System;
using System.Reflection;
using PromptGarten.Domain.Test;
using PromptGarten.Domain.Model;

namespace PromptGarten.Web.Controllers
{
    class DupeDiControllerFactory : DefaultControllerFactory
    {
        private readonly Dictionary<Type, Object> _bindings;
        private readonly IRepository _rep;
        private readonly ICommandHandler<AddTeacherCommand> _addTeacher;
        public DupeDiControllerFactory()
        {
            _rep = new InMemoryRepository()
                .WithKey<Teacher>(t => t.Id);
            _addTeacher = new AddTeacher(_rep);

            //
            // Fill InMemoryRepository
            //
            _rep.Insert(new Teacher(12563, "Pedro Félix"));
            _rep.Insert(new Teacher(13125, "Duarte Nunes"));
            _rep.Insert(new Teacher(76152, "Luís Falcão"));
            _rep.Insert(new Teacher(10702, "FM"));
            //
            // Fill bindings
            //
            _bindings = new Dictionary<Type, object>();
            _bindings[typeof(IRepository)] = _rep;
            _bindings[typeof(ICommandHandler<AddTeacherCommand>)] = _addTeacher;
        }
        protected override IController GetControllerInstance(
            System.Web.Routing.RequestContext requestContext,
            System.Type controllerType)
        {
            IController res = null;
            foreach (ConstructorInfo ctorInfo in controllerType.GetConstructors())
            {
                IEnumerable<Type> paramstype = ctorInfo.GetParameters().Select(p => p.ParameterType);
                if (paramstype.Except(_bindings.Keys).Count() == 0)
                {
                    res = (IController) ctorInfo.Invoke(paramstype.Select(t => _bindings[t]).ToArray());
                    break;
                }

            }
            return res ?? (res = base.GetControllerInstance(requestContext, controllerType));
        }
    }
}