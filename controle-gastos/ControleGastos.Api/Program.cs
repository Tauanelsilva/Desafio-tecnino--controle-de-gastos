using ControleGastos.Api.Data;
using ControleGastos.Api.Repositories;
using ControleGastos.Api.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Configurar Entity Framework com SQLite, lendo a string de conexão do appsettings.json
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

// Configurar CORS para permitir requisições do frontend React
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp", policy =>
    {
        policy.WithOrigins("http://localhost:5173")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

// Registrar Repositories para injeção de dependência (Acesso a dados)
builder.Services.AddScoped<IPessoaRepository, PessoaRepository>();
builder.Services.AddScoped<ITransacaoRepository, TransacaoRepository>();

// Registrar Services para injeção de dependência (Regras de negócio)
builder.Services.AddScoped<IPessoaService, PessoaService>();
builder.Services.AddScoped<ITransacaoService, TransacaoService>();

// Registrar Controllers
builder.Services.AddControllers();

// Registrar Swagger para documentação da API
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "ControleGastos.Api", Version = "v1" });
    
    // Configurar o Swagger para usar os comentários XML gerados
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    if (File.Exists(xmlPath))
    {
        c.IncludeXmlComments(xmlPath);
    }
});

var app = builder.Build();

// Garantir que o banco de dados é criado automaticamente e as migrations (se houverem) são aplicadas
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    dbContext.Database.EnsureCreated();
}

// Configurar pipeline HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ControleGastos.Api v1"));
}

app.UseCors("AllowReactApp");
app.UseAuthorization();
app.MapControllers();

app.Run();
