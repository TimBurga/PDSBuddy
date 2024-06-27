using PDSBuddy;
using Coravel;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

var builder = Host.CreateApplicationBuilder();

builder.Services.AddScheduler();

builder.Services.AddTransient<PdsBackupJob>();
builder.Services.AddTransient<GithubService>();
builder.Services.AddTransient<MailService>();

builder.Services.AddHttpClient<PdsClient>(config =>
{
    config.BaseAddress = Config.PDS_URL;
});

builder.Services.AddLogging(x => x.AddSimpleConsole().SetMinimumLevel(LogLevel.Information));

var app = builder.Build();

app.Services.UseScheduler(scheduler => 
    scheduler.Schedule<PdsBackupJob>()
        .Daily()
        .RunOnceAtStart()
        .PreventOverlapping(Guid.NewGuid().ToString()));

try
{
    app.Run();
}
catch (Exception ex)
{
    using ILoggerFactory factory = LoggerFactory.Create(x => x.AddConsole().SetMinimumLevel(LogLevel.Information));
    var logger = factory.CreateLogger("Program");
    logger.LogCritical(ex, "Fatal error - unable to recover");

    var mailService = app.Services.GetRequiredService<MailService>();
    await mailService.Send(ex.Message);

    await app.StopAsync();
}