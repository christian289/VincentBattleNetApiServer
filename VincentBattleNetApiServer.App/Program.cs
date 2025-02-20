using VincentBattleNetApiServer.Core;
using VincentBattleNetApiServer.Core.Databases;
using VincentBattleNetApiServer.Core.Options;
using VincentBattleNetApiServer.Core.ServiceInterfaces;
using VincentBattleNetApiServer.Core.Services;

#region Build WebApplication return 'app'
var builder = WebApplication.CreateSlimBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddMemoryCache();
builder.Services.AddCors();
builder.Services.AddOutputCache(options =>
{
    options.AddBasePolicy(policy => policy.Expire(TimeSpan.FromMinutes(10)));
});

// SwaggerUI 대체 (Microsoft 공식 Document UI지원)
// https://learn.microsoft.com/ko-kr/aspnet/core/fundamentals/openapi/aspnetcore-openapi?view=aspnetcore-9.0&tabs=visual-studio#customize-the-openapi-document-name
// 문서명: api_index_document
// Endpoint: openapi/api_index_document.json
builder.Services.AddOpenApi("api_index_document");

builder.Services.AddTransient<ILiteDbService, LiteDbService>();
builder.Services.AddScoped<ICacheService, CacheService>();
builder.Services.AddScoped<IBattleNetAccessTokenService, BattleNetAccessTokenService>();
builder.Services.AddScoped<IRealmApiService, RealmApiService>();
builder.Services.AddScoped<IToyApiService, ToyApiService>();
builder.Services.AddScoped<IWowTokenApiService, WowTokenApiService>();
builder.Services.AddScoped<IAccountProfileSummaryService, AccountProfileSummaryService>();
builder.Services.AddScoped<IAccountToysCollectionSummaryService, AccountToysCollectionSummaryService>();
builder.Services.AddScoped<ICharacterAchievementsSummaryService, CharacterAchievementsSummaryService>();

builder.Services.AddHttpClient();
builder.Services.AddHttpClient<IBattleNetAccessTokenService, BattleNetAccessTokenService>();
builder.Services.AddHttpClient<IRealmApiService, RealmApiService>();
builder.Services.AddHttpClient<IToyApiService, ToyApiService>();
builder.Services.AddHttpClient<IWowTokenApiService, WowTokenApiService>();
builder.Services.AddHttpClient<IAccountProfileSummaryService, AccountProfileSummaryService>();
builder.Services.AddHttpClient<IAccountToysCollectionSummaryService, AccountToysCollectionSummaryService>();
builder.Services.AddHttpClient<ICharacterAchievementsSummaryService, CharacterAchievementsSummaryService>();

builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.TypeInfoResolverChain.Insert(0, SourceGenerationContext.Default);
});

builder.Services.Configure<BattleNetApiLiteDB>(builder.Configuration.GetSection(nameof(BattleNetApiLiteDB)));
builder.Services.Configure<BattleNetAuth>(builder.Configuration.GetSection(nameof(BattleNetAuth)));

builder.Configuration.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

if (builder.Environment.IsDevelopment())
{
    builder.Configuration.AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true);
}

var app = builder.Build();
#endregion

app.UseOutputCache();
app.UseCors("AllowNextJS");

if (app.Environment.IsDevelopment())
{
    // 기본 Endpoints: /openapi/{documentName}/openapi.json
    // https://learn.microsoft.com/ko-kr/aspnet/core/fundamentals/openapi/aspnetcore-openapi?view=aspnetcore-9.0&tabs=visual-studio#customize-the-openapi-document-name
    app.MapOpenApi().CacheOutput();
}

app.UseHttpsRedirection();

#region Endpoints
// Minimal API 사용할 때는 HttpClient를 바로 주입받아서 사용할 수 없고 HttpClientFactory를 이용해서 HttpClient를 사용해야 한다.
// HttpClient는 사실상 Http 메서드를 사용하기 위한 껍데기이기에 수명관리의 의미가 없다.
// 정작 중요한 것은 HttpMessageHandler인데 HttpClientFactory를 이용해서 HttpClient를 사용할 경우
//HttpMessageHandler가 유효한 경우 재사용 될 수 있기 때문에 CreateClient() 메서드가 의미가 있는 것이다.

app.MapGet("/api/realmsindex", async ([FromServices] IRealmApiService realmServ) =>
{
    try
    {
        var gameData = await realmServ.GetRealmsIndexAsync();
        return Results.Ok(gameData);
    }
    catch (Exception ex)
    {
        return Results.Problem(detail: ex.Message);
    }
});
app.MapGet("/api/toyindex", async ([FromServices] IToyApiService toyServ) =>
{
    try
    {
        var gameData = await toyServ.GetToyIndexAsync();
        return Results.Ok(gameData);
    }
    catch (Exception ex)
    {
        return Results.Problem(detail: ex.Message);
    }
});
app.MapGet("/api/toy/{toyId}", async ([FromServices] IToyApiService toyServ, int toyId) =>
{
    try
    {
        var gameData = await toyServ.GetToyAsync(toyId);
        return Results.Ok(gameData);
    }
    catch (Exception ex)
    {
        return Results.Problem(detail: ex.Message);
    }
});
app.MapGet("/api/wowtokenindex", async ([FromServices] IWowTokenApiService wowTokenServ) =>
{
    try
    {
        var gameData = await wowTokenServ.GetWowTokenIndexAsync();
        return Results.Ok(gameData);
    }
    catch (Exception ex)
    {
        return Results.Problem(detail: ex.Message);
    }
});
app.MapGet("/api/accountprofilesummary", async ([FromServices] IAccountProfileSummaryService accountProfileSummaryServ) =>
{
    try
    {
        var gameData = await accountProfileSummaryServ.GetResAccountProfileSummaryAsync();
        return Results.Ok(gameData);
    }
    catch (Exception ex)
    {
        return Results.Problem(detail: ex.Message);
    }
});
app.MapGet("/api/accounttoyscollectionsummary", async ([FromServices] IAccountToysCollectionSummaryService accountToysCollectionSummaryServ) =>
{
    try
    {
        var gameData = await accountToysCollectionSummaryServ.GetResAccountToysCollectionSummaryAsync();
        return Results.Ok(gameData);
    }
    catch (Exception ex)
    {
        return Results.Problem(detail: ex.Message);
    }
});
app.MapGet("/api/characterachievementssummary/{realmSlug}/{characterName}/achievements", async ([FromServices] ICharacterAchievementsSummaryService characterAchievementsSummaryServ, string realmSlug, string characterName) =>
{
    try
    {
        var gameData = await characterAchievementsSummaryServ.GetResCharacterAchievementsSummaryAsync(realmSlug, characterName);
        return Results.Ok(gameData);
    }
    catch (Exception ex)
    {
        return Results.Problem(detail: ex.Message);
    }
});
#endregion

// launchSettings.json에 있는 launchUrl을 기준으로 브라우저를 실행시킨다.
// 개발 모드로 실행할 것인지도 인자를 통해 설정할 수 있다.
app.Run();
