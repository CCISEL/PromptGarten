using System.Collections.Generic;
using System.Web.Mvc;
using System.Linq;
using PromptGarten.Domain.Commands;
using PromptGarten.Domain.Handlers;
using PromptGarten.Domain.Services;
using System;
using PromptGarten.Domain.Test;
using PromptGarten.Domain.Model;

namespace PromptGarten.Web.Controllers
{
    class DupeDiControllerFactory : DefaultControllerFactory
    {
        private readonly Dictionary<Type, Object> _bindings;
        public DupeDiControllerFactory()
        {
            _bindings = new Dictionary<Type, object>();
            _bindings[typeof(IRepository)] = new InMemoryRepository().WithKey<Teacher>(t => t.Id);
            _bindings[typeof(ICommandHandler<AddTeacherCommand>)] = new AddTeacher(GetInstance<IRepository>());
        }
        protected override IController GetControllerInstance(
            System.Web.Routing.RequestContext requestContext,
            System.Type controllerType)
        {
            var res = (from ctorInfo in controllerType.GetConstructors()
                       let paramstype = ctorInfo.GetParameters().Select(p => p.ParameterType)
                       where paramstype.Except(_bindings.Keys).Count() == 0
                       select (IController)ctorInfo.Invoke(paramstype.Select(t => _bindings[t]).ToArray()));
            return res.Count() != 0 ? 
                res.First() : 
                base.GetControllerInstance(requestContext, controllerType);
        }
        public T GetInstance<T>()
        {
            return (T) _bindings[typeof(T)];
        }
    }
}