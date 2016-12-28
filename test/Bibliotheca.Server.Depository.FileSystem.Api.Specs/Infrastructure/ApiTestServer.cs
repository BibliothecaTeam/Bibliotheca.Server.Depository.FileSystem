using System;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;

namespace Bibliotheca.Server.Depository.FileSystem.Api.Specs
{
    public sealed class ApiTestServer : IDisposable
    {
        private static volatile TestServer _instance;
        private static object _syncRoot = new object();
        private bool _disposed = false;

        private ApiTestServer() { }

        public static TestServer Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_syncRoot)
                    {
                        if (_instance == null)
                        {
                            _instance = CreateTestServer();
                        }
                    }
                }

                return _instance;
            }
        }

        private static TestServer CreateTestServer()
        {
            var contentDirectory = TestContext.GetContentDirectory();

            var webHostBuilder = new WebHostBuilder()
                .UseContentRoot(contentDirectory)
                .UseStartup<Startup>();

            return new TestServer(webHostBuilder);
        }

        private void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _instance.Dispose();
                }

                _disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~ApiTestServer()
        {
            Dispose(false);
        }
    }
}