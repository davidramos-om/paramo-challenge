namespace Sat.Recruitment.InfraEstructure.DataContext
{
    public interface ILoadUsers
    {
        public abstract List<string[]> FromFile(char delimiter, string filePath);
    }
}