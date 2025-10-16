using System.Data;
using Davinti___Teste_Pratico.Infra.Repository;
using Davinti___Teste_Pratico.Services;
using MySqlConnector;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

var connectionString = builder.Configuration.GetConnectionString("MySQLDb");
builder.Services.AddTransient<IDbConnection>(db => new MySqlConnection(connectionString));

builder.Services.AddControllers();
builder.Services.AddScoped<ContatoService>();
builder.Services.AddScoped<ContatoRepository>();
builder.Services.AddScoped<TelefoneRepository>();
builder.Services.AddScoped<LoggingService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();
app.MapRazorPages()
    .WithStaticAssets();

app.Run();