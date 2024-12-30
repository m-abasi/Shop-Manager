using Entities;
using Entities.IdentityEntities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Services;
using ServicesContract;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ISearchTypeService, SearchTypeService>();
builder.Services.AddScoped<IBranchService, BranchService>();
builder.Services.AddDbContext<ShopDbContext>(options=>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddIdentity<ApplicationUser, ApplicationRole>()
    .AddEntityFrameworkStores<ShopDbContext>()
    .AddDefaultTokenProviders();

var app = builder.Build();
app.UseStaticFiles();
app.MapControllers();
app.UseRouting();


app.Run();
