using Microsoft.EntityFrameworkCore;
using Yzk.share;
using Yznk.WebApi.Db;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

string connectonString = builder.Configuration.GetConnectionString("DbConnection")!;

builder.Services.AddDbContext<AppDbContext>(opt =>
{
    opt.UseSqlServer(connectonString);
},
ServiceLifetime.Transient,
ServiceLifetime.Transient
);

//builder.Services.AddScoped<AdoDotnetService>(n => new AdoDotnetService(connectonString));
//builder.Services.AddScoped<DapperService>(n => new DapperService(connectonString));



builder.Services.AddScoped(n => new AdoDotnetService(connectonString));
builder.Services.AddScoped(n => new DapperService(connectonString));

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
