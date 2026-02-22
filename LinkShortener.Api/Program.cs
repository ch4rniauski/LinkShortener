using ch4rniauski.LinkShortener.Application.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddMediatrConfiguration();
builder.Services.AddAutoMapperConfiguration();
builder.Services.AddValidationConfiguration();
builder.Services.AddTokenProvidersConfiguration();

var app = builder.Build();

app.MapControllers();

await app.RunAsync();
