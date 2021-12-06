using Haulmer_Comercio;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var starup = new Startup(builder.Configuration);
starup.ConfigureServices(builder.Services);
var app = builder.Build();

// Configure the HTTP request pipeline.
starup.Configure(app, app.Environment);
app.Run();
