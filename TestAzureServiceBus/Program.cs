using MediatR;
using TestAzureServiceBus;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHostedService<QueueConsumerService>();
builder.Services.AddSingleton<QueueSenderService>();
builder.Services.Configure<QueueSettings>(builder.Configuration.GetSection(nameof(QueueSettings)));
builder.Services.AddMediatR(typeof(Program));

var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();
app.MapControllers();
app.Run();