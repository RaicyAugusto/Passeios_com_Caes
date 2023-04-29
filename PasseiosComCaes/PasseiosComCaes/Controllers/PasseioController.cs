using Microsoft.AspNetCore.Mvc;
using PasseiosComCaes.Helpers;
using PasseiosComCaes.Interfaces;
using PasseiosComCaes.Models;
using PasseiosComCaes.Repository;
using PasseiosComCaes.ViewModels;

namespace PasseiosComCaes.Controllers
{
    public class PasseioController : Controller
    {
        private readonly IPasseioRepository _passeioRepository;
        private readonly IFotoService _fotoService;
        private readonly IHttpContextAccessor _httpContextAccessor;


        public PasseioController(IPasseioRepository passeioRepository, IFotoService fotoService, IHttpContextAccessor httpContextAccessor)
        {
            _passeioRepository = passeioRepository;
            _fotoService = fotoService;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<IActionResult> Index()
        {
            IEnumerable<Passeio> passeios =await _passeioRepository.GetAllPasseiosAsync();
            return View(passeios);
        }

        public async Task<IActionResult> Detalhes(int id)
        {
            var passeio = await _passeioRepository.GetPasseioByIdAsync(id);
            return View(passeio);
        }
            
        public IActionResult Criar()
        {
            var curUserId = _httpContextAccessor.HttpContext?.User.GetUserId();
            var criarPasseioViewModel = new CriarPasseiosViewModel() { AppUserId = curUserId };
            return View(criarPasseioViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Criar(CriarPasseiosViewModel passeioVm)
        {
            if (ModelState.IsValid)
            {
                var result = await _fotoService.AddPhotoAsync(passeioVm.Imagem);

                var passeio = new Passeio
                {
                    Titulo = passeioVm.Titulo,
                    Descricao = passeioVm.Descricao,
                    Imagem = result.Url.ToString(),
                    AppUserId = passeioVm.AppUserId,
                    Endereco = new Endereco
                    {
                        Bairro = passeioVm.Endereco.Bairro,
                        Cidade = passeioVm.Endereco.Cidade,
                        Estado = passeioVm.Endereco.Estado,
                        Cep = passeioVm.Endereco.Cep
                    }
                };
                _passeioRepository.CriarPasseio(passeio);
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", "Upload da foto falhou");
            }
            return View(passeioVm);
        }

        [HttpGet]
        public async Task<IActionResult> Editar(int id)
        {
            var passeio = await _passeioRepository.GetPasseioByIdAsyncNoTracking(id);
            if (passeio == null) return View("Error");
            var passeioVm = new EditarPasseioViewModel()
            {
                Titulo = passeio.Titulo,
                Descricao = passeio.Descricao,
                EnderecoId = passeio.EnderecoId,
                Endereco = passeio.Endereco,
                URL = passeio.Imagem,
            };
            return View(passeioVm);
        }

        [HttpPost]
        public async Task<IActionResult> Editar(int id, EditarPasseioViewModel passeioVm)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "A ediçao do club falhou");
            }

            var userPasseio = await _passeioRepository.GetPasseioByIdAsyncNoTracking(id);

            if (userPasseio != null)
            {
                try
                {
                    await _fotoService.DeletePhotoAsync(userPasseio.Imagem);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Não foi possivél excluir a foto");
                    return View(passeioVm);
                }

                var fotoResult = await _fotoService.AddPhotoAsync(passeioVm.Imagem);
                var passeio = new Passeio
                {
                    Id = id,
                    Titulo = passeioVm.Titulo,
                    Descricao = passeioVm.Descricao,
                    Imagem = fotoResult.Url.ToString(),
                    EnderecoId = passeioVm.EnderecoId,
                    Endereco = passeioVm.Endereco,
                };
                _passeioRepository.UpdatePasseio(passeio);

                return RedirectToAction("Index");
            }
            else
            {
                return View(passeioVm);
            }

        }

        public async Task<IActionResult> Deletar(int id)
        {
            var passeioDetalhes = await _passeioRepository.GetPasseioByIdAsync(id);
            if (passeioDetalhes == null)
            {
                return View("Error");
            }
            return View(passeioDetalhes);
        }

        [HttpPost, ActionName("Deletar")]
        public async Task<IActionResult> DeletarClub(int id)
        {
            var passeioDetalhes = await _passeioRepository.GetPasseioByIdAsync(id);
            if (passeioDetalhes == null)
            {
                return View("Error");
            }

            _passeioRepository.DeletePasseio(passeioDetalhes);
            return RedirectToAction("Index");
        }

    }
}
