namespace Sat.Recruitment.Common
{
    public static class FileReader
    {
        public static StreamReader ReadFile(string fullPath)
        {
            FileStream fileStream = new(fullPath, FileMode.Open);
            StreamReader reader = GetReader(fileStream);
            return reader;
        }

        private static StreamReader GetReader(FileStream fileStream)
        {
            return new StreamReader(fileStream);
        }
    }
}