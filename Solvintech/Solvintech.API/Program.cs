using Solvintech.API.Extensions.Core;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddWebApi(builder.Configuration);

var app = builder.Build();

app.UseWebApi();

app.Run();
