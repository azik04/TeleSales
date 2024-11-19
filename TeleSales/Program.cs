using Microsoft.EntityFrameworkCore;
using TeleSales.Core.Interfaces.Auth;
using TeleSales.Core.Interfaces.Call;
using TeleSales.Core.Interfaces.Kanal;
using TeleSales.Core.Interfaces.User;
using TeleSales.Core.Services.AUTH;
using TeleSales.Core.Services.Call;
using TeleSales.Core.Services.Kanal;
using TeleSales.Core.Services.User;
using TeleSales.DataProvider.Context;
using FluentValidation;
using FluentValidation.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Регистрация FluentValidation
builder.Services.AddControllers()
    .AddFluentValidation(fv =>
    {
        fv.RegisterValidatorsFromAssemblyContaining<Program>(); // Регистрация валидаторов из сборки, содержащей Program
    });
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var serviceProvider = builder.Services.BuildServiceProvider();

builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IKanalService, KanalService>();
builder.Services.AddScoped<ICallService, CallService>();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseInMemoryDatabase("InMemoryDbName"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
