using PDSBuddy;
using Coravel;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = Host.CreateApplicationBuilder();

builder.Services.AddScheduler();

builder.Services.AddTransient<PdsBackupJob>();
builder.Services.AddTransient<GithubService>();

builder.Services.AddHttpClient<PdsClient>(config =>
{
    config.BaseAddress = Config.PDS_URL;
});

var app = builder.Build();

app.Services.UseScheduler(scheduler => 
    scheduler.Schedule<PdsBackupJob>()
        .Daily()
        .RunOnceAtStart()
        .PreventOverlapping(Guid.NewGuid().ToString()));

app.Run();