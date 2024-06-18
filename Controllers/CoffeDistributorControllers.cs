using coffe.distributor.model;
using coffe.distributor.service;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace coffe.distributor.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CoffeDistributorControllers : ControllerBase
    {
        private readonly PriceCalculatorService _priceCalculatorService;

        public CoffeDistributorControllers()
        {
            _priceCalculatorService = new PriceCalculatorService();
        }

        [HttpGet("drinks")]
        public ActionResult<IEnumerable<Drink>> GetDrinks()
        {
            var drinks = _priceCalculatorService.GetAvailableDrinks();
            return Ok(drinks);
        }

        [HttpGet("price/{drinkName}")]
        public ActionResult<decimal> GetDrinkPrice(string drinkName)
        {
            var drinks = _priceCalculatorService.GetAvailableDrinks();
            var drink = drinks.Find(d => d.Name.Equals(drinkName, StringComparison.OrdinalIgnoreCase));

            if (drink == null)
            {
                return NotFound("Drink not found");
            }

            var price = _priceCalculatorService.CalculateDrinkPrice(drink);
            return Ok(price);
        }
    }
}
