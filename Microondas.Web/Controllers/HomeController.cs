using Microsoft.AspNetCore.Mvc;
using ProjetoMicroondas.Aplicacao.DTOs;
using ProjetoMicroondas.Aplicacao.Interfaces;
using ProjetoMicroondas.Aplicacao.Services;
using ProjetoMicroondas.Dominio.Interfaces;

namespace ProjetoMicroondas.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMicroondasService _microondasService;
        private readonly IProgramaAquecimentoServico _programaAquecimentoService;
        private readonly IProgramaAquecimentoCustomizadoService _programaAquecimentoCustomizadoService;

        public HomeController(IMicroondasService microondasService, IProgramaAquecimentoServico programaAquecimentoService, IProgramaAquecimentoCustomizadoService programaAquecimentoCustomizadoService)
        {
            _microondasService = microondasService;
            _programaAquecimentoService = programaAquecimentoService;
            _programaAquecimentoCustomizadoService = programaAquecimentoCustomizadoService;
        }

        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            var programasPreDefinidos = _programaAquecimentoService.ObterTodos();
            var programasCustomizados = _programaAquecimentoCustomizadoService.ObterTodos();
            ViewBag.ProgramasPreDefinidos = programasPreDefinidos;
            ViewBag.ProgramasCustomizados = programasCustomizados;
            return View();
        }

        [HttpGet]
        [Route("status")]
        public IActionResult ObterStatus()
        {
            var status = new
            {
                TempoRestante = _microondasService.ObterTempoRestante(),
                Estado = _microondasService.ObterEstado()
            };

            return Json(status);
        }

        [HttpPost]
        [Route("iniciar")]
        public IActionResult IniciarAquecimento([FromBody] AquecimentoRequest request)
        {
            try
            {
                _microondasService.IniciarAquecimento(request.Tempo, request.Potencia);
                var status = new
                {
                    Estado = _microondasService.ObterEstado(),
                    TempoRestante = _microondasService.ObterTempoRestante()
                };
                return Json(new { success = true, message = "Aquecimento iniciado com sucesso!", status });
            }
            catch (ArgumentException ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        [HttpPost]
        [Route("pausar")]
        public IActionResult PausarOuCancelar()
        {
            _microondasService.PausarAquecimento();
            var status = new
            {
                Estado = _microondasService.ObterEstado(),
                TempoRestante = _microondasService.ObterTempoRestante()
            };
            return Json(new { success = true, message = "Aquecimento pausado ou cancelado!", status });
        }

        [HttpPost]
        [Route("acrescentar")]
        public IActionResult AcrescentarTempo([FromBody] TempoRequest request)
        {
            _microondasService.AcrescentarTempo(request.Segundos);
            var status = new
            {
                Estado = _microondasService.ObterEstado(),
                TempoRestante = _microondasService.ObterTempoRestante()
            };
            return Json(new { success = true, message = $"Tempo aumentado em {request.Segundos} segundos.", status });
        }

        [HttpPost]
        [Route("iniciarPrograma")]
        public IActionResult IniciarPrograma([FromBody] ProgramaAquecimentoRequest request)
        {
            try
            {
                _microondasService.IniciarProgramaAquecimento(request.Nome);
                var status = new
                {
                    Estado = _microondasService.ObterEstado(),
                    TempoRestante = _microondasService.ObterTempoRestante()
                };
                return Json(new { success = true, message = "Programa de aquecimento iniciado com sucesso!", status });
            }
            catch (ArgumentException ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        [HttpPost]
        [Route("cadastrarPrograma")]
        public IActionResult CadastrarPrograma([FromBody] ProgramaAquecimentoCustomizadoDTO programaDto)
        {
            try
            {
                _programaAquecimentoCustomizadoService.Adicionar(programaDto);
                return Json(new { success = true, message = "Programa de aquecimento customizado cadastrado com sucesso!" });
            }
            catch (ArgumentException ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }
    }

    public class AquecimentoRequest
    {
        public int Tempo { get; set; }
        public int Potencia { get; set; }
    }

    public class TempoRequest
    {
        public int Segundos { get; set; }
    }

    public class ProgramaAquecimentoRequest
    {
        public string Nome { get; set; }
    }
}
