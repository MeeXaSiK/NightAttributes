using System;
using System.Linq;
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
            var fields = type.
                GetFields().
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
            var properties = type.
                GetProperties().
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
            var instance = instances.Length > 0 
                ? instances[0] 
                : new GameObject($"[LazyFind] {type.Name}", type).GetComponent(type);
            
            for (var i = 1; i < instances.Length; i++)
                Object.Destroy(instances[i]);

            return instance;
        }
    }
}