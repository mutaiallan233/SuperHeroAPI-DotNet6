﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SuperHeroAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuperHeroController : ControllerBase
    {
        private static List<SuperHero> heroes = new List<SuperHero>
            {
            new SuperHero {
                Id = 1,
                Name = "Spider Man",
                FirstName = "Peter",
                LastName = "Parker",
                Place = "NewYork City"
                },
            new SuperHero {
                Id = 2,
                Name = "Iron Man",
                FirstName = "Tony",
                LastName = "Stark",
                Place = "Long Island"
                },
            };
        private readonly DataContext _context;

        public SuperHeroController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<SuperHero>>> Get()
        {
            return Ok(await _context.SuperHeroes.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SuperHero>> Get(int id)
        {
            var hero = await _context.SuperHeroes.FindAsync(id);
            if (hero == null)
                return BadRequest("Hero not Found");
            return Ok(hero);
        }

        [HttpPost]
        public async Task<ActionResult<List<SuperHero>>> AddHero(SuperHero hero)
        {
            _context.SuperHeroes.Add(hero);
            await _context.SaveChangesAsync();
            return Ok(await _context.SuperHeroes.ToListAsync());
        }

        [HttpPut]
        public async Task<ActionResult<List<SuperHero>>> UpdateHero(SuperHero request)
        {
            var dbhero = await _context.SuperHeroes.FindAsync(request.Id);
            if (dbhero == null)
                return BadRequest("Hero not Found");

            dbhero.Name = request.Name;
            dbhero.FirstName = request.FirstName;
            dbhero.LastName = request.LastName;
            dbhero.Place = request.Place;

            await _context.SaveChangesAsync();

            return Ok(await _context.SuperHeroes.ToListAsync());
        }

        [HttpDelete]
        public async Task<ActionResult<List<SuperHero>>> Delete(int id)
        {
            var dbhero = await _context.SuperHeroes.FindAsync(id);
            if (dbhero == null)
                return BadRequest("Hero not Found");

            _context.SuperHeroes.Remove(dbhero);
            await _context.SaveChangesAsync();

            return Ok(await _context.SuperHeroes.ToListAsync());

        }
    }
}
