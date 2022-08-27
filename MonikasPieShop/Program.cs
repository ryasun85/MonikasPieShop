using Microsoft.EntityFrameworkCore;
using MonikasPieShop.Models;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("MonikasPieShopDbContextConnection") ?? throw new InvalidOperationException("Connection string 'MonikasPieShopDbContextConnection' not found.");

builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IPieRepository, PieRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>(); 

//shopping carts services injected
builder.Services.AddScoped<IShoppingCart, ShoppingCart>(sp=> ShoppingCart.GetCart(sp));
builder.Services.AddSession();
builder.Services.AddHttpContextAccessor();

builder.Services.AddControllersWithViews()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    });
builder.Services.AddRazorPages();

builder.Services.AddDbContext<MonikasPieShopDbContext>(options =>
{
    options.UseSqlServer(
        builder.Configuration["ConnectionStrings:MonikasPieShopDbContextConnection"]);
});

builder.Services.AddDefaultIdentity<IdentityUser>(/*options => options.SignIn.RequireConfirmedAccount = true*/)
    .AddEntityFrameworkStores<MonikasPieShopDbContext>();
//builder.Services.AddServerSideBlazor();

//builder.Services.AddControllers();

var app = builder.Build();


app.UseStaticFiles();
//middleware
app.UseSession();
app.UseAuthentication();
app.UseAuthorization();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.MapDefaultControllerRoute();
//app.MapControllerRoute(
//    name: "default",
//    pattern: "{controller=Hole}/{action=Index}/{id}");

app.MapRazorPages();
//app.MapControllers();

//app.MapBlazorHub();
//app.MapFallbackToPage("/app/{*catchall}", "/App/Index");

DbInitializer.Seed(app);
app.Run();
