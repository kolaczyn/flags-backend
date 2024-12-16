using FeatureFlags;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

var flags = new FeatureDto[]
{
    new()
    {
        Id = Guid.NewGuid()
            .ToString(),
        Value = true,
        Label = "greetUser"
    },
    new()
    {
        Id = Guid.NewGuid()
            .ToString(),
        Value = false,
        Label = "aboutSection"
    },
};

app.MapGet("/flags", () => flags)
    .WithName("GetWeatherForecast");

app.Run();