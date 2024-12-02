using ProjetoMicroondas;
using ProjetoMicroondas.Infra.IoC;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AdicionarInfraestrutura();

builder.Services.AddSession(options =>
{
	options.IdleTimeout = TimeSpan.FromMinutes(30);
	options.Cookie.HttpOnly = true;
	options.Cookie.IsEssential = true;
});

// Adicionar suporte a Controllers com Views
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configurações de ambiente
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Error");
	app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseSession();
app.UseAuthorization();

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();