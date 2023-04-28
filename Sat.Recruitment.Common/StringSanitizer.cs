using System.Text.RegularExpressions;

namespace Sat.Recruitment.Common
{
    public static class StringSanitizer
    {
        public static bool IsValidEmail(this string email)
        {
            string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            return Regex.IsMatch(email, pattern);
        }

        public static string NormalizeEmail(this string email)
        {
            email = email.Trim();
            email = email.ToLower();

            string[] aux = email.Split(new char[] { '@' }, StringSplitOptions.RemoveEmptyEntries);
            int atIndex = aux[0].IndexOf("+", StringComparison.Ordinal);
            aux[0] = atIndex < 0 ? aux[0].Replace(".", "") : aux[0].Replace(".", "").Remove(atIndex);
            return string.Join("@", new string[] { aux[0], aux[1] });
        }

        public static string Capitalizate(this string content)
        {
            return Regex.Replace(content, @"(?:(M|m)(c)|(\b))([a-z])", delegate (Match m) {
                return string.Concat(m.Groups[1].Value.ToUpper(), m.Groups[2].Value, m.Groups[3].Value, m.Groups[4].Value.ToUpper());
            });
        }
    }
}