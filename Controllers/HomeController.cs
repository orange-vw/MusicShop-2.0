using Humanizer.Localisation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MusicShop.Data;
using MusicShop.Models;
using System.Diagnostics;

namespace MusicShop.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly MusicShopContext _context;

        public IActionResult Login()
        {
            return View();
        }

        private ShoppingCart shop;

        private readonly IHttpContextAccessor _httpContextAccessor;

        public HomeController(ILogger<HomeController> logger, MusicShopContext context)
        {
            _logger = logger;
            _context = context;
            shop = new ShoppingCart();
            shop.SelectedSongs = new Dictionary<int, int>();
        }

        public async Task<IActionResult> Index()
        {
            return _context.MusicLibrary != null ?
                        View(await _context.MusicLibrary.ToListAsync()) :
                        Problem("Entity set 'MusicShopContext.MusicLibrary'  is null.");
        }

        public async Task<IActionResult> Shop(string _Genre, string _Artist)
        {
            if (_context == null)
            {
                return Problem("Entity set 'MusicShopContext.MusicLibrary' is null.");
            }

            if (shop.SelectedSongs == null)
            {

            }
            var TotalSongs = from a in _context.MusicLibrary
                        select a;

            var Songs = from a in _context.MusicLibrary
                        select a;

            List<string> ArtistList = new List<string>();
            if (!String.IsNullOrEmpty(_Genre))
            {
                Songs = Songs.Where(s => s.Genre == _Genre);

                //If a genre is selected, show possible artists
                ArtistList = new List<string>();
                foreach (var song in Songs)
                {
                    if (!ArtistList.Contains(song.ArtistName))
                        ArtistList.Add(song.ArtistName);
                }
            }

            if (!String.IsNullOrEmpty(_Artist))
            {
                Songs = Songs.Where(s => s.ArtistName == _Artist);
            }

            var TotalSongsList = TotalSongs.ToList();
            var SongsList = Songs.ToList();
            var genreList = new List<string>();
            foreach (var song in TotalSongsList)
            {
                if (!genreList.Contains(song.Genre))
                    genreList.Add(song.Genre);
            }
            ViewData["Genres"] = genreList;
            ViewData["Artists"] = ArtistList;
            ViewData["Songs"] = SongsList;

            return View(await Songs.ToListAsync());
        }

        public IActionResult ShoppingCart()
        {
            MusicLibrary[] songs = new MusicLibrary[shop.SelectedSongs.Count()];
            int[] Ammount = new int[shop.SelectedSongs.Count()];

            int[] songIDs = shop.SelectedSongs.Keys.ToArray();
            for (int i = 0; i < songs.Length; i++)
            {
                songs[i] = _context.MusicLibrary.Find(songIDs[i]);
            }

            ViewData["Songs"] = songs;
            ViewData["Ammount"] = Ammount;

            decimal Total = 0;
            foreach (var songPair in shop.SelectedSongs)
            {
                Total += _context.MusicLibrary.Find(songPair.Key).Price * songPair.Value;
            }
            ViewData["Total"] = Total;

            return View();
        }

        public IActionResult AddToCart(int songId, int Ammount)
        {
            shop.SelectedSongs.Add(songId, Ammount);

            return RedirectToAction("ShoppingCart", "Home");
        }

        public IActionResult RemoveFromCart(int songId)
        {
            shop.SelectedSongs.Remove(songId);
            return RedirectToAction("Cart", "Home");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        [HttpPost]
        public IActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.Username == "123" && model.Password == "123")
                {
                    return RedirectToAction("Index", "MusicLibrary");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid username or password");
                }
            }

            return View(model);
        }
    }
}