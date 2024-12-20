﻿using ProjetoMicroondas.Dominio.Entidades;
using ProjetoMicroondas.Dominio.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace ProjetoMicroondas.Infraestrutura.Repositories
{
    public class ProgramaAquecimentoRepositorio : IProgramaAquecimentoRepositorio
    {
        private readonly List<ProgramaAquecimento> _programas;

        public ProgramaAquecimentoRepositorio()
        {
            _programas = new List<ProgramaAquecimento>
            {
                new ProgramaAquecimento
                {
                    Nome = "Pipoca",
                    Alimento = "Pipoca (de micro-ondas)",
                    Tempo = 180,
                    Potencia = 7,
                    StringAquecimento = "p",
                    Instrucoes = "Observar o barulho de estouros do milho, caso houver um intervalo de mais de 10 segundos entre um estouro e outro, interrompa o aquecimento."
                },
                new ProgramaAquecimento
                {
                    Nome = "Leite",
                    Alimento = "Leite",
                    Tempo = 300,
                    Potencia = 5,
                    StringAquecimento = "l",
                    Instrucoes = "Cuidado com aquecimento de líquidos, o choque térmico aliado ao movimento do recipiente pode causar fervura imediata causando risco de queimaduras."
                },
                new ProgramaAquecimento
                {
                    Nome = "Carnes de boi",
                    Alimento = "Carne em pedaço ou fatias",
                    Tempo = 840,
                    Potencia = 4,
                    StringAquecimento = "c",
                    Instrucoes = "Interrompa o processo na metade e vire o conteúdo com a parte de baixo para cima para o descongelamento uniforme."
                },
                new ProgramaAquecimento
                {
                    Nome = "Frango",
                    Alimento = "Frango (qualquer corte)",
                    Tempo = 480,
                    Potencia = 7,
                    StringAquecimento = "f",
                    Instrucoes = "Interrompa o processo na metade e vire o conteúdo com a parte de baixo para cima para o descongelamento uniforme."
                },
                new ProgramaAquecimento
                {
                    Nome = "Feijão",
                    Alimento = "Feijão congelado",
                    Tempo = 480,
                    Potencia = 9,
                    StringAquecimento = "j",
                    Instrucoes = "Deixe o recipiente destampado e em casos de plástico, cuidado ao retirar o recipiente pois o mesmo pode perder resistência em altas temperaturas."
                }
            };
        }

        public IEnumerable<ProgramaAquecimento> ObterTodos()
        {
            return _programas;
        }

        public ProgramaAquecimento ObterPorNome(string nome)
        {
            return _programas.FirstOrDefault(p => p.Nome.ToLower() == nome.ToLower());
        }
    }
}
