using Microsoft.EntityFrameworkCore;
using System.Reflection;
using TestTodoListLiveIpi2023.Models.Context;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Version = "V1",
        Title = "Todo API",
        Description = "A simple example ASP.NET Core Web API",
        TermsOfService = new Uri("https://google.com"),
        Contact = new Microsoft.OpenApi.Models.OpenApiContact
        {
            Name = "Thomas BDC",
            Email = "mail@mail.com",
            Url = new Uri("https://google.com")
        }
    });

    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
});

builder.Services.AddDbContext<TodoContext>(opt => opt.UseInMemoryDatabase("TodoListIpi2023"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseDefaultFiles();
app.UseStaticFiles();

app.MapControllers();

app.Run();
