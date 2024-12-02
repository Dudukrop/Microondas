using ProjetoMicroondas.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoMicroondas.Dominio.Interfaces
{
    public interface IProgramaAquecimentoRepositorio
    {
        IEnumerable<ProgramaAquecimento> ObterTodos(); 
        ProgramaAquecimento ObterPorNome(string nome);
    }
}
