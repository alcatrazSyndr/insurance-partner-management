using Dapper;
using InsurancePartnerManagement.Database;
using InsurancePartnerManagement.Helpers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Partner/Index");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Partner}/{action=Index}/{id?}");

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")!;
SqlMapper.AddTypeHandler(new DecimalTypeHandler());
DatabaseInitializer.Initialize(connectionString);

app.Run();
