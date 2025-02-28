using BattleNetApiDiscordBot.Commands;
using BattleNetApiDiscordBot.Services;
using VincentBattleNetApiServer.Core.Databases;
using VincentBattleNetApiServer.Core.Options;
using VincentBattleNetApiServer.Core.ServiceInterfaces;
using VincentBattleNetApiServer.Core.Services;

await Host.CreateDefaultBuilder(args)
    .ConfigureAppConfiguration((hostingContext, config) =>
    {
        config.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

        if (hostingContext.HostingEnvironment.IsDevelopment())
            config.AddJsonFile($"appsettings.{hostingContext.HostingEnvironment.EnvironmentName}.json", optional: true, reloadOnChange: true);
    })
    .ConfigureServices((hostContext, services) =>
    {
        services.AddMemoryCache();
        services.AddHttpClient();

        services.Configure<BattleNetApiLiteDB>(hostContext.Configuration.GetSection(nameof(BattleNetApiLiteDB)));

        services.AddTransient<ILiteDbService, LiteDbService>();
        services.AddTransient<ICacheService, CacheService>();
        services.AddTransient<IBattleNetAccessTokenService, BattleNetAccessTokenService>();
        services.AddTransient<GetKoreaWowTokenCommand>();

        services.AddHostedService<DiscordBotService>();
    })
    .ConfigureLogging(logging =>
    {
        logging.ClearProviders();
        logging.AddConsole();
    })
    .Build()
    .RunAsync();