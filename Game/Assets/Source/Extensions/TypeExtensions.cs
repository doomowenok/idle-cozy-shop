using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Extensions
{
    public static class TypeExtensions
    {
        public static List<Type> FindAllTypesImplementing<T>() where T : class
        {
            Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
        
            List<Type> implementingTypes = new List<Type>();
        
            foreach (var assembly in assemblies)
            {
                Type[] types;
                try
                {
                    types = assembly.GetTypes();
                }
                catch (ReflectionTypeLoadException e)
                {
                    types = e.Types.Where(t => t != null).ToArray();
                }
                
                foreach (Type type in types)
                {
                    if (type != null && typeof(T).IsAssignableFrom(type) && type.IsClass && !type.IsAbstract)
                    {
                        implementingTypes.Add(type);
                    }
                }
            }
        
            return implementingTypes;
        }
    }
}