namespace BulkyBook.Tests.UI
{
    public class HelperMethods
    {
        public static string GetProjectRootPath()
        {
            var currentDirectory = Directory.GetCurrentDirectory();
            var rootDirectory = new DirectoryInfo(currentDirectory);
            while (rootDirectory != null && !rootDirectory.GetFiles("*.csproj").Any())
            {
                rootDirectory = rootDirectory.Parent;
            }

            return rootDirectory?.FullName;
        }
    }
}

