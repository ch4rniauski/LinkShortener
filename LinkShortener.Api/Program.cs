using ch4rniauski.LinkShortener.Api.Extensions;
using ch4rniauski.LinkShortener.Application.Extensions;
using ch4rniauski.LinkShortener.Infrastructure.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddCors(opt =>
    opt.AddDefaultPolicy(policy =>
    {
        policy
            .AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod();
    }));

builder.Services.AddSwaggerGen();

builder.Services.AddMediatrConfiguration();
builder.Services.AddAutoMapperConfiguration();
builder.Services.AddValidationConfiguration();
builder.Services.AddTokenProvidersConfiguration();
builder.Services.AddLinksContextConfiguration(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors();

app.MapControllers();

await app.ApplyMigrationsAsync();

await app.RunAsync();
