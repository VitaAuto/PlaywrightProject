using System;

namespace PlaywrightProject.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Property | AttributeTargets.Method)]
    public class NameAttribute : Attribute
    {
        public string Value { get; }
        public NameAttribute(string value) => Value = value;
    }
}