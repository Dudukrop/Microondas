using ProjetoMicroondas.Dominio.Entidades;
using ProjetoMicroondas.Dominio.Interfaces;

namespace ProjetoMicroondas.Infraestrutura.Repositories
{
	public class MicroondasRepositorio : IMicroondasRepositorio
	{
		private static Microondas _microondas;

		public Microondas Obter()
		{
			if (_microondas == null)
			{
				_microondas = new Microondas();
			}
			return _microondas;
		}

		public void Salvar(Microondas microondas)
		{
			_microondas = microondas;
		}
	}
}
