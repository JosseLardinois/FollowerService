using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.Runtime;
using FollowerService.Contracts.Interfaces;
using FollowerService.Contracts.Repositories;
using FollowerService.Interfaces;
using FollowerService.SQSProcessors;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.Net;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var awsOptions = builder.Configuration.GetAWSOptions();
awsOptions.Credentials = new EnvironmentVariablesAWSCredentials();
builder.Services.AddDefaultAWSOptions(awsOptions);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        IssuerSigningKeyResolver = (s, securityToken, identifier, parameters) =>
                        {
                            // get JsonWebKeySet from AWS
                            var json = new WebClient().DownloadString(parameters.ValidIssuer + "/.well-known/jwks.json");
                            // serialize the result
                            var keys = JsonConvert.DeserializeObject<JsonWebKeySet>(json).Keys;
                            // cast the result to be the type expected by IssuerSigningKeyResolver
                            return (IEnumerable<SecurityKey>)keys;
                        },

                        ValidIssuer = "https://cognito-idp.eu-west-1.amazonaws.com/eu-west-1_QRNd3f8Cu",
                        ValidateIssuerSigningKey = true,
                        ValidateIssuer = true,
                        ValidateLifetime = true,
                        ValidAudience = "6uad7uthfhv3a7p9aihdbjl3tt",
                        ValidateAudience = false
                    };
                });

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
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();