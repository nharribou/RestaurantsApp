using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestaurantsApp.Models;
using RestaurantsApp.Models.Context;

namespace RestaurantsApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RestaurantsController : ControllerBase
    {
        private readonly RestaurantDbContext _context;

        public RestaurantsController(RestaurantDbContext context)
        {
            _context = context;
        }

        // GET: api/Restaurants
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Restaurant>>> GetRestaurants()
        {
            if (_context.Restaurants == null)
            {
                return NotFound();
            }

    
            var restaurants = await _context.Restaurants
                .Include(r => r.Category)
                .ToListAsync();

            return restaurants;
        }

        // GET: api/Restaurants/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Restaurant>> GetRestaurant(int id)
        {
            if (_context.Restaurants == null)
            {
                return NotFound();
            }

       
            var restaurant = await _context.Restaurants
                .Include(r => r.Category)
                .FirstOrDefaultAsync(r => r.Id == id);

            if (restaurant == null)
            {
                return NotFound();
            }

            return restaurant;
        }

        [HttpGet("SearchByName")]
        public async Task<ActionResult<IEnumerable<Restaurant>>> SearchByName(string name)
        {
            if (_context.Restaurants == null)
            {
                return NotFound();
            }

            var restaurants = await _context.Restaurants
                .Include(r => r.Category)
                .Where(r => r.Name.Contains(name))
                .ToListAsync();

            return restaurants;
        }

        [HttpGet("TopRated")]
        public async Task<ActionResult<IEnumerable<Restaurant>>> GetTopRatedRestaurants()
        {
            if (_context.Restaurants == null)
            {
                return NotFound();
            }

            var topRatedRestaurants = await _context.Restaurants
                .Include(r => r.Category) 
                .OrderByDescending(r => r.Rating)
                .ToListAsync();

            return topRatedRestaurants;
        }

        [HttpGet("ByCategory/{categoryId}")]
        public async Task<ActionResult<IEnumerable<Restaurant>>> GetRestaurantsByCategory(int categoryId)
        {
            if (_context.Restaurants == null)
            {
                return NotFound();
            }

            var restaurantsByCategory = await _context.Restaurants
                .Where(r => r.CategoryId == categoryId)
                .Include(r => r.Category) 
                .ToListAsync();

            return restaurantsByCategory;
        }


        // PUT: api/Restaurants/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRestaurant(int id, Restaurant restaurant)
        {
            if (id != restaurant.Id)
            {
                return BadRequest();
            }

            _context.Entry(restaurant).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RestaurantExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Restaurants
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Restaurant>> PostRestaurant(Restaurant restaurant)
        {
          if (_context.Restaurants == null)
          {
              return Problem("Entity set 'RestaurantDbContext.Restaurants'  is null.");
          }
            _context.Restaurants.Add(restaurant);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRestaurant", new { id = restaurant.Id }, restaurant);
        }

        // DELETE: api/Restaurants/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRestaurant(int id)
        {
            if (_context.Restaurants == null)
            {
                return NotFound();
            }
            var restaurant = await _context.Restaurants.FindAsync(id);
            if (restaurant == null)
            {
                return NotFound();
            }

            _context.Restaurants.Remove(restaurant);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RestaurantExists(int id)
        {
            return (_context.Restaurants?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
