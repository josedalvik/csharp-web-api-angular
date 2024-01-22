using Demo.DAL;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.Sqlite;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//SQLITE database
var Config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
var cs = Config.GetValue<String>("ConnectionStrings:cs");
builder.Services.AddDbContext<ApplicationDBContext>(options => options.UseSqlite(cs));

//SQLITE create database
using (SqliteConnection con = new SqliteConnection(cs))
using (SqliteCommand command = con.CreateCommand())
{
    con.Open();
    command.CommandText = "CREATE TABLE IF NOT EXISTS inbox (id TEXT, subject TEXT, message TEXT);";
    command.ExecuteNonQuery();
    con.Close();
}

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

//static files
app.UseHttpsRedirection();
var options = new DefaultFilesOptions();
options.DefaultFileNames.Clear();
options.DefaultFileNames.Add("index.html");
app.UseDefaultFiles(options);
app.UseStaticFiles();
app.UseRouting();

app.Run();
