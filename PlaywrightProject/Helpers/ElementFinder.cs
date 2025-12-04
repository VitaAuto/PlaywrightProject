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
            var type = pageObject.GetType();

            // 1. Поиск свойства с атрибутом Name
            var property = type.GetProperties(BindingFlags.Instance | BindingFlags.Public)
                .FirstOrDefault(p => p.GetCustomAttributes(typeof(NameAttribute), false)
                    .Cast<NameAttribute>().Any(a => a.Value == name)
                    && p.GetIndexParameters().Length == 0);

            if (property != null)
                return property.GetValue(pageObject);

            // 2. Поиск поля с атрибутом Name
            var field = type.GetFields(BindingFlags.Instance | BindingFlags.Public)
                .FirstOrDefault(f => f.GetCustomAttributes(typeof(NameAttribute), false)
                    .Cast<NameAttribute>().Any(a => a.Value == name));

            if (field != null)
                return field.GetValue(pageObject);

            // 3. Поиск метода с атрибутом Name (если нужно)
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
                    return resultProperty?.GetValue(task);
                }
                return result;
            }

            // 4. Рекурсивный поиск во вложенных компонентах (по свойствам)
            foreach (var prop in type.GetProperties(BindingFlags.Instance | BindingFlags.Public))
            {
                var value = prop.GetValue(pageObject);
                if (value == null) continue;
                if (value is BaseComponent)
                {
                    try
                    {
                        var nestedResult = FindElementByName(value, name);
                        if (nestedResult != null)
                            return nestedResult;
                    }
                    catch
                    {
                        // Игнорируем ошибку и ищем дальше
                    }
                }
            }

            // 5. Рекурсивный поиск во вложенных компонентах (по полям)
            foreach (var fld in type.GetFields(BindingFlags
.Instance | BindingFlags.Public))
            {
                var value = fld.GetValue(pageObject);
                if (value == null) continue;
                if (value is BaseComponent)
                {
                    try
                    {
                        var nestedResult = FindElementByName(value, name);
                        if (nestedResult != null)
                            return nestedResult;
                    }
                    catch
                    {
                        // Игнорируем ошибку и ищем дальше
                    }
                }
            }

            // 6. Не найдено
            throw new Exception($"Element or component with name '{name}' not found.");
        }
    }
}