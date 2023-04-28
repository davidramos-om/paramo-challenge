using Sat.Recruitment.Common;

namespace Sat.Recruitment.InfraEstructure.DataContext
{
    public sealed class LoadUsers : ILoadUsers
    {
        public List<string[]> FromFile(char delimiter, string filePath)
        {
            List<string[]> attributes = new();

            string appPath = Helper.GetAppDirectory();
            string fullPath = Helper.CombinePath(appPath, filePath);

            StreamReader reader = FileReader.ReadFile(fullPath);

            while (reader.Peek() >= 0)
            {
                var line = reader.ReadLineAsync().Result;
                if (line == null)
                    continue;

                var chunks = line.Split(delimiter, StringSplitOptions.RemoveEmptyEntries);
                if (chunks.Length == 0)
                    continue;

                attributes.Add(chunks);
            }

            reader.Close();
            return attributes;
        }
    }
}
