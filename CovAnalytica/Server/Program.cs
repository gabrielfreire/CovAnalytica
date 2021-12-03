using CovAnalytica.Server.Data;
using CovAnalytica.Server.Extensions;
using Microsoft.AspNetCore.ResponseCompression;

// --------------- Notes -------------------------
// Data source: https://github.com/owid/covid-19-data
/**
 * https://github.com/MarkPflug/Sylvan/blob/main/docs/Csv/Examples.md
 * Example of Csv usage in Sylvan.Data.Csv:
 * class Record {
        public int Id {get; set;}
        public int Name {get; set;}
        public int Date {get; set;}
    }

    using var csv = CsvDataReader.Create("demo.csv");
    foreach(var record in csv.GetRecords<Record>())
        var id = record.Id;
        var name = record.Name;
        var date = record.Date;
    }
 */

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.AddApplicationServices(builder.Configuration);

builder.Logging.AddConfiguration(builder.Configuration);
builder.Logging.AddConsole();
builder.Logging.AddFilter("Microsoft.EntityFramework.Database.Command", LogLevel.Warning);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.DocumentTitle = "CovAnalytica API v1";
});

app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();


app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

try
{
    using (var scope = app.Services.CreateScope())
    {
        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        context.Database.EnsureCreated();
    }

    app.Run();
}
catch (Exception ex)
{
    // log: failed to start application
    Console.WriteLine(ex.ToString());
}
