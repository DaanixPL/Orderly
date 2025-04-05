using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Orderly.Models;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Orderly.Controllers
{
    [Route("api/[controller]")]
    [ApiController] // Dodanie API Controller
    public class RestaurantsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public RestaurantsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetRestaurants()
        {
            var restaurants = await _context.Restaurants.ToListAsync();

            // Log and return an empty list if no restaurants found
            if (restaurants == null || restaurants.Count == 0)
            {
                System.Diagnostics.Debug.WriteLine("Warning: No restaurants found in database.");
                restaurants = new List<Restaurant>();
            }
            return Ok(restaurants);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetRestaurant(int id)
        {
            var restaurant = await _context.Restaurants
                .Include(r => r.MenuItems)
                .FirstOrDefaultAsync(r => r.Id == id);

            if (restaurant == null)
                return NotFound("Restauracja nie istnieje.");

            return Ok(restaurant);
        }
    }
}
