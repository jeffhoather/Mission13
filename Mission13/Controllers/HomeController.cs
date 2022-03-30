using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Mission13.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Mission13.Controllers
{
    public class HomeController : Controller
    {
        private BowlersDbContext _context { get; set; }
 
        public HomeController(BowlersDbContext temp)
        {
            _context = temp;
        }

        public IActionResult Index()
        {
            var blah = _context.Bowlers
                .Include(x => x.Team)
                .OrderBy(x => x.Team.TeamName)
                //.Where(b => b.Category == bookType || bookType == null)
                .ToList();

            return View(blah);
        }

        [HttpGet]
        public IActionResult AddBowler()
        {
            ViewBag.Teams = _context.Teams.ToList();

            return View();
        }

        [HttpPost]
        public IActionResult AddBowler(Bowler b)
        {
            b.BowlerID = (_context.Bowlers.ToList().Capacity + 1);

            _context.Add(b);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int bowlerid)
        {
            ViewBag.Teams = _context.Teams.ToList();

            var application = _context.Bowlers.Single(x => x.BowlerID == bowlerid);
            
            return View("AddBowler", application);
        }

        [HttpPost]
        public IActionResult Edit(Bowler b)
        {
            _context.Update(b);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult Delete(Bowler b)
        {
            _context.Bowlers.Remove(b);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }


        
    }
}
