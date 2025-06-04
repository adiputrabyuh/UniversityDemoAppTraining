using Microsoft.AspNetCore.Cors.Infrastructure;
using MySqlX.XDevAPI;
using UniversityDemoAppTraining.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Add services to the container.
//builder.Services.AddSingleton<DbMySQLService>();
builder.Services.AddSingleton<StudentService>();
builder.Services.AddSingleton<TeacherService>();
builder.Services.AddSingleton<CourseService>();
builder.Services.AddScoped<DbMySQLService>();


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
