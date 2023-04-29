using Microsoft.AspNetCore.Mvc;
using PasseiosComCaes.Interfaces;
using PasseiosComCaes.ViewModels;

namespace PasseiosComCaes.Controllers
{
    public class Dashboard : Controller
    {
        private readonly IDashboardRepository _dashboardRepository;

        public Dashboard(IDashboardRepository dashboardRepository)
        {
            _dashboardRepository = dashboardRepository;
        }
        public async Task<IActionResult> Index()
        {
            var userPasseios = await _dashboardRepository.GetAllUsuarioPasseio();
            var userClubs = await _dashboardRepository.GetAllUsuarioClub();
            var dashboarViewModel = new DashboardViewModel
            {
                Passeios = userPasseios,
                Clubs = userClubs,
            };
            return View(dashboarViewModel);

        }
    }
}
