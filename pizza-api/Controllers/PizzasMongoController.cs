using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using pizza_api.Models;
using pizza_api.Services;
using System;
using System.Collections.Generic;

namespace pizza_api.Controllers
{
    [Route("api/mongo")]
    [ApiController]
    public class PizzasMongoController : ControllerBase
    {
        private readonly PizzaService pizzaService;

        // Constructor
        public PizzasMongoController(PizzaService pizzaService)
        {
            this.pizzaService = pizzaService;
        }

        // GET: /api/pizzas/mongo
        // Return all the pizzas.
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                List<Pizza> pizzas = pizzaService.Get();
                if (pizzas.Count != 0)
                    return Ok(pizzas);
                return NotFound(new { error = "There is no pizzas at the moment." });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { serverError = ex });
            }
        }

        // GET: /api/pizzas/mongo/{name}
        // Return the pizza with that name.
        [HttpGet("{name}", Name = "GetPizzaMongo")]
        public IActionResult Get(string name)
        {
            try
            {
                Pizza pizza = pizzaService.Get(name);
                if (pizza == null)
                    return NotFound(new { error = "That pizza dosn't exist." });
                return Ok(pizza);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { serverError = ex });
            }
        }

        // POST: /api/pizzas/mongo
        // Add a new pizza, the pizza must have to be different.
        [HttpPost]
        public IActionResult Add(Pizza pizza)
        {
            if (ModelState.IsValid)
            {
                bool isExistingPizza = false;
                try
                {
                    Pizza pizzaFound = pizzaService.Get(pizza.Name);
                    if (pizzaFound != null)
                        isExistingPizza = true;
                }
                catch(Exception ex)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, new { serverError = ex });
                }
                if (isExistingPizza)
                {
                    return Conflict(new { error = "The pizza you are trying to add already exists." });
                }
                else
                {
                    try
                    {
                        pizzaService.Create(pizza);
                        return new CreatedAtRouteResult("GetPizzaMongo", new { name = pizza.Name }, pizza);
                    }
                    catch (Exception ex)
                    {
                        return StatusCode(StatusCodes.Status500InternalServerError, new { serverError = ex });
                    }
                }
            }
            return BadRequest(ModelState);
        }

        // PUT: /api/pizzas/mongo/{name}
        // Update an existing pizza.
        [HttpPut("{name}")]
        public IActionResult Update(string name, Pizza pizza)
        {
            if (name.Equals(pizza.Name))
            {
                bool isExistingPizza = false;
                try
                {
                    Pizza pizzaFound = pizzaService.Get(pizza.Name);
                    if (pizzaFound != null)
                        isExistingPizza = true;
                }
                catch (Exception ex)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, new { serverError = ex });
                }
                if (isExistingPizza)
                {
                    try
                    {
                        pizzaService.Update(name, pizza);
                        return new CreatedAtRouteResult("GetPizzaMongo", new { name = pizza.Name }, pizza);
                    }
                    catch (Exception ex)
                    {
                        return StatusCode(StatusCodes.Status500InternalServerError, new { serverError = ex });
                    }
                }
                else
                {
                    return NotFound(new { error = "That pizza dosn't exist." });
                }
            }
            return BadRequest(new { error = "The name can't be different" });
        }

        // DELETE: /api/pizzasmongo/{name}
        // Delete an existing pizza.
        [HttpDelete("{name}")]
        public IActionResult Delete(string name)
        {
            bool isExistingPizza = false;
            try
            {
                Pizza pizzaFound = pizzaService.Get(name);
                if (pizzaFound != null)
                    isExistingPizza = true;
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { serverError = ex });
            }
            if (isExistingPizza)
            {
                try
                {
                    pizzaService.Delete(name);
                    return NoContent();
                }
                catch (Exception ex)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, new { serverError = ex });
                }
            }
            else
            {
                return NotFound(new { error = "That pizza dosn't exist." });
            }
        }
    }
}