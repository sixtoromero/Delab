using Delab.AccessData.Data;
using Delab.Shared.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Delab.Backend.Controllers;

[Route("api/cities")]
[ApiController]
public class CitiesController : ControllerBase
{
    private readonly DataContext _context;

    public CitiesController(DataContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<City>>> GetListAsync()
    {
        var listCountry = await _context.Cities.OrderBy(x => x.Name).ToListAsync();
        return Ok(listCountry);
    }

    [HttpPost]
    public async Task<IActionResult> PostAsync([FromBody] City model)
    {
        try
        {
            _context.Add(model);
            await _context.SaveChangesAsync();
            return Ok();
        }
        catch (DbUpdateException dbEx)
        {
            if (dbEx.InnerException!.Message.Contains("duplicate"))
            {
                return BadRequest("Ya existe un registro con el mismo nombre.");
            }
            else
            {
                return BadRequest(dbEx.InnerException.Message);
            }
        }

    }

    [HttpGet("{id}")]
    public async Task<ActionResult<City>> GetAsync(int id)
    {
        var ItemModel = await _context.Cities.FindAsync(id);
        return Ok(ItemModel);
    }

    [HttpPut]
    public async Task<ActionResult<City>> PutAsync([FromBody] City model)
    {
        try
        {
            var updateModel = await _context.Cities.FirstOrDefaultAsync(x => x.CityId == model.CityId);
            updateModel!.Name = model.Name;
            updateModel!.StateId = model.StateId;

            _context.Cities.Update(updateModel);
            await _context.SaveChangesAsync();

            return Ok(updateModel);
        }
        catch (DbUpdateException dbEx)
        {
            if (dbEx.InnerException!.Message.Contains("duplicate"))
            {
                return BadRequest("Ya existe un registro con el mismo nombre.");
            }
            else
            {
                return BadRequest(dbEx.InnerException.Message);
            }
        }
    }

    [HttpDelete("{Id}")]
    public async Task<IActionResult> DeleteAsync(int Id)
    {
        try
        {
            var DataRemove = await _context.Cities.FindAsync(Id);
            if (DataRemove == null)
            {
                return BadRequest("No se encontró el registro a borrar");
            }
            _context.Cities.Remove(DataRemove);
            await _context.SaveChangesAsync();

            return Ok();
        }
        catch (DbUpdateException dbEx)
        {
            if (dbEx.InnerException!.Message.Contains("REFERENCE"))
            {
                return BadRequest("No puede eliminar el registro porque tiene datos relacionados.");
            }
            else
            {
                return BadRequest(dbEx.InnerException.Message);
            }
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
