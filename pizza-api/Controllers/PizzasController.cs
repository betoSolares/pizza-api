using Microsoft.AspNetCore.Mvc;
using pizza_api.Models;
using System.Collections.Generic;

namespace pizza_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PizzasController : ControllerBase
    {
        private static readonly List<Pizza> pizzas = new List<Pizza>();

        // GET: /api/pizzas
        // Return all the pizzas.
        [HttpGet]
        public IActionResult GetPizzas()
        {
            if (pizzas.Count != 0)
                return Ok(pizzas);
            return NotFound(new { error = "There is no pizzas at the moment." });

        }

        // GET: /api/pizzas/{name}
        // Return the pizza with that name.
        [HttpGet("{name}", Name = "GetPizza")]
        public IActionResult GetPizza(string name)
        {
            Pizza pizza = pizzas.Find(p => p.Name == name);
            if (pizza == null)
                return NotFound(new { error = "That pizza dosn't exist." });
            return Ok(pizza);

        }

        // POST: /api/pizzas
        // Add a new pizza, the pizza must have to be different.
        [HttpPost]
        public IActionResult AddPizza(Pizza pizza)
        {
            if (ModelState.IsValid)
            {
                if (pizzas.Contains(pizza))
                {
                    return Conflict(new { error = "The object you are trying to add already exists." });
                }
                else
                {
                    pizzas.Add(pizza);
                    return new CreatedAtRouteResult("GetPizza", new { name = pizza.Name }, pizza);
                }
            }
            return BadRequest(ModelState);
        }

        // PUT: /api/pizzas/{name}
        // Update an existing pizza.
        [HttpPut("{name}")]
        public IActionResult UpdatePizza(string name, Pizza pizza)
        {
            if (name.Equals(pizza.Name))
            {
                int index = pizzas.FindIndex(p => p.Name == name);
                if (index == -1)
                    return NotFound(new { error = "That pizza dosn't exist." });
                pizzas[index] = pizza;
                return new CreatedAtRouteResult("GetPizza", new { name }, pizza);
            }
            return BadRequest(new { error = "The name can't be different" });
        }

        // DELETE: /api/pizzas/{name}
        // Delete an existing pizza.
        [HttpDelete("{name}")]
        public IActionResult DeletePizza(string name)
        {
            int index = pizzas.FindIndex(p => p.Name == name);
            if (index == -1)
                return NotFound(new { error = "That pizza dosn't exist." });
            pizzas.RemoveAt(index);
            return NoContent();
        }
    }
}