using System.Data.Common;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Internal;

namespace TheEmployeeAPI.Tests;

public class CustomWebApplicationFactory : WebApplicationFactory<Program>
{
    public static TestSystemClock SystemClock { get; } = new TestSystemClock();

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services => 
        {
            var systemClockDescriptor = services.Single(d => d.ServiceType == typeof(ISystemClock));
            services.Remove(systemClockDescriptor);
            services.AddSingleton<ISystemClock>(SystemClock);
        });
    }
}

public class TestSystemClock : ISystemClock
{
    public DateTimeOffset UtcNow { get; } = DateTimeOffset.Parse("2022-01-01T00:00:00Z");
}