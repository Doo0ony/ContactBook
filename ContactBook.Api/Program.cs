using ContactBook.Api.Validators.User;
using ContactBook.Application.Extensions;
using ContactBook.Infrastructure.Extensions;
using FluentValidation;
using FluentValidation.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// 1️⃣ Добавляем сервисы контроллеров
builder.Services.AddControllers();

// 2️⃣ Swagger/OpenAPI
builder.Services.AddOpenApi();

// 3️⃣ Инфраструктура
builder.Services.AddInfrastructure(builder.Configuration);

// 4️⃣ Сервисы приложения
builder.Services.AddApplicationServices();

// 5️⃣ FluentValidation
builder.Services.AddValidatorsFromAssemblyContaining<Program>();
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddFluentValidationClientsideAdapters();

var app = builder.Build();

// 6️⃣ Middleware
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UseAuthorization();

// 7️⃣ Маршрутизация контроллеров
app.MapControllers();

app.Run();
