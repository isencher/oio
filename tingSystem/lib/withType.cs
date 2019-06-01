using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Infrastructure;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ting.lib
{
    public class withType
    {

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

        public static dynamic InstanceOfType(string assemblyname, string typeName)
        {
            Type T = GetType(assemblyname, typeName);
            if (T == null) { return null; }
            else { return Activator.CreateInstance(T); }

        }
        public static dynamic ListofType(string typename)
        {
            Type T = GetType(typename);

            var listType = typeof(List<>);
            var constructedListType = listType.MakeGenericType(T);

            var instance = Activator.CreateInstance(constructedListType);

            return instance;
        }
        public static T IsExist<T>(T instance, int tag)
            where T : ContainerControl, new()
        {
            // 1. instance == null   --> new a new instance
            // 2. instance != null, but isdisposed == true    --> new a new instance
            // 3. instance != null, and isdisposed == false, but tag is not same -->disposed,and new a new instance
            // 4. instance != null, and isdisposed == false, and tag is same  --> no way
            if (instance == null) { instance = new T(); instance.Tag = tag; }
            else if (instance != null && instance.IsDisposed == true) { instance = new T(); instance.Tag = tag; }
            else if (instance != null && instance.IsDisposed == false && Convert.ToInt32(instance.Tag) != tag)
            { instance.Dispose(); instance = new T(); instance.Tag = tag; }
            return instance;
        }

        public static string[] GetNaviProps<T>()
            where T: class,new()
        {
            return typeof(T).GetProperties()
                        .Where(p => (( typeof(IEnumerable).IsAssignableFrom(p.PropertyType) 
                             && p.PropertyType != typeof(string) )
                             || p.PropertyType.Namespace == typeof(T).Namespace)
                             && !Attribute.IsDefined(p, typeof(NotMappedAttribute)) )
                        .Select(p => p.Name)
                        .ToArray();
        }

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
        /// Perform a deep Copy of the object.
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
