using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.Loader;

namespace VacancyAggregator.Core.Utils
{
    public class AssemblyLoader
    {
        public static T Load<T>(string libPath, string connectionString)
        {
            AssemblyLoadContext loadContext = new AssemblyLoadContext(libPath);

            var assembly = loadContext.LoadFromAssemblyPath(libPath);

            var requiredType = FindImplementingRequiredInterfaceType(assembly.GetTypes(), typeof(T).FullName);

            if (requiredType == null)
            {
                throw new Exception($"В библиотеке {libPath} не найден класс, реализующий интерфейс {typeof(T).Name}.");
            }

            return (T)Activator.CreateInstance(requiredType, connectionString);
        }

        private static Type FindImplementingRequiredInterfaceType(IEnumerable<Type> types, string typeName)
        {
            Type connectorType = null;
            foreach (var type in types)
            {
                if (type.GetInterface(typeName) != null)
                {
                    connectorType = type;
                    break;
                }
            }
            return connectorType;
        }
    }
}
