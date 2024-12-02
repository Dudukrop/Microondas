using ProjetoMicroondas.Dominio.Entidades;
using System.Collections.Generic;

namespace ProjetoMicroondas.Dominio.Interfaces
{
    public interface IProgramaAquecimentoCustomizadoRepository
    {
        IEnumerable<ProgramaAquecimentoCustomizado> ObterTodos();
        ProgramaAquecimentoCustomizado ObterPorNome(string nome);
        void Adicionar(ProgramaAquecimentoCustomizado programa);
    }
}
