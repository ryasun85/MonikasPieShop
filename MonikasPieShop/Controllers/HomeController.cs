using Microsoft.AspNetCore.Mvc;
using MonikasPieShop.Models;
using MonikasPieShop.ViewModels;

namespace MonikasPieShop.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPieRepository _pieRepository;

        public HomeController(IPieRepository pieRepository)
        {
            _pieRepository = pieRepository;
        }

        public IActionResult Index()
        {
            var piesOfTheWeek = _pieRepository.PiesOfTheWeek;
            var homeViewModel = new HomeViewModel(piesOfTheWeek); 
            return View(homeViewModel);
        }
    }
}
