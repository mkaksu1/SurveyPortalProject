var builder = WebApplication.CreateBuilder(args);

// MVC ve Razor Pages ile Session desteði ekleniyor
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.AddSession(); // Session kullanýlacak
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

var app = builder.Build();

// Production ortamý için hata yönetimi
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error"); // Razor Pages Error sayfasý
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseSession(); // Session aktifleþiyor
app.UseAuthorization();

// MVC route ayarlarý
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}/{id?}"); // Login sayfasý ana sayfa oldu

// Razor Pages yönlendirmesi
app.MapRazorPages();

app.Run();
