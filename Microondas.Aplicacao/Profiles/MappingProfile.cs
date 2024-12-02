using AutoMapper;
using ProjetoMicroondas.Aplicacao.DTOs;
using ProjetoMicroondas.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoMicroondas.Aplicacao.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ProgramaAquecimento, ProgramaAquecimentoDTO>().ReverseMap();
            CreateMap<Microondas, MicroondasDTO>().ReverseMap();
            CreateMap<ProgramaAquecimentoCustomizado, ProgramaAquecimentoCustomizadoDTO>().ReverseMap();
        }
    }
}
