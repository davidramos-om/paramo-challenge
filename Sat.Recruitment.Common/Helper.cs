
using System.Reflection;

namespace Sat.Recruitment.Common
{
    public static class Helper
    {
        public static string GetAppDirectory()
        {
            var asm = Assembly.GetExecutingAssembly();
            var directory = asm.Location;
            if (directory == null)
                return "";

            var path = Path.GetDirectoryName(directory);
            return path ?? "";
        }

        public static string CombinePath(string path1, string path2)
        {
            return Path.Combine(path1, path2);
        }
    }
}