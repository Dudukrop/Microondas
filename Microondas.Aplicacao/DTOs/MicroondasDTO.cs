using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoMicroondas.Aplicacao.DTOs
{
    public class MicroondasDTO
    {
        public int TempoSegundos { get; set; }
        public int Potencia { get; set; }
        public string Estado { get; set; }
        public DateTime? InicioAquecimento { get; set; }
    }
}
