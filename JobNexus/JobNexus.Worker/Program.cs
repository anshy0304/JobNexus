using JobNexus.Storage;
using JobNexus.Worker;

var builder = Host.CreateApplicationBuilder(args);
string dbConnectionString = "Server=(localdb)\\mssqllocaldb;Database=JobNexusDb;Trusted_Connection=True;";
builder.Services.AddStorage(dbConnectionString);
builder.Services.AddHostedService<Worker>();

var host = builder.Build();
host.Run();
