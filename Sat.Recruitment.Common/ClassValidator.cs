using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace Sat.Recruitment.Common
{
    public static class ClassValidator
    {
        public static Dictionary<string, string> GetRequiredFields<T>() where T : new()
        {
            Dictionary<string, string> requiredFields = new();

            Type type = typeof(T);
            PropertyInfo[] properties = type.GetProperties();

            foreach (PropertyInfo property in properties)
            {
                if (Attribute.IsDefined(property, typeof(RequiredAttribute)))
                {
                    requiredFields.Add(property.Name, "");
                }
            }

            return requiredFields;
        }

        public static void PopulateValueRequiredFields<T>(T userRegistration, Dictionary<string, string> requiredFields)
        {
            if (userRegistration == null)
                return;

            foreach (var property in userRegistration.GetType().GetProperties())
            {
                if (requiredFields.ContainsKey(property.Name))
                {
                    requiredFields[property.Name] = property?.GetValue(userRegistration)?.ToString() ?? "";
                }
            }
        }

        public static List<string> ValidateRequiredFields(Dictionary<string, string> fields)
        {
            List<string> errors = new();

            foreach (var field in fields)
            {
                if (string.IsNullOrEmpty(field.Value))
                {
                    errors.Add($"{field.Key.Capitalizate()} is required.");
                }
            }

            return errors;
        }
    }
}