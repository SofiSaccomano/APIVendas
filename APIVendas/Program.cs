using APIVendas.Data;
using APIVendas.Repositorios;
using APIVendas.Repositorios.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

/* Adicionar Chave Secreta */
string chaveSecreta = "0cbc84a9-98c5-4837-99bf-ddb35bf588a0";

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

/* Configurando o Swagger para aceitar autenticação JWT */
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Sistema de Vendas - API",
        Version = "v1"
    });

    var securitySchema = new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Description = "Digite 'Bearer {seu token}' para autenticar",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        Reference = new OpenApiReference
        {
            Type = ReferenceType.SecurityScheme,
            Id = JwtBearerDefaults.AuthenticationScheme
        }
    };

    c.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, securitySchema);
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        { securitySchema, Array.Empty<string>() }
    });
});

/* Configuração do banco de dados */
builder.Services.AddDbContext<SistemaVendasDbContext>(
    options => options.UseSqlServer(builder.Configuration.GetConnectionString("DataBase"))
);

/* Injeção de dependência dos repositórios */
builder.Services.AddScoped<IUsuariosRepositorio, UsuariosRepositorio>();
builder.Services.AddScoped<IProdutosRepositorio, ProdutosRepositorio>();
builder.Services.AddScoped<IPedidosRepositorio, PedidosRepositorio>();
builder.Services.AddScoped<IPedidosProdutosRepositorio, PedidosProdutosRepositorio>();
builder.Services.AddScoped<ICategoriasRepositorio, CategoriasRepositorio>();

/* Configuração da autenticação JWT */
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = "APIVendas",
        ValidAudience = "APIVendas",
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(chaveSecreta))
    };
});

builder.Services.AddAuthorization();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication(); // primeiro autenticação
app.UseAuthorization();  // depois autorização

app.MapControllers();

app.Run();
