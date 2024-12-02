using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoMicroondas.Aplicacao.DTOs
{
    public class ProgramaAquecimentoCustomizadoDTO
    {
        public string Nome { get; set; }
        public string Alimento { get; set; }
        public int Tempo { get; set; }
        public int Potencia { get; set; }
        public string StringAquecimento { get; set; }
        public string Instrucoes { get; set; }
    }
}
