using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration.Json;
using Microsoft.Extensions.FileProviders;

namespace Flags.Tests.Common;

public sealed class FlagsApplicationFactory : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureAppConfiguration((context, config) =>
        {
            config.Sources.Add(new JsonConfigurationSource
            {
                Path = "appsettings.integration.json",
                Optional = false,
                ReloadOnChange = true,
                FileProvider = new PhysicalFileProvider(Environment.CurrentDirectory)
            });
        });
    }
}