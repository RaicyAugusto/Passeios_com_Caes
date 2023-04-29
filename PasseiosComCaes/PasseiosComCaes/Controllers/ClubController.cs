using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PasseiosComCaes.Data;
using PasseiosComCaes.Helpers;
using PasseiosComCaes.Interfaces;
using PasseiosComCaes.Models;
using PasseiosComCaes.ViewModels;

namespace PasseiosComCaes.Controllers
{
    public class ClubController : Controller
    {
        private readonly IClubRepository _clubRepository;
        private readonly IFotoService _fotoService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ClubController(IClubRepository clubRepository, IFotoService fotoService, IHttpContextAccessor httpContextAccessor)
        {
            _clubRepository = clubRepository;
            _fotoService = fotoService;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IActionResult> Index()
        {
            var clubs = await _clubRepository.GetALlClubsAsync();
            return View(clubs);
        }

        public async Task<IActionResult> Detalhes(int id)
        {
            var club = await _clubRepository.GetClubIdAsync(id);
            return View(club);
        }


        public IActionResult Criar()
        {
            var curUserId = _httpContextAccessor.HttpContext?.User.GetUserId();
            var criarClubViewModel = new CriarClubViewModel() { AppUserId = curUserId };
            return View(criarClubViewModel);  
        }

        [HttpPost]
        public async Task<IActionResult> Criar(CriarClubViewModel clubVm)
        {
            if (ModelState.IsValid)
            {
                var result = await _fotoService.AddPhotoAsync(clubVm.Imagem);

                var club = new Club
                {
                    Nome = clubVm.Nome,
                    Descricao = clubVm.Descricao,
                    Imagem = result.Url.ToString(),
                    AppUserId = clubVm.AppUserId,
                    Endereco = new Endereco
                    {
                        Bairro = clubVm.Endereco.Bairro,
                        Cidade = clubVm.Endereco.Cidade,
                        Estado = clubVm.Endereco.Estado,
                        Cep = clubVm.Endereco.Cep
                    }
                };
                _clubRepository.CriarClub(club);
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", "Upload da foto falhou");
            }
            return View(clubVm);
        }

        [HttpGet]
        public async Task<IActionResult> Editar(int id)
        {
            var club = await _clubRepository.GetClubIdAsyncNoTracking(id);
            if (club == null) return View("Error");
            var clubVM = new EditarClubViewModel
            {
                Nome = club.Nome,
                Descricao = club.Descricao,
                EnderecoId = club.EnderecoId,
                Endereco = club.Endereco,
                URL = club.Imagem,
                TamanhoDoClub = club.TamanhoDoClub
            };
            return View(clubVM);
        }

        [HttpPost]
        public async Task<IActionResult> Editar(int id, EditarClubViewModel clubVM)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "A ediçao do club falhou");
            }

            var userClub = await _clubRepository.GetClubIdAsyncNoTracking(id);

            if (userClub != null)
            {
                try
                {
                    await _fotoService.DeletePhotoAsync(userClub.Imagem);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Não foi possivél excluir a foto");
                    return View(clubVM);
                }

                var fotoResult = await _fotoService.AddPhotoAsync(clubVM.Imagem);
                var club = new Club
                {
                    Id = id,
                    Nome = clubVM.Nome,
                    Descricao = clubVM.Descricao,
                    Imagem = fotoResult.Url.ToString(),
                    EnderecoId = clubVM.EnderecoId,
                    Endereco = clubVM.Endereco, 
                };
                _clubRepository.UpdateClub(club);

                return RedirectToAction("Index");
            }
            else
            {
                return View(clubVM);
            }
            
        }

        public async Task<IActionResult> Deletar(int id)
        {
            var clubDetalhes = await _clubRepository.GetClubIdAsync(id);
            if (clubDetalhes == null) return View("Error");
            return View(clubDetalhes);
        }

        [HttpPost, ActionName("Deletar")]
        public async Task<IActionResult> DeletarClub(int id)
        {
            var clubDetalhes = await _clubRepository.GetClubIdAsync(id);
            if (clubDetalhes == null) return View("Error");

            _clubRepository.DeleteClub(clubDetalhes);
            return RedirectToAction("Index");
        }
    }
}   
