using Flags.Api.Endpoints;
using Flags.Application.UseCases;
using Flags.Domain.Repositories;
using Flags.Infrastructure.EFCore;
using Flags.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi().AddControllers();
{
    builder.Services.AddTransient<GetAllFlagsUseCase>();
    builder.Services.AddTransient<PatchFlagUseCase>();
    builder.Services.AddTransient<PostFlagUseCase>();
    builder.Services.AddTransient<IFlagsRepository, FlagsRepository>();

    builder.Services.AddDbContextPool<FlagsContext>(options =>
        options.UseNpgsql(builder.Configuration.GetConnectionString("PostgresConnection"),
            b => b.MigrationsAssembly("Flags.Api"))
    );
}

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference(options => options.WithTheme(ScalarTheme.DeepSpace));
}

app.UseHttpsRedirection();

app.MapGetAllFlagsEndpoint();
app.MapPostFlagEndpointExtension();
app.MapPatchFlagEndpointExtension();

app.Run();

// Needed for test
// ReSharper disable once ClassNeverInstantiated.Global
public partial class Program
{
}