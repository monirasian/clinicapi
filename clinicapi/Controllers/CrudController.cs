using clinicapi.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace clinicapi.Controllers;

[ApiController]
[Authorize]
public abstract class CrudController<TEntity> : ControllerBase where TEntity : class
{
    protected CrudController(ClinicDbContext db)
    {
        Db = db;
    }

    protected ClinicDbContext Db { get; }

    protected DbSet<TEntity> Set => Db.Set<TEntity>();

    [HttpGet]
    public virtual async Task<ActionResult<IEnumerable<TEntity>>> GetAll([FromQuery] int skip = 0, [FromQuery] int take = 100)
    {
        if (skip < 0) skip = 0;
        if (take <= 0) take = 100;
        if (take > 500) take = 500;

        var items = await Set
            .AsNoTracking()
            .Skip(skip)
            .Take(take)
            .ToListAsync();

        return Ok(items);
    }

    [HttpGet("{id:int}")]
    public virtual async Task<ActionResult<TEntity>> GetById(int id)
    {
        var entity = await Set.FindAsync(id);
        if (entity is null)
        {
            return NotFound();
        }

        return Ok(entity);
    }

    [HttpPost]
    public virtual async Task<ActionResult<TEntity>> Create([FromBody] TEntity entity)
    {
        Set.Add(entity);
        await Db.SaveChangesAsync();

        var id = GetEntityId(entity);
        return CreatedAtAction(nameof(GetById), new { id }, entity);
    }

    [HttpPut("{id:int}")]
    public virtual async Task<IActionResult> Update(int id, [FromBody] TEntity entity)
    {
        SetEntityId(entity, id);
        Db.Entry(entity).State = EntityState.Modified;

        try
        {
            await Db.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            // If the record doesn't exist, return 404. Otherwise rethrow.
            if (!await Exists(id))
            {
                return NotFound();
            }

            throw;
        }

        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public virtual async Task<IActionResult> Delete(int id)
    {
        var entity = await Set.FindAsync(id);
        if (entity is null)
        {
            return NotFound();
        }

        Set.Remove(entity);
        await Db.SaveChangesAsync();

        return NoContent();
    }

    private async Task<bool> Exists(int id)
    {
        var entity = await Set.FindAsync(id);
        return entity is not null;
    }

    private static int GetEntityId(TEntity entity)
    {
        var prop = GetIdPropertyOrThrow();
        var raw = prop.GetValue(entity);
        return raw switch
        {
            int i => i,
            null => 0,
            _ => Convert.ToInt32(raw),
        };

        static PropertyInfo GetIdPropertyOrThrow()
        {
            var prop = typeof(TEntity).GetProperty("Id", BindingFlags.Instance | BindingFlags.Public | BindingFlags.IgnoreCase);
            if (prop is null)
            {
                throw new InvalidOperationException($"{typeof(TEntity).Name} must have an Id property for CrudController.");
            }

            return prop;
        }
    }

    private static void SetEntityId(TEntity entity, int id)
    {
        var prop = typeof(TEntity).GetProperty("Id", BindingFlags.Instance | BindingFlags.Public | BindingFlags.IgnoreCase);
        if (prop is null || !prop.CanWrite)
        {
            return;
        }

        prop.SetValue(entity, id);
    }
}
