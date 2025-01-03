using Microsoft.EntityFrameworkCore;
using MyWebApi.Database;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllers();

builder.Services.AddDbContext<AppDbContext>(options =>
{
	options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
		.UseLazyLoadingProxies()
		.EnableSensitiveDataLogging()
		.LogTo(Console.WriteLine);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();
app.Run();