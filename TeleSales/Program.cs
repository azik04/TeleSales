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
using TeleSales.Core.Validation.Auth;
using TeleSales.Core.Dto.Call;
using TeleSales.Core.Validation.Call;
using TeleSales.Core.Validation.User;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddFluentValidationClientsideAdapters();
builder.Services.AddValidatorsFromAssemblyContaining<AuthDtoValidation>();
builder.Services.AddValidatorsFromAssemblyContaining<CreateCallDtoValidation>();
builder.Services.AddValidatorsFromAssemblyContaining<CreateUserDtoValidation>();

var serviceProvider = builder.Services.BuildServiceProvider();

builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IKanalService, KanalService>();
builder.Services.AddScoped<ICallService, CallService>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowLocalhost",
        builder => builder.WithOrigins("http://localhost:3000")
                          .AllowAnyMethod()
                          .AllowAnyHeader());
});

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

app.UseCors("AllowLocalhost");

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
