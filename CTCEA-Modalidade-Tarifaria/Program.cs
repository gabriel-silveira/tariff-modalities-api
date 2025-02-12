using CTCEA_Modalidade_Tarifaria.Database.Configuration;
using CTCEA_Modalidade_Tarifaria.Database.Repositories.Base;
using CTCEA_Modalidade_Tarifaria.Database.Repositories.Interfaces;
using CTCEA_Modalidade_Tarifaria.Database.Repositories;
using CTCEA_Modalidade_Tarifaria.Repositories.Base;
using CTCEA_Modalidade_Tarifaria.Services;
using CTCEA_Modalidade_Tarifaria.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigureDatabase(builder.Configuration);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// services
builder.Services.AddTransient<ICalculadoraService, CalculadoraService>();
builder.Services.AddTransient<IClassificacaoService, ClassificacaoService>();
builder.Services.AddTransient<ICobrancaAVistaService, CobrancaAVistaService>();
builder.Services.AddTransient<ICompanhiaAereaSGTANService, CompanhiaAereaSGTANService>();

// base repository
builder.Services.AddScoped(typeof(IRepositoryBase<>), typeof(RepositoryBase<>));

// repositories
builder.Services.AddTransient<ICobrancaAVistaRepository, CobrancaAVistaRepository>();
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
