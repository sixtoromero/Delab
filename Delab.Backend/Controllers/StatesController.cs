using Delab.AccessData.Data;
using Delab.Shared.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Delab.Backend.Controllers;

[Route("api/states")]
[ApiController]
public class StatesController : ControllerBase
{
    private readonly DataContext _context;

    public StatesController(DataContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<State>>> GetListAsync()
    {
        var listCountry = await _context.States.OrderBy(x => x.Name).ToListAsync();
        return Ok(listCountry);
    }

    [HttpPost]
    public async Task<IActionResult> PostAsync([FromBody] State model)
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
    public async Task<ActionResult<State>> GetAsync(int id)
    {
        var ItemModel = await _context.States.FindAsync(id);
        return Ok(ItemModel);
    }

    [HttpPut]
    public async Task<ActionResult<State>> PutAsync([FromBody] State model)
    {
        try
        {
            var updateModel = await _context.States.FirstOrDefaultAsync(x => x.StateId == model.StateId);
            updateModel!.Name = model.Name;
            updateModel!.CountryId = model.CountryId;

            _context.States.Update(updateModel);
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
            var DataRemove = await _context.States.FindAsync(Id);
            if (DataRemove == null)
            {
                return BadRequest("No se encontró el registro a borrar");
            }
            _context.States.Remove(DataRemove);
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
