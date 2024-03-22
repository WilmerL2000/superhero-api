using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SuperHeroAPI.Data;
using SuperHeroAPI.Entities;

namespace SuperHeroAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuperHeroController : ControllerBase
    {
        
        private readonly DataContext _dataContext;

        public SuperHeroController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        [HttpGet]
        public async Task<ActionResult<List<SuperHero>>> GetAllHeroes()
        {
            var heroes = await _dataContext.SupeHero.ToListAsync();

            return Ok(heroes);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SuperHero>> GetHero(int id)
        {
            var hero = await _dataContext.SupeHero.FindAsync(id);

            if (hero is null)
                return BadRequest("Hero not found");
           

            return Ok(hero);
        }

        [HttpPost]
        public async Task<ActionResult<List<SuperHero>>> AddHero(SuperHero newHero) 
        {
             _dataContext.SupeHero.Add(newHero);

            await _dataContext.SaveChangesAsync();

            return Ok(await _dataContext.SupeHero.ToListAsync());
        }

        [HttpPut]
        public async Task<ActionResult<SuperHero>> UpdadteHero(SuperHero hero)
        {
            var dbHero = await _dataContext.SupeHero.FindAsync(hero.Id);

            if (dbHero is null)
                return BadRequest("Hero not found");

            dbHero.Name = hero.Name;
            dbHero.FirstName = hero.FirstName;
            dbHero.LastName = hero.LastName;
            dbHero.Place = hero.Place;

            await _dataContext.SaveChangesAsync();

            return Ok(await _dataContext.SupeHero.ToListAsync());
        }

        [HttpDelete]
        public async Task<ActionResult<List<SuperHero>>> DeleteHero(int id)
        {
            var dbHero = await _dataContext.SupeHero.FindAsync(id);

            if (dbHero is null)
                return BadRequest("Hero not found");

            _dataContext.SupeHero.Remove(dbHero);
            await _dataContext.SaveChangesAsync();

            return Ok(await _dataContext.SupeHero.ToListAsync());
        }

    }
}
