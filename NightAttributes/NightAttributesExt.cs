using System;
using System.Linq;
using System.Reflection;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Development.Global.Code.NightAttributes
{
    public static class NightAttributesExt
    {
        public static void CheckForNightAttributes(this MonoBehaviour monoBehaviour)
        {
            var type = monoBehaviour.GetType();
            LazeFindAttribute(monoBehaviour, type);
        }

        private static void LazeFindAttribute(MonoBehaviour monoBehaviour, Type type)
        {
            LazyFindFields(monoBehaviour, type);
            LazyFindProperties(monoBehaviour, type);
        }
        
        private static void LazyFindFields(MonoBehaviour monoBehaviour, Type type)
        {
            var flags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance |
                        BindingFlags.CreateInstance | BindingFlags.Static | BindingFlags.GetField;
            
            var fields = type.
                GetFields(flags).
                Where(field => Attribute.IsDefined(field, typeof(LazyFindAttribute)));

            foreach (var field in fields)
            {
                var fieldType = field.FieldType;
                var instance = GetLazy(fieldType);
                    
                field.SetValue(monoBehaviour, instance);
            }
        }
        
        private static void LazyFindProperties(MonoBehaviour monoBehaviour, Type type)
        {
            var flags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance |
                        BindingFlags.CreateInstance | BindingFlags.Static | BindingFlags.GetProperty;
            
            var properties = type.
                GetProperties(flags).
                Where(property => Attribute.IsDefined(property, typeof(LazyFindAttribute)));
                
            foreach (var property in properties)
            {
                var propertyType = property.PropertyType;
                var instance = GetLazy(propertyType);
                    
                property.SetValue(monoBehaviour, instance);
            }
        }
        
        private static object GetLazy(Type type)
        {
            var instances = Object.FindObjectsOfType(type, true);

            for (var i = 1; i < instances.Length; i++)
                Object.Destroy(instances[i]);

            var instance = instances[0];

            if (instance == null)
                instance = new GameObject($"[LazyFind] {type.Name}", type);

            return instance;
        }
    }
}