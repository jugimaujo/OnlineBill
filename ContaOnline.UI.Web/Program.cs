using OnlineBill.Domain.Interfaces;
using OnlineBill.Repository;
using OnlineBill.UI.Web.Code;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Configure session service for the application
builder.Services.AddSession();

builder.Services.AddMvc();

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ICheckingAccountRepository, CheckingAccountRepository>();
builder.Services.AddScoped<IBillCategoryRepository, BillCategoryRepository>();
builder.Services.AddScoped<IContactRepository, ContactRepository>();

builder.Services.AddHttpContextAccessor();
builder.Services.AddTransient<IAppHelper, AppHelper>();

var app = builder.Build();

// Enable the Session Middleware
app.UseSession();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=App}/{action=Home}/{id?}");

app.Run();
