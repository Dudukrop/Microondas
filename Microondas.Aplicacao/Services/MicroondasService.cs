using ProjetoMicroondas.Aplicacao.DTOs;
using ProjetoMicroondas.Aplicacao.Interfaces;
using ProjetoMicroondas.Dominio.Entidades;
using ProjetoMicroondas.Dominio.Interfaces;
using System;

namespace ProjetoMicroondas.Aplicacao.Services
{
    public class MicroondasService : IMicroondasService
    {
        private readonly IMicroondasRepositorio _microondasRepositorio;
        private readonly IProgramaAquecimentoServico _programaAquecimentoService;

        public MicroondasService(IMicroondasRepositorio microondasRepositorio, IProgramaAquecimentoServico programaAquecimentoService)
        {
            _microondasRepositorio = microondasRepositorio;
            _programaAquecimentoService = programaAquecimentoService;
        }

        public void DefinirTempo(int tempoSegundos)
        {
            var microondas = _microondasRepositorio.Obter();
            if (tempoSegundos < 1 || tempoSegundos > 120)
                throw new ArgumentException("Tempo deve ser entre 1 segundo e 2 minutos.");
            microondas.TempoSegundos += tempoSegundos;
            _microondasRepositorio.Salvar(microondas);
        }

        public void DefinirPotencia(int potencia)
        {
            var microondas = _microondasRepositorio.Obter();
            if (potencia < 1 || potencia > 10)
                throw new ArgumentException("Potência deve ser entre 1 e 10.");
            microondas.Potencia = potencia;
            _microondasRepositorio.Salvar(microondas);
        }

        public void IniciarAquecimento(int tempoSegundos, int potencia = 10)
        {
            if (tempoSegundos < 1 || tempoSegundos > 120)
                throw new ArgumentException("O tempo deve ser entre 1 segundo e 2 minutos.");
            if (potencia < 1 || potencia > 10)
                throw new ArgumentException("A potência deve ser entre 1 e 10.");

            var microondas = _microondasRepositorio.Obter();

            microondas.TempoSegundos += tempoSegundos;
            microondas.Potencia = potencia;
            microondas.Estado = "Em funcionamento";
            microondas.InicioAquecimento = DateTime.Now;

            _microondasRepositorio.Salvar(microondas);
        }

        public void PausarAquecimento()
        {
            var microondas = _microondasRepositorio.Obter();
            if (microondas.Estado == "Em funcionamento")
            {
                int tempoRestante = microondas.TempoSegundos - (int)(DateTime.Now - microondas.InicioAquecimento.Value).TotalSeconds;
                tempoRestante = Math.Max(tempoRestante, 1);
                microondas.TempoSegundos = tempoRestante;
                microondas.Estado = "Pausado";
            }
            else if (microondas.Estado == "Pausado")
            {
                microondas.Estado = "Cancelado";
                microondas.TempoSegundos = 0;
                microondas.Potencia = 10;
            }
            _microondasRepositorio.Salvar(microondas);
        }

        public void CancelarAquecimento()
        {
            var microondas = _microondasRepositorio.Obter();
            microondas.Estado = "Cancelado";
            microondas.TempoSegundos = 0;
            microondas.Potencia = 10;
            _microondasRepositorio.Salvar(microondas);
        }

        public void AcrescentarTempo(int tempoSegundos)
        {
            var microondas = _microondasRepositorio.Obter();
            if (microondas.Estado == "Cancelado")
                throw new InvalidOperationException("Não é possível acrescentar tempo enquanto cancelado.");
            if (microondas.TempoSegundos + tempoSegundos > 120)
                throw new ArgumentException("Tempo máximo excedido.");
            microondas.TempoSegundos += tempoSegundos;
            _microondasRepositorio.Salvar(microondas);
        }

        public int ObterTempoRestante()
        {
            var microondas = _microondasRepositorio.Obter();
            if (microondas.Estado == "Em funcionamento")
            {
                return Math.Max(microondas.TempoSegundos - (int)(DateTime.Now - microondas.InicioAquecimento.Value).TotalSeconds, 0);
            }
            return microondas.TempoSegundos;
        }

        public string ObterEstado()
        {
            var microondas = _microondasRepositorio.Obter();
            return microondas.Estado;
        }

        public void IniciarProgramaAquecimento(string nomePrograma)
        {
            var microondas = _microondasRepositorio.Obter();
            var programa = _programaAquecimentoService.ObterPorNome(nomePrograma);

            if (programa == null)
                throw new ArgumentException("Programa de aquecimento não encontrado.");

            microondas.TempoSegundos = programa.Tempo;
            microondas.Potencia = programa.Potencia;
            microondas.Estado = "Em funcionamento";
            microondas.InicioAquecimento = DateTime.Now;

            _microondasRepositorio.Salvar(microondas);
        }

        public int ObterPotencia()
        {
            var microondas = _microondasRepositorio.Obter(); 
            return microondas.Potencia;
        }

        public Microondas ObterMicroondas()
        {
            return _microondasRepositorio.Obter();
        }
    }
}