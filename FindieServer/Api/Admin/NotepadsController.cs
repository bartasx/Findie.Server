using System;
using System.Linq;
using System.Threading.Tasks;
using Findie.Common.Models.AdminsModel;
using FindieServer.DbModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FindieServer.Api.Admin
{
    [Produces("application/json")]
    [Route("api/Notepads")]
    public class NotepadsController : Controller
    {
        private readonly DatabaseContext db;

        public NotepadsController(DatabaseContext context)
        {
            db = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetNotepads()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var query = await (from p in db.NotepadTables select p).ToListAsync();

            return Ok(query);
        }

        // PUT: api/Notepads/5
        [HttpPut]
        public async Task<IActionResult> PutNotepadModel([FromBody]Notepads notepadModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var query = await (from p in db.NotepadTables where p.Id == notepadModel.Id select p).FirstOrDefaultAsync();
            query.NotepadContent = notepadModel.NotepadContent;
            query.TimeStamp = DateTime.Now;          

            db.Entry(query).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
            }

            return NoContent();
        }

        // POST: api/Notepads
        [HttpPost]
        [Route("AddNewNoteAsync")]
        public async Task<IActionResult> AddNewNoteAsync([FromBody] Notepads notepadModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            notepadModel.Username = "Administrator";
            notepadModel.TimeStamp = DateTime.Now;

            db.NotepadTables.Add(notepadModel);
            await db.SaveChangesAsync();

            return Ok();
        }

        // DELETE: api/Notepads/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNotepadModel([FromHeader]int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var notepadModel = await db.NotepadTables.Where(note => note.Id == id).FirstOrDefaultAsync();

          //var notepadModel = await db.NotepadTables.Where(note => note.Id == model.Id).FirstOrDefaultAsync();

            if (notepadModel == null)
            {
                return NotFound();
            }

            db.NotepadTables.Remove(notepadModel);
            await db.SaveChangesAsync();

            return Ok();
        }
        
        private bool NotepadModelExists(int id)
        {
            return db.NotepadTables.Any(e => e.Id == id);
        }
    }
}