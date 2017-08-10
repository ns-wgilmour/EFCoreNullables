namespace EFCoreNullable
{
    public interface IFileHelper
    {
        string GetPath(string filename, bool deleteIfExists = false);
    }
}
