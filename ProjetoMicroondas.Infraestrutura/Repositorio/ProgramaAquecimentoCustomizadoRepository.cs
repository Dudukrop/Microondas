using ProjetoMicroondas.Dominio.Entidades;
using ProjetoMicroondas.Dominio.Interfaces;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace ProjetoMicroondas.Infraestrutura.Repositories
{
    public class ProgramaAquecimentoCustomizadoRepository : IProgramaAquecimentoCustomizadoRepository
    {
        private readonly string _caminhoArquivo = "programasCustomizados.json";
        private List<ProgramaAquecimentoCustomizado> _programas;

        public ProgramaAquecimentoCustomizadoRepository()
        {
            if (File.Exists(_caminhoArquivo))
            {
                var json = File.ReadAllText(_caminhoArquivo);
                _programas = JsonSerializer.Deserialize<List<ProgramaAquecimentoCustomizado>>(json);
            }
            else
            {
                _programas = new List<ProgramaAquecimentoCustomizado>();
            }
        }

        public IEnumerable<ProgramaAquecimentoCustomizado> ObterTodos()
        {
            return _programas;
        }

        public ProgramaAquecimentoCustomizado ObterPorNome(string nome)
        {
            return _programas.FirstOrDefault(p => p.Nome.ToLower() == nome.ToLower());
        }

        public void Adicionar(ProgramaAquecimentoCustomizado programa)
        {
            if (_programas.Any(p => p.StringAquecimento == programa.StringAquecimento || programa.StringAquecimento == "."))
            {
                throw new ArgumentException("Caractere de aquecimento já utilizado ou inválido.");
            }

            _programas.Add(programa);
            Salvar();
        }

        private void Salvar()
        {
            var json = JsonSerializer.Serialize(_programas, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(_caminhoArquivo, json);
        }
    }
}
