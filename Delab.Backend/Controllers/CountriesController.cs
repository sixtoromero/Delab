using Delab.AccessData.Data;
using Delab.Shared.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Delab.Backend.Controllers;

[Route("api/countries")]
[ApiController]
public class CountriesController : ControllerBase
{
    private readonly DataContext _context;
    public CountriesController(DataContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Country>>> GetCountries()
    {
        var listCountry = await _context.Countries
            .Include(x => x.States)!
            .ThenInclude(x => x.Cities).OrderBy(x=> x.Name).ToListAsync();
        
        return Ok(listCountry);
    }

    [HttpPost]
    public async Task<IActionResult> PostCountry([FromBody] Country model)
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
    public async Task<ActionResult<Country>> GetCountry(int id)
    {
        var country = await _context.Countries.FindAsync(id);        
        return Ok(country);
    }

    [HttpPut]
    public async Task<ActionResult<Country>> PutCountry([FromBody] Country model)
    {
        try
        {
            var updateCountry = await _context.Countries.FirstOrDefaultAsync(x => x.CountryId == model.CountryId);
            updateCountry!.Name = model.Name;
            updateCountry!.CodPhone = model.CodPhone;

            _context.Countries.Update(updateCountry);
            await _context.SaveChangesAsync();

            return Ok(updateCountry);
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
    public async Task<IActionResult> DeleteCountry(int Id)
    {
        try
        {
            var DataRemove = await _context.Countries.FindAsync(Id);
            if (DataRemove == null)
            {
                return BadRequest("No se encontró el registro a borrar");
            }
            _context.Countries.Remove(DataRemove);
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
        catch(Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
