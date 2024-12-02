using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ProjetoMicroondas.Aplicacao.Interfaces;
using ProjetoMicroondas.Aplicacao.Profiles;
using ProjetoMicroondas.Aplicacao.Services;
using ProjetoMicroondas.Dominio.Interfaces;
using ProjetoMicroondas.Infraestrutura.Contexto;
using ProjetoMicroondas.Infraestrutura.Repositories;

namespace ProjetoMicroondas.Infra.IoC
{
	public static class InjecaoDeDependencia
	{
		public static IServiceCollection AdicionarInfraestrutura(this IServiceCollection services)
		{
			//services.AddDbContext<ApplicationDbContext>(options =>
			// options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"
			//), b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));

			services.AddDbContext<AplicacaoDbContexto>(options =>
			options.UseInMemoryDatabase("ProjetoMicroondas"));

            services.AddAutoMapper(typeof(MappingProfile));

            services.AddScoped<IMicroondasService, MicroondasService>();
			services.AddScoped<IMicroondasRepositorio, MicroondasRepositorio>();
			services.AddScoped<IProgramaAquecimentoRepositorio, ProgramaAquecimentoRepositorio>();
			services.AddScoped<IProgramaAquecimentoServico, ProgramaAquecimentoService>();
			services.AddScoped<IProgramaAquecimentoCustomizadoService, ProgramaAquecimentoCustomizadoService>();
			services.AddScoped<IProgramaAquecimentoCustomizadoRepository, ProgramaAquecimentoCustomizadoRepository>();

			return services;
		}
	}
}