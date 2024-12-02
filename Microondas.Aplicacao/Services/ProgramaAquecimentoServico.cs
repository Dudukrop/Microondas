using AutoMapper;
using ProjetoMicroondas.Aplicacao.DTOs;
using ProjetoMicroondas.Aplicacao.Interfaces;
using ProjetoMicroondas.Dominio.Interfaces;
using System.Collections.Generic;

namespace ProjetoMicroondas.Aplicacao.Services
{
    public class ProgramaAquecimentoService : IProgramaAquecimentoServico
    {
        private readonly IProgramaAquecimentoRepositorio _programaAquecimentoRepository;
        private readonly IMapper _mapper;

        public ProgramaAquecimentoService(IProgramaAquecimentoRepositorio programaAquecimentoRepository, IMapper mapper)
        {
            _programaAquecimentoRepository = programaAquecimentoRepository;
            _mapper = mapper;
        }

        public IEnumerable<ProgramaAquecimentoDTO> ObterTodos()
        {
            var programas = _programaAquecimentoRepository.ObterTodos();
            return _mapper.Map<IEnumerable<ProgramaAquecimentoDTO>>(programas);
        }

        public ProgramaAquecimentoDTO ObterPorNome(string nome)
        {
            var programa = _programaAquecimentoRepository.ObterPorNome(nome);
            return _mapper.Map<ProgramaAquecimentoDTO>(programa);
        }
    }
}
