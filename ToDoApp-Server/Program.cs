using Microsoft.EntityFrameworkCore;
using ToDoApp.DAL.Data;
using ToDoApp.DAL.Repositories.Interfaces.Base;
using ToDoApp.DAL.Repositories.Realizations.Base;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddOpenApi();

builder.Services.AddScoped<IRepositoryWrapper, RepositoryWrapper>();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<ToDoAppDbContext>(options => 
    options.UseSqlServer(connectionString, opt => {

}));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
