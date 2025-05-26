var builder = WebApplication.CreateBuilder(args);

// MVC ve Razor Pages ile Session deste�i ekleniyor
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.AddSession(); // Session kullan�lacak
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

var app = builder.Build();

// Production ortam� i�in hata y�netimi
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error"); // Razor Pages Error sayfas�
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseSession(); // Session aktifle�iyor
app.UseAuthorization();

// MVC route ayarlar�
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}/{id?}"); // Login sayfas� ana sayfa oldu

// Razor Pages y�nlendirmesi
app.MapRazorPages();

app.Run();
