using System.IO;

namespace Bibliotheca.Server.Depository.FileSystem.Api.Specs
{
    public static class TestContext
    {
        public static string GetContentDirectory()
        {
            var contentDirectory = Directory.GetCurrentDirectory();
            if (!contentDirectory.EndsWith("Bibliotheca.Server.Depository.FileSystem.Api.Specs"))
            {
                contentDirectory = Path.Combine(contentDirectory, "test/Bibliotheca.Server.Depository.FileSystem.Api.Specs");
            }

            return contentDirectory;
        }
    }
}