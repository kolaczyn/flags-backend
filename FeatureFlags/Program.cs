using FeatureFlags.Application.UseCases;
using FeatureFlags.Domain.Repositories;
using FeatureFlags.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi().AddControllers();
{
    builder.Services.AddTransient<GetAllFlagsUseCase>();
    builder.Services.AddTransient<IFlagsRepository, FlagsRepository>();
}

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();