using ProjetoMicroondas.Dominio.Entidades;

namespace ProjetoMicroondas.Dominio.Interfaces
{
	public interface IMicroondasService
	{
		void DefinirTempo(int tempoSegundos);
		void DefinirPotencia(int potencia);
		void IniciarAquecimento(int tempoSegundos, int potencia = 10);
        void PausarAquecimento();
		void CancelarAquecimento();
		void AcrescentarTempo(int tempoSegundos);
		int ObterTempoRestante();
		string ObterEstado();
		void IniciarProgramaAquecimento(string nomePrograma);
		Microondas ObterMicroondas();
    }
}
