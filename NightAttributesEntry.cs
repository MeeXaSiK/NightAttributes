using System;
using System.Linq;
using DG.DemiLib.Attributes;
using UnityEngine;

namespace Development.Global.Code.NightAttributes
{
    [DeScriptExecutionOrder(-9000), DisallowMultipleComponent]
    public sealed class NightAttributesEntry : MonoBehaviour
    {
        private void Awake()
        {
            var all = FindObjectsOfType<MonoBehaviour>();

            foreach (var monoBehaviour in all)
            {
                var type = monoBehaviour.GetType();
                
                LazyFindFields(monoBehaviour, type);
                LazyFindProperties(monoBehaviour, type);
            }
        }

        private void LazyFindFields(MonoBehaviour monoBehaviour, Type type)
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

        private void LazyFindProperties(MonoBehaviour monoBehaviour, Type type)
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
        
        private object GetLazy(Type type)
        {
            var instances = FindObjectsOfType(type, true);

            for (var i = 1; i < instances.Length; i++)
                Destroy(instances[i]);

            var instance = instances[0];

            if (instance == null)
                instance = new GameObject($"[LazyFind] {type.Name}", type);

            return instance;
        }
    }
}