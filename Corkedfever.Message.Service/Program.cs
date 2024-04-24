using Corkedfever.Message.Business;
using Corkedfever.Message.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System.Numerics;
using Corkedfever.Common.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddSingleton<IMessageService, MessageService>();
builder.Services.AddSingleton<IMessageRepository, MessageRepository>();
builder.Services.AddDbContextFactory<CorkedFeverDataContext>(options =>
{
    if (builder.Environment.IsDevelopment())
    {
        options.UseSqlServer(builder.Configuration.GetConnectionString("DevelopmentConnection"), b => b.MigrationsAssembly("Corkedfever.Jobs.Service"));
    }
    else
    {
        options.UseSqlServer(builder.Configuration.GetConnectionString("ContainerConnection"), b => b.MigrationsAssembly("Corkedfever.Jobs.Service"));
    }
});


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
