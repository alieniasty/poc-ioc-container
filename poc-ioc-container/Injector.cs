using System;
using System.Collections.Generic;
using System.Linq;

namespace poc_ioc_container
{
    /*Very poor injector. Just a PoC. Not gonna work with static classes :)*/
    public class Injector
    {
        private readonly IDictionary<Type, Func<object>> _registrations = new Dictionary<Type, Func<object>>();

        public void Bind<TType, TConcrete>() where TConcrete : TType
        {
            _registrations[typeof(TType)] = () => ResolveByType(typeof(TConcrete));
        }

        public void Bind<T>(T instance)
        {
            _registrations[typeof(T)] = () => instance;
        }

        private object ResolveByType(Type type)
        {
            var constructor = type.GetConstructors().Single();
            var constructorArguments = constructor.GetParameters()
                .Select(p => Resolve(p.ParameterType))
                .ToArray();

            return constructor.Invoke(constructorArguments);
        }

        public object Resolve(Type type)
        {
            if (_registrations.TryGetValue(type, out var registerActor))
            {
                return registerActor();
            }

            return ResolveByType(type);
        }
    }
}
