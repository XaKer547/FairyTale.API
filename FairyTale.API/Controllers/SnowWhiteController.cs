using FairyTale.API.Data;
using FairyTale.API.Models;
using FairyTale.API.Models.DTOs;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FairyTale.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class SnowWhiteController : ControllerBase
    {
        private ApplicationDbContext _context;

        public SnowWhiteController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var snowWhite = await _context.SnowWhites.FirstOrDefaultAsync();

            return new JsonResult(new SnowWhiteDTO
            {
                FullName= snowWhite.FullName,
                Dwarfs = snowWhite.Dwarfs.Select(x => new DwarfDTO
                {
                    Id = x.Id,
                    Name = x.Name,
                })
            });
        }

        [HttpGet("{id}/dwarfs")]
        public async Task<IActionResult> Get(int id)
        {
            var dwarves = await _context.Dwarfs.Where(x=> x.SnowWhiteId == id)
                .Select(x=> new DwarfDTO
                {
                    Id = x.Id,
                    Name = x.Name,
                })
                .ToArrayAsync();

            return new JsonResult(dwarves);
        }
    }
}
