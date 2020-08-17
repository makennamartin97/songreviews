using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using songreviews.Models;

namespace songreviews.Controllers
{
    public class HomeController : Controller
    {
        private MyContext _context;

        public HomeController(MyContext context)
        {
            _context = context;
        }

        [HttpGet("")]

        public IActionResult Index()
        {
            List<Song> Songs = _context.Songs.ToList();
            return View("Index", Songs);
        }
        [HttpGet("new")]

        public IActionResult New()
        {

            return View();
        }


        [HttpPost("new")]
        public IActionResult Create(Song newsong)
        {

            if(ModelState.IsValid)
            {
                DateTime localDate = DateTime.Now;
                newsong.CreatedAt = localDate;
                newsong.UpdatedAt = localDate;
                _context.Songs.Add(newsong);
                _context.SaveChanges();
                return RedirectToAction("Index");

            }
            else{
                return View("new"); 
            }
        }
        [HttpGet("songreviews/{id}")]

        public IActionResult Details(int id )
        {
            Song thesong = _context.Songs.FirstOrDefault(lp => lp.songID == id);
            return View(thesong);
        }
        [HttpGet("{id}")]

        public IActionResult Edit(int id )
        {
            Song thesong = _context.Songs.FirstOrDefault(lp => lp.songID == id);
            return View("Edit", thesong);
        }
        [HttpPost("{id}/update")]
        public IActionResult Update(int id, Song s)

        {
            if (ModelState.IsValid)
            {
                Song chosen = _context.Songs.FirstOrDefault(lp => lp.songID == id);
                chosen.Title = s.Title;
                chosen.Artist = s.Artist;
                chosen.Genre = s.Genre;
                chosen.Stars = s.Stars;
                chosen.Review = s.Review;
                chosen.UpdatedAt = DateTime.Now;
                _context.SaveChanges();
                
                return Redirect("/");
            }
            else
            {
                s.songID = id;
                return View("Edit", s);
            }
         
        }
        [HttpGet("delete/{id}")]
        public IActionResult Delete(int id)
        {
            Song removesong = _context.Songs.SingleOrDefault(lp => lp.songID == id);
            _context.Songs.Remove(removesong);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }




        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
