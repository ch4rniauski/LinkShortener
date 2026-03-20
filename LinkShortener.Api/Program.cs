using ch4rniauski.LinkShortener.Api.Common;
using ch4rniauski.LinkShortener.Application.Extensions;
using ch4rniauski.LinkShortener.Infrastructure.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddCors(opt =>
    opt.AddDefaultPolicy(policy =>
    {
        policy
            .WithOrigins("http://localhost:4200")
            .AllowAnyHeader()
            .AllowAnyMethod();
    }));

builder.Services.AddSwaggerGen();

EnvironmentalHelper.SetEnvironmentVariablesFromDotEnvFile();
builder.Services.AddMediatrConfiguration();
builder.Services.AddAutoMapperConfiguration();
builder.Services.AddValidationConfiguration();
builder.Services.AddTokenProvidersConfiguration();
builder.Services.AddTokenConfiguration();
builder.Services.AddLinksContextConfiguration(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors();

app.MapControllers();

//await app.ApplyMigrationsAsync();

await app.RunAsync();
