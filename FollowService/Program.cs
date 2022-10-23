using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.Runtime;
using FollowerService.Contracts.Interfaces;
using FollowerService.Contracts.Repositories;
using FollowerService.Interfaces;
using FollowerService.SQSProcessors;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var awsOptions = builder.Configuration.GetAWSOptions();
awsOptions.Credentials = new EnvironmentVariablesAWSCredentials();
builder.Services.AddDefaultAWSOptions(awsOptions);

//builder.Services.AddAWSService<AmazonDynamoDB>();
builder.Services.AddScoped<ISQSProcessor, SQSProcessor>();
builder.Services.AddScoped<IFollowersRepository, FollowersRepository>();
//builder.Services.AddHostedService<SQSProcessor>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();