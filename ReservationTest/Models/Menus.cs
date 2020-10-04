using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReservationTest.Models
{
    public class Menus
    {
        public int ID { get; set; }
        public string Name { get; set; }
        //public List<string> ListOfDrinks { get; internal set; }
        public string Description { get; set; }
        public List<Meal> ListOfMeals { get; set; }
    }
}
