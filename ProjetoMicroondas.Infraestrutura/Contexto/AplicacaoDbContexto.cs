using Microsoft.EntityFrameworkCore;
using ProjetoMicroondas.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoMicroondas.Infraestrutura.Contexto
{
    public class AplicacaoDbContexto : DbContext
    {
		public AplicacaoDbContexto(DbContextOptions<AplicacaoDbContexto> options) : base(options)
		{ }

		public DbSet<Microondas> Microondas { get; set; }
	}
}
