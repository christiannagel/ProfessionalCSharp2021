﻿var builder = new HostApplicationBuilder(args);
builder.Services.AddSingleton<IGreetingService, GreetingService>();
builder.Services.AddTransient<HomeController>();
using var host = builder.Build();

var controller = host.Services.GetRequiredService<HomeController>();
string result = controller.Hello("Matthias");
Console.WriteLine(result);
