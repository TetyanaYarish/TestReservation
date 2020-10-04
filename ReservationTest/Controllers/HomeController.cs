using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ReservationTest.Data;
using ReservationTest.Models;
using ReservationTest.Services;

namespace ReservationTest.Controllers
{


    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;

        Menus menus = new Menus();
        Meal meal = new Meal();


        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _context = context;
            _logger = logger;

            AddDataToDB();

            try
            {
                _context = context ?? throw new ArgumentNullException(nameof(context));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new Exception(ex.Message);
            }
        }

        [HttpGet]
        public IActionResult Index()
        {
                return View();
        }


        // Метод що додає данні в db
        private void AddDataToDB()
        {
            List<Meal> meals = new List<Meal>();

            meal.Name = "Meal1";
            meal.Description = "Description kjnjhbvjbjhvgv";

            meals.Add(meal);
            _context.Add(meal);

            menus.Name = "Menues";
            menus.Description = "As the premier restaurant in Rhodes, we are proud to offer a fabulous array of fresh, authentic cuisine. Phoenix offers our famous menu in Rhodes as well as A La Carte options daily, with exclusive, ever-changing menu’s to suit all tastes";
            menus.ListOfMeals = meals;

            _context.Add(menus);
            _context.SaveChanges();
        }
      
        [HttpGet]
        public IActionResult Menus()
        {

            return View(_context.Menus.ToList());
        }

  
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
