using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using PlaywrightProject.Attributes;
using PlaywrightProject.Components;

namespace PlaywrightProject.Helpers
{
    public static class ElementFinder
    {
        public static object FindElementByName(object pageObject, string name)
        {
            // 1. Поиск свойства с атрибутом Name в текущем объекте
            var property = pageObject.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public)
                .FirstOrDefault(p => p.GetCustomAttributes(typeof(NameAttribute), false)
                    .Cast<NameAttribute>().Any(a => a.Value == name)
                    && p.GetIndexParameters().Length == 0);

            if (property != null)
                return property.GetValue(pageObject);

            // 2. Поиск метода с атрибутом Name (если нужно)
            var method = pageObject.GetType().GetMethods(BindingFlags.Instance | BindingFlags.Public)
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
                    return resultProperty?.GetValue(task);
                }
                return result;
            }

            // 3. Рекурсивный поиск во вложенных компонентах
            foreach (var prop in pageObject.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public))
            {
                var value = prop.GetValue(pageObject);
                if (value == null) continue;

                // Проверяем, что это компонент (наследует BaseComponent)
                if (value is BaseComponent)
                {
                    var nestedResult = FindElementByName(value, name);
                    if (nestedResult != null)
                        return nestedResult;
                }
            }

            // 4. Не найдено
            throw new Exception($"Element or component with name '{name}' not found.");
        }
    }
}