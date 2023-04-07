/* Logger is already registered inside CreateBuilder*/
using DreamFlats.Logging;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// For using Serilog
//Log.Logger = new LoggerConfiguration().MinimumLevel.Debug()
//    .WriteTo.File("log/FlatLog.txt", rollingInterval: RollingInterval.Day).CreateLogger();

//builder.Host.UseSerilog();

//builder.Services.AddControllers().AddNewtonsoftJson(); //Add AddNewtonsoftJson for working with Patch
builder.Services.AddControllers(options =>
{
    //options.ReturnHttpNotAcceptable = true; // To explicity return only JSON, and not any other type such as XML, text etc.
}).AddNewtonsoftJson().AddXmlDataContractSerializerFormatters(); //Add AddNewtonsoftJson for working with Patch
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

/*
Below process is used for Creating Custom Logs
1. Create an Interface and a class
2. Use DI in APIController
3. Add the Logger in "builder.Services.AddSingleton"
 */
builder.Services.AddSingleton<ILogging, Logging>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

