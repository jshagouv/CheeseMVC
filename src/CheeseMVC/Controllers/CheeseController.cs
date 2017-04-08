using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CheeseMVC.Models; //List the namespace you want to pull in.

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace CheeseMVC.Controllers
{
    public class CheeseController : Controller
    {
        //This belongs in a model
        //static private List<Cheese> Cheeses = new List<Cheese>();

        // GET: /<controller>/
        public IActionResult Index()
        {
            ViewBag.cheeses = CheeseData.GetAll();

            return View();
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [Route("/Cheese/Add")]
        public IActionResult NewCheese(Cheese newCheese) //the Cheese object can only
            //be created if the same names as the class properties are used in the 
            //View file for this method. This is Model Binding.
            //Must have a default constructor for Model Binding to work.
            /*Conceptually, what this does is something like this:
                Cheese newCheese = new Cheese();
                newCheese.Name = Request.get("name")
                newCheese.Description = Request.get("description")
             */
        {
            // Add the new cheese to my existing cheeses using
            // constructor with arguments that set the properties (lines 13-17 of
            // Cheese.cs)
            //Cheeses.Add(new Cheese(name, description));

            //Add cheese using default constructor
            //Instructor's preference, as then don't need to define a 
            //constructor whose only purpose is to define property values.
            //With refactored code separating out the data methods into a model
            //don't need these lines because we can now pass a Cheese type object
            //that will already have name and description
            /*Cheese newCheese = new Cheese
            {
                Description = description,
                Name = name
            };
            /*
             Above 5 lines is shorthand for this:
             Cheese newCheese = new Cheese();
             newCheese.Description = description;
             newCheese.Name = name;
             */
            CheeseData.Add(newCheese);

            return Redirect("/Cheese");
        }

        public IActionResult Remove()
        {
            ViewBag.title = "Remove Cheeses";
            ViewBag.cheeses = CheeseData.GetAll();
            return View();
        }

        [HttpPost]
        public IActionResult Remove(int[] cheeseIds)
        {
            //Not really appropriate to have this data process contained in 
            //the controller; it should be in the model. So it has been moved.
            /* foreach (int cheeseId in cheeseIds)
            {
                Cheeses.RemoveAll(x => x.CheeseId == cheeseId);
            } */

            foreach (int cheeseId in cheeseIds)
            {
                CheeseData.Remove(cheeseId);
            }
            return Redirect("/");
        }
    }
}
