using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace ting.lib.net
{
    public partial class withType
    {
        /// <summary>
        /// to get type by type string name
        /// </summary>
        /// <param name="typeName">type string name</param>
        /// <returns>type</returns>
        public static Type GetType(string typeName)
        {
            Type type = null;
            Assembly[] assemblyArray = AppDomain.CurrentDomain.GetAssemblies();
            int assemblyArrayLength = assemblyArray.Length;
            for (int i = 0; i < assemblyArrayLength; ++i)
            {
                if (typeName != "" && typeName != null)
                {
                    type = assemblyArray[i].GetType(typeName);
                    if (type != null)
                    {
                        return type;
                    }
                }
            }

            for (int i = 0; (i < assemblyArrayLength); ++i)
            {
                Type[] typeArray = assemblyArray[i].GetTypes();
                int typeArrayLength = typeArray.Length;
                for (int j = 0; j < typeArrayLength; ++j)
                {
                    if (typeArray[j].Name.Equals(typeName))
                    {
                        return typeArray[j];
                    }
                }
            }
            return type;
        }
        /// <summary>
        /// get type by type string name and dll file name
        /// </summary>
        /// <param name="dllname">dll file name</param>
        /// <param name="typeName">type string name</param>
        /// <returns>type</returns>
        public static Type GetType(string dllname, string typeName)
        {
            Type type = null;
            Assembly assembly = Assembly.Load(dllname);

            if (typeName != "" && typeName != null)
            {
                type = assembly.GetType(typeName);
                if (type != null)
                {
                    return type;
                }
            }

            Type[] typeArray = assembly.GetTypes();
            int typeArrayLength = typeArray.Length;

            for (int j = 0; j < typeArrayLength; ++j)
            {
                if (typeArray[j].Name.Equals(typeName))
                {
                    return typeArray[j];
                }
            }

            return type;
        }
        /// <summary>
        /// to get a instance by assembly name and type string name
        /// </summary>
        /// <param name="assemblyname"></param>
        /// <param name="typeName"></param>
        /// <returns></returns>
        public static dynamic InstanceOfType(string assemblyname, string typeName)
        {
            Type T = GetType(assemblyname, typeName);
            if (T == null) { return null; }
            else { return Activator.CreateInstance(T); }

        }
        /// <summary>
        /// to get a list<T> type instance by type string name
        /// </summary>
        /// <param name="typename">type string name</param>
        /// <returns>list<T> type instance</returns>
        public static dynamic ListofType(string typename)
        {
            Type T = GetType(typename);

            var listType = typeof(List<>);
            var constructedListType = listType.MakeGenericType(T);

            var instance = Activator.CreateInstance(constructedListType);

            return instance;
        }
        /// <summary>
        /// to get navigation propertyinfos of some class
        /// </summary>
        /// <typeparam name="T">a class</typeparam>
        /// <returns>propertyinfos</returns>
        public static string[] GetNaviProps<T>()
            where T : class, new()
        {
            return typeof(T).GetProperties()
                        .Where(p => ((typeof(IEnumerable).IsAssignableFrom(p.PropertyType)
                             && p.PropertyType != typeof(string))
                             || p.PropertyType.Namespace == typeof(T).Namespace)
                             && !Attribute.IsDefined(p, typeof(NotMappedAttribute)))
                        .Select(p => p.Name)
                        .ToArray();
        }
        /// <summary>
        /// to get propertyinfos of some class
        /// </summary>
        /// <typeparam name="T">a class</typeparam>
        /// <returns>propertyinfos</returns>
        public static List<PropertyInfo> GetProps<T>()
            where T : class, new()
        {
            return typeof(T).GetProperties()
                        .Where(p => ((typeof(IEnumerable).IsAssignableFrom(p.PropertyType)
                             && p.PropertyType != typeof(string))
                             || p.PropertyType.Namespace == typeof(T).Namespace)
                             )
                        .Select(p => p)
                        .ToList();
        }

        /// <summary>
        /// to perform a deep copy of the object.
        /// </summary>
        /// <typeparam name="T">The type of object being copied.</typeparam>
        /// <param name="source">The object instance to copy.</param>
        /// <returns>The copied object.</returns>
        public static T Clone<T>(T source)
        {
            if (!typeof(T).IsSerializable)
            {
                throw new ArgumentException("The type must be serializable.", "source");
            }

            // Don't serialize a null object, simply return the default for that object
            if (Object.ReferenceEquals(source, null))
            {
                return default(T);
            }

            IFormatter formatter = new BinaryFormatter();
            Stream stream = new MemoryStream();
            using (stream)
            {
                formatter.Serialize(stream, source);
                stream.Seek(0, SeekOrigin.Begin);
                return (T)formatter.Deserialize(stream);
            }
        }

    }
}
