using Microsoft.EntityFrameworkCore;
using FluentValidation;
using ReadNest.Data;
using ReadNest.Repositories;
using ReadNest.Endpoints;

var builder = WebApplication.CreateBuilder(args);

var connectionString = Environment.GetEnvironmentVariable("CONNECTION_STRING")
                        ?? throw new InvalidOperationException("Connection string not set.");

builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly, includeInternalTypes: true);
builder.Services.AddOpenApi();
builder.Services.AddDbContext<AppDbContext>(options =>
        options.UseNpgsql(connectionString));

builder.Services.AddScoped<IBookGenreRepository, BookGenreRepository>();
builder.Services.AddScoped<IBookRepository, BookRepository>();
builder.Services.AddScoped<IBorrowedInfoRepository, BorrowedInfoRepository>();
builder.Services.AddScoped<ILoanedInfoRepository, LoanedInfoRepository>();
builder.Services.AddScoped<IReadingLogRepository, ReadingLogRepository>();
builder.Services.AddScoped<IHabitRepository, HabitRepository>();

builder.Services.AddExceptionHandler<ValidationExceptionHandler>();
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy
            .WithOrigins("http://localhost:3000")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

var app = builder.Build();

app.UseCors("AllowFrontend");
app.UseStaticFiles();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UseExceptionHandler();

app.MapGet("/", () => Results.Ok("Hello from API"));
app.MapBookGenreEndpoints();
app.MapBookEndpoints();
app.MapBorrowedInfoEndpoints();
app.MapLoanedInfoEndpoints();
app.MapReadingLogEndpoints();
app.MapHabitsEndpoints();

app.Run();

public partial class Program { }
