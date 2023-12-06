using Solvintech.API.Extensions.Core;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

builder.Services.AddWebApi(configuration);

var app = builder.Build();

app.UseWebApi();

app.Run();
