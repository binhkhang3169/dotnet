using EntityFramework.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddDbContext<MyBlogContext>(option =>
{
    string connectString = builder.Configuration.GetConnectionString("MyBlogContext");
    option.UseSqlServer(connectString);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();

/*
dotnet aspnet-codegenerator razorpage -m EntityFramework.Models.Article -dc EntityFramework.Models.MyBlogContext -udl -outDir Pages/Blog  --referenceScriptLibraries
dotnet aspnet-codegenerator razorpage -m Article -dc ArticleContext -udl -outDir Pages/Blog --referenceScriptLibraries

*/