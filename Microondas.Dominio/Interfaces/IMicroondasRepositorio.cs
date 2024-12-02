using ProjetoMicroondas.Dominio.Entidades;

namespace ProjetoMicroondas.Dominio.Interfaces
{
	public interface IMicroondasRepositorio
	{
		Microondas Obter();
		void Salvar(Microondas microondas);
	}
}
