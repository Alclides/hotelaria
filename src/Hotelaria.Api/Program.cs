using System.Text.Json.Serialization;
using Hotelaria.Api.Middlewares;
using Hotelaria.Application.Ports.Entrada;
using Hotelaria.Application.Services;
using Hotelaria.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddControllers()
    .AddJsonOptions(options =>
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));
builder.Services.AddOpenApi();

builder.Services.AddProblemDetails();
builder.Services.AddExceptionHandler<TratadorDeErros>();
builder.Services.AddScoped<IHospedeService, HospedeService>();
// builder.Services.AddScoped<IQuartoService, QuartoService>();
// builder.Services.AddScoped<IReservaService, ReservaService>();

var connectionString = builder.Configuration.GetConnectionString("Hotelaria")
    ?? throw new InvalidOperationException("Connection string 'Hotelaria' não encontrada no appsettings.json.");

builder.Services.AddInfrastructure(connectionString);

var app = builder.Build();

app.UseExceptionHandler();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();

    
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/openapi/v1.json", "Hotelaria API v1");
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
