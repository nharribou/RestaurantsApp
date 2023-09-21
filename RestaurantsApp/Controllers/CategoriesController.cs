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
    public class CategoriesController : ControllerBase
    {
        private readonly RestaurantDbContext _context;

        public CategoriesController(RestaurantDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Récupère une liste de catégories de restaurants.
        /// </summary>
        /// <remarks>
        /// Cette méthode permet de récupérer la liste complète des catégories de restaurants disponibles.
        /// </remarks>
        /// <returns>Une liste de catégories de restaurants.</returns>
        /// <response code="200">La liste de catégories a été récupérée avec succès.</response>
        /// <response code="401">Vous n'êtes pas autorisé à consulter la ressource.</response>
        /// <response code="403">La ressource que vous avez tenté d'atteindre n'a pas été trouvée.</response>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Category>>> GetCategories()
        {
          if (_context.Categories == null)
          {
              return NotFound();
          }
            return await _context.Categories.ToListAsync();
        }

        /// <summary>
        /// Récupère une catégorie de restaurant par son ID.
        /// </summary>
        /// <param name="id">ID de la catégorie à récupérer.</param>
        /// <returns>La catégorie de restaurant correspondant à l'ID spécifié.</returns>
        /// <response code="200">La catégorie a été récupérée avec succès.</response>
        /// <response code="401">Vous n'êtes pas autorisé à consulter la ressource.</response>
        /// <response code="403">La ressource que vous avez tenté d'atteindre n'a pas été trouvée.</response>
        [HttpGet("{id}")]
        public async Task<ActionResult<Category>> GetCategory(int id)
        {
          if (_context.Categories == null)
          {
              return NotFound();
          }
            var category = await _context.Categories.FindAsync(id);

            if (category == null)
            {
                return NotFound();
            }

            return category;
        }

        /// <summary>
        /// Met à jour une catégorie de restaurant par son ID.
        /// </summary>
        /// <param name="id">ID de la catégorie à mettre à jour.</param>
        /// <param name="category">Données de la catégorie à mettre à jour.</param>
        /// <returns>Aucun contenu en cas de succès.</returns>
        /// <response code="204">La catégorie a été mise à jour avec succès.</response>
        /// <response code="400">Mauvaise requête : l'ID de catégorie spécifié ne correspond pas à la catégorie fournie.</response>
        /// <response code="401">Vous n'êtes pas autorisé à consulter la ressource.</response>
        /// <response code="404">La catégorie avec l'ID spécifié n'a pas été trouvée.</response>
        /// <response code="500">Échec de traitement de la requête par l'application.</response>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCategory(int id, Category category)
        {
            if (id != category.Id)
            {
                return BadRequest();
            }

            _context.Entry(category).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategoryExists(id))
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

        /// <summary>
        /// Crée une nouvelle catégorie de restaurant.
        /// </summary>
        /// <param name="category">Données de la catégorie à créer.</param>
        /// <returns>La catégorie de restaurant créée.</returns>
        /// <response code="201">La catégorie a été créée avec succès.</response>
        /// <response code="400">Mauvaise requête : la création de la catégorie a échoué.</response>
        /// <response code="401">Vous n'êtes pas autorisé à consulter la ressource.</response>
        /// <response code="500">Échec de traitement de la requête par l'application.</response>
        [HttpPost]
        public async Task<ActionResult<Category>> PostCategory(Category category)
        {
          if (_context.Categories == null)
          {
              return Problem("Entity set 'RestaurantDbContext.Categories'  is null.");
          }
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCategory", new { id = category.Id }, category);
        }

        /// <summary>
        /// Supprime une catégorie de restaurant par son ID.
        /// </summary>
        /// <param name="id">ID de la catégorie à supprimer.</param>
        /// <returns>Aucun contenu en cas de succès.</returns>
        /// <response code="204">La catégorie a été supprimée avec succès.</response>
        /// <response code="401">Vous n'êtes pas autorisé à consulter la ressource.</response>
        /// <response code="404">La catégorie avec l'ID spécifié n'a pas été trouvée.</response>
        /// <response code="500">Échec de traitement de la requête par l'application.</response>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            if (_context.Categories == null)
            {
                return NotFound();
            }
            var category = await _context.Categories.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }

            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CategoryExists(int id)
        {
            return (_context.Categories?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
