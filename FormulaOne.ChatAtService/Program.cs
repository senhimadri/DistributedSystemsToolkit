using FormulaOne.ChatAtService;
using FormulaOne.ChatAtService.Hubs;
using Microsoft.AspNetCore.SignalR;
using System.Diagnostics.Metrics;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddSignalR();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.MapPost("broadcast",async (string message, IHubContext<ChatHub,IChatClient> context)=>
{
    await context.Clients.All.ReceiveMessage(message);

    return Results.NoContent();
});


app.MapPost("SendMessageToSpecific", async (string ClientId,string message, IHubContext<ChatHub, IChatClient> context) =>
{
    await context.Clients.Client(ClientId).ReceiveMessage(message);

    return Results.NoContent();
});



app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapHub<ChatHub>(pattern:"/chat-hub");

app.UseCors();

app.Run();
