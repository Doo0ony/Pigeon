using ChatService.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

//Add data access context
builder.Services.AddDataAccess(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.Run();

