﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FountainPensNg.Server.Data;
using FountainPensNg.Server.Data.Models;
using System.Text.Json;
using AutoMapper;
using FountainPensNg.Server.Data.DTO;

namespace FountainPensNg.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FountainPensController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public FountainPensController(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/FountainPens
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FountainPenDownloadDTO>>> GetFountainPens()
        {
            var temp = await _context
                .FountainPens
                .Include(x => x.InkedUps)
                .ThenInclude(inkup => inkup.Ink)
                .ToListAsync();
            var res = new List<FountainPenDownloadDTO>();
            foreach (var f in temp) {
                var dto = _mapper.Map<FountainPenDownloadDTO>(f);
                SetCurrentInkup(f, dto);
                res.Add(dto);
            }
            return res;
        }

        // GET: api/FountainPens/5
        [HttpGet("{id}")]
        public async Task<ActionResult<FountainPenDownloadDTO>> GetFountainPen(int id)
        {
            var fountainPen = await _context.FountainPens
                .Include(pen => pen.InkedUps)
                .ThenInclude(inkup => inkup.Ink)
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();

            if (fountainPen == null)
            {
                return NotFound();
            }

            var result = _mapper.Map<FountainPenDownloadDTO>(fountainPen);
            SetCurrentInkup(fountainPen, result);
            return result;
        }

        private void SetCurrentInkup(FountainPen fountainPen, FountainPenDownloadDTO fountainPenDownloadDTO) {
            var currInkup = fountainPen.InkedUps.FirstOrDefault(x => x.IsCurrent);
            if (currInkup != null) {
                fountainPenDownloadDTO.CurrentInk =  _mapper.Map<InkDTO>(currInkup.Ink);
                fountainPenDownloadDTO.CurrentInkId = currInkup.Ink.Id;
                fountainPenDownloadDTO.CurrentInkRating = currInkup.MatchRating;
            }
        }

        // PUT: api/FountainPens/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFountainPen(int id, FountainPenUploadDTO dto)
        {
            var fountainPen = _mapper.Map<FountainPen>(dto);
            if (fountainPen == null || id != fountainPen.Id)
            {
                return BadRequest();
            }

            var find = await _context.FountainPens.FindAsync(fountainPen.Id);
            if (find == null) return NotFound();
            
            FountainPen old = find;

            //TODO: auto add inkedup 
            // if (old.CurrentInkId != fountainPen.CurrentInkId 
            //     && fountainPen.CurrentInkId.HasValue
            //     && fountainPen.CurrentInkId != 0) {
            //     _context.InkedUps.Add(new InkedUp() {
            //         FountainPenId = fountainPen.Id,
            //         InkId = fountainPen.CurrentInkId.Value,
            //         MatchRating = fountainPen.CurrentInkRating ?? 0
            //     });
            // }
                
            _context.Entry(old).CurrentValues.SetValues(fountainPen);
            old.ModifiedAt = DateTime.UtcNow;
    

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FountainPenExists(id))
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

        // POST: api/FountainPens
        [HttpPost]
        public async Task<ActionResult<FountainPen>> PostFountainPen(FountainPenUploadDTO dto)
        {
            var fountainPen = _mapper.Map<FountainPen>(dto);
            if (fountainPen == null)
            {
                return BadRequest();
            }
            _context.FountainPens.Add(fountainPen);
            //TODO: auto add inkedup 
            // if (fountainPen.CurrentInkId.HasValue && fountainPen.CurrentInkId > 0) {
            //     _context.InkedUps.Add(new InkedUp() {
            //         FountainPen = fountainPen,
            //         InkId = fountainPen.CurrentInkId.Value,
            //         MatchRating = fountainPen.CurrentInkRating ?? 0
            //     });
            // }
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFountainPen", new { id = fountainPen.Id }, dto);
        }

        // DELETE: api/FountainPens/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFountainPen(int id)
        {
            var fountainPen = await _context.FountainPens.FindAsync(id);
            if (fountainPen == null)
            {
                return NotFound();
            }

            _context.FountainPens.Remove(fountainPen);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool FountainPenExists(int id)
        {
            return _context.FountainPens.Any(e => e.Id == id);
        }
    }
}
