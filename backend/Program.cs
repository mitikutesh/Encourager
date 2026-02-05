using Encourager.Api.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
        policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
});

builder.Services.AddSingleton<VerseService>();
builder.Services.AddOpenApi();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseCors();

app.MapGet("/api/verse/random", (VerseService verseService, string? lang, int? index) =>
{
    var result = index.HasValue
        ? verseService.GetByIndex(lang ?? "en", index.Value)
        : verseService.GetRandom(lang ?? "en");
    return Results.Ok(new { result.Verse.Text, result.Verse.Reference, result.Index });
})
.WithName("GetRandomVerse");

app.Run();
