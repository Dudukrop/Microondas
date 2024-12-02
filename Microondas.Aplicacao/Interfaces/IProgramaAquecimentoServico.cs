using ProjetoMicroondas.Aplicacao.DTOs;
using ProjetoMicroondas.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoMicroondas.Aplicacao.Interfaces
{
    public interface IProgramaAquecimentoServico
    {
        IEnumerable<ProgramaAquecimentoDTO> ObterTodos();
        ProgramaAquecimentoDTO ObterPorNome(string nome);
    }
}
