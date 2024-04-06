using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.IRepository;
using WebApplication1.IService;
using WebApplication1.Repository;
using WebApplication1.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var connectionString = builder.Configuration.GetConnectionString("connectionString");
builder.Services.AddDbContext<MyDbContext>(options => options.UseSqlServer(connectionString));
builder.Services.AddScoped(typeof(IUserSevice<>),typeof(UserSevice<>));
builder.Services.AddScoped(typeof(IUserRepository<>),typeof(UserRepository<>)); 

var app = builder.Build();

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
