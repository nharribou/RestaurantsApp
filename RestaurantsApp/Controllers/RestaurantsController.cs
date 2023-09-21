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


        /// <summary>
        /// Récupère une liste de restaurants.
        /// </summary>
        /// <remarks>
        /// Cette méthode permet de récupérer la liste complète de restaurants disponibles.
        /// </remarks>
        /// <returns>Une liste de restaurants.</returns>
        /// <response code="200">La liste de restaurants a été récupérée avec succès.</response>
        /// <response code="401">Vous n'êtes pas autorisé à consulter la ressource.</response>
        /// <response code="403">La ressource que vous avez tenté d'atteindre n'a pas été trouvée.</response>
        /// <response code="500">Échec de traitement de la requête par l'application.</response>
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

        /// <summary>
        /// Récupère un restaurant par son ID.
        /// </summary>
        /// <param name="id">ID du restaurant à récupérer.</param>
        /// <returns>Le restaurant correspondant à l'ID spécifié.</returns>
        /// <response code="200">Le restaurant a été récupéré avec succès.</response>
        /// <response code="401">Vous n'êtes pas autorisé à consulter la ressource.</response>
        /// <response code="403">La ressource que vous avez tenté d'atteindre n'a pas été trouvée.</response>
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

        /// <summary>
        /// Recherche des restaurants par nom.
        /// </summary>
        /// <param name="name">Nom du restaurant à rechercher.</param>
        /// <returns>Une liste de restaurants correspondant au nom spécifié.</returns>
        /// <response code="200">La liste de restaurants a été récupérée avec succès.</response>
        /// <response code="401">Vous n'êtes pas autorisé à consulter la ressource.</response>
        /// <response code="403">La ressource que vous avez tenté d'atteindre n'a pas été trouvée.</response>
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

        /// <summary>
        /// Récupère les restaurants les mieux notés.
        /// </summary>
        /// <returns>Une liste de restaurants triés par note décroissante.</returns>
        /// <response code="200">La liste de restaurants a été récupérée avec succès.</response>
        /// <response code="401">Vous n'êtes pas autorisé à consulter la ressource.</response>
        /// <response code="403">La ressource que vous avez tenté d'atteindre n'a pas été trouvée.</response>
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

        /// <summary>
        /// Récupère les restaurants par catégorie.
        /// </summary>
        /// <param name="categoryId">ID de la catégorie des restaurants à récupérer.</param>
        /// <returns>Une liste de restaurants appartenant à la catégorie spécifiée.</returns>
        /// <response code="200">La liste de restaurants a été récupérée avec succès.</response>
        /// <response code="401">Vous n'êtes pas autorisé à consulter la ressource.</response>
        /// <response code="403">La ressource que vous avez tenté d'atteindre n'a pas été trouvée.</response>
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

        /// <summary>
        /// Met à jour un restaurant par son ID.
        /// </summary>
        /// <param name="id">ID du restaurant à mettre à jour.</param>
        /// <param name="restaurantDTO">Données du restaurant à mettre à jour.</param>
        /// <returns>Aucun contenu en cas de succès.</returns>
        /// <response code="204">Le restaurant a été mis à jour avec succès.</response>
        /// <response code="401">Vous n'êtes pas autorisé à consulter la ressource.</response>
        /// <response code="404">Le restaurant avec l'ID spécifié n'a pas été trouvé.</response>
        /// <response code="500">Échec de traitement de la requête par l'application.</response>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRestaurant(int id, [FromBody] RestaurantCreateDTO restaurantDTO)
        {
            try
            {
                var existingRestaurant = await _context.Restaurants.FindAsync(id);
                if (existingRestaurant == null)
                {
                    return NotFound($"Restaurant with id {id} not found.");
                }

                // Update the existing restaurant properties with the DTO data
                existingRestaurant.Name = restaurantDTO.Name;
                existingRestaurant.Address = restaurantDTO.Address;
                existingRestaurant.City = restaurantDTO.City;
                existingRestaurant.CategoryId = restaurantDTO.CategoryId;
                existingRestaurant.Rating = restaurantDTO.Rating;

                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                // Log the exception for debugging purposes
                Console.WriteLine(ex);
                return StatusCode(500, "An error occurred while updating the restaurant.");
            }
        }


        /// <summary>
        /// Crée un nouveau restaurant.
        /// </summary>
        /// <param name="restaurantDTO">Données du restaurant à créer.</param>
        /// <returns>Le restaurant créé.</returns>
        /// <response code="201">Le restaurant a été créé avec succès.</response>
        /// <response code="400">Mauvaise requête : l'ID de catégorie spécifié n'existe pas.</response>
        /// <response code="401">Vous n'êtes pas autorisé à consulter la ressource.</response>
        /// <response code="500">Échec de traitement de la requête par l'application.</response>
        [HttpPost]
        public async Task<ActionResult<Restaurant>> PostRestaurant([FromBody] RestaurantCreateDTO restaurantDTO)
        {
            if (_context.Restaurants == null)
            {
                return Problem("Entity set 'RestaurantDbContext.Restaurants' is null.");
            }

            // Check if the specified categoryId exists
            var existingCategory = await _context.Categories.FindAsync(restaurantDTO.CategoryId);
            if (existingCategory == null)
            {
                return BadRequest("Invalid categoryId. Category not found.");
            }

            // Map the DTO to your entity model
            var restaurant = new Restaurant
            {
                Name = restaurantDTO.Name,
                Address = restaurantDTO.Address,
                City = restaurantDTO.City,
                CategoryId = restaurantDTO.CategoryId,
                Rating = restaurantDTO.Rating
            };

            _context.Restaurants.Add(restaurant);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRestaurant", new { id = restaurant.Id }, restaurant);
        }



        /// <summary>
        /// Supprime un restaurant par son ID.
        /// </summary>
        /// <param name="id">ID du restaurant à supprimer.</param>
        /// <returns>Aucun contenu en cas de succès.</returns>
        /// <response code="204">Le restaurant a été supprimé avec succès.</response>
        /// <response code="401">Vous n'êtes pas autorisé à consulter la ressource.</response>
        /// <response code="404">Le restaurant avec l'ID spécifié n'a pas été trouvé.</response>
        /// <response code="500">Échec de traitement de la requête par l'application.</response>
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
