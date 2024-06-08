using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace AppMobilenBlog.Helpers
{
    public static class PropertyUtil
    {
        /// <summary>
        /// Extension method for copying property values from one object to another. 
        /// Property must have the same type and name in order to be copied.
        /// </summary>
        /// <typeparam name="TargetType">Target type to which values will be copied.</typeparam>
        /// <typeparam name="SourceType">Source type to which values will be copied.</typeparam>
        /// <param name="targetObject">Target object to which values will be copied.</param>
        /// <param name="sourceObject">Source object to which values will be copied.</param>
        /// <returns>Target object.</returns>
        public static T CopyProperties<T, T2>(this T targetObject, T2 sourceObject)
        {
            targetObject.GetTypeProperties().Where(p => p.CanWrite).ToList()
                .ForEach(property => FindAndReplaceProperty(targetObject, sourceObject, property));
            return targetObject;
        }

        private static void FindAndReplaceProperty<T, T2>(T targetObject, T2 sourceObject, PropertyInfo property)
        {
            if (sourceObject.GetTypeProperties().Any(prop => CheckIfPropertyExistInSource(prop, property)))
            {
                property.SetValue(targetObject, sourceObject.GetPropertyValue(property.Name), default);
            }
        }
        private static IEnumerable<PropertyInfo> GetTypeProperties<T2>(this T2 sourceObject)
            => sourceObject.GetType().GetProperties();
        private static bool CheckIfPropertyExistInSource(PropertyInfo prop, PropertyInfo property)
            => string.Equals(property.Name, prop.Name, StringComparison.InvariantCultureIgnoreCase)
                    && prop.PropertyType.Equals(property.PropertyType);
        private static object GetPropertyValue<T>(this T source, string propertyName)
            => source.GetType().GetProperty(propertyName).GetValue(source, null);
    }
}
