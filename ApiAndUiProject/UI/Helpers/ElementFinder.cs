using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using ApiAndUiProject.UI.Attributes;
using ApiAndUiProject.UI.Components;

namespace ApiAndUiProject.UI.Helpers
{
    public class ElementFinder : IElementFinder
    {
        public BaseComponent? FindElementByName(object pageObject, string name)
        {
            var type = pageObject.GetType();

            var property = type.GetProperties(BindingFlags.Instance | BindingFlags.Public)
                .FirstOrDefault(p => p.GetCustomAttributes(typeof(NameAttribute), false)
                    .Cast<NameAttribute>().Any(a => a.Value == name)
                    && p.GetIndexParameters().Length == 0);

            if (property != null)
            {
                var component = property.GetValue(pageObject);
                if (component is BaseComponent baseComponent)
                    return baseComponent;
            }

            var field = type.GetFields(BindingFlags.Instance | BindingFlags.Public)
                .FirstOrDefault(f => f.GetCustomAttributes(typeof(NameAttribute), false)
                    .Cast<NameAttribute>().Any(a => a.Value == name));

            if (field != null)
            {
                var component = field.GetValue(pageObject);
                if (component is BaseComponent baseComponent)
                    return baseComponent;
            }

            var method = type.GetMethods(BindingFlags.Instance | BindingFlags.Public)
                .FirstOrDefault(m => m.GetCustomAttributes(typeof(NameAttribute), false)
                    .Cast<NameAttribute>().Any(a => a.Value == name)
                    && m.GetParameters().Length == 0);

            if (method != null)
            {
                var result = method.Invoke(pageObject, null);
                if (result is Task task)
                {
                    task.GetAwaiter().GetResult();
                    var resultProperty = task.GetType().GetProperty("Result");
                    var taskComponent = resultProperty?.GetValue(task);
                    if (taskComponent is BaseComponent _baseComponent)
                        return _baseComponent;
                }
                if (result is BaseComponent baseComponent)
                    return baseComponent;
            }

            foreach (var prop in type.GetProperties(BindingFlags.Instance | BindingFlags.Public))
            {
                var value = prop.GetValue(pageObject);
                if (value == null) continue;
                if (value is BaseComponent)
                {
                    try
                    {
                        var nestedComponent = FindElementByName(value, name);
                        if (nestedComponent != null)
                            return nestedComponent;
                    }
                    catch { }
                }
            }

            foreach (var fld in type.GetFields(BindingFlags.Instance | BindingFlags.Public))
            {
                var value = fld.GetValue(pageObject);
                if (value == null) continue;
                if (value is BaseComponent)
                {
                    try
                    {
                        var nestedComponent = FindElementByName(value, name);
                        if (nestedComponent != null)
                            return nestedComponent;
                    }
                    catch { }
                }
            }

            throw new Exception($"Element or component with name '{name}' not found.");
        }
    }
}