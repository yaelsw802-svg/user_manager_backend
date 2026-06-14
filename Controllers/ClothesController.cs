using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Store.Core.Entities;
using Store.Data;
using System.Threading.Tasks;

namespace ClothingStoreApi.Controllers
{
    [ApiController]
    [Route("api/clothes")]
    public class ClothesController : ControllerBase
    {
        private readonly DataContext _context;

        public ClothesController(DataContext context)
        {
            this._context = context;
        }

        // GET: api/clothes
        [HttpGet]
        [Authorize]
        public List<ClothingItem> GetAll()
        {
            return _context.Clothes.ToList();
        }

        [HttpGet("{id}")]
        public ActionResult<ClothingItem> GetById(int id)
        {
            var item = _context.Clothes.FirstOrDefault(c => c.Id == id);

            if (item == null)
                return NotFound();

            return item;
        }

        [HttpPost]
        public async Task<ActionResult> Add([FromBody] ClothingItem newItem)
        {
            _context.Clothes.Add(newItem);
            await _context.SaveChangesAsync();

            return Ok(newItem);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, [FromBody] ClothingItem updatedItem)
        {
            var item = _context.Clothes.FirstOrDefault(c => c.Id == id);
            if (item == null)
                return NotFound();

            item.Name = updatedItem.Name;
            item.Size = updatedItem.Size;
            item.Color = updatedItem.Color;
            item.Price = updatedItem.Price;
            item.Category = updatedItem.Category;

           await  _context.SaveChangesAsync();

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var item = _context.Clothes.FirstOrDefault(c => c.Id == id);
            if (item == null)
                return NotFound();

            _context.Clothes.Remove(item);
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}