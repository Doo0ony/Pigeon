using AuthService.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

//Add data access context
builder.Services.AddDataAccess(builder.Configuration);

//Add asp net identity
builder.Services.AddIdentity();

//Add Jwt authentication
builder.Services.AddJwtAuthentication(builder.Configuration);

//Add services
builder.Services.AddServices();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

//Middleware
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

