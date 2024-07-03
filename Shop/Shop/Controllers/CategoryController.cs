using Microsoft.AspNetCore.Mvc;
using Shop.Exceptions;
using Shop.Services.DTO;
using Shop.Services.Interfaces;

namespace Shop.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        // GET: api/Categories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryDto>>> GetAllCategories(CancellationToken cancellationToken = default)
        {
            var categories = await _categoryService.GetAllCategoriesAsync(cancellationToken);
            return Ok(categories);
        }

        // GET: api/Categories/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryDto>> GetCategoryById(int id, CancellationToken cancellationToken = default)
        {
            try
            {
                var category = await _categoryService.GetCategoryByIdAsync(id, cancellationToken);
                return Ok(category);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        // POST: api/Categories
        [HttpPost]
        public async Task<ActionResult<CategoryDto>> CreateCategory(CategoryDto newCategory, CancellationToken cancellationToken = default)
        {
            try
            {
                var createdCategory = await _categoryService.CreateCategoryAsync(newCategory, cancellationToken);
                return CreatedAtAction(nameof(GetCategoryById), new { id = createdCategory.Id }, createdCategory);
            }
            catch (RepeatingNameException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT: api/Categories/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory(int id, CategoryDto updatedCategory, CancellationToken cancellationToken = default)
        {
            try
            {
                await _categoryService.UpdateCategoryAsync(id, updatedCategory, cancellationToken);
                return NoContent();
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (RepeatingNameException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE: api/Categories/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id, CancellationToken cancellationToken = default)
        {
            try
            {
                await _categoryService.DeleteCategoryAsync(id, cancellationToken);
                return NoContent();
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
