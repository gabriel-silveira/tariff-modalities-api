using CTCEA_Tariff_Modalities.Database.Configuration;
using CTCEA_Tariff_Modalities.Database.Repositories.Base;
using CTCEA_Tariff_Modalities.Database.Repositories.Interfaces;
using CTCEA_Tariff_Modalities.Database.Repositories;
using CTCEA_Tariff_Modalities.Services;
using CTCEA_Tariff_Modalities.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigureDatabase(builder.Configuration);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// services
builder.Services.AddTransient<ICalculatorService, CalculatorService>();
builder.Services.AddTransient<IClassificationService, ClassificationService>();
builder.Services.AddTransient<IImmediateBillingService, ImmediateBillingService>();
builder.Services.AddTransient<IFlightCompanySGTANService, CompanhiaAereaSGTANService>();

// base repository
builder.Services.AddScoped(typeof(IRepositoryBase<>), typeof(RepositoryBase<>));

// repositories
builder.Services.AddTransient<IImmediateBillingRepository, CobrancaAVistaRepository>();
builder.Services.AddTransient<ICompanhiaAereaSGTANRepository, CompanhiaAereaSGTANRepository>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});

// Add services to the container.
builder.Services.AddHttpClient();


var app = builder.Build();


app.UseCors("AllowAll");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
