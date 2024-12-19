using FeatureFlags.Application.UseCases;
using FeatureFlags.Domain.Repositories;
using FeatureFlags.Infrastructure.Repositories;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi().AddControllers();
{
    builder.Services.AddTransient<GetAllFlagsUseCase>();
    builder.Services.AddTransient<PatchFlagUseCase>();
    // TODO This should be transient, but I'm storing data and I want it to be the same across all requests 
    builder.Services.AddSingleton<IFlagsRepository, FlagsRepository>();
}

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference(options => options.WithTheme(ScalarTheme.DeepSpace));
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();