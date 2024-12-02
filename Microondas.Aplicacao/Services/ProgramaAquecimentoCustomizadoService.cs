using AutoMapper;
using ProjetoMicroondas.Aplicacao.DTOs;
using ProjetoMicroondas.Aplicacao.Interfaces;
using ProjetoMicroondas.Dominio.Entidades;
using ProjetoMicroondas.Dominio.Interfaces;
using System.Collections.Generic;

namespace ProjetoMicroondas.Aplicacao.Services
{
    public class ProgramaAquecimentoCustomizadoService : IProgramaAquecimentoCustomizadoService
    {
        private readonly IProgramaAquecimentoCustomizadoRepository _programaAquecimentoCustomizadoRepository;
        private readonly IMapper _mapper;

        public ProgramaAquecimentoCustomizadoService(IProgramaAquecimentoCustomizadoRepository programaAquecimentoCustomizadoRepository, IMapper mapper)
        {
            _programaAquecimentoCustomizadoRepository = programaAquecimentoCustomizadoRepository;
            _mapper = mapper;
        }

        public IEnumerable<ProgramaAquecimentoCustomizadoDTO> ObterTodos()
        {
            var programas = _programaAquecimentoCustomizadoRepository.ObterTodos();
            return _mapper.Map<IEnumerable<ProgramaAquecimentoCustomizadoDTO>>(programas);
        }

        public ProgramaAquecimentoCustomizadoDTO ObterPorNome(string nome)
        {
            var programa = _programaAquecimentoCustomizadoRepository.ObterPorNome(nome);
            return _mapper.Map<ProgramaAquecimentoCustomizadoDTO>(programa);
        }

        public void Adicionar(ProgramaAquecimentoCustomizadoDTO programaDto)
        {
            var programa = _mapper.Map<ProgramaAquecimentoCustomizado>(programaDto);
            _programaAquecimentoCustomizadoRepository.Adicionar(programa);
        }
    }
}
