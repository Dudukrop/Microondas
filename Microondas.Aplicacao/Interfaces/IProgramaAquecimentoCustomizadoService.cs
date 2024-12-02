using ProjetoMicroondas.Aplicacao.DTOs;
using System.Collections.Generic;

namespace ProjetoMicroondas.Aplicacao.Interfaces
{
    public interface IProgramaAquecimentoCustomizadoService
    {
        IEnumerable<ProgramaAquecimentoCustomizadoDTO> ObterTodos();
        ProgramaAquecimentoCustomizadoDTO ObterPorNome(string nome);
        void Adicionar(ProgramaAquecimentoCustomizadoDTO programaDto);
    }
}
